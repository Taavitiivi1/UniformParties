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
        public static HashSet<CharacterObject> AllNobleTroops = new HashSet<CharacterObject>();
        public static HashSet<CharacterObject> AllRegularTroops = new HashSet<CharacterObject>();
        public static HashSet<CharacterObject> AllTroops = new HashSet<CharacterObject>();
        public static List<FactionTroops> AllFactionTroops = new List<FactionTroops>();

        public FactionTroops(string cultureId) {
            Culture = MBObjectManager.Instance.GetObject<CultureObject>(cultureId);
            var basicTroops = Helpers.GetCultureBasicTroops(cultureId);
            RegularRecruit = basicTroops[0];
            NobleRecruit = basicTroops[1];
            PopulateListsByTier();
            //PrintArrayContents();
            AllFactionTroops.Add(this);
        }

        private void PopulateListsByTier() {
            // TODO: Add caravan guards, caravan masters, villagers etc.
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

            if (noble) AllNobleTroops.Add(character);
            else AllRegularTroops.Add(character);

            AllTroops.Add(character);

            if (character.UpgradeTargets.Length > 0) {
                for (int i = 0; i < character.UpgradeTargets.Length; i++) {
                    GetTroop(character.UpgradeTargets[i], listToAddTo, noble);
                }
            }
        }

        public void PrintArrayContents() {
            /*
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
            foreach (var troop in AllNobleTroops) {
                Helpers.Message(troop.ToString());
            }
            */
            Helpers.Message("\nAll troops: ");
            foreach (var troop in AllTroops) {
                Helpers.Message(troop.ToString());
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
