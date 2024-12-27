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
                // TODO: only log this if we take action
                _log.Info($"Prefix: {__instance.entityId} ({__instance.GetDebugName()}) marked for unload at position {__instance.position.ToCultureInvariantString()}");

                switch (__instance.GetType())
                {
                    // TODO: check more
                    default:
                        _log.Trace($"detected type: {__instance.GetType()}");
                        // TODO: do more
                        break;
                }

                if (__instance.position.y < ModApi.MAP_MIN.y)
                {
                    _log.Trace($"detected fell out of bottom of world");
                    // TODO: do more
                }

                // TODO: do more for x/z bounds
            }
            catch (Exception e)
            {
                _log.Error("Prefix", e);
            }
            return true;
        }
    }
}
