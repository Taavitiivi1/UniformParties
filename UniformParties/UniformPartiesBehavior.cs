using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        public static List<FactionTroopsBase> FactionTroopsList = new List<FactionTroopsBase>();

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
        }

        private void UpdateTroopRoster(MobileParty mobileParty, CharacterObject troop, int count) {
            if (mobileParty == null || mobileParty.LeaderHero == null || mobileParty.LeaderHero.Culture == null || troop == null) return;

            var troopRoster = mobileParty.MemberRoster;
            var leaderHero = mobileParty.LeaderHero;
            var leaderCulture = leaderHero.Culture;
            var troopCulture = troop.Culture;
            var troopTier = troop.Tier; // If tier does not exist, find the next closest one
            var factionTroopsInstance = FindFactionTroops(); FactionTroopsBase.Find(leaderCulture);

            if (FactionTroopsBase.AllFactionTroopsList.Contains(mobileParty.))

            if (factionTroopsInstance.AllowedTroops.AllTroops.Contains(troop) || troopRoster == null || mobileParty.IsMainParty || troopCulture.StringId == "neutral_culture") return;

            List<CharacterObject> troopsOfTier = GetTroopsOfTier(factionTroopsInstance, troop);

            for (int i = 0; i < count; i++) {
                CharacterObject randomCharacterObject = troopsOfTier[MBRandom.RandomInt(0, troopsOfTier.Count - 1)];
                mobileParty.AddElementToMemberRoster(randomCharacterObject, 1);
                Helpers.Message($"{randomCharacterObject} added to party.");
            }

            troopRoster.RemoveTroop(troop, count);
            Helpers.Message($"{count} {troop} removed from party.");
        }

        private List<CharacterObject> GetTroopsOfTier(FactionTroopsBase factionTroops, CharacterObject troop) {
            if (factionTroops.NobleRecruits.Contains(troop)) return factionTroops.AllowedTroops.NobleTiers[troop.Tier - 1];
            else return factionTroops.AllowedTroops.RegularTiers[troop.Tier - 1];
        }

        public override void SyncData(IDataStore dataStore) {}

        private List<FactionTroopsBase> PopulateFactionTroops() {
            var factionTroops = new List<FactionTroopsBase>();

            foreach (var kingdom in Kingdom.All) {
                if (kingdom.IsKingdomFaction) {
                    CultureObject culture = kingdom.Culture;
                    if (culture.StringId == "empire_w" || culture.StringId == "empire_s") continue;
                    factionTroops.Add(new KingdomFactionTroops(culture));
                    Helpers.Message(culture.StringId);
                }
            }

            foreach (var clan in Clan.All) {
                if (clan.IsMinorFaction && !clan.IsBanditFaction && !(clan == Clan.PlayerClan)) {
                    string stringId = clan.StringId;
                    Helpers.Message(stringId);
                    factionTroops.Add(new MinorClanFactionTroops(clan));
                }
            }

            return factionTroops;
        }
    }
}
