using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.ObjectSystem;

namespace UniformParties {
    internal class FactionTroops {
        public CultureObject Culture { get; private set; }
        public CharacterObject RegularRecruit { get; private set; }
        public CharacterObject NobleRecruit { get; private set; }

        public List<CharacterObject>[] RegularTiers { get; private set; } = (from i in Enumerable.Range(0, 7)
                                                                             select new List<CharacterObject>()).ToArray();
        public List<CharacterObject>[] NobleTiers { get; private set; } = (from i in Enumerable.Range(0, 7)
                                                                           select new List<CharacterObject>()).ToArray();

        /// <summary>
        /// HashSet is used to quickly compare a string with the contents of the hashset to see if a troop is noble
        /// </summary>
        public static HashSet<string> AllNobleTroopIds = new HashSet<string>();
        public static List<FactionTroops> AllFactionTroops = new List<FactionTroops>();

        public FactionTroops(string cultureId) {
            Culture = MBObjectManager.Instance.GetObject<CultureObject>(cultureId);
            var basicTroops = Helpers.GetCultureBasicTroops(cultureId);
            RegularRecruit = basicTroops[0];
            NobleRecruit = basicTroops[1];
            PopulateListsByTier();
            DisplayArrayContents();
            AllFactionTroops.Add(this);
        }

        private void PopulateListsByTier() {
            GetTroop(RegularRecruit, RegularTiers, false);
            GetTroop(NobleRecruit, NobleTiers, true);
        }

        private void GetTroop(CharacterObject character, List<CharacterObject>[] listToAddTo, bool noble) {
            if (character == null) {
                Helpers.Message("character was null");
                return;
            }

            int troopTier = character.Tier;
            listToAddTo[troopTier - 1].Add(character);

            if (noble) AllNobleTroopIds.Add(character.StringId);

            if (character.UpgradeTargets.Length > 0) {
                for (int i = 0; i < character.UpgradeTargets.Length; i++) {
                    GetTroop(character.UpgradeTargets[i], listToAddTo, noble);
                }
            }
        }

        public void DisplayArrayContents() {
            Helpers.Message("\nRegular troops: ");

            foreach (var tierList in RegularTiers) {
                string message = "";

                foreach (var character in tierList) {
                    message += $" {character.StringId} ";
                }
                Helpers.Message(message);
            }

            Helpers.Message("\nNoble troops: ");

            foreach (var tierList in NobleTiers) {
                string message = "";

                foreach (var character in tierList) {
                    message += $" {character.StringId} ";
                }
                Helpers.Message(message);
            }

            Helpers.Message("\nAll noble troops: ");
            foreach (var troopId in AllNobleTroopIds) {
                Helpers.Message(troopId);
            }
        }

        public static FactionTroops Find(CultureObject culture) {
            foreach (var factionTroops in AllFactionTroops) {
                if (culture == factionTroops.Culture) return factionTroops;
            }

            Helpers.Message("FactionTroops could not be found. Returned empire FactionTroops.");
            return Find(MBObjectManager.Instance.GetObject<CultureObject>("empire"));
        }
    }
}
