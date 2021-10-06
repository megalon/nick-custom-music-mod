# NASB Custom Music Mod

This mod allows you to add custom songs to each stage and menu in the game!

## Installation

*If your game isn't modded with BepinEx, DO THAT FIRST!*
Simply go to the [latest BepinEx release](https://github.com/BepInEx/BepInEx/releases) and extract `BepinEx_x64_VERSION.zip` directly into your game's folder, then run the game once to install BepinEx properly.

Next, go to the [latest release of this mod](https://github.com/megalon/NickCustomMusicMod/releases/latest) and extract the zip in your game's install directory.

This will place the dll in `BepInEx\plugins\`, and the create the `BepInEx\CustomSongs\*` folder structure.

## Usage

Place a `.wav`, `.ogg`, or `.mp3` files into the folder corresponding to the stage or menu you want to change.

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
