using System;
using System.Collections.Generic;
using System.Linq;
using D2ModKit.Properties;

namespace D2ModKit
{
    public class Pair
    {
        private string key, val;

        public string Val
        {
            get { return val; }
            set { val = value; }
        }

        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        public Pair(string _key, string _val)
        {
            key = _key;
            val = _val;
        }

        public override string ToString()
        {
            // determine amount of whitespace
            string whitespace = "";
            int count = 80 - key.Length;
            if (count > 2)
            {
                for (int i = 0; i < count; i++)
                {
                    whitespace += " ";
                }
            }
            // the nextKey is really long, so just add some tabs.
            else
            {
                whitespace += "\t\t";
            }

            string str = "\t\t\"" + key + "\"" + whitespace + "\"" + val + "\"\n";
            return str;
        }
    }

    public class AddonEnglishEntry
    {
        private int numPairs;

        public int NumPairs
        {
            get { return numPairs; }
            set { numPairs = value; }
        }

        public AddonEnglishEntry()
        {
        }
    }

    public class ModifierEntry : AddonEnglishEntry
    {
        private Pair name, description;

        public Pair Description
        {
            get { return description; }
            set { description = value; }
        }

        public ModifierEntry(string _name)
        {
            // prevent modifier_modifier names
            name = new Pair("DOTA_Tooltip_" + _name, getVal(_name));

            // Noya says modifier tooltips are like this DOTA_Tooltip_some_modifier
            /*if (_name.Length > 8)
            {
                if (_name.Substring(0, 8) == "modifier")
                {
                    name = new Pair("DOTA_Tooltip_" + _name, getVal(_name));
                }
            }*/

            description = new Pair(name.Key + "_Description", "");
        }

        // tries to auto-generate the value
        private string getVal(string name)
        {
            string[] parts = name.Split('_');
            string val = "";
            for (int i = 0; i < parts.Count(); i++)
            {
                string part = parts[i];
                // we don't want 'Item' as part of the value.
                if (i == 0 && part == "modifier")
                {
                    continue;
                }


                // ensure valid string to work with
                if (part.Length > 0)
                {
                    // make the first char uppercase
                    string firstLetter = part.ElementAt(0).ToString().ToUpper();
                    try
                    {
                        part = firstLetter + part.Substring(1);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        // part was just 1 char.
                        part = firstLetter;
                    }
                    // now add this part to the final value.
                    val += part;
                    // add a space
                    if (i != parts.Count() - 1)
                    {
                        val += " ";
                    }
                }
            }
            return val;
        }

        public Pair Name
        {
            get { return name; }
            set { name = value; }
        }

        public override string ToString()
        {
            string str = "";
			str += Name.ToString();
			str += Description.ToString();
            return str;
        }
    }

    public class UnitEntry : AddonEnglishEntry
    {
        private Pair name;

        public UnitEntry(string _name)
        {
            name = new Pair(_name, getVal(_name));
        }

        // tries to auto-generate the value
        private string getVal(string name)
        {
            string[] parts = name.Split('_');
            string val = "";
            for (int i = 0; i < parts.Count(); i++)
            {
                string part = parts[i];
                // we don't want 'npc' as part of the value.
                if (i == 0 && part == "npc")
                {
                    continue;
                }

                // ensure valid string to work with
                if (part.Length > 0)
                {
                    // make the first char uppercase
                    string firstLetter = part.ElementAt(0).ToString().ToUpper();
                    try
                    {
                        part = firstLetter + part.Substring(1);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        // part was just 1 char.
                        part = firstLetter;
                    }
                    // now add this part to the final value.
                    val += part;
                    // add a space
                    if (i != parts.Count() - 1)
                    {
                        val += " ";
                    }
                }
            }
            return val;
        }

        public Pair Name
        {
            get { return name; }
            set { name = value; }
        }

        public override string ToString()
        {
			return name.ToString();
        }
    }

    public class HeroEntry : AddonEnglishEntry
    {
        private Pair name;

        public Pair Name
        {
            get { return name; }
            set { name = value; }
        }

        public HeroEntry(string _name)
        {
            try
            {
                // remove the npc_dota_hero part for the value.
                name = new Pair(_name, getVal(_name.Substring(14)));
            }
            catch (IndexOutOfRangeException)
            {
                name = new Pair(_name, "");
            }
        }

        // tries to auto-generate the value
        private string getVal(string name)
        {
            string[] parts = name.Split('_');
            string val = "";
            for (int i = 0; i < parts.Count(); i++)
            {
                string part = parts[i];

                // ensure valid string to work with
                if (part.Length > 0)
                {
                    // make the first char uppercase
                    string firstLetter = part.ElementAt(0).ToString().ToUpper();
                    try
                    {
                        part = firstLetter + part.Substring(1);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        // part was just 1 char.
                        part = firstLetter;
                    }
                    // now add this part to the final value.
                    val += part;
                    // add a space
                    if (i != parts.Count() - 1)
                    {
                        val += " ";
                    }
                }
            }
            return val;
        }

        public override string ToString()
        {
            return name.ToString();
        }
    }

    public class AbilityEntry : AddonEnglishEntry
    {
        // description and lore are default for abilities.
        private Pair name, description, lore, note0;

        private List<Pair> abilitySpecials = new List<Pair>();

        public List<Pair> AbilitySpecials
        {
            get { return abilitySpecials; }
            set { abilitySpecials = value; }
        }

        public Pair Name
        {
            get { return name; }
            set { name = value; }
        }

        public Pair Description
        {
            get { return description; }
            set { description = value; }
        }

        public Pair Lore
        {
            get { return lore; }
            set { lore = value; }
        }

        public Pair Note0
        {
            get { return note0; }
            set { note0 = value; }
        }

        private Addon addon;

        public AbilityEntry(Addon addon, string _name, List<string> keys)
        {
            this.addon = addon;
            name = new Pair("DOTA_Tooltip_ability_" + _name, getVal(_name));
            description = new Pair(name.Key + "_Description", "");
            note0 = new Pair(name.Key + "_Note0", "");
            lore = new Pair(name.Key + "_Lore", "");

            if (keys != null)
            {
                AbilitySpecials = new List<Pair>(keys.Count());
                for (int i = 0; i < keys.Count(); i++)
                {
                    string abilSpecial = keys.ElementAt(i);
                    AbilitySpecials.Add(new Pair(name.Key + "_" + abilSpecial, getAbilSpecialVal(abilSpecial)));
                }
            }
        }

        private string getAbilSpecialVal(string key)
        {
            string[] parts = key.Split('_');
            string val = "";
            for (int i = 0; i < parts.Count(); i++)
            {
                string part = parts[i];

                // ensure valid string to work with
                if (part.Length > 0)
                {
                    // make everything uppercase
                    part = part.ToUpper();

                    // now add this part to the final value.
                    val += part;
                    // add a space
                    if (i != parts.Count() - 1)
                    {
                        val += " ";
                    }
                    else
                    {
                        // add a colon
                        val += ":";
                    }
                }
            }
            return val;
        }

        // tries to auto-generate the value
        private string getVal(string name)
        {
            string[] parts = name.Split('_');
            string val = "";
            for (int i = 0; i < parts.Count(); i++)
            {
                string part = parts[i];
                // we don't want 'Item' as part of the value.
                if (i == 0 && part == "item")
                {
                    continue;
                }


                // ensure valid string to work with
                if (part.Length > 0)
                {
                    // make the first char uppercase
                    string firstLetter = part.ElementAt(0).ToString().ToUpper();
                    try
                    {
                        part = firstLetter + part.Substring(1);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        // part was just 1 char.
                        part = firstLetter;
                    }
                    // now add this part to the final value.
                    val += part;
                    // add a space
                    if (i != parts.Count() - 1)
                    {
                        val += " ";
                    }
                }
            }
            return val;
        }
        
        public override string ToString()
        {
            string str = "";
            str += Name.ToString();
            str += Description.ToString();
            if (addon.create_note0_lore)
            {
                str += Note0.ToString();
                str += Lore.ToString();
            }
            for (int i = 0; i < abilitySpecials.Count(); i++)
            {
                str += abilitySpecials.ElementAt(i).ToString();
            }
            return str;
        }
    }
}