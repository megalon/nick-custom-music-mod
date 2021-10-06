using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Reflection;

namespace NickCustomMusic
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static Plugin Instance;
        private void Awake()
        {
            Logger.LogDebug($"Plugin {PluginInfo.PLUGIN_NAME} is loaded!");

            if (Instance)
            {
                DestroyImmediate(this);
                return;
            }
            Instance = this;

            var config = this.Config;

            // Harmony patches
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        }

        internal static void LogDebug(string message) => Instance.Log(message, LogLevel.Debug);
        internal static void LogInfo(string message) => Instance.Log(message, LogLevel.Info);
        internal static void LogError(string message) => Instance.Log(message, LogLevel.Error);
        private void Log(string message, LogLevel logLevel) => Logger.Log(logLevel, message);
    }
}
