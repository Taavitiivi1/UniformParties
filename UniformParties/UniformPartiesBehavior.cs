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
using TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace UniformParties {
    internal class UniformPartiesBehavior : CampaignBehaviorBase {

        public static List<FactionTroops> FactionTroopsList = new List<FactionTroops>();
        public static List<Clan> MinorClans { get; private set; } = new List<Clan>();

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
            MinorClans = FindMinorClans();
        }

        private void UpdateTroopRoster(MobileParty mobileParty, CharacterObject troop, int count) {
            if (mobileParty == null || mobileParty.LeaderHero == null || mobileParty.LeaderHero.Culture == null || troop == null) return;

            var troopRoster = mobileParty.MemberRoster;
            var leaderHero = mobileParty.LeaderHero;
            var leaderCulture = leaderHero.Culture;
            var factionTroopsInstance = FactionTroops.Find(leaderCulture, FactionTroopsList);
            var troopCulture = troop.Culture;
            var troopTier = troop.Tier; // If tier does not exist, find the next closest one

            if (factionTroopsInstance.AllowedTroops.Contains(troop) || troopRoster == null || mobileParty.IsMainParty ||
                troopCulture == leaderCulture || troopCulture.StringId == "neutral_culture") return;

            List<CharacterObject> troopsOfTier = GetTroopsOfTier(factionTroopsInstance, troop);

            for (int i = 0; i < count; i++) {
                CharacterObject randomCharacterObject = troopsOfTier[MBRandom.RandomInt(0, troopsOfTier.Count - 1)];
                mobileParty.AddElementToMemberRoster(randomCharacterObject, 1);
            }

            troopRoster.RemoveTroop(troop, count);
            Helpers.Message("Roster updated");
        }

        private List<CharacterObject> GetTroopsOfTier(FactionTroops factionTroops, CharacterObject troop) {
            if (factionTroops.NobleTroops.Contains(troop)) return factionTroops.NobleTiers[troop.Tier - 1];
            else return factionTroops.RegularTiers[troop.Tier - 1];
        }

        public override void SyncData(IDataStore dataStore) {}

        private List<Clan> FindMinorClans() {
            var minorClans = new List<Clan>();

            foreach (var clan in Clan.All) {
                if (clan.IsMinorFaction && !clan.IsBanditFaction) {
                    minorClans.Add(clan);
                    Helpers.Message(clan.Name.ToString());
                }
            }

            return minorClans;
        }

        private List<FactionTroops> PopulateFactionTroops() {
            var factionTroops = new List<FactionTroops>();

            foreach (var kingdom in Kingdom.All) {
                if (kingdom.IsKingdomFaction) {
                    string stringId = kingdom.StringId;
                    if (stringId == "empire_w" || stringId == "empire_s") continue;
                    factionTroops.Add(new FactionTroops(stringId));
                    Helpers.Message(stringId);
                }
            }
            Helpers.Message("\n---\n");
            return factionTroops;
        }
    }
}
