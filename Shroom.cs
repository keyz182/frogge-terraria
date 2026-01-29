using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Frogge;

public class Shroom: ModItem
{
	public override void SetDefaults() {
		Item.CloneDefaults(ItemID.WispinaBottle);
		
		Item.damage = 0;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.shoot = ModContent.ProjectileType<FroggeProjectile>();
		Item.width = 16;
		Item.height = 30;
		Item.UseSound = SoundID.Item2;
		Item.useAnimation = 20;
		Item.useTime = 20;
		Item.rare = ItemRarityID.Yellow;
		Item.noMelee = true;
		Item.value = Item.sellPrice(0, 5, 50);
		Item.buffType = ModContent.BuffType<FroggeBuff>();
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ItemID.Mushroom, 5)
			.AddIngredient(ItemID.Frog, 1)
			.AddTile(TileID.DemonAltar)
			.Register();
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame) {
		if (player.whoAmI == Main.myPlayer && player.itemTime == 0) {
			player.AddBuff(Item.buffType, 3600);
		}
	}
}