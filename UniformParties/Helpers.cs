using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace UniformParties {
    internal class Helpers {
        public static PartyTemplateObject FindPartyTemplate(string partyTemplateId) {
            var partyTemplate = MBObjectManager.Instance.GetObject<PartyTemplateObject>(partyTemplateId);

            if (partyTemplate == null) {
                InformationManager.DisplayMessage(new InformationMessage("partyTemplate could not be found. Returning 'looters_template'."));
                partyTemplate = MBObjectManager.Instance.GetObject<PartyTemplateObject>("looters_template");
            }

            return partyTemplate;
        }

        public static Kingdom GetRandomKingdomFaction() {
            return GetKingdomFactions().GetRandomElementInefficiently();
        }

        public static List<Kingdom> GetKingdomFactions() {
            return Kingdom.All.ToList();
        }

        public static Settlement GetRandomSettlement() {
            return GetSettlements().GetRandomElementInefficiently();
        }

        public static List<Settlement> GetSettlements() {
            return Settlement.FindAll(x => !x.IsHideout).ToList();
        }

        public static List<Settlement> GetSettlementsOfKingdom(Kingdom kingdom) {
            return Settlement.FindAll(x => !x.IsHideout && x.MapFaction == kingdom).ToList();
        }

        public static List<Settlement> GetWalledSettlementsOfKingdom(Kingdom kingdom) {
            return Settlement.FindAll(x => !x.IsHideout && x.MapFaction == kingdom && !x.IsVillage).ToList();
        }

        public static void Message(String message) {
            InformationManager.DisplayMessage(new InformationMessage(message));
        }
    }
}
