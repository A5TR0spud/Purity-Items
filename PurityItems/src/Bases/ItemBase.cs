using PureItemsPlugin;
using PurityItems.src.Utils;
using R2API;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace PurityItems.src.Bases
{
    public abstract class ItemBase : GenericBase<ItemDef>
    {
        protected virtual bool IsConsumed => false;
        protected virtual bool IsRemovable => false;
        protected virtual bool IsHidden => false;
        protected virtual GameObject PickupModelPrefab => Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Mystery/PickupMystery.prefab").WaitForCompletion();
        protected virtual Sprite PickupIconSprite => Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Common/MiscIcons/texMysteryIcon.png").WaitForCompletion();
        protected virtual ItemTag[] Tags => [];
        protected virtual CombinedItemTier Tier => ItemTier.NoTier;

        protected override void Create()
        {
            Value = ScriptableObject.CreateInstance<ItemDef>();
            Value.name = Name;
            Value.isConsumed = IsConsumed;
            Value.canRemove = IsRemovable;
            Value.hidden = IsHidden;
            Value.pickupModelPrefab = PickupModelPrefab;
            Value.pickupIconSprite = PickupIconSprite;
            Value.tags = Tags;
            Value._itemTierDef = Tier;
            Value.deprecatedTier = Tier;

            if (Value)
            {
                Value.AutoPopulateTokens();
                Value.nameToken = PIPlugin.TokenPrefix + Value.nameToken;
                Value.pickupToken = PIPlugin.TokenPrefix + Value.pickupToken;
                Value.descriptionToken = PIPlugin.TokenPrefix + Value.descriptionToken;
                Value.loreToken = PIPlugin.TokenPrefix + Value.loreToken;

                LogDisplay();
            }

            ItemAPI.Add(new CustomItem(Value, ItemDisplay()));
        }
        protected virtual ItemDisplayRuleDict ItemDisplay() => new ItemDisplayRuleDict();
        protected virtual void LogDisplay() { }
    }

    public class FloatingPointFix : MonoBehaviour
    {
        private Transform transformComponent;
        private ModelPanelParameters modelComponent;
        private static readonly int sizeModifier = 100;

        private void Awake()
        {
            transformComponent = GetComponent<Transform>();
            modelComponent = GetComponent<ModelPanelParameters>();
        }
        private void Start()
        {
            if (!transformComponent || !modelComponent) return;
            if (SceneCatalog.currentSceneDef.cachedName == "logbook")
            {
                transformComponent.localScale *= sizeModifier;
                modelComponent.maxDistance *= sizeModifier;
                modelComponent.minDistance *= sizeModifier;
            }
        }
    }
}
