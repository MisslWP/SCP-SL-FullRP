using System;
using System.Reflection;
using HarmonyLib;

namespace Scanner.Patches
{
    [HarmonyPatch(typeof(CustomPlayerEffects.Corroding), nameof(CustomPlayerEffects.Corroding.PublicUpdate))]
    class CustomPocketDamage
    {
        public static bool Prefix(CustomPlayerEffects.Corroding __instance)
        {
            if (ScanMod.config.enable106overhaul)
            {
                if (__instance.Enabled)
                {
                    if (PocketProperties.customDamageEnabled)
                    {
                        __instance.TimeBetweenTicks = PocketProperties.customDelay;


                        Type t = __instance.GetType();
                        FieldInfo[] fields = t.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

                        foreach (FieldInfo fi in fields)
                        {
                            if (fi.Name == "_damagePerTick")
                            {
                                fi.SetValue(__instance, PocketProperties.customDamage);
                            }
                        }
                    }
                }
            }
            return true;
        }
    }
}
