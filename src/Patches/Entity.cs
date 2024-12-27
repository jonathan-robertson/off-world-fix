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
                _log.Trace($"Prefix: {__instance.entityId} ({__instance.GetDebugName()}) marked for unload at position {__instance.position.ToCultureInvariantString()}");

                // TODO: check ownership... maybe teleport backpacks directly in front of player, for instance?

                if (IsWithinWorldBounds(__instance.position))
                {
                    _log.Trace($"detected entity {__instance.entityId} was within world bounds");
                    return true; // not out of bounds, just unloaded normally
                }

                switch (__instance.GetType())
                {
                    // TODO: check more
                    default:
                        _log.Trace($"detected type: {__instance.GetType()}");
                        // TODO: do more
                        break;
                }

                if (IsWithinWorldBoundsXAndZ(__instance.position) && !IsWithinWorldBoundsY(__instance.position))
                {
                    var newPos = __instance.position;
                    newPos.y = GameManager.Instance.World.GetHeightAt(__instance.position.x, __instance.position.z);
                    if (!IsWithinWorldBounds(newPos))
                    {
                        _log.Info($"A valid position could not be determined to recover {__instance.entityId}; allowing entity unload.");
                        return true;
                    }
                    _log.Info($"Detected entity {__instance.entityId} fell out of bottom of world; repositioning from {__instance.position} to {newPos}");
                    __instance.SetPosition(newPos);
                    ConnectionManager.Instance.SendPackage(NetPackageManager.GetPackage<NetPackageEntityTeleport>().Setup(__instance), false, -1, -1, -1, __instance.position, 192);
                    return false;
                }

                // TODO: check for other out-of-bounds possibilities                
            }
            catch (Exception e)
            {
                _log.Error("Prefix", e);
            }
            return true;
        }

        private static bool IsWithinWorldBoundsXAndZ(UnityEngine.Vector3 position)
        {
            return position.x > ModApi.MAP_MIN.x
                    && position.z > ModApi.MAP_MIN.z
                    && position.x <= ModApi.MAP_MAX.x
                    && position.z <= ModApi.MAP_MAX.z;
        }

        private static bool IsWithinWorldBoundsY(UnityEngine.Vector3 position)
        {
            return position.y > ModApi.MAP_MIN.y
                    && position.y <= ModApi.MAP_MAX.y;
        }

        private static bool IsWithinWorldBounds(UnityEngine.Vector3 position)
        {
            return IsWithinWorldBoundsXAndZ(position)
                && IsWithinWorldBoundsY(position);
        }
    }
}
