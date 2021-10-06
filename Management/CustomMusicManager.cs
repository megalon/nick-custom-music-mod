using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using Nick;
using static Nick.MusicMetaData;

namespace NickCustomMusicMod.Management
{
    internal class CustomMusicManager
	{ 
			public static void Init()
			{
				Task.Run(delegate ()
				{
					LoadSongsFromFolder();
				});
			}

			public static void LoadSongsFromFolder()
			{
				string path = Path.Combine(Paths.BepInExRootPath, "CustomSongs");
				Directory.CreateDirectory(path);
				CustomMusicManager.songEntries = new Dictionary<string, MusicItem>();
				foreach (string text in from x in Directory.GetFiles(path)
					where x.ToLower().EndsWith(".ogg") || x.ToLower().EndsWith(".wav")
					select x)
					{
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);
					
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
}
