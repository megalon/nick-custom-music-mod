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
            Task.Run(delegate ()
			{
				foreach (string stageID in Consts.StageIDs)
				{
					LoadSongsFromFolder(Path.Combine("Stages", stageID));
				}
				foreach (string menuID in Consts.MenuIDs)
				{
					LoadSongsFromFolder(Path.Combine("Menus", menuID));
				}
			});
        }

		public static void LoadSongsFromFolder(string folderName)
		{
			string path = Path.Combine(Paths.BepInExRootPath, Path.Combine("CustomSongs", folderName));
			Directory.CreateDirectory(path);
			CustomMusicManager.songEntries = new Dictionary<string, MusicItem>();
			foreach (string text in from x in Directory.GetFiles(path)
				where x.ToLower().EndsWith(".ogg") || x.ToLower().EndsWith(".wav")
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

				songEntries.Add(music.id, music);
			}
		}
		internal static Dictionary<string, MusicItem> songEntries;
	}
}
