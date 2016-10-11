using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace paradoxSaveEditor
{
    class saveEditFn
    {
        public static void alterAggExp(fileReader reader, string input)
        {
            string[] tags = input.Replace(" ", "").Split(',');

            int[] locations = reader.findAll("active_relations=");
            foreach (int location in locations)
            {
                reader.setLineNum(location);
                reader.setCharNum(0);
                textBlock relationsBlock = new textBlock('{', '}', reader);
                foreach (string tag in tags)
                {
                    int pos = relationsBlock.getPosition("\t\t\t" + tag + "=", false, 1);
                    if (pos != -1)
                    {
                        reader.goTo(location + pos);
                        textBlock relations = new textBlock('{', '}', reader);
                        int innerPos = relations.getPosition("modifier=\"aggressive_expansion\"", false, 2);
                        if (innerPos != -1)
                        {
                            reader.changeLine(location + pos + innerPos + 2, "\t\t\t\t\tcurrent_opinion=0.000");
                        }
                    }
                }
            }
        }

        public static void alterTechLevel(fileReader reader, string input)
        {
            string[] tags = input.Replace(" ", "").Split(',');
            int countriesLoc = reader.getPosition("countries=\r", 0, '{', '}', true);
            reader.setLineNum(countriesLoc);
            reader.setCharNum(0);

            textBlock countries = new textBlock('{', '}', reader);
            foreach (string tag in tags)
            {
                int countryLoc = countries.getPosition(tag + "=");
                techLevelForm techLevel = new techLevelForm(reader, countriesLoc + countryLoc, tag);
                techLevel.ShowDialog();
            }
        }

        public static void changeMonarchAbilities(fileReader reader, string inputText)
        {
            string[] tags = inputText.Replace(" ", "").Split(',');
            int countriesLoc = reader.getPosition("countries=\r", 0, '{', '}', true);
            reader.setLineNum(countriesLoc);
            reader.setCharNum(0);

            textBlock countries = new textBlock('{', '}', reader);
            foreach (string tag in tags)
            {
                int countryLoc = countries.getPosition(tag + "=");
                monarchAbilitiesForm monarchAbilities = new monarchAbilitiesForm(reader, countriesLoc + countryLoc, tag);
                monarchAbilities.ShowDialog();
            }
        }

        public static void changeCountyResouces(fileReader reader, string inputText)
        {
            string[] tags = inputText.Replace(" ", "").Split(',');
            int countriesLoc = reader.getPosition("countries=\r", 0, '{', '}', true);
            reader.setLineNum(countriesLoc);
            reader.setCharNum(0);

            textBlock countries = new textBlock('{', '}', reader);
            foreach (string tag in tags)
            {
                int countryLoc = countries.getPosition(tag + "=");
                countryResources resources = new countryResources(reader, countriesLoc + countryLoc, tag);
                resources.ShowDialog();
            }
        }

        public static void changeProv(fileReader reader)
        {
            Console.Write("Enter province ids of the provinces you want to edit (province id ranges can be entered with a dash, e.g 4-20; note that this will take approximately 0.1 seconds per province): ");
            string[] provs = Console.ReadLine().Replace(" ", "").Split(',');
            List<string> provRange = new List<string>();
            foreach (string s in provs)
            {
                if (s.Contains("-"))
                {
                    int start = Convert.ToInt32(s.Split('-')[0]);
                    int end = Convert.ToInt32(s.Split('-')[1]) + 1;
                    foreach (int i in Enumerable.Range(start, end - start))
                    {
                        provRange.Add(i.ToString());
                    }
                }
                else
                {
                    provRange.Add(s);
                }
            }
            provs = provRange.ToArray();
            Console.Write("Would you like to transfer province ownership? (Y/n): ");
            bool transfer = Console.ReadLine().Contains('Y');
            string transferTag = "";
            if (transfer)
            {
                Console.Write("Enter the tag(s) of the country or countries you want to transfer provences to: ");
                transferTag = Console.ReadLine();
            }
            Console.Write("Would you like to make the provinces cores? (Y/n): ");
            bool cores = Console.ReadLine().Contains('Y');
            string coreTag = "";
            if (cores)
            {
                Console.Write("Enter the tag(s) of the country or countries you want to add cores for: ");
                coreTag = Console.ReadLine();
            }

            Console.Write("Would you like to change the provinces culture? (Y/n): ");
            bool cultureChange = Console.ReadLine().Contains('Y');
            string culture = "";
            if (cultureChange)
            {
                Console.Write("Enter the provinces' new culture: ");
                culture = Console.ReadLine();
            }

            Console.Write("Would you like to change the provinces religion? (Y/n): ");
            bool religionChange = Console.ReadLine().Contains('Y');
            string religion = "";
            if (religionChange)
            {
                Console.Write("Enter the provinces' new religion: ");
                religion = Console.ReadLine();
            }

            Console.Write("Would you like to change the provinces base tax? (Y/n): ");
            bool baseTaxChange = Console.ReadLine().Contains('Y');

            bool multiplyBaseTax = false;
            if (baseTaxChange)
            {
                Console.Write("Would you like to change the base tax to a particular value (1), or multiply by a constant (2)? ");
                multiplyBaseTax = Console.ReadLine().Contains("2");
            }

            int baseTax = 0;
            if (baseTaxChange)
            {
                Console.Write("Enter the provinces' new base tax: ");
                baseTax = Convert.ToInt32(Console.ReadLine());
            }

            Console.Write("Would you like to change the provinces base manpower? (Y/n): ");
            bool manpowerChange = Console.ReadLine().Contains('Y');

            bool multiplyManpower = false;
            if (manpowerChange)
            {
                Console.Write("Would you like to change the base manpower to a particular value (1), or multiply by a constant (2)? ");
                multiplyManpower = Console.ReadLine().Contains("2");
            }

            int baseManpower = 0;
            if (multiplyManpower)
            {
                Console.Write("Enter the provinces' new base multiplayer: ");
                baseManpower = Convert.ToInt32(Console.ReadLine());
            }

            int provincesLoc = reader.getPosition("provinces=", 0, '{', '}');
            reader.goTo(provincesLoc);
            textBlock provinces = new textBlock('{', '}', reader);

            foreach (string prov in provs)
            {
                Console.WriteLine("Currently editing province " + prov);
                int provLoc = provincesLoc + provinces.getPosition("-" + prov + "=");
                reader.goTo(provLoc);
                textBlock provBlock = new textBlock('{', '}', reader);
                int i = 0;

                if (transfer)
                {
                    int ownerLoc = provLoc + provBlock.getPosition("owner=") + i;
                    if (ownerLoc == provLoc - 1)
                    {
                        provinces.addLine("\t\towner=" + transferTag, provLoc + 3 - provincesLoc);
                        reader.addLine("\t\towner=" + transferTag, provLoc + 3);
                        i++;
                    }
                    else
                    {
                        reader.changeLine(ownerLoc, reader.readLine(ownerLoc).Split('=')[0] + "=" + transferTag);
                    }

                    int controllerLoc = provLoc + provBlock.getPosition("controller=") + i;
                    if (controllerLoc == provLoc + i - 1)
                    {
                        provinces.addLine("\t\tcontroller=" + transferTag, provLoc + 3 - provincesLoc);
                        reader.addLine("\t\tcontroller=" + transferTag, provLoc + 3);
                        i++;
                    }
                    else
                    {
                        reader.changeLine(controllerLoc, reader.readLine(controllerLoc).Split('=')[0] + "=" + transferTag);
                    }
                }

                if (cores)
                {
                    int coreLoc = provLoc + provBlock.getPosition("core=") + i;
                    if (coreLoc == provLoc + i - 1)
                    {
                        provinces.addLine("\t\tcore=" + coreTag, provLoc + 3 - provincesLoc);
                        reader.addLine("\t\tcore=" + coreTag, provLoc + 3);
                        i++;
                    }
                    else
                    {
                        provinces.addLine("\t\tcore=" + coreTag, coreLoc - provincesLoc);
                        reader.addLine("\t\tcore=" + coreTag, coreLoc);
                        i++;
                    }
                }

                if (cultureChange)
                {
                    int cultureLoc = provLoc + provBlock.getPosition("culture=") + i;
                    if (cultureLoc == provLoc + i - 1)
                    {
                        provinces.addLine("\t\tculture=" + culture, provLoc + 3 - provincesLoc);
                        reader.addLine("\t\tculture=" + culture, provLoc + 3);
                        i++;
                    }
                    else
                    {
                        reader.changeLine(cultureLoc, reader.readLine(cultureLoc).Split('=')[0] + "=" + culture);
                    }
                }

                if (religionChange)
                {
                    int religionLoc = provLoc + provBlock.getPosition("religion=") + i;
                    if (religionLoc == provLoc + i - 1)
                    {
                        provinces.addLine("\t\treligion=" + culture, provLoc + 3 - provincesLoc);
                        reader.addLine("\t\treligion=" + culture, provLoc + 3);
                        i++;
                    }
                    else
                    {
                        reader.changeLine(religionLoc, reader.readLine(religionLoc).Split('=')[0] + "=" + religion);
                    }
                }

                if (baseTaxChange)
                {
                    if (multiplyBaseTax)
                    {
                        int baseTaxLoc = provLoc + provBlock.getPosition("base_tax=") + i;
                        if (baseTaxLoc == provLoc + i - 1)
                        {
                            provinces.addLine("\t\tbase_tax=" + baseTax, provLoc + 3 - provincesLoc);
                            reader.addLine("\t\tbase_tax=" + baseTax, provLoc + 3);
                            i++;
                        }
                        else
                        {
                            string oldBaseTaxString = reader.readLine(baseTaxLoc).Trim().Split('=')[1];
                            double oldBaseTax = Convert.ToDouble(oldBaseTaxString);
                            double newBaseTax = oldBaseTax * baseTax;
                            reader.changeLine(baseTaxLoc, reader.readLine(baseTaxLoc).Split('=')[0] + "=" + newBaseTax.ToString());
                        }
                    }
                    else
                    {
                        int baseTaxLoc = provLoc + provBlock.getPosition("base_tax=") + i;
                        if (baseTaxLoc == provLoc + i - 1)
                        {
                            provinces.addLine("\t\tbase_tax=" + baseTax, provLoc + 3 - provincesLoc);
                            reader.addLine("\t\tbase_tax=" + baseTax, provLoc + 3);
                            i++;
                        }
                        else
                        {
                            reader.changeLine(baseTaxLoc, reader.readLine(baseTaxLoc).Split('=')[0] + "=" + baseTax);
                        }
                    }
                }

                if (manpowerChange)
                {
                    if (multiplyManpower)
                    {
                        int baseManLoc = provLoc + provBlock.getPosition("manpower=") + i;
                        if (baseManLoc == provLoc + i - 1)
                        {
                            provinces.addLine("\t\tmanpower=" + baseManpower, provLoc + 3 - provincesLoc);
                            reader.addLine("\t\tmanpower=" + baseManpower, provLoc + 3);
                            i++;
                        }
                        else
                        {
                            string oldBaseTaxString = reader.readLine(baseManLoc).Trim().Split('=')[1];
                            double oldBaseMan = Convert.ToDouble(oldBaseTaxString);
                            double newBaseMan = oldBaseMan * baseTax;
                            reader.changeLine(baseManLoc, reader.readLine(baseManLoc).Split('=')[0] + "=" + newBaseMan.ToString());
                        }
                    }
                    else
                    {
                        int baseTaxLoc = provLoc + provBlock.getPosition("base_tax=") + i;
                        if (baseTaxLoc == provLoc + i - 1)
                        {
                            provinces.addLine("\t\tbase_tax=" + baseTax, provLoc + 3 - provincesLoc);
                            reader.addLine("\t\tbase_tax=" + baseTax, provLoc + 3);
                            i++;
                        }
                        else
                        {
                            reader.changeLine(baseTaxLoc, reader.readLine(baseTaxLoc).Split('=')[0] + "=" + baseTax);
                        }
                    }
                }
            }
        }

        public static void writeFile(fileReader reader, string outputFile)
        {
            reader.writeFile(outputFile);
        }
    }
}
