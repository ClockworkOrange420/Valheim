﻿using System;
using HarmonyLib;

namespace ABearCodes.Valheim.CraftingWithContainers.Patches
{
    [HarmonyPatch]
    public static class ReversePatches
    {
        [HarmonyReversePatch]
        [HarmonyPatch(typeof(Inventory), "CountItems")]
        public static int InventoryCountItems(Inventory __instance, string name)
        {
            // We override the default behaviour but we still want to the initial implementation
            throw new NotImplementedException("Stub");
        }

        [HarmonyReversePatch]
        [HarmonyPatch(typeof(Inventory), "RemoveItem", typeof(string), typeof(int))]
        public static void InventoryRemoveItemByString(Inventory __instance, string name, int amount)
        {
            // We override the default behaviour but we still want to the initial implementation
            throw new NotImplementedException("Stub");
        }

        [HarmonyReversePatch]
        [HarmonyPatch(typeof(Container), "CheckAccess", typeof(long))]
        public static bool ContainerCheckAccess(Container __instance, long playerID)
        {
            // Container.Check is private by default
            throw new NotImplementedException("Stub");
        }

        [HarmonyReversePatch]
        [HarmonyPatch(typeof(Container), "Save")]
        public static void ContainerSave(Container __instance)
        {
            // Container.Save is private by default
            throw new NotImplementedException("Stub");
        }
    }
}