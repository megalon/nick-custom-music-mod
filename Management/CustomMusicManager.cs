using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using Nick;
using static Nick.MusicMetaData;
using NickCustomMusicMod.Utils;
using System.Threading;

namespace NickCustomMusicMod.Management
{
    internal class CustomMusicManager
	{
		public static string rootCustomSongsPath;

		public static void Init()
        {
			songDictionaries = new Dictionary<string, Dictionary<string, MusicItem>>();

			rootCustomSongsPath = Path.Combine(Paths.BepInExRootPath, "CustomSongs");

			// Create the folder if it doesn't exist
			Directory.CreateDirectory(rootCustomSongsPath);

			Plugin.LogInfo("Loading songs from subfolders...");
			LoadFromSubDirectories(Consts.stagesFolderName);
			LoadFromSubDirectories(Consts.menusFolderName);
			LoadFromSubDirectories(Consts.victoryThemesFolderName);
			LoadFromSongPacks();


			Plugin.LogInfo("Generating folders if they don't exist...");
			foreach (string menuID in Consts.MenuIDs)
			{
				Directory.CreateDirectory(Path.Combine(rootCustomSongsPath, Consts.menusFolderName, menuID));
			}

			foreach (string stageName in Consts.StageIDs.Keys)
			{
				Directory.CreateDirectory(Path.Combine(rootCustomSongsPath, Consts.stagesFolderName, stageName));
			}

			foreach (string characterName in Consts.CharacterIDs.Keys)
			{
				Directory.CreateDirectory(Path.Combine(rootCustomSongsPath, Consts.victoryThemesFolderName, characterName));
			}

			Directory.CreateDirectory(Path.Combine(rootCustomSongsPath, Consts.songPacksFolderName));
		}

		public static void LoadFromSubDirectories(string parentFolderName)
		{
			if (!Directory.Exists(Path.Combine(rootCustomSongsPath, parentFolderName))) return;

			var subDirectories = Directory.GetDirectories(Path.Combine(rootCustomSongsPath, parentFolderName));

			Plugin.LogInfo($"Looping through sub directories in \"{parentFolderName}\"");

			// Copy files from old folders to new
			foreach (string directory in subDirectories)
			{
				FileHandlingUtils.UpdateOldFormat(directory);
			}

			// Since we may have deleted folders in the previous step, get the list again
			subDirectories = Directory.GetDirectories(Path.Combine(rootCustomSongsPath, parentFolderName));
			foreach (string directory in subDirectories)
			{
				var folderName = new DirectoryInfo(directory).Name;

				LoadSongsFromFolder(parentFolderName, folderName);
			}
		}

		public static void LoadSongsFromFolder(string parentFolderName, string folderName)
		{
			Plugin.LogInfo($"LoadSongsFromFolder \"{folderName}\"");
			
			string path = Path.Combine(rootCustomSongsPath, parentFolderName, folderName);

			Dictionary<string, MusicItem> musicItemDict = new Dictionary<string, MusicItem>();

			foreach (string songPath in from x in Directory.GetFiles(path)
				where x.ToLower().EndsWith(".ogg") || x.ToLower().EndsWith(".wav") || x.ToLower().EndsWith(".mp3")
				select x)
				{
				addMusicItemToDict(musicItemDict, songPath);
			}

			songDictionaries.Add(constructDictionaryKey(parentFolderName, folderName), musicItemDict);
		}

		public static void LoadFromSongPacks() {
			if (!Directory.Exists(Path.Combine(rootCustomSongsPath, Consts.songPacksFolderName))) return;

			var subDirectories = Directory.GetDirectories(Path.Combine(rootCustomSongsPath, Consts.songPacksFolderName));

			foreach (string directory in subDirectories) {
				var packName = new DirectoryInfo(directory).Name;

				LoadPack(packName);
			}
		}

		public static void LoadPack(string packName) {
			Plugin.LogInfo($"Loading SongPack:\"{packName}\"");
			var subDirectories = Directory.GetDirectories(Path.Combine(rootCustomSongsPath, Consts.songPacksFolderName, packName));

			foreach (string directory in subDirectories) {
				var folderName = new DirectoryInfo(directory).Name;

				if (folderName.Equals(Consts.musicBankFolderName)) continue;

				LoadFromPackSubdirectory(packName, folderName);
			}
		}

		public static void LoadFromPackSubdirectory(string packName, string folderName) {
			var folderPath = Path.Combine(rootCustomSongsPath, Consts.songPacksFolderName, packName, folderName);

			foreach (string textFileName in from x in Directory.GetFiles(folderPath) where x.ToLower().EndsWith(".txt") select x)
			{
				Plugin.LogInfo($"LoadSongsFromList {packName}\\{folderName}\\{textFileName}");

				string musicBankPath = Path.Combine(rootCustomSongsPath, Consts.songPacksFolderName, packName, Consts.musicBankFolderName);
				string listPath = Path.Combine(folderPath, textFileName);

				Dictionary<string, MusicItem> musicItemDict = songDictionaries[constructDictionaryKey(folderName, Path.GetFileNameWithoutExtension(textFileName))];

				foreach (string songFileName in File.ReadLines(listPath))
				{
					Plugin.LogInfo($"Line text: \"{songFileName}\"");

					string songPath = Path.Combine(musicBankPath, songFileName);

					addMusicItemToDict(musicItemDict, songPath);
				}
			}
		}

		/// <summary>
		/// Victory Theme keys need a special prefix, while others do not.
		/// This function constructs the correct key for each song type.
		/// </summary>
		/// <param name="parentFolderName"></param>
		/// <param name="folderName"></param>
		/// <returns>Dictionary key for "songDictionaries" dictionary</returns>
		private static string constructDictionaryKey(string parentFolderName, string folderName)
		{
			if (parentFolderName == Consts.stagesFolderName || parentFolderName == Consts.menusFolderName)
				return FileHandlingUtils.TranslateFolderNameToID(folderName);
			else
				return $"{parentFolderName}_{FileHandlingUtils.TranslateFolderNameToID(folderName)}";
		}

		private static void addMusicItemToDict(Dictionary<string, MusicItem> musicItemDict, string songPath)
        {
			if (File.Exists(songPath))
			{
				Plugin.LogInfo($"Found custom song: \"{songPath}\"");

				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(songPath);

				MusicItem music = new MusicItem
				{
					id = "CUSTOM_" + fileNameWithoutExtension,
					originalName = fileNameWithoutExtension,
					resLocation = songPath
				};

				if (musicItemDict.ContainsKey(music.id))
				{
					Plugin.LogWarning($"Ignoring \"{songPath}\" because duplicate file was detected! Do you have two different files with the same name in this folder?");
				} else
                {
					musicItemDict.Add(music.id, music);
				}
			}
		}

		internal static Dictionary<string, Dictionary<string, MusicItem>> songDictionaries;
	}

}
