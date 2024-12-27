using HarmonyLib;
using OffWorldFix.Utilities;
using System;
using System.Reflection;

namespace OffWorldFix
{
    internal class ModApi : IModApi
    {
        private const string ModMaintainer = "kanaverum";
        private const string SupportLink = "https://discord.gg/hYa2sNHXya";

        private static readonly ModLog<ModApi> _log = new ModLog<ModApi>();

        public static Vector3i MAP_MIN { get; private set; }
        public static Vector3i MAP_MAX { get; private set; }
        public static bool DebugMode { get; set; } = false;

        public void InitMod(Mod _modInstance)
        {
            try
            {
                new Harmony(GetType().ToString()).PatchAll(Assembly.GetExecutingAssembly());
                ModEvents.GameStartDone.RegisterHandler(OnGameStartDone);
            }
            catch (Exception e)
            {
                _log.Error($"Failed to start up Off World Fix mod; take a look at logs for guidance but feel free to also reach out to the mod maintainer {ModMaintainer} via {SupportLink}", e);
            }
        }

        public Vector3i GetMapMin() { return MAP_MIN; }

        private static void OnGameStartDone()
        {
            try
            {
                if (!GameManager.Instance.World.GetWorldExtent(out var _min, out var _max))
                {
                    throw new Exception("World.GetWorldExtent failed when checking for limits; this is not expected and may indicate an error.");
                }
                MAP_MIN = _min;
                MAP_MAX = _max;
                _log.Info($"Off World Fix mod determined total world size of {MAP_MAX - MAP_MIN}, spanning from {MAP_MIN} to {MAP_MAX}");
            }
            catch (Exception e)
            {
                _log.Error($"OnGameStartDone Failed for Off World Fix mod; take a look at logs for guidance but feel free to also reach out to the mod maintainer {ModMaintainer} via {SupportLink}", e);
            }
        }
    }
}
