using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.ObjectSystem;

namespace UniformParties {
    internal class KingdomFactionTroops : FactionTroopsBase {
        public CultureObject Culture { get; private set; }

        public KingdomFactionTroops(CultureObject culture) : base(culture.StringId) {
            Culture = culture;
            GetCultureBasicTroops();
            AllowedTroops = new Troops(RegularRecruits, NobleRecruits);
        }

        private void GetCultureBasicTroops() {
            if (Culture == null) {
                Helpers.Message("Culture was null");
                return;
            }

            RegularRecruits.Add(Culture.BasicTroop);
            NobleRecruits.Add(Culture.EliteBasicTroop);
        }
    }
}
