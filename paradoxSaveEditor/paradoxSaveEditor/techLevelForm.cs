using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace paradoxSaveEditor
{
    /// <summary>
    /// Form for handling changes in countries tech level
    /// </summary>
    public partial class techLevelForm : Form
    {
        fileReader reader;//reader containing the file to edit
        public int loc;//Where in the file the country block to edit begins
        public string admLevel;//Admin tech level
        public string dipLevel;//diplomatic tech level
        public string milLevel;//military tech level
        public int techLoc;//where in the country block the technology information is
        textBlock country;
        /// <summary>
        /// Initialize a tech level editing form
        /// </summary>
        /// <param name="readerIn">The reader containing the file to edit</param>
        /// <param name="Loc">The location in the reader of the country to edit</param>
        /// <param name="tag">The three letter identifier of the country to edit (e.g. FRA for france)</param>
        public techLevelForm(fileReader readerIn, int Loc, string tag)
        {
            loc = Loc;
            reader = readerIn;
            InitializeComponent();
            reader.goTo(Loc);
            country = new textBlock('{', '}', reader);
            techLoc = country.getPosition("technology=");
            reader.goTo(Loc + techLoc + 2);
            string currentTech = reader.peekLine();
            oldAdmLabel.Text = currentTech.Split('=')[1].Trim();
            reader.moveBy(1);
            currentTech = reader.peekLine();
            oldDipLabel.Text = currentTech.Split('=')[1].Trim();
            reader.moveBy(1);
            currentTech = reader.peekLine();
            oldMilLabel.Text = currentTech.Split('=')[1].Trim();
            instructionText.Text = "Please enter new tech values for country " + tag + "\nor leave a box blank to leave that text level unchanged.";
        }

        /// <summary>
        /// Applies any changes based on user input
        /// </summary>
        private void onDoneButton(object sender, EventArgs e)
        {
            //change tech levels and units appropriately
            admLevel = admBox.Text;
            dipLevel = dipBox.Text;
            milLevel = milBox.Text;
            if (admLevel != "")
            {
                reader.changeLine(loc + techLoc + 2, "\t\t\tadm_tech=" + admLevel);
            }
            if (dipLevel != "")
            {
                int dipInt = Convert.ToInt32(dipLevel);
                reader.changeLine(loc + techLoc + 3, "\t\t\tdip_tech=" + dipLevel);

                int heavyPos = country.getPosition("heavy_ship=");
                if (dipInt >= 3 && heavyPos == -1)
                {
                    reader.addLine("\t\theavy_ship=\"early_carrack\"", loc + country.getPosition("cavalry=") + 1);
                }
                if (dipInt < 3 && heavyPos != -1)
                {
                    reader.deleteLine(loc + heavyPos);
                }
                int lightPos = country.getPosition("light_ship=");
                if (dipInt >= 2 && lightPos == -1)
                {
                    reader.addLine("\t\tlight_ship=\"barque\"", loc + country.getPosition("cavalry=") + 1);
                }
                if (dipInt < 2 && lightPos != -1)
                {
                    reader.deleteLine(loc + lightPos);
                }
                int galleyPos = country.getPosition("galley=");
                if (dipInt >= 2 && galleyPos == -1)
                {
                    reader.addLine("\t\tgalley=\"galley\"", loc + lightPos + 1);
                }
                if (dipInt < 2 && galleyPos != -1)
                {
                    reader.deleteLine(loc + galleyPos);
                }
                int transPos = country.getPosition("transport=");
                if (dipInt >= 2 && transPos == -1)
                {
                    reader.addLine("\t\ttransport=\"cog\"", loc + galleyPos + 1);
                }
                if (dipInt < 2 && transPos != -1)
                {
                    reader.deleteLine(loc + transPos);
                }

                heavyPos = loc + country.getPosition("heavy_ship=");
                lightPos = loc + country.getPosition("light_ship=");
                galleyPos = loc + country.getPosition("galley=");
                transPos = loc + country.getPosition("transport=");
                if ( dipInt >= 2)
                {
                    reader.changeLine(lightPos, "\t\tlight_ship=\"barque\"");
                    reader.changeLine(galleyPos, "\t\tgalley=\"galley\"");
                    reader.changeLine(transPos, "\t\ttransport=\"cog\"");
                }
                if (dipInt >= 3)
                {
                    reader.changeLine(heavyPos, "\t\theavy_ship=\"early_carrack\"");
                }
                if (dipInt >= 9)
                {
                    reader.changeLine(heavyPos, "\t\theavy_ship=\"carrack\"");
                    reader.changeLine(lightPos, "\t\tlight_ship=\"caravel\"");
                }
                if (dipInt >= 10)
                {
                    reader.changeLine(galleyPos, "\t\tgalley=\"war_galley\"");
                    reader.changeLine(transPos, "\t\ttransport=\"flute\"");
                }
                if (dipInt >= 14)
                {
                    reader.changeLine(galleyPos, "\t\tgalley=\"galleass\"");
                    reader.changeLine(transPos, "\t\ttransport=\"brig\"");
                }
                if (dipInt >= 15)
                {
                    reader.changeLine(heavyPos, "\t\theavy_ship=\"galleon\"");
                    reader.changeLine(lightPos, "\t\tlight_ship=\"early_frigate\"");
                }
                if (dipInt >= 17)
                {
                    reader.changeLine(galleyPos, "\t\tgalley=\"galiot\"");
                    reader.changeLine(transPos, "\t\ttransport=\"merchantman\"");
                }
                if (dipInt >= 19)
                {
                    reader.changeLine(heavyPos, "\t\theavy_ship=\"wargalleon\"");
                    reader.changeLine(lightPos, "\t\tlight_ship=\"frigate\"");
                }
                if (dipInt >= 21)
                {
                    reader.changeLine(galleyPos, "\t\tgalley=\"chebeck\"");
                }
                if (dipInt >= 22)
                {
                    reader.changeLine(heavyPos, "\t\theavy_ship=\"twodecker\"");
                    reader.changeLine(transPos, "\t\ttransport=\"trabakul\"");
                }
                if (dipInt >= 23)
                {
                    reader.changeLine(lightPos, "\t\tlight_ship=\"heavy_frigate\"");
                }
                if (dipInt >= 24)
                {
                    reader.changeLine(galleyPos, "\t\tgalley=\"archipelago_frigate\"");
                }
                if (dipInt >= 25)
                {
                    reader.changeLine(heavyPos, "\t\theavy_ship=\"threedecker\"");
                }
                if (dipInt >= 26)
                {
                    reader.changeLine(transPos, "\t\ttransport=\"eastindiaman\"");
                    reader.changeLine(lightPos, "\t\tlight_ship=\"great_frigate\"");
                }
                
            }
            if (milLevel != "")
            {
                reader.changeLine(loc + techLoc + 4, "\t\t\tmil_tech=" + milLevel);
                int artPos = country.getPosition("artillery=");
                if (Convert.ToInt32(milLevel) > 6 && artPos == -1)
                {
                    reader.addLine("\t\tartillery=\"houfnice\"", loc + country.getPosition("cavalry=") + 1);
                }
                if (Convert.ToInt32(milLevel) < 7 && artPos != -1)
                {
                    reader.deleteLine(loc + artPos);
                }
            }
            this.Close();
        }
    }
}
