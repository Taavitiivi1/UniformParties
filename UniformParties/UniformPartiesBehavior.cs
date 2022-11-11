using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;

namespace UniformParties {
    internal class UniformPartiesBehavior : CampaignBehaviorBase {
        public override void RegisterEvents() {
            CampaignEvents.DailyTickPartyEvent.AddNonSerializedListener(this, new Action<MobileParty>(OnDailyTickParty));
            CampaignEvents.DailyTickSettlementEvent.AddNonSerializedListener(this, new Action<Settlement>(OnDailyTickSettlement));
            CampaignEvents.MapEventStarted.AddNonSerializedListener(this, new Action<MapEvent, PartyBase, PartyBase>(OnMapEventStarted));
        }

        private void OnDailyTickParty(MobileParty mobileParty) {
            // Update TroopRoster
        }

        private void OnDailyTickSettlement(Settlement settlement) {
            // Update TroopRoster
        }

        private void OnMapEventStarted(MapEvent mapEvent, PartyBase partyBase1, PartyBase partyBase2) {
            // Update TroopRoster
        }

        private void UpdateTroopRoster(TroopRoster troopRoster) {

        }

        public override void SyncData(IDataStore dataStore) {}
    }
}
