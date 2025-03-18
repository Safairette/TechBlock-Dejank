using System.Reflection;
using HarmonyLib;
using Verse;

namespace TechBlock_DeJank
{
    [StaticConstructorOnStartup]
    public class HarmonyInit
    {
        static HarmonyInit()
        {
            var harmony = new Harmony("safair.techblock.dejank");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}