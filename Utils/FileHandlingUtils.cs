using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NickCustomMusicMod.Utils
{
    class FileHandlingUtils
	{
		public static string TranslateFolderNameToID(string folderName)
		{
			if (Consts.StageIDs.ContainsKey(folderName))
			{
				return Consts.StageIDs[folderName];
			}
			else if (Consts.CharacterIDs.ContainsKey(folderName))
			{
				return Consts.CharacterIDs[folderName];
			}
			return folderName;
		}

		public static void UpdateOldFormat(string folderPath)
		{
			var folderName = Path.GetFileName(folderPath);

			if (Consts.StageIDs.ContainsValue(folderName))
			{
				string updatedStageName = "";
				foreach (string key in Consts.StageIDs.Keys)
				{
					if (Consts.StageIDs[key] == folderName)
					{
						updatedStageName = key;
					}
				}

				// StageID and display name are the same. EX: Omashu
				if (folderName.Equals(updatedStageName))
				{
					return;
				}

				string updatedFolderPath = Path.Combine(Directory.GetParent(folderPath).FullName, updatedStageName);

				try
				{
					Plugin.LogInfo($"Renaming \"{folderName}\" to \"{updatedStageName}\"...");
					Directory.Move(folderPath, updatedFolderPath);
				}
				catch (IOException ex)
				{
					Plugin.LogInfo($"Could not rename directory \"{folderName}\"! Maybe the new directory already exists?");
					Plugin.LogInfo($"Attempting to copy files from \"{folderName}\" to \"{updatedStageName}\" instead.");
					CopyFilesAndDeleteOriginalFolder(folderPath, updatedFolderPath);
				}
				catch (Exception ex)
				{
					Plugin.LogError($"Failed to rename old folder \"{folderName}\" to \"{updatedStageName}\"!");
					Plugin.LogError($"Exception {ex.Message}");
				}
			}
		}

		public static bool CopyFilesAndDeleteOriginalFolder(string originalDirPath, string targetDirPath)
		{
			string[] files = Directory.GetFiles(originalDirPath);

			try
			{
				// Copy the files and overwrite destination files if they already exist.
				foreach (string filePath in files)
				{
					string fileName = Path.GetFileName(filePath);
					string destPath = Path.Combine(targetDirPath, fileName);
					Plugin.LogInfo($"Copying file \"{fileName}\" to \"{destPath}\"");
					File.Copy(filePath, destPath, true);
				}

				Plugin.LogInfo($"Finished copying files. Deleting original folder \"{originalDirPath}\"");

				try
				{
					// Delete og folder after copying files
					Directory.Delete(originalDirPath, true);
					Plugin.LogInfo($"Deleted \"{originalDirPath}\"");
				}
				catch (Exception ex)
				{
					Plugin.LogError($"Failed to delete original folder \"{originalDirPath}\"!");
					Plugin.LogError($"Exception {ex.Message}");
					return false;
				}
			}
			catch (Exception ex)
			{
				Plugin.LogError($"Failed to copy files from folder \"{originalDirPath}\" to \"{targetDirPath}\"!");
				Plugin.LogError($"Exception {ex.Message}");
				return false;
			}

			return true;
		}
	}
}
