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

			Plugin.LogDebug($"newMusicId: {newMusicId}");

			// Special case for main menu to prevent it from restarting when you navigate menu system
			if (Plugin.previousMusicID != null)
			{
				Plugin.LogDebug($"Plugin.previousMusicID: {Plugin.previousMusicID}");
				if (newMusicId.Equals("MainMenu") && Plugin.previousMusicID.Equals("MainMenu"))
				{
					Plugin.LogDebug($"previousMusicID was {Plugin.previousMusicID}! Skipping StartMusic");
					return false;
				}
			}
			Plugin.previousMusicID = newMusicId;
			return true;
		}
	}
}
