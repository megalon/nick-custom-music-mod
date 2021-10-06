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

namespace NickCustomMusicMod.Management
{
    internal class CustomMusicManager
	{
		public static string rootCustomSongsPath;

		public static void Init()
        {
			songDictionaries = new Dictionary<string, Dictionary<string, MusicItem>>();

			rootCustomSongsPath = Path.Combine(Paths.BepInExRootPath, "CustomSongs");

			Task.Run(delegate ()
			{
				// Create the folder if it doesn't exist
				Directory.CreateDirectory(rootCustomSongsPath);

				// Generate folders, incase any were deleted
				foreach (string menuID in Consts.MenuIDs)
				{
					Directory.CreateDirectory(Path.Combine(rootCustomSongsPath, "Menus", menuID));
				}

				foreach (string stageID in Consts.StageIDs)
				{
					Directory.CreateDirectory(Path.Combine(rootCustomSongsPath, "Stages", stageID));
				}

				// Load songs
				LoadFromSubDirectories("Stages");
				LoadFromSubDirectories("Menus");
			});
        }

		public static void LoadFromSubDirectories(string parentFolderName)
		{
			var subDirectories = Directory.GetDirectories(Path.Combine(rootCustomSongsPath, parentFolderName));

			foreach (string directory in subDirectories)
			{
				var directoryName = Path.GetFileName(directory);

				Plugin.LogDebug($"directory {directoryName} full path {directory}");
				LoadSongsFromFolder(parentFolderName, directoryName);
			}
		}

		public static void LoadSongsFromFolder(string parentFolderName, string folderName)
		{
			Plugin.LogDebug($"LoadSongsFromFolder parentFolderName:{parentFolderName} folderName:{folderName}");
			
			string path = Path.Combine(rootCustomSongsPath, parentFolderName, folderName);

			Dictionary<string, MusicItem> musicItemDict = new Dictionary<string, MusicItem>();

			foreach (string text in from x in Directory.GetFiles(path)
				where x.ToLower().EndsWith(".ogg") || x.ToLower().EndsWith(".wav") || x.ToLower().EndsWith(".mp3")
				select x)
				{
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);

				Plugin.LogInfo($"Found custom song: {parentFolderName}\\{folderName}\\{Path.GetFileName(text)}");

				MusicItem music = new MusicItem
				{
					id = "CUSTOM_" + fileNameWithoutExtension,
					originalName = fileNameWithoutExtension,
					resLocation = text,
				};

				musicItemDict.Add(music.id, music);
			}

			songDictionaries.Add(folderName, musicItemDict);
		}

		internal static Dictionary<string, Dictionary<string, MusicItem>> songDictionaries;
	}
}
