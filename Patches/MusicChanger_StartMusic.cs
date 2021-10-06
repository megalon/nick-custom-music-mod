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
    [HarmonyPatch(typeof(MusicChanger), "StartMusic")]
    class MusicChanger_StartMusic
	{
		static bool Prefix(ref string newMusicId)
		{
			Debug.Log($"newMusicId: {newMusicId}");
			if (newMusicId.Equals("MainMenu") || newMusicId.Equals(""))
			{
				// Custom music here
				Debug.Log("Attempting to access values");

				// Get first element from dictionary, I think
				var e = CustomMusicManager.songEntries.Values.GetEnumerator();
				e.MoveNext();
				MusicItem musicEntry = e.Current;

				// Intercept the ID and use our custom one
				newMusicId = musicEntry.id;
				Debug.Log($"set newMusicId to: {newMusicId}");
			}
			return true;
		}
	}
}
