using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Frogge;

public class DivineFroggeProjectile: FroggeProjectile
{
    public override int DashCooldown => 500;
    public override float DashSpeed => 30f;
    public override int FullBrightTicks => 400;
    public override float Range => 600f;

    public Dictionary<int, float> WeightedDrops = new()
    {
        {ItemID.CopperCoin, 1f},
        {ItemID.SilverCoin, 0.1f},
        {ItemID.GoldCoin, 0.01f},
        {ItemID.PlatinumCoin, 0.001f}
    };
    
    public IntRange SpawnTickRange => new(5*60*60, 20*60*60);
    public IntRange SpawnCountRange => new(1, 5);

    private int spawnTimer = -1;

    public override void AI()
    {
        base.AI();

        if (Projectile.owner != Main.myPlayer)
            return;

        if (spawnTimer < 0)
        {
            spawnTimer = SpawnTickRange.RandInRange();
        }

        spawnTimer--;

        if (spawnTimer <= 0)
        {
            SpawnDrop();
            spawnTimer = SpawnTickRange.RandInRange();
        }
    }

    private void SpawnDrop()
    {
        Player player = Main.player[Projectile.owner];
        
        int itemType = GetWeightedDrop();
        int count = SpawnCountRange.RandInRange();

        for (int i = 0; i < count; i++)
        {
            Vector2 spawnPos = player.Center + Main.rand.NextVector2Circular(100f, 100f);
            Item.NewItem(Projectile.GetSource_FromAI(), spawnPos, itemType, 1);
        }
    }

    private int GetWeightedDrop()
    {
        float totalWeight = WeightedDrops.Values.Sum();
        float choice = Main.rand.NextFloat(totalWeight);
        float currentWeight = 0;

        foreach (var drop in WeightedDrops)
        {
            currentWeight += drop.Value;
            if (choice <= currentWeight)
            {
                return drop.Key;
            }
        }

        return WeightedDrops.Keys.First();
    }
}