using BepInEx.Configuration;
using PureItemsPlugin;
using PurityItems.src.Bases;
using PurityItems.src.ItemTiers;
using PurityItems.src.Utils;
using R2API;
using RoR2;
using RoR2.Items;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PurityItems.src.Items
{
    public class RefractionOfBlood : ItemBase
    {
        protected override string Name => "REFRACTION_OF_BLOOD";
        public static ItemDef ItemDef;

        protected override CombinedItemTier Tier => PiousWhite.PiousWhiteTierDef;
        //protected override Sprite PickupIconSprite => LegacyResourcesAPI.Load<Sprite>("Textures/MiscIcons/texGameResultUnknownIcon");
        //protected override GameObject PickupModelPrefab => ;
        private static List<ItemIndex> convertItems;

        protected override bool IsEnabled()
        {
            return true;
        }

        protected override void Initialize()
        {
            ItemDef = Value;
            PiousWhite.ItemTierPool.Add(ItemDef.itemIndex);
            convertItems = new List<ItemIndex>();
            convertItems.Add(RoR2Content.Items.FlatHealth.itemIndex);        //bison steak
            convertItems.Add(RoR2Content.Items.PersonalShield.itemIndex);    //personal shield generator
            convertItems.Add(RoR2Content.Items.HealWhileSafe.itemIndex);     //cautious slug
            PiousWhite.ItemCounterpartPool.Add(new PiousWhite.WhitePiousCounterpart
            {
                originalItems = convertItems,
                piousItem = ItemDef.itemIndex
            });

            RecalculateStatsAPI.GetStatCoefficients += (sender, args) =>
            {
                if (sender && sender.inventory)
                {
                    int count = sender.inventory.GetItemCount(ItemDef);

                    args.levelHealthAdd += 2 * count;
                    args.levelRegenAdd += 0.1f * count;
                }
            };
        }
    }
}
