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

            Directory.CreateDirectory(Path.Combine(rootCustomSongsPath, Consts.musicBankFolderName));
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

			foreach (string text in from x in Directory.GetFiles(path)
				where x.ToLower().EndsWith(".ogg") || x.ToLower().EndsWith(".wav") || x.ToLower().EndsWith(".mp3") || x.ToLower().EndsWith(".txt")
				select x)
				{
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);

				Plugin.LogInfo($"Found custom song: {parentFolderName}\\{folderName}\\{Path.GetFileName(text)}");

				MusicItem music = new MusicItem {
					id = "CUSTOM_" + fileNameWithoutExtension,
					originalName = fileNameWithoutExtension,
					resLocation = text,
				};

				// Files with a .txt extension will be redirected to the Music Bank folder to find music of the same name, with a naming convention of example.ogg.txt
				if (Path.GetExtension(text) == ".txt") {
					music.resLocation = Path.Combine(rootCustomSongsPath, Consts.musicBankFolderName, fileNameWithoutExtension).ToString();
					if (File.Exists(music.resLocation))
						fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileNameWithoutExtension);
						music.id = "CUSTOM_" + fileNameWithoutExtension;
						music.originalName = fileNameWithoutExtension;
						musicItemDict.Add(music.id, music);
					// As to not double up and not add non existent files
					continue;
				}

				if(musicItemDict.ContainsKey(music.id))
                {
					Plugin.LogWarning($"Ignoring \"{text}\" because duplicate file was detected! Do you have two different files with the same name in this folder?");
					continue;
                }

				musicItemDict.Add(music.id, music);
			}

			string prefix;

			if (parentFolderName == Consts.stagesFolderName || parentFolderName == Consts.menusFolderName)
				prefix = String.Empty;
			else
				prefix = $"{parentFolderName}_";

			songDictionaries.Add(prefix + FileHandlingUtils.TranslateFolderNameToID(folderName), musicItemDict);
		}

		internal static Dictionary<string, Dictionary<string, MusicItem>> songDictionaries;
	}
}
