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
        public static readonly Dictionary<string, string> StageIDs = new Dictionary<string,string> {
            { "Jellyfish Fields", "SlideHouse" },
            { "The Flying Dutchmans Ship", "Shanty" },
            { "Rooftop Rumble", "Rooftop" },
            { "The Dump", "Trash" },
            { "The Loud House", "Loud" },
            { "Glove World", "CarnivalLofi" },
            { "Sewers Slam", "SewersCombined" },
            { "Irken Armada Invasion", "Armada" },
            { "Technodrom Takedown", "Drome" },
            { "Royal Woods Cemetery", "YMC" },
            { "CatDog's House", "House" },
            { "Traffic Jam", "Stomp" },
            { "Western Air Temple", "Temple2" },
            { "Wild Waterfall", "Waterfall" },
            { "Powdered Toast Trouble", "DuoKitchen" },
            { "Ghost Zone", "Zone" },
            { "Harmonic Convergence", "SpiritWorld" },
            { "Omashu", "Omashu" },
            { "Showdown at Teeter Totter Gulch", "Playground" }
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
