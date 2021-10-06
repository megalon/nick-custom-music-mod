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
        public static void Init()
        {
			songDictionaries = new Dictionary<string, Dictionary<string, MusicItem>>();

			Task.Run(delegate ()
			{
				foreach (string stageID in Consts.StageIDs)
				{
					LoadSongsFromFolder("Stages", stageID);
				}
				foreach (string menuID in Consts.MenuIDs)
				{
					LoadSongsFromFolder("Menus", menuID);
				}
			});
        }

		public static void LoadSongsFromFolder(string parentFolderName, string folderName)
		{
			string path = Path.Combine(Paths.BepInExRootPath, Path.Combine("CustomSongs", parentFolderName, folderName));
			Directory.CreateDirectory(path);
			Dictionary<string, MusicItem> musicItemDict = new Dictionary<string, MusicItem>();
			foreach (string text in from x in Directory.GetFiles(path)
				where x.ToLower().EndsWith(".ogg") || x.ToLower().EndsWith(".wav") || x.ToLower().EndsWith(".mp3")
				select x)
				{
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);

				Console.WriteLine($"Loaded {text}");
					
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
