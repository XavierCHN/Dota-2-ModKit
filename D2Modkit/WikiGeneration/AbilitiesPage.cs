using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2ModKit.WikiGeneration
{
    public class AbilitiesPage
    {
        private class AbilityEntry
        {
            public string abilityBehavior;
            public string abilityUnitTargetTeam;
            public string abilityUnitTargetType;
            public string baseClass;
            public string abilityTextureName;
            public string abilityCastRange;
            public string abilityCastPoint;
            public string abilityCooldown;
            public string abilityManaCost;
            public string kvName;
            public string actualName;

        }

        private List<AbilityEntry> abilEntries = new List<AbilityEntry>();

        private List<AbilityEntry> AbilEntries
        {
            get { return abilEntries; }
            set { abilEntries = value; }
        }

        private Wiki wiki;

        public AbilitiesPage(Wiki wiki)
        {
            this.wiki = wiki;
            /*bool isSuccessfulParse = parse();
            if (isSuccessfulParse)
            {
                writeHTMLPage();
            }*/
        }

    }
}
