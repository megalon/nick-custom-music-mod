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
			return true;
		}
	}
}
