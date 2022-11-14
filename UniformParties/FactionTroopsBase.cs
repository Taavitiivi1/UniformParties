using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.ObjectSystem;

namespace UniformParties {
    internal abstract class FactionTroopsBase {
        /// <summary>
        /// Represents the string name of the kingdom or minor faction, which is used to find the correct FactionTroops class.
        /// For kingdoms it is cultureId and for minor factions it is clan name
        /// TODO: Find a better way to do this?
        /// </summary>
        public string FactionId { get; protected set; }
        public Troops AllowedTroops { get; protected set; }

        public List<CharacterObject> RegularRecruits { get; protected set; } = new List<CharacterObject>();
        public List<CharacterObject> NobleRecruits { get; protected set; } = new List<CharacterObject>();

        public static List<FactionTroopsBase> AllFactionTroopsList { get; } = new List<FactionTroopsBase>();

        public FactionTroopsBase(string factionStringId) {
            FactionId = factionStringId;
        }

        public static FactionTroopsBase Find(CultureObject culture) {
            foreach (var factionTroops in AllFactionTroopsList) {
                if (factionTroops is KingdomFactionTroops kingdomFaction) {
                    if (culture == kingdomFaction.Culture) return factionTroops;
                }
            }

            Helpers.Message("FactionTroops could not be found. Returned null");
            return null;
        }

        public static FactionTroopsBase Find(Clan clan) {
            foreach (var factionTroops in AllFactionTroopsList) {
                if (factionTroops is MinorClanFactionTroops minorFaction) {
                    if (clan == minorFaction.clan) return factionTroops;
                }
            }

            Helpers.Message("FactionTroops could not be found. Returned null");
            return null;
        }
    }
}
