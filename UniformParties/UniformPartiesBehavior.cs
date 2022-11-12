using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;

namespace UniformParties {
    internal class UniformPartiesBehavior : CampaignBehaviorBase {

        private Dictionary<CharacterObject, int> removeDict = new Dictionary<CharacterObject, int>();
        private Dictionary<CharacterObject, int> addDict = new Dictionary<CharacterObject, int>();
        private Dictionary<string, FactionTroops> cultureTroops = new Dictionary<string, FactionTroops>();

        public override void RegisterEvents() {
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(OnSessionLaunched));
            CampaignEvents.OnTroopRecruitedEvent.AddNonSerializedListener(this, new Action<Hero, Settlement, Hero, CharacterObject, int>(OnTroopRecruited));
        }

        private void OnTroopRecruited(Hero recruiter, Settlement settlement, Hero recruitmentSource, CharacterObject troop, int count) {
            UpdateTroopRoster(recruiter.PartyBelongedTo);
        }

        private void OnSessionLaunched(CampaignGameStarter starter) {
            cultureTroops.Add("empire", new FactionTroops("empire"));
            cultureTroops.Add("sturgia", new FactionTroops("sturgia"));
            cultureTroops.Add("battania", new FactionTroops("battania"));
            cultureTroops.Add("vlandia", new FactionTroops("vlandia"));
            cultureTroops.Add("khuzait", new FactionTroops("khuzait"));
            cultureTroops.Add("aserai", new FactionTroops("aserai"));
        }

        private void UpdateTroopRoster(MobileParty mobileParty) {
            if (mobileParty == null || mobileParty.LeaderHero == null || mobileParty.MemberRoster == null || mobileParty.IsMainParty) return;

            var troopRoster = mobileParty.MemberRoster;
            var leaderHero = mobileParty.LeaderHero;
            var leaderCulture = leaderHero.Culture;
            var factionTroops = FactionTroops.Find(leaderCulture);

            removeDict.Clear();
            addDict.Clear();

            foreach (TroopRosterElement troop in troopRoster.GetTroopRoster()) {
                if (troop.Character.Culture == leaderCulture || troop.Character.Culture.StringId == "neutral_culture" || !FactionTroops.AllTroops.Contains(troop.Character)) continue;

                var character = troop.Character;
                var troopTier = character.Tier;
                List<CharacterObject> troopsOfTier;

                RemoveFromDict(character, removeDict, troop.Number);

                if (FactionTroops.AllNobleTroops.Contains(character)) troopsOfTier = factionTroops.NobleTiers[troopTier - 1];
                else troopsOfTier = factionTroops.RegularTiers[troopTier - 1];
                
                for (int i = 0; i < troop.Number; i++) {
                    // pick random new unit from list
                    CharacterObject randomCharacterObject = troopsOfTier[MBRandom.RandomInt(0, troopsOfTier.Count - 1)];
                    AddToDict(randomCharacterObject, addDict);
                }
            }

            // add all units that are in add troops list
            foreach (var troop in addDict) {
                Helpers.Message($"{troop.Value} {troop.Key} added to party.");
                mobileParty.AddElementToMemberRoster(troop.Key, troop.Value);
            }

            // remove all units that are in removeable troops list
            foreach (var troop in removeDict) {
                Helpers.Message($"{troop.Value} {troop.Key} removed from party.");
                troopRoster.RemoveTroop(troop.Key, troop.Value);  
            }

        }

        public override void SyncData(IDataStore dataStore) {}

        private void AddToDict(CharacterObject character, Dictionary<CharacterObject, int> dict) {
            if (!dict.ContainsKey(character)) dict.Add(character, 1);
            else dict[character]++;
        }

        private void RemoveFromDict(CharacterObject character, Dictionary<CharacterObject, int> dict, int count) {
            if (!dict.ContainsKey(character)) dict.Add(character, count);
            else dict[character]++;
        }
    }
}
