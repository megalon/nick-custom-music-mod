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
using NickCustomMusic;

namespace NickCustomMusicMod.Patches
{
    [HarmonyPatch(typeof(GameMusicBank), "GetMusic")]
    class GameMusicBank_GetMusic
    {
        static bool Prefix(ref string id, ref GameMusic gm)
        {
            if (id.Equals("MAIN"))
            {
                // Custom music here

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
				//Plugin.Instance.LogError(audioLoader.error);
				Debug.LogError(audioLoader.error);
				yield break;
			}
			music.clip = DownloadHandlerAudioClip.GetContent(audioLoader);
			GameMusicSystem gmsInstance;
			GameMusicSystem.TryGetInstance(out gmsInstance) {
				gmsInstance.InvokeMethod("play", new object[]
				{
					entry.id,
					music
				});
			}

			yield break;
		}
	}
}
