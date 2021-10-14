using System;
using System.Collections.Generic;
using System.Text;

namespace NickCustomMusicMod.Utils
{
    // This list dictates which folders are auto generated
    // It also translates the folder names to their in game ID
    public static class Consts
    {
        public static readonly string stagesFolderName = "Stages";
        public static readonly string menusFolderName = "Menus";
        public static readonly string victoryThemesFolderName = "Victory Themes";
        public static readonly string musicBankFolderName = "Music Bank";

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

        public static readonly Dictionary<string, string> CharacterIDs = new Dictionary<string, string> {
            { "SpongeBob", "char_apple" },
            { "Patrick", "char_star" },
            { "Sandy", "char_diver" },
            { "Aang", "char_kite" },
            { "Korra", "char_athlete" },
            { "Toph", "char_clay" },
            { "Lincoln Loud", "char_rascal" },
            { "Lucy Loud", "char_goth" },
            { "Leonardo", "char_moon" },
            { "Michelangelo", "char_pizza" },
            { "April", "char_reporter" },
            { "Ren and Stimpy", "char_duo" },
            { "Powdered ToastMan", "char_hero" },
            { "Zim", "char_alien" },
            { "CatDog", "char_chimera" },
            { "Reptar", "char_mascot" },
            { "Nigel Thornberry", "char_narrator" },
            { "Helga", "char_rival" },
            { "Danny Phantom", "char_plasma" },
            { "Oblina", "char_snake" }
        };
    }
}
