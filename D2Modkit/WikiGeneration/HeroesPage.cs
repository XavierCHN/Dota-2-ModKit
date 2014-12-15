using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using KVLib;
using System.IO;

namespace D2ModKit.WikiGeneration
{
    public class HeroesPage
    {
        private class HeroEntry
        {
            public int numAbilities;
            public string movementSpeed, movementTurnRate;
            public string baseName;
            public string[] abilities = new string[17]; // first index is blank.
            public string visionDaytimeRange;
            public string visionNighttimeRange;
            public string statusHealth;
            public string attackRate;
            public string attackCapabilities;
            public string statusMana;
            public string attackDamageMax;
            public string attackDamageMin;
            public string movementCapabilities;
            public string kvName;
            public string htmlPath;
            public string actualName;
        }

        private List<HeroEntry> heroEntries = new List<HeroEntry>();

        private List<HeroEntry> HeroEntries
        {
            get { return heroEntries; }
            set { heroEntries = value; }
        }

        private Wiki wiki;

        public HeroesPage(Wiki wiki)
        {
            this.wiki = wiki;
            bool isSuccessfulParse = parse();
            if (isSuccessfulParse)
            {
                writeHTMLPage();
            }
        }

        private bool parse()
        {
            if (!File.Exists(wiki.Addon.HeroesCustomPath))
            {
                return false;
            }

            KeyValue[] heroKeyVals = KVParser.ParseAllKVRootNodes(File.ReadAllText(wiki.Addon.HeroesCustomPath));

            // ensure we can get to the main heroKey data.
            if (heroKeyVals.Count() == 0 || !heroKeyVals[0].HasChildren)
            {
                return false;
            }

            IEnumerable<KeyValue> entries = heroKeyVals[0].Children;

            for (int i = 0; i < entries.Count(); i++)
            {
                // create a new heroKey hero from this data
                HeroEntry hero = new HeroEntry();

                KeyValue heroKey = entries.ElementAt(i);
                hero.kvName = heroKey.Key;
                if (heroKey.HasChildren)
                {
                    IEnumerable<KeyValue> children = heroKey.Children;
                    for (int j = 0; j < children.Count(); j++)
                    {
                        KeyValue child = children.ElementAt(j);
                        string k = child.Key;
                        string v = child.GetString();

                        // Extract num of abilities.
                        if (k == "AbilityLayout")
                        {
                            hero.numAbilities = Int32.Parse(v);
                        }
                        // extract heroKey name.
                        else if (k == "override_hero")
                        {
                            hero.baseName = v;
                        }
                        else if (k.Contains("Ability"))
                        {
                            int abilNum = Int32.Parse(k.Substring(7));
                            hero.abilities[abilNum] = v;
                        }
                        else if (k == "MovementTurnRate")
                        {
                            hero.movementTurnRate = v;
                        }
                        else if (k == "MovementSpeed")
                        {
                            hero.movementSpeed = v;
                        }
                        else if (k == "VisionDaytimeRange")
                        {
                            hero.visionDaytimeRange = v;
                        }
                        else if (k == "VisionNighttimeRange")
                        {
                            hero.visionNighttimeRange = v;
                        }
                        else if (k == "MovementSpeed")
                        {
                            hero.movementSpeed = v;
                        }
                        else if (k == "StatusHealth")
                        {
                            hero.statusHealth = v;
                        }
                        else if (k == "AttackCapabilities")
                        {
                            hero.attackCapabilities = v;
                        }
                        else if (k == "AttackRate")
                        {
                            hero.attackRate = v;
                        }
                        else if (k == "StatusMana")
                        {
                            hero.statusMana = v;
                        }
                        else if (k == "AttackDamageMin")
                        {
                            hero.attackDamageMin = v;
                        }
                        else if (k == "AttackDamageMax")
                        {
                            hero.attackDamageMax = v;
                        }
                        else if (k == "MovementCapabilities")
                        {
                            hero.movementCapabilities = v;
                        }
                    }
                    // now extract the useful info from the LangKV.
                    for (int j = 0; j < wiki.LangKV.Count(); j++)
                    {
                        KeyValue kv = wiki.LangKV.ElementAt(j);
                        // check for ex. npc_dota_hero_blah
                        if (kv.Key == hero.baseName)
                        {
                            hero.actualName = kv.GetString();
                            // this is the only useful thing to extract for heroes, so break here is fine.
                            break;
                        }
                    }

                    heroEntries.Add(hero);
                }
            }

            return true;
        }

        private void writeHTMLPage()
        {
            for (int i = 0; i < HeroEntries.Count(); i++)
            {
                HeroEntry h = HeroEntries.ElementAt(i);
                h.htmlPath = Path.Combine(wiki.WikiPath, "heroes", h.kvName + ".html");

                File.Create(h.htmlPath).Close();

                // Initialize StringWriter instance.
                StringWriter stringWriter = new StringWriter();
                stringWriter.WriteLine("<!DOCTYPE html>");
                // Put HtmlTextWriter in using block because it needs to call Dispose.
                using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Html);
                    writer.RenderBeginTag(HtmlTextWriterTag.Body);

                    writer.RenderBeginTag(HtmlTextWriterTag.H2);
                    writer.Write(h.actualName + "<br>");
                    writer.RenderEndTag();

                    writer.Write("Hit Points: " + h.statusHealth + "<br>");
                    writer.Write("Mana: " + h.statusMana + "<br>");
                    writer.Write("Turn Rate: " + h.movementTurnRate + "<br>");
                    writer.Write("Attack Capability: " + h.attackCapabilities + "<br>");
                    writer.Write("Movement Speed: " + h.movementSpeed + "<br>");
                    writer.Write("Attack Rate: " + h.attackRate + "<br>");
                    writer.Write("Attack Damage: " + h.attackDamageMin + "-" + h.attackDamageMax + "<br>");
                    writer.Write("Day Vision Range: " + h.visionDaytimeRange + "<br>");
                    writer.Write("Night Vision Range: " + h.visionNighttimeRange + "<br><br>");
                    for (int j = 1; j <= h.numAbilities; j++)
                    {
                        writer.Write("Ability #" + j + ": " + h.abilities[j] + "<br>");
                    }

                    writer.RenderEndTag();
                    writer.RenderEndTag();
                    File.WriteAllText(h.htmlPath, stringWriter.ToString());
                }
            }
        }

    }
}
