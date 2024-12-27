using HarmonyLib;
using OffWorldFix.Utilities;
using System;

namespace OffWorldFix.Patches
{
    [HarmonyPatch(typeof(Entity), nameof(Entity.MarkToUnload))]
    internal class Entity_MarkToUnload_Patches
    {
        private static readonly ModLog<Entity_MarkToUnload_Patches> _log = new ModLog<Entity_MarkToUnload_Patches>();

        public static bool Prefix(Entity __instance)
        {
            try
            {
                _log.Info($"Prefix: {__instance.entityId} marked for unload at position {__instance.position}");
            }
            catch (Exception e)
            {
                _log.Error("Prefix", e);
            }
            return true;
        }
    }
}
