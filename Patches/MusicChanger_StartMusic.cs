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
		static bool Prefix(ref string newMusicId, MusicChanger __instance)
		{
			Plugin.LogDebug($"MusicChanger_StartMusic newMusicId: \"{newMusicId}\"");

			// Passing an empty string is meant to load the id set in the editor
			// This id is stored in the private field "musicId"
			if (newMusicId.Equals(""))
            {
				newMusicId = __instance.GetPrivateField<string>("musicId");
			}

			// Skip the function if the previous ID is the same as the current ID
			if (Plugin.previousMusicID != null)
			{
				Plugin.LogDebug($"Plugin.previousMusicID: {Plugin.previousMusicID}");
				if (newMusicId.Equals(Plugin.previousMusicID))
				{
					Plugin.LogDebug($"\"previousMusicID\" is the same as \"newMusicId\"! Skipping StartMusic");
					return false;
				}
			}
			
			Plugin.previousMusicID = newMusicId;

			return true;
		}
	}
}
