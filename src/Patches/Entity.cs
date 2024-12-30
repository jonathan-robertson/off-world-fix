using HarmonyLib;
using OffWorldFix.Utilities;
using System;

namespace OffWorldFix.Patches
{
    [HarmonyPatch(typeof(Entity), nameof(Entity.MarkToUnload))]
    internal class Entity_MarkToUnload_Patches
    {
        private static readonly ModLog<Entity_MarkToUnload_Patches> _log = new ModLog<Entity_MarkToUnload_Patches>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="__instance"></param>
        /// <returns>Whether to unload naturally from world.</returns>
        public static bool Prefix(Entity __instance)
        {
            try
            {
                if (!IsWithinWorldBoundsXAndZ(__instance.position))
                {
                    _log.Trace($"Detected {__instance.entityType} entity {__instance.entityId} ({__instance.GetType()} // {__instance.GetDebugName()}) was outside the x/z world bounds but we are ignoring those for now; allowing natural unload.");
                    return true;
                }

                if (IsWithinWorldBoundsY(__instance.position))
                {
                    _log.Trace($"Detected {__instance.entityType} entity {__instance.entityId} ({__instance.GetType()} // {__instance.GetDebugName()}) was within world bounds; allowing natural unload.");
                    return true;
                }

                // TODO: check ownership... maybe teleport backpacks directly in front of player, for instance?

                //switch (__instance.GetType())
                //{
                //    // TODO: check more
                //    default:
                //        _log.Trace($"detected type: {__instance.GetType()}");
                //        // TODO: do more
                //        break;
                //}

                if (!IsWithinWorldBoundsY(__instance.position))
                {
                    var newPos = __instance.position;
                    newPos.y = GameManager.Instance.World.GetHeightAt(__instance.position.x, __instance.position.z);
                    if (!IsWithinWorldBoundsY(newPos))
                    {
                        _log.Trace($"A valid position could not be determined to recover {__instance.entityId}; allowing entity unload.");
                        return true;
                    }
                    _log.Info($"Detected {__instance.entityType} entity {__instance.entityId} ({__instance.GetType()} // {__instance.GetDebugName()}) fell out of bottom of world; repositioning from {__instance.position.ToCultureInvariantString()} to {newPos.ToCultureInvariantString()}");
                    __instance.SetPosition(newPos);
                    ConnectionManager.Instance.SendPackage(NetPackageManager.GetPackage<NetPackageEntityTeleport>().Setup(__instance), false, -1, -1, -1, __instance.position, 192);
                    return false;
                }
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
    }
}
