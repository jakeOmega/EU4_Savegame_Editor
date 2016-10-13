using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace paradoxSaveEditor
{
    /// <summary>
    /// Handles the logic for changing the save file
    /// </summary>
    class saveEditFn
    {
        /// <summary>
        /// Eliminates the aggressive expansion opinion penalty for one or more countries
        /// </summary>
        /// <param name="reader">The reader for the save file</param>
        /// <param name="input">A string containing a comma separated list of country tags</param>
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

        /// <summary>
        /// Launches the technology level change form for one or more countries
        /// </summary>
        /// <param name="reader">The reader for the save file</param>
        /// <param name="input">A string containing a comma separated list of country tags</param>
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

        /// <summary>
        /// Launches the monarch change form for one or more countries
        /// </summary>
        /// <param name="reader">The reader for the save file</param>
        /// <param name="input">A string containing a comma separated list of country tags</param>
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

        /// <summary>
        /// Launches the country resource change form for one or more countries
        /// </summary>
        /// <param name="reader">The reader for the save file</param>
        /// <param name="input">A string containing a comma separated list of country tags</param>
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

        /// <summary>
        /// Write the (altered) save game to file
        /// </summary>
        /// <param name="reader">The reader containing the file</param>
        /// <param name="outputFile">Where to write the file</param>
        public static void writeFile(fileReader reader, string outputFile)
        {
            reader.writeFile(outputFile);
        }
    }
}
