using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace UniformParties {
    public class SubModule : MBSubModuleBase {
        protected override void OnSubModuleLoad() {
            base.OnSubModuleLoad();
        }

        protected override void InitializeGameStarter(Game game, IGameStarter starterObject) {
            base.InitializeGameStarter(game, starterObject);
            if (starterObject is CampaignGameStarter starter) {
                starter.AddBehavior(new UniformPartiesBehavior());
            }
        }
    }
}