using PureItemsPlugin;
using PurityItems.src.Bases;
using R2API;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static RoR2.ColorCatalog;

namespace PurityItems.src.ItemTiers
{
    public class PiousWhite : ItemTierBase
    {
        protected override string Name => "PiousWhite";

        public static ItemTierDef PiousWhiteTierDef;

        protected override ColorIndex Color => ColorsAPI.RegisterColor(new Color32(141, 201, 255, 255));
        protected override ColorIndex DarkColor => ColorsAPI.RegisterColor(new Color32(102, 135, 225, 255));

        //protected override Texture IconBackgroundTexture => PIPlugin.Bundle.LoadAsset<Sprite>("PureBackgroundIcon").texture;
        protected override GameObject DropletDisplayPrefab => CreateDroplet();

        protected override bool CanBeScrapped => false;

        public static List<ItemIndex> ItemTierPool = [];

        public static List<WhitePiousCounterpart> ItemCounterpartPool = [];

        public struct WhitePiousCounterpart
        {
            public List<ItemIndex> originalItems;
            public ItemIndex piousItem;
        }

        protected override void Initialize()
        {
            PiousWhiteTierDef = Value;
        }

        private GameObject CreateDroplet()
        {
            GameObject orbDrop = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Common/LunarOrb.prefab").WaitForCompletion().InstantiateClone("PrimordialOrb", true);
            Color nearbyColor = new Color32(102, 135, 225, 255);
            Color trailingOffColor = new Color32(64, 78, 124, 255);

            var trail = orbDrop.GetComponentInChildren<TrailRenderer>();
            if (trail)
            {
                trail.startColor = nearbyColor;
                trail.set_startColor_Injected(ref nearbyColor);
                trail.endColor = trailingOffColor;
                trail.set_endColor_Injected(ref trailingOffColor);
            }

            foreach (ParticleSystem particle in orbDrop.GetComponentsInChildren<ParticleSystem>())
            {
                var main = particle.main;
                var colorLifetime = particle.colorOverLifetime;

                main.startColor = new ParticleSystem.MinMaxGradient(nearbyColor);
                colorLifetime.color = nearbyColor;
            }

            var light = orbDrop.GetComponentInChildren<Light>();
            if (light)
            {
                light.color = nearbyColor;
                //light.intensity = 20;
                //light.range = 3.5f;
            }

            return orbDrop;
        }
    }
}
