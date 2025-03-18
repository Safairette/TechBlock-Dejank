using System.Collections.Generic;
using System.Linq;
using RimWorld;
using TechBlock;
using Verse;

namespace TechBlock_DeJank
{
    public class GameComponent_ResearchCache : GameComponent
    {

        public GameComponent_ResearchCache(Game game)
        {
        }

        public List<ResearchProjectDef> AvailableNeoProjects = new List<ResearchProjectDef>();
        public List<ResearchProjectDef> AvailableMediProjects = new List<ResearchProjectDef>();
        public List<ResearchProjectDef> AvailableIndProjects = new List<ResearchProjectDef>();
        public List<ResearchProjectDef> AvailableSpaceProjects = new List<ResearchProjectDef>();
        public List<ResearchProjectDef> AvailableUltraProjects = new List<ResearchProjectDef>();
        public List<ResearchProjectDef> AvailableArchoProjects = new List<ResearchProjectDef>();

        public void RefreshResearchCache()
        {
            AvailableNeoProjects = DefDatabase<ResearchProjectDef>.AllDefsListForReading.Where(x => x.techLevel == TechLevel.Neolithic && !x.IsFinished && x.CanStartNow && !TechBlocker.IsBlockTech(x)).ToList();
            AvailableMediProjects = DefDatabase<ResearchProjectDef>.AllDefsListForReading.Where(x => x.techLevel == TechLevel.Medieval && !x.IsFinished && x.CanStartNow && !TechBlocker.IsBlockTech(x)).ToList();
            AvailableIndProjects = DefDatabase<ResearchProjectDef>.AllDefsListForReading.Where(x => x.techLevel == TechLevel.Industrial && !x.IsFinished && x.CanStartNow && !TechBlocker.IsBlockTech(x)).ToList();
            AvailableSpaceProjects = DefDatabase<ResearchProjectDef>.AllDefsListForReading.Where(x => x.techLevel == TechLevel.Spacer && !x.IsFinished && x.CanStartNow && !TechBlocker.IsBlockTech(x)).ToList();
            AvailableUltraProjects = DefDatabase<ResearchProjectDef>.AllDefsListForReading.Where(x => x.techLevel == TechLevel.Ultra && !x.IsFinished && x.CanStartNow && !TechBlocker.IsBlockTech(x)).ToList();
            AvailableArchoProjects = DefDatabase<ResearchProjectDef>.AllDefsListForReading.Where(x => x.techLevel == TechLevel.Archotech && !x.IsFinished && x.CanStartNow && !TechBlocker.IsBlockTech(x)).ToList();
        }

        public bool RemoveResearchProject(ResearchProjectDef proj, TechLevel techLevel)
        {
            bool result = false;
            switch (techLevel)
            {
                case TechLevel.Neolithic:
                    result = AvailableNeoProjects.Remove(proj);
                    return result;
                case TechLevel.Medieval:
                    result = AvailableMediProjects.Remove(proj);
                    return result;
                case TechLevel.Industrial:
                    result = AvailableIndProjects.Remove(proj);
                    return result;
                case TechLevel.Spacer:
                    result = AvailableSpaceProjects.Remove(proj);
                    return result;
                case TechLevel.Ultra:
                    result = AvailableUltraProjects.Remove(proj);
                    return result;
                case TechLevel.Archotech:
                    result = AvailableArchoProjects.Remove(proj);
                    return result;
                default:
                    return false;
            }
        }
        
        public override void StartedNewGame()
        {
            base.StartedNewGame();
            RefreshResearchCache();
        }

        public override void LoadedGame()
        {
            base.LoadedGame();
            RefreshResearchCache();
        }
    }
}