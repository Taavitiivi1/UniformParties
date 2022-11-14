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
        public List<CharacterObject> NobleTroops { get; private set; } = new List<CharacterObject>();
        public List<CharacterObject> RegularTroops { get; private set; } = new List<CharacterObject>();
        public List<CharacterObject> AllowedTroops { get; private set; } = new List<CharacterObject>();
        

        public FactionTroops(string cultureId) {
            Culture = MBObjectManager.Instance.GetObject<CultureObject>(cultureId);
            var basicTroops = GetCultureBasicTroops(cultureId);
            RegularRecruit = basicTroops[0];
            NobleRecruit = basicTroops[1];
            PopulateListsByTier();
            //PrintArrayContents();
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

            if (noble) NobleTroops.Add(character);
            else RegularTroops.Add(character);

            AllowedTroops.Add(character);

            if (character.UpgradeTargets.Length > 0) {
                for (int i = 0; i < character.UpgradeTargets.Length; i++) {
                    GetTroop(character.UpgradeTargets[i], listToAddTo, noble);
                }
            }
        }

        public static FactionTroops Find(CultureObject culture, List<FactionTroops> factionTroopsList) {
            foreach (var factionTroops in factionTroopsList) {
                if (culture == factionTroops.Culture) return factionTroops;
            }

            Helpers.Message("FactionTroops could not be found. Returned empire FactionTroops.");
            return Find(MBObjectManager.Instance.GetObject<CultureObject>("empire"), factionTroopsList);
        }

        private CharacterObject[] GetCultureBasicTroops(string cultureId) {
            var basicCharacters = new CharacterObject[2];

            var culture = MBObjectManager.Instance.GetObject<CultureObject>(cultureId);

            if (culture == null) {
                Helpers.Message("culture was null");
                return basicCharacters;
            }

            basicCharacters[0] = culture.BasicTroop;
            basicCharacters[1] = culture.EliteBasicTroop;

            return basicCharacters;
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
            Helpers.Message("\nAllowed troops: ");
            foreach (var troop in AllowedTroops) {
                Helpers.Message(troop.ToString());
            }
        }
    }
}
