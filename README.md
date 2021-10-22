# NASB Custom Music Mod

This mod allows you to add custom songs to each stage and menu in the game!
_____

## 🚀 Installation

### Slime Mod Manager

Download the latest version of this mod from the [Slime Mod Manager](https://github.com/legoandmars/SlimeModManager/releases/latest)!

## ℹ Usage

For basic usage see the simple format shown below.

For more advanced usage, see [Song Packs](#-song-packs).

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
        ↳ Victory Themes
            ↳ Aang
            ↳ April
              ...
```

Place `.wav`, `.ogg`, or `.mp3` files into the folder corresponding to the stage or menu you want to change.

For example, if you want to use a different song for the Jellyfish Fields stage, place the audio file in `BepInEx\CustomSongs\Stages\Jellyfish Fields`

If multiple audio files are in the same folder, one is randomly selected each time that stage / menu is loaded!

### ⟲ Loop points *(optional)*

To define a loop start and end point, create a JSON file in the same folder as your audio file, and give it the same name as your audio file.

A JSON file is just a regular text file, but instead of `.txt` it is `.json`

You may need to [turn on file extensions on Windows](https://fileinfo.com/help/windows_10_show_file_extensions) to be able to see and edit the `.json` extension.

**File structure example**
```
Stages
    ↳ Jellyfish Fields
        ↳ Song1.wav
        ↳ Song1.json
```

**JSON file contents**
```json
{
  "loopStartPointSec": "16.109",
  "loopEndPointSec": "62.215"
}
```

When `62.215` seconds have elapsed in the song, it will loop back to `16.109` seconds!

To find loop points in your song, you can use some free audio software like [Audacity](https://www.audacityteam.org/).

I would recommend using a DAW with more precise BPM / looping support, such as Reaper, Ableton Live, FL Studio, etc.

## 🎵 Song Packs (Advanced usage)

Version 1.4.0 added **Song Pack** support.

A Song Pack is a more efficient way to reuse songs across multiple stages / menus / victory themes!

```
↳ SongPack1
    ↳ _Music Bank
        ↳ song1.mp3
        ↳ different-song.ogg
        ↳ looped-song.mp3
        ↳ looped-song.json
        ↳ 4th-song.ogg
          ...
    ↳ Menus
        ↳ ArcadeMap.txt
        ↳ LoseV1.txt
          ...
    ↳ Stages
        ↳ CatDogs House.txt
        ↳ Ghost Zone.txt
          ...
    ↳ Victory Themes
        ↳ Aang.txt
        ↳ April.txt
          ...
```

How to make a Song Pack
1. Download an extract this [Song Pack Template.zip](https://github.com/megalon/nick-custom-music-mod/files/7394635/Song.Pack.Template.zip)
1. Navigate into the `CustomSongs/_Song Packs/Template Song Pack/` folder.
1. Place all of the songs you want to use in this song pack into the `_Music Bank` folder.
1. Open the text file for the menu / stage / victory theme you want to add music to. For example, the main menu is `Menus/MainMenu.txt`
1. In this text file, enter a list of song file names from the `_Music Bank` folder that you want to play on this menu / stage / victory theme. The order doesn't matter, but __***you must put each song on it's own line!***__

Example `MainMenu.txt`:
```
song1.mp3
looped-song.mp3
different-song.ogg
```
You can then reuse these songs in other menus / stages / victory themes!

Example `Jellyfish Fields.txt`
```
song1.mp3
looped-song.mp3
4th-song.ogg
```

When you're finished making your Song Pack, be sure to rename the `Template Song Pack` folder to the name of your Song Pack.

Finally, zip the entire folder structure `CustomSongs/_Song Packs/{your song pack}`. Be sure to only include your Song Pack, and not the rest of your custom songs! 

**Note:** *If you want the songs to loop at specific points, you will need to place corresponding json files in the `_Music Bank` folder as well. See [Loop Points.](#-loop-points-optional)*

## 📝 Configuration

**Run the game once with the mod installed to generate the config file:**

`BepInEx\config\megalon.nick_custom_music_mod.cfg`

| Setting | Possible Values | Default | Description |
|-----|-----|-----|-----|
| `Use Default Songs` | `true` or `false` | `true` | Include the built-in songs when the mod is randomly selecting a song to play. However, character specific Victory Themes will *always* play, if available. |
| `Skip OnlineMenu Music if Empty`| `true` or `false` | `true` | If no song files are found in the `OnlineMenu` folder, the online menu music will not play. | 

Simply edit this file in a text editor, and save it, then launch the game again.

## ❔ FAQ

### "My .mp3 didn't work!"
I've had multiple people unable to load certain mp3 files.

If you've downloaded a file from a youtube downloader, such as `ytmp3.cc`, your file might actually be an `.m4a` disguised as a `.mp3`

Unity cannot load these (it seems).

Use a different converter, or convert to `.ogg` instead! Thanks.

### "What happens if I have multiple songs in the same folder?"

The mod will randomly select a song to play each time that stage / menu / victory is loaded.

### "Can I include the default songs in the random selection?"

Yes. You must enable the option in the config file. See *Configuration* above.

## 🔧 Developing

As of **v1.3.x**, this project requires `Newtonsoft.Json.dll`! 

You can install it with `JsonDotNet` via [Slime Mod Manager](https://github.com/legoandmars/slimemodmanager/releases/latest)

### Setup

Clone the project, then create a file in the root of the project directory named:

`NickCustomMusicMod.csproj.user`

Here you need to set the `GameDir` property to match your install directory.

Example:
```xml
<?xml version="1.0" encoding="utf-8"?>
<Project>
  <PropertyGroup>
    <GameDir>D:\SteamLibrary\steamapps\common\Nickelodeon All-Star Brawl</GameDir>
  </PropertyGroup>
</Project>
```

Now when you build the mod, it should resolve your references automatically, and the build event will copy the plugin into your `BepInEx\plugins` folder!

### Notes

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
| ArcadeMap | Menu music played when you enter the Arcade mode menu. |
| LoseV1 | Currently only played when you lose in Arcade mode. |
| LoseV2 | Not sure why there are two of these yet! |
| MainMenu | Main menu music! |
| OnlineMenu | Menu music played when you enter the Online mode menu. |
| Versus | The "VS" text screen before match starts. |
| Victory | End of match victory screen. |
