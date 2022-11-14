using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.ObjectSystem;

namespace UniformParties {
    internal class FactionTroops {
        /// <summary>
        /// Represents the string name of the kingdom or minor faction, which is used to find the correct FactionTroops class.
        /// For kingdoms it is cultureId and for minor factions it is clan name
        /// TODO: Find a better way to do this?
        /// </summary>
        public string FactionId { get; private set; }
        public Troops AllowedTroops { get; private set; }
        public Clan? Clan { get; private set; }
        public CultureObject? Culture { get; private set; }

        public List<CharacterObject> RegularRecruits { get; private set; } = new List<CharacterObject>();
        public List<CharacterObject> NobleRecruits { get; private set; } = new List<CharacterObject>();

        public FactionTroops(string factionId, Clan clan, CultureObject culture) {
            FactionId = factionId;
            Clan = clan;
            Culture = culture;

            if (Culture != null) GetKingdomRecruits(Culture);
            else if (Clan != null) GetMinorClanRecruits(Clan);

            AllowedTroops = new Troops(RegularRecruits, NobleRecruits);
        }

        public static FactionTroops Find(string factionId, List<FactionTroops> searchList) {
            foreach (var factionTroops in searchList) {
                if (factionTroops.FactionId == factionId) return factionTroops;
            }

            Helpers.Message("FactionTroops could not be found. Returned null");
            return null;
        }

        private void GetKingdomRecruits(CultureObject culture) {
            if (culture == null) {
                Helpers.Message("culture was null");
                return;
            }

            RegularRecruits.Add(culture.BasicTroop);
            NobleRecruits.Add(culture.EliteBasicTroop);
        }

        private void GetMinorClanRecruits(Clan clan) {
            if (clan == null) {
                Helpers.Message("clan was null");
                return;
            }

            RegularRecruits.Add(clan.BasicTroop);
        }

        public void Print() {
            Helpers.Message("\n" + FactionId);
            foreach (var troop in AllowedTroops.AllTroops) {
                Helpers.Message(troop.StringId);
            }
        }
    }
}
