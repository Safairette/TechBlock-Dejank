using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using TechBlock;
using Verse;

namespace TechBlock_DeJank
{

    public static class SettingsUtil
    {
        private static TechBlockSettings _setting;

        public static TechBlockSettings Setting
        {
            get
            {
                if (_setting != null) return _setting;
                
                _setting = LoadedModManager.GetMod<TechBlockMod>().GetSettings<TechBlockSettings>();
                return _setting;
            }
        }
    }
 
    
    [HarmonyPatch(typeof(TechBlock_Component), nameof(TechBlock_Component.GameComponentUpdate))]
    public class CancelGameCompUpdate
    {
        public static bool Prefix(TechBlock_Component __instance)
        {
            return false;
        }
    }

    [HarmonyPatch(typeof(ResearchManager), nameof(ResearchManager.FinishProject))]
    public class FinishProjectPatch
    {
        public static void Postfix(ResearchManager __instance, ResearchProjectDef proj)
        {
            TechBlocker.RecalculateBlockValues(proj.techLevel);
            var gameComp = Current.Game.GetComponent<GameComponent_ResearchCache>();
            gameComp.RefreshResearchCache();
            if (!gameComp.RemoveResearchProject(proj, proj.techLevel))
            {
                Log.Warning("TechBlock DeJank :: Failed to remove research project from research cache\nIf you see this on new game start, it should be harmless.");
            }
        }
    }

    [HarmonyPatch(typeof(ResearchManager), nameof(ResearchManager.AddProgress))]
    public class AddProgressPatch
    {
        public static void Postfix(ResearchManager __instance, ResearchProjectDef proj, float amount)
        {
            if (!TechBlocker.IsBlockTech(proj) || !SettingsUtil.Setting.randomInsights) return;
            
            var gameComp = Current.Game.GetComponent<GameComponent_ResearchCache>();

            switch (proj.techLevel)
            {
                case TechLevel.Neolithic:
                    Find.ResearchManager.AddProgress(gameComp.AvailableNeoProjects.RandomElement(), amount * SettingsUtil.Setting.randomInsightRate);
                    return;
                case TechLevel.Medieval:
                    Find.ResearchManager.AddProgress(gameComp.AvailableMediProjects.RandomElement(), amount * SettingsUtil.Setting.randomInsightRate);
                    return;
                case TechLevel.Industrial:
                    Find.ResearchManager.AddProgress(gameComp.AvailableIndProjects.RandomElement(), amount * SettingsUtil.Setting.randomInsightRate);
                    return;
                case TechLevel.Spacer:
                    Find.ResearchManager.AddProgress(gameComp.AvailableSpaceProjects.RandomElement(), amount * SettingsUtil.Setting.randomInsightRate);
                    return;
                case TechLevel.Ultra:
                    Find.ResearchManager.AddProgress(gameComp.AvailableUltraProjects.RandomElement(), amount * SettingsUtil.Setting.randomInsightRate);
                    return;
                case TechLevel.Archotech:
                    Find.ResearchManager.AddProgress(gameComp.AvailableArchoProjects.RandomElement(), amount * SettingsUtil.Setting.randomInsightRate);
                    return;
                default:
                    return;
            }
        }
    }

    [HarmonyPatch(typeof(ResearchManager), nameof(ResearchManager.ResearchPerformed))]
    public class ResearchPerformedPatch
    {
        public static void Postfix(ResearchManager __instance, float amount, ResearchProjectDef ___currentProj)
        {
            if (!TechBlocker.IsBlockTech(___currentProj) || !SettingsUtil.Setting.randomInsights) return;
            
            var gameComp = Current.Game.GetComponent<GameComponent_ResearchCache>();
            //amount *= ResearchManager.ResearchPointsPerWorkTick;
            
            switch (___currentProj.techLevel)
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