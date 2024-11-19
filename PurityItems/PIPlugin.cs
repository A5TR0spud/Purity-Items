using BepInEx;
using PurityItems.src.Items;
using PurityItems.src.ItemTiers;
using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

//[assembly: HG.Reflection.SearchableAttribute.OptIn]
namespace PureItemsPlugin
{
    [BepInDependency(ItemAPI.PluginGUID)]

    [BepInDependency(LanguageAPI.PluginGUID)]

    [BepInDependency(ColorsAPI.PluginGUID)]

    [BepInDependency(PrefabAPI.PluginGUID)]

    [BepInDependency(RecalculateStatsAPI.PluginGUID)]

    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]

    public class PIPlugin : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "A5TR0spud";
        public const string PluginName = "PurityItems";
        public const string PluginVersion = "0.1.0";
        public const string TokenPrefix = "PURITY_ITEMS_";
        public static PIPlugin Instance;
        public static AssetBundle Bundle;

        public void Awake()
        {
            Instance = this;
            Log.Init(Logger);

            Bundle = AssetBundle.LoadFromFile(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Info.Location), "AssetBundles", "purityitems"));

            InitItems();
        }

        private void InitItems()
        {
            //tiers
            new PiousWhite();

            //pious white items
            new RefractionOfBlood();
        }
    }
}
