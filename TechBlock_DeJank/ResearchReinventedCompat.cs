using System;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using TechBlock;
using Verse;

namespace TechBlock_DeJank
{
    public static class ResearchReinventedCompat
    {
        private static Type TypeOfResearchOpportunity = AccessTools.TypeByName("PeteTimesSix.ResearchReinvented.Opportunities.ResearchOpportunity");

        [HarmonyPatch]
        public static class ResearchPerformedPatch
        {
            public static bool Prepare()
            {
                return TypeOfResearchOpportunity != null;
            }

            public static MethodBase TargetMethod()
            {
                return AccessTools.Method(TypeOfResearchOpportunity, "ResearchPerformed");
            }

            public static void Postfix(float amount)
            {
                ResearchProjectDef curProj = Find.ResearchManager.GetProject();
                if (curProj == null || !TechBlocker.IsBlockTech(curProj) || !SettingsUtil.Setting.randomInsights) return;
            
                var gameComp = Current.Game.GetComponent<GameComponent_ResearchCache>();
                //amount *= ResearchManager.ResearchPointsPerWorkTick;
            
                switch (curProj.techLevel)
                {
                    case TechLevel.Neolithic:
                        if (gameComp.AvailableNeoProjects.Empty()) return;
                        Find.ResearchManager.AddProgress(gameComp.AvailableNeoProjects.RandomElement(), amount * SettingsUtil.Setting.randomInsightRate);
                        return;
                    case TechLevel.Medieval:
                        if (gameComp.AvailableMediProjects.Empty()) return;
                        Find.ResearchManager.AddProgress(gameComp.AvailableMediProjects.RandomElement(), amount * SettingsUtil.Setting.randomInsightRate);
                        return;
                    case TechLevel.Industrial:
                        if (gameComp.AvailableIndProjects.Empty()) return;
                        Find.ResearchManager.AddProgress(gameComp.AvailableIndProjects.RandomElement(), amount * SettingsUtil.Setting.randomInsightRate);
                        return;
                    case TechLevel.Spacer:
                        if (gameComp.AvailableSpaceProjects.Empty()) return;
                        Find.ResearchManager.AddProgress(gameComp.AvailableSpaceProjects.RandomElement(), amount * SettingsUtil.Setting.randomInsightRate);
                        return;
                    case TechLevel.Ultra:
                        if (gameComp.AvailableUltraProjects.Empty()) return;
                        Find.ResearchManager.AddProgress(gameComp.AvailableUltraProjects.RandomElement(), amount * SettingsUtil.Setting.randomInsightRate);
                        return;
                    case TechLevel.Archotech:
                        if (gameComp.AvailableArchoProjects.Empty()) return;
                        Find.ResearchManager.AddProgress(gameComp.AvailableArchoProjects.RandomElement(), amount * SettingsUtil.Setting.randomInsightRate);
                        return;
                    default:
                        return;
                }
            }
        }
    }
}