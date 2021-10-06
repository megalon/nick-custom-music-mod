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
    [HarmonyPatch(typeof(MusicChanger), "StartMusic")]
    class MusicChanger_StartMusic
	{
		static bool Prefix(ref string newMusicId)
		{
			// Fix startup case where string is empty
			if (newMusicId.Equals("")) newMusicId = "MainMenu";
			
			Debug.Log($"newMusicId: {newMusicId}");

			if (newMusicId.Equals("MainMenu"))
			{
				// Custom music here
				int numSongs = CustomMusicManager.songEntries.Keys.Count<string>();

				if (numSongs > 0)
				{
					string randomSong = CustomMusicManager.songEntries.Keys.ToArray<string>()[UnityEngine.Random.Range(0, numSongs)];
					MusicItem musicEntry = CustomMusicManager.songEntries[randomSong];

					// Intercept the ID and use our custom one
					newMusicId = musicEntry.id;
					Debug.Log($"set newMusicId to: {newMusicId}");
                } else
                {
					Debug.LogWarning("No songs found in CustomMusicManager! Using default music.");
				}
			}
			return true;
		}
	}
}
