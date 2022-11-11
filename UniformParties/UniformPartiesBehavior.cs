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

        private List<TroopRosterElement> removeList = new List<TroopRosterElement>();
        private List<TroopRosterElement> addList = new List<TroopRosterElement>();

        public override void RegisterEvents() {
            CampaignEvents.DailyTickPartyEvent.AddNonSerializedListener(this, new Action<MobileParty>(OnDailyTickParty));
            CampaignEvents.MapEventStarted.AddNonSerializedListener(this, new Action<MapEvent, PartyBase, PartyBase>(OnMapEventStarted));
        }

        private void OnDailyTickParty(MobileParty mobileParty) {
            UpdateTroopRoster(mobileParty.MemberRoster, mobileParty.LeaderHero);
        }


        private void OnMapEventStarted(MapEvent mapEvent, PartyBase partyBase1, PartyBase partyBase2) {
            if (mapEvent.IsFieldBattle || mapEvent.IsSiegeAssault || mapEvent.IsSiegeOutside) {
                Helpers.Message("MapEvent conditions passed");
                UpdateTroopRoster(partyBase1.MemberRoster, partyBase1.LeaderHero);
                UpdateTroopRoster(partyBase2.MemberRoster, partyBase2.LeaderHero);
            }
        }

        private void UpdateTroopRoster(TroopRoster troopRoster, Hero leaderHero) {
            removeList.Clear();
            addList.Clear();

            foreach (TroopRosterElement troop in troopRoster.GetTroopRoster()) {
                CharacterObject character = troop.Character;

                // if culture is same continue
                if (character.Culture == leaderHero.Culture) continue;

                // add to removable troops list
                removeList.Add(troop);

                // for loop with trooprosterelement.Number
                for (int i = 0; i < troop.Number; i++) {
                    // Get troop tier
                    int tier = character.Tier;
                    // pick random new unit from list
                    // add to addList
                }
            }
            // add all units that are in add troops list
            // remove all units that are in removeable troops list
        }

        public override void SyncData(IDataStore dataStore) {}
    }
}
