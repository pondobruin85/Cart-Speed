//Cart Speed
//Copyright (C) 2023 Seth Reavis
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System.Runtime.CompilerServices;
using CartSpeedMod;
using Eco.Core.Plugins.Interfaces;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.ModKit.Internal;
using Eco.Mods.TechTree;
using Eco.Shared.Gameplay;
using Eco.Shared.Localization;
using Eco.Shared.Logging;
using Eco.World.Blocks;
using User = Eco.Gameplay.Players.User;

namespace CartSpeedMod
{
    public class CartSpeedMod : IModInit
    {
        public static ModRegistration Register() => new()
        {
            ModName = "CartSpeed",
            ModDescription = "Changes human powered carts' speed to be based on the deployer's footwear.",
            ModDisplayName = "Cart Speed Mod",
        };
    }

    public static partial class CartSpeed
    {
        static float GetCartSpeedMultiplier(User user)
        {
            ClothingItem playerShoes = null;

            foreach (ItemStack playerInventoryItem in user.Inventory.Clothing.NonEmptyStacks)
            {
                if (playerInventoryItem.Item is ClothingItem maybeShoes && maybeShoes.Slot == AvatarAppearanceSlots.Shoes)
                {
                    playerShoes = maybeShoes;
                    break;
                }
            }

            switch (playerShoes)
            {
                case null:                      return 4;                     //barefoot
                case GardenBootsItem:           return 8;
                case TallBootsItem:             return 8;
                case SandalsItem:               return 8;
                case LowTopShoesItem:           return 8;
                case RunningShoesItem:          return 13;
                case WorkBootsItem:             return 13;
                case BuilderBootsItem:          return 17;       //top tier
                case SmithBootsItem:            return 17;
                case ExplorerBootsItem:         return 17;
                case FarmerBootsItem:           return 17;
                case TailorShoesItem:           return 17;
                case ChefShoesItem:             return 17;
                case ShipwrightSandalsItem:     return 17;  //top speed
                default:                        return 4;
            }
        }

        public static float SetCartSpeed (User user, string vehicle)
        {
            switch (vehicle)
            {
                case "WheelbarrowObject": return GetCartSpeedMultiplier(user)        * 2f;
                case "SmallWoodCartObject": return GetCartSpeedMultiplier(user)      * 1.5f;
                case "WoodCartObject": return GetCartSpeedMultiplier(user)           * 1.0f;
                case "WoodShopCartObject": return GetCartSpeedMultiplier(user)       * 1.0f;
                case "HandPlowObject": return GetCartSpeedMultiplier(user)           * 1.0f;
            }
            return 1.0f;
        }
    }
}
