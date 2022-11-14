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

        public List<CharacterObject> AllTroops { get; private set; } = new List<CharacterObject>();

        public Troops(List<CharacterObject> regularRecruitTroops, List<CharacterObject> nobleRecruitTroops) {
            PopulateListsByTier(regularRecruitTroops, nobleRecruitTroops);
        }

        private void PopulateListsByTier(List<CharacterObject> regularRecruitTroops, List<CharacterObject> nobleRecruitTroops) {
            GetTroop(regularRecruitTroops, false);
            GetTroop(nobleRecruitTroops, true);
        }

        private void GetTroop(List<CharacterObject> characters, bool isNoble) {
            foreach (var character in characters) {
                if (character == null) {
                    Helpers.Message("character was null");
                    continue;
                }

                int troopTier = character.Tier;

                if (isNoble) NobleTiers[troopTier - 1].Add(character);
                else RegularTiers[troopTier - 1].Add(character);

                AllTroops.Add(character);

                if (character.UpgradeTargets.Length > 0) {
                    for (int i = 0; i < character.UpgradeTargets.Length; i++) {
                        GetTroop(character.UpgradeTargets.ToList(), isNoble);
                    }
                }
            }
        }
    }
}
