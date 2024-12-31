using HarmonyLib;
using OffWorldFix.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace OffWorldFix.Patches
{
    [HarmonyPatch(typeof(Entity), nameof(Entity.MarkToUnload))]
    internal class Entity_MarkToUnload_Patches
    {
        private static readonly ModLog<Entity_MarkToUnload_Patches> _log = new ModLog<Entity_MarkToUnload_Patches>();
        private static readonly Vector3 BLOCK_CENTER = new Vector3(0.5f, 0, 0.5f);

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
                    var newPos = __instance.GetBlockPosition() + BLOCK_CENTER;
                    var entitySize = new Vector3(__instance.width, __instance.height, __instance.depth);

                    // find new y position based on height
                    newPos.y = GameManager.Instance.World.GetHeightAt(__instance.position.x, __instance.position.z) + entitySize.y;
                    if (!IsWithinWorldBoundsY(newPos))
                    {
                        _log.Trace($"A valid position could not be determined to recover {__instance.entityId}; allowing entity unload.");
                        return true;
                    }
                    while (GetEntitiesAt(newPos, entitySize).Count > 0 && newPos.y < ModApi.MAP_MAX.y)
                    {
                        newPos.y += entitySize.y;
                    }
                    if (newPos.y >= ModApi.MAP_MAX.y)
                    {
                        _log.Trace($"A valid position was crawled but could not be determined to recover {__instance.entityId} due to too many entities at each possible vertical respawn destination; allowing entity unload.");
                        return true; // TODO: test!
                    }

                    // move entity
                    _log.Info($"Detected {__instance.entityType} entity {__instance.entityId} ({__instance.GetType()} // {__instance.GetDebugName()}) fell out of bottom of world; repositioning from {__instance.position.ToCultureInvariantString()} to {newPos.ToCultureInvariantString()}");
                    __instance.SetVelocity(Vector3.zero);
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

        private static bool IsWithinWorldBoundsXAndZ(Vector3 position)
        {
            return position.x > ModApi.MAP_MIN.x
                    && position.z > ModApi.MAP_MIN.z
                    && position.x <= ModApi.MAP_MAX.x
                    && position.z <= ModApi.MAP_MAX.z;
        }

        private static bool IsWithinWorldBoundsY(Vector3 position)
        {
            return position.y > ModApi.MAP_MIN.y
                    && position.y <= ModApi.MAP_MAX.y;
        }

        /// <summary>
        /// Retrieve all entities currently within the given block position.
        /// </summary>
        /// <param name="blockPos">Vector3i block position to check for entities within.</param>
        /// <returns>List of entities currently within the provided block position.</returns>
        private static List<EntityAlive> GetEntitiesAt(Vector3 pos, Vector3 entitySize)
        {
            return GameManager.Instance.World.GetLivingEntitiesInBounds(null, new Bounds(pos, entitySize));
        }
    }
}
