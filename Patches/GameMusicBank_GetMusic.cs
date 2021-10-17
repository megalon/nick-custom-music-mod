using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HarmonyLib;
using Nick;
using UnityEngine;
using static Nick.MusicMetaData;
using UnityEngine.Networking;
using NickCustomMusicMod;
using NickCustomMusicMod.Utils;
using NickCustomMusicMod.Management;
using System.Linq;
using Newtonsoft.Json;
using NickCustomMusicMod.Data;

namespace NickCustomMusicMod.Patches
{
    [HarmonyPatch(typeof(GameMusicBank), "GetMusic")]
    class GameMusicBank_GetMusic
    {
        static bool Prefix(ref string id)
		{
			var config = Plugin.Instance.Config;
			Plugin.previousMusicID = id;

			// Get a random song for this stage / menu
			if (CustomMusicManager.songDictionaries.ContainsKey(id))
			{
				Dictionary<string, MusicItem> musicDict = CustomMusicManager.songDictionaries[id];
				int numSongs = musicDict.Keys.Count;

				if (numSongs > 0)
				{
					int randInt = UnityEngine.Random.Range(0, numSongs + (Plugin.Instance.useDefaultSongs.Value ? 1 : 0));

					if (randInt >= numSongs) {
						Plugin.LogInfo("Randomly selected default music instead of custom songs!");
					} else {
						string randomSong = musicDict.Keys.ToArray<string>()[randInt];
						MusicItem musicEntry = musicDict[randomSong];

						// Intercept the ID and use our custom one
						Plugin.LogDebug($"Intercepting GetMusic id: {id} and changing to {musicEntry.id}");
						id = musicEntry.id;
					}
				}
				else
				{
					Plugin.LogInfo($"No songs found for {id}! Using default music instead.");
				}
			}

			// Intercept custom songs
			if (id.StartsWith("CUSTOM_"))
            {
				// Custom music here
				Plugin.LogInfo($"Attempting to load custom song for id: {id}");

				MusicItem musicItem = null;

				// Get MusicItem from dictionary
				foreach (Dictionary<string, MusicItem> keyValuePairs in CustomMusicManager.songDictionaries.Values)
                {
					if (keyValuePairs.ContainsKey(id))
                    {
						musicItem = keyValuePairs[id];
						break;
					}
                }

				if (musicItem != null)
				{
					Plugin.LogInfo("Loading song: " + musicItem.resLocation);
					Plugin.Instance.StartCoroutine(LoadCustomSong(musicItem));
					return false;
				}

				Debug.LogError($"Error! Could not find {id} in key value pairs inside CustomMusicManager.songDictionaries");
            }

			return true;
		}

		public static IEnumerator LoadCustomSong(MusicItem entry)
		{
			GameMusic music = ScriptableObject.CreateInstance<GameMusic>();
			AudioType audioType = AudioType.UNKNOWN;

			switch (Path.GetExtension(entry.resLocation).ToLower())
			{
				case ".wav":
					audioType = AudioType.WAV;
					break;
				case ".mp3":
					audioType = AudioType.MPEG;
					break;
				case ".ogg":
					audioType = AudioType.OGGVORBIS;
					break;
				default:
					yield break;
			}

            UnityWebRequest audioLoader = UnityWebRequestMultimedia.GetAudioClip(entry.resLocation, audioType);
			// this stops the lag!
            (audioLoader.downloadHandler as DownloadHandlerAudioClip).streamAudio = true;
            yield return audioLoader.SendWebRequest();
			if (audioLoader.error != null)
			{
				Debug.LogError(audioLoader.error);
				yield break;
			}
			music.clip = DownloadHandlerAudioClip.GetContent(audioLoader);

			HandleCustomMusicData(entry, music);

			GameMusicSystem gmsInstance;
			if (GameMusicSystem.TryGetInstance(out gmsInstance)) {
				gmsInstance.InvokeMethod("play", entry.id, music);
			};

			yield break;
		}

		private static void HandleCustomMusicData(MusicItem entry, GameMusic music)
        {
			// Get loop points from json file here
			string jsonPath = Path.Combine(Path.GetDirectoryName(entry.resLocation), Path.GetFileNameWithoutExtension(entry.resLocation) + ".json");
			if (File.Exists(jsonPath))
			{
				try
				{
					string jsonFile = File.ReadAllText(jsonPath);

					var customMusicData = JsonConvert.DeserializeObject<CustomMusicData>(jsonFile);

					// If no sample loop points are found, use seconds instead
					if (!HandleLoopPointsSamples(customMusicData, music, jsonPath))
                    {
						HandleLoopPointsSec(customMusicData, music, jsonPath);
					}
				}
				catch (Exception e)
				{
					Plugin.LogError($"Error reading json data for {jsonPath}");
					Plugin.LogError(e.Message);
				}
			}
			else
			{
				Plugin.LogInfo($"No json file found for {Path.GetFileName(entry.resLocation)}");
			}
		}

		private static bool HandleLoopPointsSamples(CustomMusicData customMusicData, GameMusic music, string jsonPath)
		{
			Plugin.LogDebug($"customMusicData Samples: {customMusicData.loopStartPointSamples}, {customMusicData.loopEndPointSamples}");
			if (customMusicData.loopStartPointSamples == 0)
			{
				Plugin.LogWarning($"\"loopStartPointSamples\" is 0 for json file \"{jsonPath}\"! It might not be in the file, or is misspelled!");
			}

			if (customMusicData.loopEndPointSamples == 0)
			{
				Plugin.LogWarning($"\"loopEndPointSamples\" is 0 for json file \"{jsonPath}\"! It might not be in the file, or is misspelled!");
			}

			if (customMusicData.loopStartPointSamples == 0 && customMusicData.loopEndPointSamples == 0)
            {
				// No loop points defined
				return false;
            }

			if (customMusicData.loopEndPointSamples == 0 && customMusicData.loopStartPointSamples > 0)
			{
				Plugin.LogWarning($"\"loopStartPointSamples\" is greater than 0, but \"loopEndPointSamples\" is 0! Setting \"loopEndPointSamples\" to length of song for \"{jsonPath}\"");
				customMusicData.loopEndPointSamples = music.clip.samples;
			}

			if (customMusicData.loopEndPointSamples > 0 && customMusicData.loopStartPointSamples > 0 && customMusicData.loopStartPointSamples == customMusicData.loopEndPointSamples)
			{
				Plugin.LogWarning($"\"loopStartPointSamples\" and \"loopEndPointSamples\" are the same value for \"{jsonPath}\"! Did you mean to do that?");
			}

			// TODO actually handle the loop points in samples!
			// Currently it expects seconds!
			// music.loopWhere = customMusicData.loopStartPointSamples;
			// music.loopTime = customMusicData.loopEndPointSamples;

			return true;
		}

		private static void HandleLoopPointsSec(CustomMusicData customMusicData, GameMusic music, string jsonPath)
		{
			customMusicData.loopStartPointSec = Mathf.Clamp(customMusicData.loopStartPointSec, 0, music.clip.length);
			customMusicData.loopEndPointSec = Mathf.Clamp(customMusicData.loopEndPointSec, 0, music.clip.length);

			Plugin.LogDebug($"customMusicData Seconds: {customMusicData.loopStartPointSec}, {customMusicData.loopEndPointSec}");
			if (customMusicData.loopStartPointSec == 0)
			{
				Plugin.LogWarning($"\"loopStartPointSec\" is 0 for json file \"{jsonPath}\"! It might not be in the file, or is misspelled!");
			}

			if (customMusicData.loopEndPointSec == 0)
			{
				Plugin.LogWarning($"\"loopEndPointSec\" is 0 for json file \"{jsonPath}\"! It might not be in the file, or is misspelled!");
			}

			if (customMusicData.loopEndPointSec == 0 && customMusicData.loopStartPointSec > 0)
			{
				Plugin.LogWarning($"\"loopStartPointSec\" is greater than 0, but \"loopEndPointSec\" is 0! Setting \"loopEndPointSec\" to length of song for \"{jsonPath}\"");
				customMusicData.loopEndPointSec = music.clip.length;
			}

			if (customMusicData.loopEndPointSec > 0 && customMusicData.loopStartPointSec > 0 && customMusicData.loopStartPointSec == customMusicData.loopEndPointSec)
			{
				Plugin.LogWarning($"\"loopStartPointSec\" and \"loopEndPointSec\" are the same value for \"{jsonPath}\"! Did you mean to do that?");
			}

			music.loopWhere = customMusicData.loopStartPointSec;
			music.loopTime = customMusicData.loopEndPointSec;
		}
	}
}
