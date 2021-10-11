using System;
using System.Collections.Generic;
using System.Text;
using Nick;
using HarmonyLib;
using System.Linq;
using System.Reflection.Emit;
using NickCustomMusicMod.Management;
using NickCustomMusicMod.Utils;
using System.Reflection;

namespace NickCustomMusicMod.Patches
{
    [HarmonyPatch(typeof(ResultsAudioManager), "OnResultsLoaded")]
    class ResultsAudioManager_OnResultsLoaded
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            int index = 0;

            bool foundFirst = false;

            foreach (var instruction in instructions)
            {
                if (instruction.operand is string && (string)instruction.operand == "Victory")
                {
                    if (!foundFirst)
                    {
                        foundFirst = true;
                        continue;
                    }
                    index -= 1;
                    break;
                }
                index++;
            }

            var musicChanger = typeof(ResultsAudioManager).GetField("musicChanger");

            instructions.ElementAt(index).opcode = OpCodes.Ldarg_1;
            instructions.ElementAt(++index).opcode = OpCodes.Ldarg_0;
            instructions.ElementAt(++index).opcode = OpCodes.Ldfld;
            instructions.ElementAt(index).operand = musicChanger;

            var method = SymbolExtensions.GetMethodInfo(() => ResultsAudioManager_OnResultsLoaded.HandleVictoryTheme(null, null));

            instructions.ElementAt(++index).opcode = OpCodes.Call;
            instructions.ElementAt(index).operand = method;

            return instructions;
        }

        public static void HandleVictoryTheme(GameResults results, MusicChanger musicChanger)
        {
            if (results == null) return;

            var winner = results.players.Where(x => x.active && x.place == 1).FirstOrDefault().characterId;
            var winnerKey = $"{Consts.victoryThemesFolderName}_{winner}";

            if (CustomMusicManager.songDictionaries.ContainsKey(winnerKey))
                musicChanger.StartMusic(winnerKey);
        }
    }
}
