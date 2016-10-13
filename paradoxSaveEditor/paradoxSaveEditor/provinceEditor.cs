using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace paradoxSaveEditor
{
    /// <summary>
    /// Handles batch editing of provinces based on user input
    /// </summary>
    public partial class provinceEditor : Form
    {
        fileReader reader;//reader containing save to change
        /// <summary>
        /// Initialize province editing form
        /// </summary>
        /// <param name="readerIn">reader containing save to edit</param>
        public provinceEditor(fileReader readerIn)
        {
            reader = readerIn;
            InitializeComponent();
        }

        /// <summary>
        /// Handles display of the changing of province editing mode based on user selection
        /// </summary>
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if( (string)editByButton.SelectedItem == "Province ID")
            {
                editInputLabel.Text = "Enter comma \nseparated province \nIDs";
            }
            else if ((string)editByButton.SelectedItem == "Owner")
            {
                editInputLabel.Text = "Enter owner tag(s)";
            }
            else if ((string)editByButton.SelectedItem == "Culture")
            {
                editInputLabel.Text = "Enter EU4 culture \nidentifier(s)";
            }
            else if ((string)editByButton.SelectedItem == "Religion")
            {
                editInputLabel.Text = "Enter EU4 religion \nidentifier(s)";
            }
            else if ((string)editByButton.SelectedItem == "Name")
            {
                editInputLabel.Text = "Enter EU4 province \nname(s)";
            }
            else if ((string)editByButton.SelectedItem == "All")
            {
                editInputLabel.Text = "All provinces \nwill be edited";
            }
        }

        /// <summary>
        /// Batch edit provinces based on user entries when run button pressed
        /// </summary>
        private void runButton_Click(object sender, EventArgs e)
        {
            string newOwner = newOwnerBox.Text;
            string newCore = newCoreBox.Text;
            string newCulture = newCultureBox.Text;
            string newReligion = newReligionBox.Text;
            string newTax = newTaxBox.Text;
            string newManpower = newManpowerBox.Text;
            bool taxMult = taxMultCheck.Checked;
            bool manMult = manpowerMultCheck.Checked;
            int provLoc = reader.getPosition("provinces=", 0, '{', '}');
            reader.goTo(provLoc);
            textBlock provinces = new textBlock('{','}', reader);
            Regex rgx = new Regex(@"-[0-9]+=");//province number finding regular expression

            string[] editList = editByInputBox.Text.Replace(" ", "").Split(',');
            int[] provLocs = provinces.findAll(x => rgx.IsMatch(x), 1, '{', '}');
            

            //for every province, make changes based on user entry in this form
            foreach ( int prov in provLocs )
            {
                //check if this province is included in the user's province specificiation
                textBlock thisProv = new textBlock();
                bool editThis = false;
                if ( (string)editByButton.SelectedItem == "Province ID")
                {
                    reader.goTo(provLoc + prov);
                    thisProv = new textBlock('{', '}', reader);
                    editThis = editList.Contains(reader.readLine(provLoc + prov).Replace("-", "").Replace("=", "").Trim());
                }
                else if ((string)editByButton.SelectedItem == "Owner")
                {
                    reader.goTo(provLoc + prov);
                    thisProv = new textBlock('{', '}', reader);
                    int ownerLoc = thisProv.getPosition("owner=");
                    if (ownerLoc != -1)
                    {
                        string owner = reader.readLine(provLoc + prov + ownerLoc).Split('=')[1].Trim();
                        editThis = editList.Contains(owner);
                    }
                }
                else if ((string)editByButton.SelectedItem == "Culture")
                {
                    reader.goTo(provLoc + prov);
                    thisProv = new textBlock('{', '}', reader);
                    int cultureLoc = thisProv.getPosition("culture=");
                    if (cultureLoc != -1)
                    {
                        string culture = reader.readLine(provLoc + prov + cultureLoc).Split('=')[1].Trim();
                        editThis = editList.Contains(culture);
                    }
                }
                else if ((string)editByButton.SelectedItem == "Religion")
                {
                    reader.goTo(provLoc + prov);
                    thisProv = new textBlock('{', '}', reader);
                    int relLoc = thisProv.getPosition("religion=");
                    if (relLoc != -1)
                    {
                        string rel = reader.readLine(provLoc + prov + relLoc).Split('=')[1].Trim();
                        editThis = editList.Contains(rel);
                    }
                }
                else if ((string)editByButton.SelectedItem == "Name")
                {
                    reader.goTo(provLoc + prov);
                    thisProv = new textBlock('{', '}', reader);
                    int nameLoc = thisProv.getPosition("name=");
                    if (nameLoc != -1)
                    {
                        string name = reader.readLine(provLoc + prov + nameLoc).Split('=')[1].Trim().Replace("\"","");
                        editThis = editList.Contains(name);
                    }
                }
                else if ((string)editByButton.SelectedItem == "All")
                {
                    reader.goTo(provLoc + prov);
                    thisProv = new textBlock('{', '}', reader);
                    editThis = true;
                }
                //If it is something to edit, edit it appropriately
                if ( editThis )
                {
                    int nameLoc = thisProv.getPosition("name=");
                    if (nameLoc != -1)
                    {
                        string name = reader.readLine(provLoc + prov + nameLoc).Split('=')[1].Trim().Replace("\"","");
                        outputBox.AppendText("Editing province " + name+"\r\n");
                    }
                    if ( newOwner != "")
                    {
                        reader.changeLine(provLoc + prov + thisProv.getPosition("owner="), "\t\towner=" + newOwner);
                        reader.changeLine(provLoc + prov + thisProv.getPosition("controller="), "\t\tcontroller=" + newOwner);
                    }
                    if (newCore != "")
                    {
                        if (thisProv.getPosition("\t\tcore=" + newCore) == -1)
                        {
                            reader.addLine("\t\tcore=" + newCore, provLoc + prov + thisProv.getPosition("core="));
                            provLoc++;
                        }
                    }
                    if (newCulture != "")
                    {
                        reader.changeLine(provLoc + prov + thisProv.getPosition("culture="), "\t\tculture=" + newCulture);
                    }
                    if (newReligion != "")
                    {
                        reader.changeLine(provLoc + prov + thisProv.getPosition("religion="), "\t\treligion=" + newReligion);
                    }
                    if (newTax != "")
                    {
                        int taxLine = thisProv.getPosition("base_tax=");
                        if ( taxLine != -1)
                        {
                            if (!taxMult)
                            {
                                reader.changeLine(provLoc + prov + taxLine, "\t\tbase_tax=" + newTax);
                            }
                            else
                            {
                                double oldTax = Convert.ToDouble(thisProv.getLine(taxLine).Split('=')[1]);
                                double newTaxDouble = Convert.ToDouble(newTax);
                                string overwriteTax = (newTaxDouble * oldTax).ToString();
                                reader.changeLine(provLoc + prov + thisProv.getPosition("base_tax="), "\t\tbase_tax=" + overwriteTax);
                            }
                        }
                    }
                    if (newManpower != "")
                    {
                        int manLine = thisProv.getPosition("manpower=");
                        if (manLine != -1)
                        {
                            if (!manMult)
                            {
                                reader.changeLine(provLoc + prov + manLine, "\t\tmanpower=" + newManpower);
                            }
                            else
                            {
                                double oldMan = Convert.ToDouble(thisProv.getLine(manLine).Split('=')[1]);
                                double newManDouble = Convert.ToDouble(newManpower);
                                string overwriteMan = (newManDouble * oldMan).ToString();
                                reader.changeLine(provLoc + prov + thisProv.getPosition("manpower="), "\t\tmanpower=" + overwriteMan);
                            }
                        }
                    }
                }
            }
            outputBox.AppendText("Done!\r\n");
        }


        //close the form when done
        private void doneButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
