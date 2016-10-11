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
    public partial class countryResources : Form
    {
        public string cash;
        public string manpower;
        public string adm;
        public string dip;
        public string mil;
        public string techGroup;
        public string capital;
        public string stability;
        public List<string> cultures;
        public string religion;
        public string government;
        public string vassalTag;
        public int vassalLoc;
        public string warExhaustion;
        int loc;
        string countryTag;
        fileReader reader;
        textBlock country;
        public countryResources(fileReader readerIn, int Loc, string tag)
        {
            loc = Loc;
            countryTag = tag;
            reader = readerIn;
            InitializeComponent();
            reader.goTo(loc);
            country = new textBlock('{', '}', reader);
            cash = country.getLine(country.getPosition("treasury=")).Split('=')[1];
            manpower = country.getLine(country.getPosition("manpower=")).Split('=')[1];
            int powerLoc = country.getPosition("powers=");
            adm = country.getLine(powerLoc + 2).Split(' ')[0].Trim();
            dip = country.getLine(powerLoc + 2).Split(' ')[1].Trim();
            mil = country.getLine(powerLoc + 2).Split(' ')[2].Trim();
            techGroup = country.getLine(country.getPosition("technology_group=")).Split('=')[1];
            capital = country.getLine(country.getPosition("capital=")).Split('=')[1];
            stability = country.getLine(country.getPosition("stability=")).Split('=')[1];
            cultures = new List<string>();
            cultures.Add(country.getLine(country.getPosition("primary_culture=")).Split('=')[1]);
            int[] acceptedCultureLocs = country.findAll("accepted_culture=");
            foreach ( int pos in acceptedCultureLocs )
            {
                cultures.Add(country.getLine(pos).Split('=')[1]);
            }
            religion = country.getLine(country.getPosition("religion=")).Split('=')[1];
            government = country.getLine(country.getPosition("government=")).Split('=')[1];
            int warExPos = country.getPosition("war_exhaustion=");
            if ( warExPos != -1)
            {
                warExhaustion = country.getLine(country.getPosition("war_exhaustion=")).Split('=')[1];
            }
            else
            {
                warExhaustion = "0.000";
            }

            cashLabel.Text = cash;
            manpowerLabel.Text = manpower;
            admLabel.Text = adm;
            dipLabel.Text = dip;
            milLabel.Text = mil;
            techGroupLabel.Text = techGroup;
            capitalLabel.Text = capital;
            stabilityLabel.Text = stability;
            cultureLabel.Text = string.Join(", ", cultures);
            religionLabel.Text = religion;
            governmentLabel.Text = government;
            warExhaustionLabel.Text = warExhaustion;

            int dipPosition = reader.getPosition("diplomacy=", 0, '{', '}');
            reader.goTo(dipPosition);
            textBlock diplomacy = new textBlock('{', '}', reader);
            int[] vassalages = diplomacy.findAll("vassal=");
            string overlord = "";
            foreach ( int i in vassalages )
            {
                reader.goTo(dipPosition + i);
                textBlock vassal = new textBlock('{', '}', reader);
                if ( vassal.getLine(vassal.getPosition("second=")).Split('=')[1] == "\"" + tag + "\"")
                {
                    overlord = vassal.getLine(vassal.getPosition("first=")).Split('=')[1].Replace("\"", "");
                    vassalLoc = dipPosition + i;
                    break;
                }
            }
            if ( overlord == "" )
            {
                vassalLabel.Text = "none";
                vassalLoc = dipPosition+2;
            }
            else
            {
                vassalLabel.Text = overlord;
            }

        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            if (cashBox.Text != "")
            {
                cash = cashBox.Text;
            }
            if (manpowerBox.Text != "")
            {
                manpower = manpowerBox.Text;
            }
            if (admBox.Text != "")
            {
                adm = admBox.Text;
            }
            if (dipBox.Text != "")
            {
                dip = dipBox.Text;
            }
            if (milBox.Text != "")
            {
                mil = milBox.Text;
            }
            if (techGroupBox.SelectedItem != null)
            {
                techGroup = (string)techGroupBox.SelectedItem;
            }
            if (capitalBox.Text != "")
            {
                capital = capitalBox.Text;
            }
            if (stabilityBox.Text != "")
            {
                stability = stabilityBox.Text;
            }
            if (cultureBox.Text != "")
            {
                cultures = cultureBox.Text.Split(',').ToList<string>();
            }
            if (religionBox.Text != "")
            {
                religion = religionBox.Text;
            }
            if ( governmentBox.SelectedItem != null)
            {
                government = (string)governmentBox.SelectedItem;
            }
            if ( vassalBox.Text != "" )
            {
                vassalTag = vassalBox.Text;
            }
            if ( warExhaustionBox.Text != "" )
            {
                warExhaustion = warExhaustionBox.Text;
            }
            reader.changeLine(loc+country.getPosition("treasury="), "\t\ttreasury="+cash);
            reader.changeLine(loc + country.getPosition("manpower="), "\t\tmanpower=" + manpower);
            reader.changeLine(loc + country.getPosition("powers=") + 2, "\t\t\t" + adm + " "+ dip + " " + mil + " ");
            reader.changeLine(loc + country.getPosition("technology_group="), "\t\ttechnology_group=" + techGroup);
            reader.changeLine(loc + country.getPosition("capital="), "\t\tcapital=" + capital);
            reader.changeLine(loc + country.getPosition("stability="), "\t\tstability=" + stability);
            int[] acceptedCultureLocs = country.findAll("accepted_culture=");
            reader.changeLine(loc + country.getPosition("primary_culture="), "\t\tprimary_culture=" + cultures[0]);
            foreach ( int i in acceptedCultureLocs)
            {
                reader.removeLine(loc + i);
            }
            int j = 1;
            while ( j < cultures.Count)
            {
                reader.addLine("\t\taccepted_culture=" + cultures[j], loc + country.getPosition("primary_culture=") + 1);
                j++;
            }
            reader.changeLine(loc + country.getPosition("religion="), "\t\treligion=" + religion);
            reader.changeLine(loc + country.getPosition("government="), "\t\tgovernment=" + government);

            int warExPos = country.getPosition("war_exhaustion=");
            if ( warExPos != -1 )
            {
                reader.changeLine(loc + warExPos, "\t\twar_exhaustion=" + warExhaustion);
            }
            else
            {
                reader.addLine("\t\twar_exhaustion=" + warExhaustion, loc + country.getPosition("last_bankrupt="));
            }

            if ( vassalTag != ""  && vassalTag != null)
            {
                if (vassalLabel.Text != "none")
                {
                    reader.goTo(vassalLoc);
                    textBlock vassal = new textBlock('{', '}', reader);
                    reader.changeLine(vassalLoc + vassal.getPosition("first="), "\t\tfirst=\"" + vassalTag + "\"");
                }
                else
                {
                    reader.addLine("\tvassal=", vassalLoc);
                    reader.addLine("\t{", vassalLoc+1);
                    reader.addLine("\t\tfirst=\""+ vassalTag+"\"", vassalLoc+2);
                    reader.addLine("\t\tsecond=\"" + countryTag + "\"", vassalLoc + 3);
                    reader.addLine("\t\tend_date=1.1.1", vassalLoc + 4);
                    reader.addLine("\t\tcancel=no", vassalLoc + 5);
                    reader.addLine("\t\tstart_date=1.1.1", vassalLoc + 6);
                    reader.addLine("\t}", vassalLoc + 7);
                }
            }

            this.Close();
        }
    }
}
