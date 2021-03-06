﻿using System.Collections.Generic;
using System.Linq;
using BepInEx.Configuration;

namespace ABearCodes.Valheim.CraftingWithContainers
{
    public partial class Plugin
    {
        public static class Settings
        {
            private static ConfigEntry<string> _allowedContainerLookupPieceNames;

            public static ConfigEntry<bool> CraftingWithContainersEnabled { get; set; }

            public static ConfigEntry<float> ContainerLookupRangeMultiplier { get; set; }

            public static ConfigEntry<bool> TakeFromPlayerInventoryFirst { get; set; }

            public static ConfigEntry<bool> ShowStationExtensionEffect { get; set; }

            public static ConfigEntry<string> AllowedContainerLookupPieceNames
            {
                get => _allowedContainerLookupPieceNames;
                set
                {
                    void SplitNewValueAndSetProperty()
                    {
                        AllowedContainerLookupPieceNamesAsList = value.Value.Split(',')
                            .Select(entry => entry.Trim())
                            .ToList();
                    }

                    _allowedContainerLookupPieceNames = value;
                    value.SettingChanged += (sender, args) => { SplitNewValueAndSetProperty(); };
                    SplitNewValueAndSetProperty();
                }
            }

            public static List<string> AllowedContainerLookupPieceNamesAsList { get; private set; }

            public static ConfigEntry<bool> ShouldFilterByContainerPieceNames { get; set; }

            public static void BindConfig(ConfigFile configFile)
            {
                Settings.CraftingWithContainersEnabled = configFile.Bind("CraftingWithContainers",
                    "Enabled", true,
                    "Enable using resources from nearby containers.\n" +
                    "Enables/disables the main functionality of the mod");
                Settings.TakeFromPlayerInventoryFirst = configFile.Bind("CraftingWithContainers",
                    "TakeFromPlayerInventoryFirst", false,
                    "Prioritize taking items from the players inventory when crafting");
                Settings.ContainerLookupRangeMultiplier = configFile.Bind("CraftingWithContainers",
                    "ContainerLookupRangeMultiplier", 1.0f,
                    "Multiplier for the range in which the mod searches for containers.\n" +
                    "Base range is equal to the range of the crafting table in use.\n" +
                    "Will not take from containers that are not currently loaded into memory.");
                Settings.ShowStationExtensionEffect = configFile.Bind("CraftingWithContainers",
                    "ShowStationExtensionEffect", true,
                    "Adds a station extension effect to chests. This effect is the one that\n" +
                    "the game uses by default for chopping blocks, tanning decks, etc\n" +
                    "Shouldn't influence performance");
                Settings.ShouldFilterByContainerPieceNames = configFile.Bind("CraftingWithContainers.Filtering",
                    "ShouldFilterByContainerPieceNames", false,
                    "If enabled, will filter the linked containers by it's owning object name.\n" +
                    "For example, you might want to not link carts or ships.");
                Settings.AllowedContainerLookupPieceNames = configFile.Bind("CraftingWithContainers.Filtering",
                    "AllowedContainerLookupPieceNames",
                    string.Join(", ", "$piece_chestwood", "$piece_chest", "$piece_chestprivate", "Cart", "$ship_karve",
                        "$ship_longship"),
                    "Comma separated list of filtered \"holders\" for the containers:" +
                    "chests, carts, ships. Uses the name of the \"Piece\" the container is attached to");
            }
        }
    }
}