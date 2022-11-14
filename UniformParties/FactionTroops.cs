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
        public Troops troops { get; private set; }
        public List<CharacterObject> AllowedTroops { get; private set; } = new List<CharacterObject>();
        
        public FactionTroops(string cultureId) {
            Culture = MBObjectManager.Instance.GetObject<CultureObject>(cultureId);
            troops = new Troops();
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
    }
}
