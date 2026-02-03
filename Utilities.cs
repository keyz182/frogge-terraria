using Terraria;
using Terraria.Utilities;

namespace Frogge;

public static class Utilities
{
    public static int RandInRange(this IntRange range) => Main.rand.Next(range.Minimum, range.Maximum + 1);

}