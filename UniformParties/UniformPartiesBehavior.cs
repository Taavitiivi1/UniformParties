using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace UniformParties {
    internal class UniformPartiesBehavior : CampaignBehaviorBase {

        public static List<FactionTroops> FactionTroopsList = new List<FactionTroops>();

        public override void RegisterEvents() {
            CampaignEvents.OnAfterSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(OnAfterSessionLaunched));
            CampaignEvents.OnTroopRecruitedEvent.AddNonSerializedListener(this, new Action<Hero, Settlement, Hero, CharacterObject, int>(OnTroopRecruited));
        }

        private void OnTroopRecruited(Hero recruiter, Settlement settlement, Hero recruitmentSource, CharacterObject troop, int count) {
            if (recruiter == null) return;
            UpdateTroopRoster(recruiter.PartyBelongedTo, troop, count);
        }

        private void OnAfterSessionLaunched(CampaignGameStarter starter) {
            FactionTroopsList = PopulateFactionTroops();
            foreach (var f in FactionTroopsList) {
                f.Print();
            }
        }

        private void UpdateTroopRoster(MobileParty mobileParty, CharacterObject troop, int count) {
            if (mobileParty == null || mobileParty.LeaderHero == null || mobileParty.LeaderHero.Culture == null || troop == null || mobileParty.IsMainParty) return;

            var troopRoster = mobileParty.MemberRoster;
            var troopCulture = troop.Culture;
            // If tier does not exist, find the next closest one

            FactionTroops factionTroopsInstance;

            if (mobileParty.ActualClan.IsMinorFaction) factionTroopsInstance = FactionTroops.Find(mobileParty.ActualClan.StringId, FactionTroopsList);
            else factionTroopsInstance = FactionTroops.Find(mobileParty.ActualClan.Kingdom.Culture.StringId, FactionTroopsList);

            if (factionTroopsInstance.AllowedTroops.AllTroops.Contains(troop) || troopRoster == null) return;

            List<CharacterObject> troopsOfTier = GetTroopsOfTier(factionTroopsInstance, troop);

            string message = "";

            for (int i = 0; i < count; i++) {
                CharacterObject randomCharacterObject = randomCharacterObject = troopsOfTier[MBRandom.RandomInt(0, troopsOfTier.Count - 1)];
                mobileParty.AddElementToMemberRoster(randomCharacterObject, 1);
                message += $"{randomCharacterObject},  ";
            }

            Helpers.Message(mobileParty.LeaderHero.ToString());
            Helpers.Message(mobileParty.LeaderHero.Culture.ToString());
            Helpers.Message(message + "added to party.");

            troopRoster.RemoveTroop(troop, count);
            Helpers.Message($"{count} {troop} removed from party.\n");
        }

        private List<CharacterObject> GetTroopsOfTier(FactionTroops factionTroops, CharacterObject troop) {
            int tier = troop.Tier;
            var nobleRecruits = factionTroops.NobleRecruits;
            var nobleTiers = factionTroops.AllowedTroops.NobleTiers;
            var regularTiers = factionTroops.AllowedTroops.RegularTiers;

            // TODO: If tier is empty, find another tier


            if (nobleRecruits.Contains(troop)) {
                return FindBest(nobleTiers, tier);
            } 
            else return FindBest(regularTiers, tier);
        }

        public override void SyncData(IDataStore dataStore) {}

        private List<FactionTroops> PopulateFactionTroops() {
            var factionTroops = new List<FactionTroops>();

            foreach (var kingdom in Kingdom.All) {
                if (kingdom.IsKingdomFaction) {
                    string stringId = kingdom.Culture.StringId;
                    if (stringId == "empire_w" || stringId == "empire_s") continue;
                    factionTroops.Add(new FactionTroops(stringId, null, kingdom.Culture));
                    //Helpers.Message(stringId);
                }
            }

            foreach (var clan in Clan.All) {
                if (clan.IsMinorFaction && !clan.IsBanditFaction && !(clan == Clan.PlayerClan)) {
                    string stringId = clan.StringId;
                    //Helpers.Message(stringId);
                    factionTroops.Add(new FactionTroops(stringId, clan, null));
                }
            }

            return factionTroops;
        }

        private List<CharacterObject> FindBest(List<CharacterObject>[] tiers, int tier) {
            int tiersCount = tiers.Length;

            for (int i = tier; i < tiersCount; i++) { // First we always find higher tier
                if (tiers[i].Count > 0) return tiers[i];
            }

            for (int i = tier - 1; i > 0; i--) { // If we reached max tier and could not find an non-empty tier, go downwards
                if (tiers[i].Count > 0) return tiers[i];
            }

            Helpers.Message("Next best tier could not be found. Returning tiers[0]");
            return tiers[0];
        }
    }
}
