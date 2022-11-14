using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.ObjectSystem;

namespace UniformParties {
    internal class Troops {
        public List<CharacterObject>[] RegularTiers { get; private set; } = (from i in Enumerable.Range(0, 7)
                                                                             select new List<CharacterObject>()).ToArray();
        public List<CharacterObject>[] NobleTiers { get; private set; } = (from i in Enumerable.Range(0, 7)
                                                                           select new List<CharacterObject>()).ToArray();

        public Troops(List<CharacterObject> regularRecruitTroops, List<CharacterObject> nobleRecruitTroops) {
            PopulateListsByTier(regularRecruitTroops, nobleRecruitTroops);
            //PrintArrayContents();
        }

        private void PopulateListsByTier(List<CharacterObject> regularRecruitTroops, List<CharacterObject> nobleRecruitTroops) {
            foreach (var character in regularRecruitTroops) {
                GetTroop(character, false);
            }

            foreach (var character in nobleRecruitTroops) {
                GetTroop(character, true);
            }
        }

        private void GetTroop(CharacterObject character, bool noble) {
            if (character == null) {
                Helpers.Message("character was null");
                return;
            }

            int troopTier = character.Tier;

            if (noble) NobleTiers[troopTier - 1].Add(character);
            else RegularTiers[troopTier - 1].Add(character);

            if (character.UpgradeTargets.Length > 0) {
                for (int i = 0; i < character.UpgradeTargets.Length; i++) {
                    GetTroop(character.UpgradeTargets[i], noble);
                }
            }
        }
    }
}
