using System;
using System.Collections.Generic;
using System.Text;

namespace NickCustomMusicMod.Utils
{
    // This list dictates which folders are auto generated
    // It also translates the folder names to their in game ID
    public static class Consts
    {
        public static readonly Dictionary<string, string> StageIDs = new Dictionary<string,string> {
            { "CatDogs House", "House" },
            { "Ghost Zone", "Zone" },
            { "Glove World", "CarnivalLofi" },
            { "Harmonic Convergence", "SpiritWorld" },
            { "Irken Armada Invasion", "Armada" },
            { "Jellyfish Fields", "SlideHouse" },
            { "Omashu", "Omashu" },
            { "Powdered Toast Trouble", "DuoKitchen" },
            { "Rooftop Rumble", "Rooftop" },
            { "Royal Woods Cemetery", "YMC" },
            { "Sewers Slam", "SewersCombined" },
            { "Showdown at Teeter Totter Gulch", "Playground" },
            { "Space Madness", "DuoMadness" },
            { "Technodrom Takedown", "Drome" },
            { "The Dump", "Trash" },
            { "The Flying Dutchmans Ship", "Shanty" },
            { "The Loud House", "Loud" },
            { "Traffic Jam", "Stomp" },
            { "Western Air Temple", "Temple2" },
            { "Wild Waterfall", "Waterfall" }
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
