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
using TaleWorlds.Core;

namespace UniformParties {
    internal class UniformPartiesBehavior : CampaignBehaviorBase {

        private List<TroopRosterElement> removeList = new List<TroopRosterElement>();
        private List<TroopRosterElement> addList = new List<TroopRosterElement>();
        private FactionTroops empire { get; set; }
        private FactionTroops battania { get; set; }
        private FactionTroops sturgia { get; set; }
        private FactionTroops vlandia { get; set; }
        private FactionTroops khuzait { get; set; }
        private FactionTroops aserai { get; set; }

        Dictionary<string, FactionTroops> cultureTroops = new Dictionary<string, FactionTroops>();

        public override void RegisterEvents() {
            CampaignEvents.DailyTickPartyEvent.AddNonSerializedListener(this, new Action<MobileParty>(OnDailyTickParty));
            CampaignEvents.MapEventStarted.AddNonSerializedListener(this, new Action<MapEvent, PartyBase, PartyBase>(OnMapEventStarted));
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(OnSessionLaunched));
        }

        private void OnSessionLaunched(CampaignGameStarter starter) {
            cultureTroops.Add("empire", new FactionTroops("empire"));
            cultureTroops.Add("sturgia", new FactionTroops("sturgia"));
            cultureTroops.Add("battania", new FactionTroops("battania"));
            cultureTroops.Add("vlandia", new FactionTroops("vlandia"));
            cultureTroops.Add("khuzait", new FactionTroops("khuzait"));
            cultureTroops.Add("aserai", new FactionTroops("aserai"));
        }

        private void OnDailyTickParty(MobileParty mobileParty) {
            UpdateTroopRoster(mobileParty);
        }


        private void OnMapEventStarted(MapEvent mapEvent, PartyBase partyBase1, PartyBase partyBase2) {
            if (mapEvent.IsFieldBattle || mapEvent.IsSiegeAssault || mapEvent.IsSiegeOutside) {
                Helpers.Message("MapEvent conditions passed");
                //UpdateTroopRoster(partyBase1.mobileParty);
                //UpdateTroopRoster(partyBase2.mobileParty);
            }
        }

        private void UpdateTroopRoster(MobileParty mobileParty) {
            if (mobileParty == null || mobileParty.LeaderHero == null || mobileParty.MemberRoster == null) return;

            var troopRoster = mobileParty.MemberRoster;
            var leaderHero = mobileParty.LeaderHero;
            var leaderCulture = leaderHero.Culture;

            removeList.Clear();
            addList.Clear();

            foreach (TroopRosterElement troop in troopRoster.GetTroopRoster()) {
                if (troop.Character.Culture == leaderCulture) continue;

                CharacterObject character = troop.Character;
                int troopTier = character.Tier;
                CultureObject culture = character.Culture;
                List<CharacterObject> troopsOfTier;

                removeList.Add(troop);
                FactionTroops factionTroops = FactionTroops.Find(culture);

                if (FactionTroops.AllNobleTroopIds.Contains(character.StringId)) troopsOfTier = factionTroops.NobleTiers[troopTier - 1];
                else troopsOfTier = factionTroops.RegularTiers[troopTier - 1];
                
                for (int i = 0; i < troop.Number; i++) {
                    // pick random new unit from list
                    CharacterObject randomCharacterObject = troopsOfTier[MBRandom.RandomInt(0, troopsOfTier.Count - 1)];
                    //addList.Add(troopsOfTier[MBRandom.RandomInt(0, troopsOfTier.Count - 1)]);
                    // add to addList
                }
            }
            // add all units that are in add troops list
            // remove all units that are in removeable troops list
        }

        public override void SyncData(IDataStore dataStore) {}

        private void AddToList(TroopRosterElement troopElement, List<TroopRosterElement> list) {
            if (list.Contains(troopElement)) {
                var troop = list.Find(x => x.Character == troopElement.Character);
                troop.Number++;
            }
            else list.Add(troopElement);
        }
    }
}
