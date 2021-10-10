# NASB Custom Music Mod

This mod allows you to add custom songs to each stage and menu in the game!

# > "My .mp3 didn't work!"
I've had multiple people unable to load certain mp3 files.

If you've downloaded a file from a youtube downloader, such as `ytmp3.cc`, your file might actually be an `.m4a` disguised as a `.mp3`

Unity cannot load these (it seems).

Use a different converter, or convert to `.ogg` instead! Thanks.


## Installation

*If your game isn't modded with BepinEx, DO THAT FIRST!*
Simply go to the [latest BepinEx release](https://github.com/BepInEx/BepInEx/releases) and extract `BepinEx_x64_VERSION.zip` directly into your game's folder, then run the game once to install BepinEx properly.

Next, go to the [latest release of this mod](https://github.com/megalon/NickCustomMusicMod/releases/latest) and extract the zip in your game's install directory.

This will place the dll in `BepInEx\plugins\`, and then create the `BepInEx\CustomSongs\*` folder structure.

## Usage

### Config file

**Run the game once with the mod installed to generate the config file `BepInEx\config\megalon.nick_custom_music_mod.cfg`**.

Simply edit this file in a text editor, and save it, then launch the game again.

| Setting | Possible Values | Description |
|-----|-----|-----|
| Use Default Songs | `true` / `false` | `true` will include the built-in songs when the mod is randomly selecting a song to play. |

### Folder structure
```
BepInEx
    ↳ CustomSongs
        ↳ Menus
            ↳ ArcadeMap
            ↳ LoseV1
              ...
        ↳ Stages
            ↳ CatDogs House
            ↳ Glove World
              ...
```

Place `.wav`, `.ogg`, or `.mp3` files into the folder corresponding to the stage or menu you want to change.

For example, if you want to use a different song for the Jellyfish Fields stage, place the audio file in `BepInEx\CustomSongs\Stages\Jellyfish Fields`

If multiple audio files are in the same folder, one is randomly selected each time that stage / menu is loaded!

# Reference

Developers may be interested in this reference for the music IDs in game.

| Stage Name | Stage ID |
|----------|--------|
| CatDog's House | House |
| Ghost Zone | Zone |
| Glove World | CarnivalLofi |
| Harmonic Convergence | SpiritWorld |
| Irken Armada Invasion | Armada |
| Jellyfish Fields | SlideHouse |
| Omashu | Omashu |
| Powdered Toast Trouble | DuoKitchen |
| Rooftop Rumble | Rooftop |
| Royal Woods Cemetery | YMC |
| Sewers Slam | SewersCombined |
| Showdown at Teeter Totter Gulch | Playground |
| Space Madness | DuoMadness |
| Technodrom Takedown | Drome |
| The Dump | Trash |
| The Flying Dutchman's Ship | Shanty |
| The Loud House | Loud |
| Traffic Jam | Stomp |
| Western Air Temple | Temple2 |
| Wild Waterfall | Waterfall |

| Menu Name | Notes |
|-----------|-------|
| ArcadeMap | Menu music played when you enter the Arcade mode menu|
| LoseV1 | |
| LoseV2 | Not sure why there are two of these yet |
| MainMenu | Main menu music! |
| OnlineMenu | The MainMenu music tends to override this |
| Versus | The "VS" text screen before match starts |
| Victory | End of match victory screen|
