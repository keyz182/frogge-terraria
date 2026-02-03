using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Frogge;

public class RibbitingShroom: ModItem
{
    public override void SetDefaults() {
        Item.CloneDefaults(ItemID.WispinaBottle);
		
        Item.damage = 0;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.shoot = ModContent.ProjectileType<DivineFroggeProjectile>();
        Item.width = 16;
        Item.height = 30;
        Item.UseSound = SoundID.Item2;
        Item.useAnimation = 20;
        Item.useTime = 20;
        Item.rare = ItemRarityID.Cyan;
        Item.noMelee = true;
        Item.value = Item.sellPrice(0, 5, 50);
        Item.buffType = ModContent.BuffType<DivineFroggeBuff>();
    }

    public override void AddRecipes() {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<Shroom>())
            .AddIngredient(ModLoader.GetMod("CalamityMod"), "FungalClump", 1)
            .AddTile(TileID.DemonAltar)
            .Register();
    }

    public override void UseStyle(Player player, Rectangle heldItemFrame) {
        if (player.whoAmI == Main.myPlayer && player.itemTime == 0) {
            player.AddBuff(Item.buffType, 3600);
        }
    }
    public override bool IsLoadingEnabled(Mod mod) {
        return ModLoader.HasMod("CalamityMod"); 
    } 
}