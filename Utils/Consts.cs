using System;
using System.Collections.Generic;
using System.Text;

namespace NickCustomMusicMod.Utils
{
    // These should really be loaded from the game, not from a stored list, but
    // this list simply dictates which folders are auto generated.
    // Currently the mod will also load files from any extra subfolder in the "Stage" or "Menu" folders,
    // incase extra stages are added in the future.
    public static class Consts
    {
        public static readonly Dictionary<string, string> StageIDs = {
            { "Jellyfish Fields", "SlideHouse" }
            /*"Shanty",
            "Rooftop",
            "Trash",
            "Loud",
            "CarnivalLofi",
            "SewersCombined",
            "Armada",
            "Drome",
            "YMC",
            "House",
            "Stomp",
            "Temple2",
            "Waterfall",
            "DuoKitchen",
            "Zone",
            "DuoMadness",
            "SpiritWorld",
            "Omashu",
            "Playground"
            */
        };

        public static readonly string[] MenuIDs = { 
            "MainMenu",
            "Versus",
            "OnlineMenu",
            "ArcadeMap",
            "Victory",
            "LoseV1",
            "LoseV2"
        };
    }
}
