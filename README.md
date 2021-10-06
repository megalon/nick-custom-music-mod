# NASB Custom Music Mod

This mod allows you to add custom songs to each stage and menu in the game!

## Installation

*If your game isn't modded with BepinEx, DO THAT FIRST!*
Simply go to the [latest BepinEx release](https://github.com/BepInEx/BepInEx/releases) and extract `BepinEx_x64_VERSION.zip` directly into your game's folder, then run the game once to install BepinEx properly.

Next, go to the [latest release of this mod](https://github.com/megalon/NickCustomMusicMod/releases/latest) and extract the zip in your game's install directory.

This will place the dll in `BepInEx\plugins\`, and the create the `BepInEx\CustomSongs\*` folder structure.

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

The folder names correspond to the ingame ID for each stage / menu. Some of them have confusing names! See the list below.

Stages
```
Armada
CarnivalLofi
Drome
DuoKitchen
DuoMadness
House
Loud
Omashu
Playground
Rooftop
SewersCombined
Shanty
SlideHouse
SpiritWorld
Stomp
Temple2
Trash
Waterfall
YMC
Zone
```

Menus
```
ArcadeMap
LoseV1
LoseV2
MainMenu
OnlineMenu
Versus
Victory
```
