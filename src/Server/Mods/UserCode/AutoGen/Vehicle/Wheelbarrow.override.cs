﻿// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
// <auto-generated from VehicleTemplate.tt />

namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Core.Controller;
    using Eco.Core.Items;
    using Eco.Core.Utils;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Components.Storage;
    using Eco.Gameplay.Components.VehicleModules;
    using Eco.Gameplay.GameActions;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Occupancy;
    using Eco.Gameplay.Systems.Exhaustion;
    using Eco.Gameplay.Systems.NewTooltip;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Math;
    using Eco.Shared.Networking;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.Items;

    [Serialized]
    [LocDisplayName("Wheelbarrow")]
    [LocDescription("Small wheelbarrow for hauling minimal loads.")]
    [IconGroup("World Object Minimap")]
    [Weight(5000)]
    [Ecopedia("Crafted Objects", "Vehicles", createAsSubPage: true)]
    public partial class WheelbarrowItem : WorldObjectItem<WheelbarrowObject>, IPersistentData
    {
        [Serialized, SyncToView, NewTooltipChildren(CacheAs.Instance, flags: TTFlags.AllowNonControllerTypeForChildren)] public object PersistentData { get; set; }
    }

    /// <summary>
    /// <para>Server side recipe definition for "Wheelbarrow".</para>
    /// <para>More information about RecipeFamily objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.RecipeFamily.html</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
    [Ecopedia("Crafted Objects", "Vehicles", subPageName: "Wheelbarrow Item")]
    public partial class WheelbarrowRecipe : RecipeFamily
    {
      public WheelbarrowRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Wheelbarrow",  //noloc
                displayName: Localizer.DoStr("Wheelbarrow"),

               // Defines the ingredients needed to craft this recipe. An ingredient items takes the following inputs
               // type of the item, the amount of the item, the skill required, and the talent used.
               ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(AdobeItem), 2,typeof(Skill)), //noloc
                    new IngredientElement("Wood", 5,typeof(Skill)), //noloc
                    new IngredientElement(typeof(PlantFibersItem), 10,typeof(Skill)),
                },

                // Define our recipe output items.
                // For every output item there needs to be one CraftingElement entry with the type of the final item and the amount
                // to create.
                items: new List<CraftingElement>
                {
                    new CraftingElement<WheelbarrowItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(75);

            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(2);

            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Wheelbarrow"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Wheelbarrow"), recipeType: typeof(WheelbarrowRecipe));
            this.ModsPostInitialize();

            // Register our RecipeFamily instance with the crafting system so it can be crafted.
            CraftingComponent.AddRecipe(tableType: typeof(WorkbenchObject), recipeFamily: this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();

        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(PaintableComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(TailingsReportComponent))]
    [RequireComponent(typeof(MovableLinkComponent))]
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(CustomTextComponent))]
    [RequireComponent(typeof(ModularStockpileComponent))]
    [RequireComponent(typeof(MinimapComponent))]           
    [Ecopedia("Crafted Objects", "Vehicles", subPageName: "Wheelbarrow Item")]
    public partial class WheelbarrowObject : PhysicsWorldObject, IRepresentsItem
    {
        static WheelbarrowObject()
        {
            WorldObject.AddOccupancy<WheelbarrowObject>(new List<BlockOccupancy>(0));
        }
        public override TableTextureMode TableTexture => TableTextureMode.Wood;
        public override bool PlacesBlocks            => false;
        public override LocString DisplayName { get { return Localizer.DoStr("Wheelbarrow"); } }
        public Type RepresentedItemType { get { return typeof(WheelbarrowItem); } }

        private WheelbarrowObject() { }
        protected override void Initialize()
        {
            this.ModsPreInitialize();
            base.Initialize();
            this.GetComponent<CustomTextComponent>().Initialize(200);
            this.GetComponent<VehicleComponent>().HumanPowered(1);
            this.GetComponent<StockpileComponent>().Initialize(new Vector3i(1,1,1));
            this.GetComponent<PublicStorageComponent>().Initialize(4, 800000);
            this.GetComponent<MinimapComponent>().InitAsMovable();
            this.GetComponent<MinimapComponent>().SetCategory(Localizer.DoStr("Vehicles"));
            this.GetComponent<VehicleComponent>().Initialize(10,1,1);
            this.GetComponent<VehicleComponent>().FailDriveMsg = Localizer.Do($"You are too hungry to pull this {this.DisplayName}!");   
            this.ModsPostInitialize();    
        }

        /// <summary>Hook for mods to customize before initialization. You can change housing values here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize after initialization.</summary>
        partial void ModsPostInitialize();
    }
}
