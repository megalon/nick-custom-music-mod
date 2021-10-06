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

namespace NickCustomMusicMod.Patches
{
    [HarmonyPatch(typeof(GameMusicBank), "GetMusic")]
    class GameMusicBank_GetMusic
    {
        static bool Prefix(ref string id)
		{
			// Intercept custom songs
			if (id.StartsWith("CUSTOM_"))
            {
				// Custom music here
				Debug.Log("Attempting to access values");

				MusicItem musicEntry = CustomMusicManager.songEntries[id];

				Debug.Log("Loading song from " + musicEntry.resLocation);
				Plugin.Instance.StartCoroutine(LoadCustomSong(musicEntry));
				return false;
            }
            return true;
		}

		public static IEnumerator LoadCustomSong(MusicItem entry)
		{
			GameMusic music = new GameMusic();
			AudioType audioType = AudioType.UNKNOWN;
			string a = Path.GetExtension(entry.resLocation).ToLower();
			if (!(a == ".ogg"))
			{
				if (a == ".wav")
				{
					audioType = AudioType.WAV;
				}
			}
			else
			{
				audioType = AudioType.OGGVORBIS;
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
