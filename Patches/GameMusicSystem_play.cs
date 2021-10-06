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
    [HarmonyPatch(typeof(GameMusicSystem), "play")]
    class GameMusicSystem_play
    {
        static bool Prefix(ref string id, ref GameMusic sm)
        {
            Debug.Log($"GameMusicSystem_play: {id}");
            return true;
        }
    }
}
