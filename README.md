# NASB Custom Music Mod

This mod allows you to add custom songs to each stage and menu in the game!

## Installation

*If your game isn't modded with BepinEx, DO THAT FIRST!*
Simply go to the [latest BepinEx release](https://github.com/BepInEx/BepInEx/releases) and extract `BepinEx_x64_VERSION.zip` directly into your game's folder, then run the game once to install BepinEx properly.

Next, go to the [latest release of this mod](https://github.com/megalon/NickCustomMusicMod/releases/latest) and extract the zip in your game's install directory.

This will place the dll in `BepInEx\plugins\`, and then create the `BepInEx\CustomSongs\*` folder structure.

## Usage

Folder structure
```
BepInEx
    ↳ CustomSongs
        ↳ Menus
            ↳ ArcadeMap
            ↳ LoseV1
              ...
        ↳ Stages
            ↳ Armada
            ↳ CarnivalLofi
              ...
```

Place `.wav`, `.ogg`, or `.mp3` files into the folder corresponding to the stage or menu you want to change.

For example, if you want to use a different song for the Jellyfish Fields stage, place the audio file in `BepInEx\CustomSongs\Stages\SlideHouse`

If multiple audio files are in the same folder, one is randomly selected each time that stage / menu is loaded!

The folder names correspond to the ingame ID for each stage / menu. Some of them have confusing names! See the list below.

Stages
```
Armada --------- Irken Armada Invasion
CarnivalLofi --- Glove World
Drome ---------- Technodrom Takedown
DuoKitchen ----- Powdered Toast Trouble
DuoMadness ----- Space Madness
House ---------- CatDog's House
Loud ----------- The Loud House
Omashu --------- Omashu
Playground ----- Showdown at Teeter Totter Gulch
Rooftop -------- Rooftop Rumble
SewersCombined - Sewers Slam
Shanty --------- The Flying Dutchman's Ship
SlideHouse ----- Jellyfish Fields
SpiritWorld ---- Harmonic Convergence
Stomp ---------- Traffic Jam
Temple2 -------- Western Air Temple
Trash ---------- The Dump
Waterfall ------ Wild Waterfall
YMC ------------ Royal Woods Cemetery
Zone ----------- Ghost Zone
```

Menus
```
ArcadeMap ----
LoseV1 ------- 
LoseV2 ------- Not sure why there are two of these yet
MainMenu -----
OnlineMenu --- The MainMenu music tends to override this
Versus -------
Victory ------
```
