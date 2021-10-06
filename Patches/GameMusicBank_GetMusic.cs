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

namespace NickCustomMusicMod.Patches
{
    [HarmonyPatch(typeof(GameMusicBank), "GetMusic")]
    class GameMusicBank_GetMusic
    {
        static bool Prefix(ref string id)
		{

			Plugin.previousMusicID = id;

			// Get a random song for this stage / menu
			if (CustomMusicManager.songDictionaries.ContainsKey(id))
			{
				Dictionary<string, MusicItem> musicDict = CustomMusicManager.songDictionaries[id];
				int numSongs = musicDict.Keys.Count;

				if (numSongs > 0)
				{
					string randomSong = musicDict.Keys.ToArray<string>()[UnityEngine.Random.Range(0, numSongs)];
					MusicItem musicEntry = musicDict[randomSong];

					// Intercept the ID and use our custom one
					id = musicEntry.id;
					Debug.Log($"set newMusicId to: {id}");
				}
				else
				{
					Debug.LogWarning($"No songs found for {id}! Using default music instead.");
				}
			}

			// Intercept custom songs
			if (id.StartsWith("CUSTOM_"))
            {
				// Custom music here
				Debug.Log($"Attempting to load custom song for id: {id}");

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
					Debug.Log("Loading song from " + musicItem.resLocation);
					Plugin.Instance.StartCoroutine(LoadCustomSong(musicItem));
					return false;
				}

				Debug.LogError($"Error! Could not find {id} in key value pairs inside CustomMusicManager.songDictionaries");
            }

			return true;
		}

		public static IEnumerator LoadCustomSong(MusicItem entry)
		{
			GameMusic music = new GameMusic();
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
			yield return audioLoader.SendWebRequest();
			if (audioLoader.error != null)
			{
				Debug.LogError(audioLoader.error);
				yield break;
			}
			music.clip = DownloadHandlerAudioClip.GetContent(audioLoader);
			GameMusicSystem gmsInstance;
			if (GameMusicSystem.TryGetInstance(out gmsInstance)) {
				gmsInstance.InvokeMethod("play", entry.id, music);
			};

			yield break;
		}
	}
}
