using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.ObjectSystem;

namespace UniformParties {
    internal class MinorClanFactionTroops : FactionTroopsBase {
        public Clan clan { get; private set; }
        public MinorClanFactionTroops(Clan clan) : base(clan.StringId) {
            GetBasicTroop(clan);
            AllowedTroops = new Troops(RegularRecruits, NobleRecruits);
        }
        

        private void GetBasicTroop(Clan clan) {
            if (clan == null) {
                Helpers.Message($"{clan.StringId} clan was null");
                return;
            };
            RegularRecruits.Add(clan.BasicTroop);
        }
    }
}
