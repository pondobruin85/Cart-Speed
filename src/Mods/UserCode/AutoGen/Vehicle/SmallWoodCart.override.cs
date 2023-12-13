﻿// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
// <auto-generated from VehicleTemplate.tt />

namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Components.VehicleModules;
    using Eco.Gameplay.GameActions;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Occupancy;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Math;
    using Eco.Shared.Networking;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.Items;
    using Eco.Gameplay.Systems.NewTooltip;
    using Eco.Core.Controller;
	using Eco.Gameplay.Components.Storage;
    using Eco.Core.Utils;
    using Eco.Gameplay.Items.Recipes;
    using Eco.ModKit.Internal;
    using CartSpeed;

    [Serialized]
    [LocDisplayName("Small Wood Cart")]
    [LocDescription("Small wood cart for hauling minimal loads.")]
    [IconGroup("World Object Minimap")]
    [Weight(5000)]
    [Ecopedia("Crafted Objects", "Vehicles", createAsSubPage: true)]
    public partial class SmallWoodCartItem : WorldObjectItem<SmallWoodCartObject>, IPersistentData
    {
        [Serialized, SyncToView, NewTooltipChildren(CacheAs.Instance, flags: TTFlags.AllowNonControllerTypeForChildren)] public object PersistentData { get; set; }
    }

    /// <summary>
    /// <para>Server side recipe definition for "SmallWoodCart".</para>
    /// <para>More information about RecipeFamily objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.RecipeFamily.html</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
    [RequiresSkill(typeof(CarpentrySkill), 1)]
    [Ecopedia("Crafted Objects", "Vehicles", subPageName: "Small Wood Cart Item")]
    public partial class SmallWoodCartRecipe : RecipeFamily
    {
        public SmallWoodCartRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "SmallWoodCart",  //noloc
                displayName: Localizer.DoStr("Small Wood Cart"),

                // Defines the ingredients needed to craft this recipe. An ingredient items takes the following inputs
                // type of the item, the amount of the item, the skill required, and the talent used.
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement("HewnLog", 10, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)), //noloc
                    new IngredientElement("WoodBoard", 15, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)), //noloc
                },

                // Define our recipe output items.
                // For every output item there needs to be one CraftingElement entry with the type of the final item and the amount
                // to create.
                items: new List<CraftingElement>
                {
                    new CraftingElement<SmallWoodCartItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 3; // Defines how much experience is gained when crafted.
            
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(50, typeof(CarpentrySkill));

            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(SmallWoodCartRecipe), start: 2, skillType: typeof(CarpentrySkill));

            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Small Wood Cart"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Small Wood Cart"), recipeType: typeof(SmallWoodCartRecipe));
            this.ModsPostInitialize();

            // Register our RecipeFamily instance with the crafting system so it can be crafted.
            CraftingComponent.AddRecipe(tableType: typeof(CarpentryTableObject), recipe: this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();

        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(TailingsReportComponent))]
    [RequireComponent(typeof(MovableLinkComponent))]
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(CustomTextComponent))]
    [RequireComponent(typeof(MinimapComponent))]           
    [Ecopedia("Crafted Objects", "Vehicles", subPageName: "SmallWoodCart Item")]
    public partial class SmallWoodCartObject : PhysicsWorldObject, IRepresentsItem
    {
        static SmallWoodCartObject()
        {
            WorldObject.AddOccupancy<SmallWoodCartObject>(new List<BlockOccupancy>(0));
        }
        public override TableTextureMode TableTexture => TableTextureMode.Wood;
        public override bool PlacesBlocks            => false;
        public override LocString DisplayName { get { return Localizer.DoStr("Small Wood Cart"); } }
        public Type RepresentedItemType { get { return typeof(SmallWoodCartItem); } }

        private SmallWoodCartObject() { }
        protected override void Initialize()
        {
            base.Initialize();         
            this.GetComponent<CustomTextComponent>().Initialize(200);
            this.GetComponent<VehicleComponent>().HumanPowered(1);
            this.GetComponent<PublicStorageComponent>().Initialize(6, 1400000);
            this.GetComponent<MinimapComponent>().InitAsMovable();
            this.GetComponent<MinimapComponent>().SetCategory(Localizer.DoStr("Vehicles"));
            this.GetComponent<VehicleComponent>().Initialize(6, 1, 1);
            this.GetComponent<VehicleComponent>().FailDriveMsg = Localizer.Do($"You are too hungry to pull this {this.DisplayName}!");
            this.GetComponent<MountComponent>().PlayerMountedEvent += ChangeSpeed;
        }
        void ChangeSpeed()
        {
            CartSpeed.ChangeCartSpeed(this.GetComponent<VehicleComponent>(), baseCartSpeed: 1.0f);
        }
    }
}
