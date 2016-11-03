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
    /// Form for listing all monarchs and heirs from a particular country
    /// and launching a monarchEditForm when the user selects one
    /// </summary>
    public partial class monarchAbilitiesForm : Form
    {
        fileReader reader; //reader for our save
        //lists of parts of save for monarchs and heirs
        List<textBlock> monarchBlocks = new List<textBlock>();
        List<textBlock> heirBlocks = new List<textBlock>();
        int historyLoc;//location in the save for this country's history
        int countryLoc;//location in the save for this country
        //locations of the beginning of the text for each monarch and heir
        int[] monarchs;
        int[] heirs;
        //list of locations for unique heirs and repeat heirs (heirs that became monarch and so are listed twice)
        List<int> uniqueHeirs;
        List<int> repeatHeirs;
        string tag;

        /// <summary>
        /// Initialize a monarchAbilitiesForm, displaying the monarchs and heirs in a specified country
        /// </summary>
        /// <param name="readerIn">A reader for the save file we're editing</param>
        /// <param name="Loc">the line at which the relevant country text starts</param>
        /// <param name="tag">the three letter identifier for the relevant country (e.g. FRA for France)</param>
        public monarchAbilitiesForm(fileReader readerIn, int Loc, string Tag)
        {
            reader = readerIn;
            countryLoc = Loc;
            tag = Tag;
            InitializeComponent();
            initialize();
        }

        /// <summary>
        /// Initialize the form.
        /// </summary>
        public void initialize()
        {
            List<string> monarchIDs = new List<string>();
            reader.goTo(this.countryLoc);
            textBlock country = new textBlock('{', '}', reader);
            int historyLocLocal = country.getPosition("history=");
            reader.goTo(this.countryLoc + historyLocLocal);
            textBlock history = new textBlock('{', '}', reader);
            monarchs = history.findAll("monarch=", 2, '{', '}');
            heirs = history.findAll("heir=", 2, '{', '}');
            uniqueHeirs = new List<int>();
            repeatHeirs = new List<int>();
            historyLoc = countryLoc + historyLocLocal;

            monarchBox.Items.Clear();
            monarchIDs.Clear();
            monarchBlocks.Clear();
            heirBox.Items.Clear();
            heirBlocks.Clear();

            int i = monarchs.Length - 1;
            while (i >= 0)
            {
                reader.goTo(historyLoc + monarchs[i]);
                textBlock monarch = new textBlock('{', '}', reader);
                string name = monarch.getLine(monarch.getPosition("name=")).Split('=')[1].Replace("\"", "").Trim();
                int thing = monarch.getPosition("id=");
                string ID = monarch.getLine(monarch.getPosition("id=", false, 2)).Split('=')[1].Trim();
                monarchBox.Items.Add(name);
                monarchIDs.Add(ID);
                monarchBlocks.Add(monarch);
                i--;
            }

            i = heirs.Length - 1;
            while (i >= 0)
            {
                reader.goTo(historyLoc + heirs[i]);
                textBlock monarch = new textBlock('{', '}', reader);
                string name = monarch.getLine(monarch.getPosition("name=")).Split('=')[1].Replace("\"", "").Trim();
                string ID = monarch.getLine(monarch.getPosition("id=", false, 2)).Split('=')[1].Trim();
                if (!monarchIDs.Contains(ID))
                {
                    heirBox.Items.Add(name);
                    heirBlocks.Add(monarch);
                    uniqueHeirs.Add(i);
                    repeatHeirs.Add(-1);
                }
                else
                {
                    repeatHeirs.Add(monarchIDs.IndexOf(ID));
                }
                i--;
            }
        }

        /// <summary>
        /// Close when done
        /// </summary>
        private void onDoneButton(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// On clicking on a monarch, launch a monarchEditForm to edit the monarch, then apply the relevant changes
        /// </summary>
        private void monarchBox_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            textBlock monarch = monarchBlocks[monarchBox.SelectedIndex];
            monarchEditForm monarchEdit = new monarchEditForm(monarch);
            monarchEdit.ShowDialog();
            int baseLocMon = historyLoc + monarchs[monarchs.Length - 1 - monarchBox.SelectedIndex];
            textBlock heir = new textBlock();

            reader.changeLine(baseLocMon + monarch.getPosition("name="), "\t\t\t\t\tname=\"" + monarchEdit.name+"\"");
            reader.changeLine(baseLocMon + monarch.getPosition("DIP="), "\t\t\t\t\tDIP=" + monarchEdit.dip);
            reader.changeLine(baseLocMon + monarch.getPosition("ADM="), "\t\t\t\t\tADM=" + monarchEdit.adm);
            reader.changeLine(baseLocMon + monarch.getPosition("MIL="), "\t\t\t\t\tMIL=" + monarchEdit.mil);
            //reader.changeLine(baseLocMon + monarch.getPosition("dynasty="), "\t\t\t\t\tdynasty=\"" + monarchEdit.dynasty+"\"");
            //reader.changeLine(baseLocMon + monarch.getPosition("birth_date="), "\t\t\t\t\tbirth_date=" + monarchEdit.birthDate);

            if (repeatHeirs.Contains(monarchBox.SelectedIndex))
            {
                int baseLocHeir = historyLoc + heirs[heirs.Length - 1 - repeatHeirs.IndexOf(monarchBox.SelectedIndex)];
                heir = heirBlocks[repeatHeirs.IndexOf(monarchBox.SelectedIndex)];
                reader.changeLine(baseLocHeir + heir.getPosition("name="), "\t\t\t\t\tname=\"" + monarchEdit.name + "\"");
                reader.changeLine(baseLocHeir + heir.getPosition("DIP="), "\t\t\t\t\tDIP=" + monarchEdit.dip);
                reader.changeLine(baseLocHeir + heir.getPosition("ADM="), "\t\t\t\t\tADM=" + monarchEdit.adm);
                reader.changeLine(baseLocHeir + heir.getPosition("MIL="), "\t\t\t\t\tMIL=" + monarchEdit.mil);
                //reader.changeLine(baseLocHeir + heir.getPosition("dynasty="), "\t\t\t\t\tdynasty=\"" + monarchEdit.dynasty + "\"");
                //reader.changeLine(baseLocHeir + heir.getPosition("birth_date="), "\t\t\t\t\tbirth_date=" + monarchEdit.birthDate);
            }
            initialize();
        }

        //Same as for monarchs above
        private void heirBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBlock monarch = heirBlocks[heirBox.SelectedIndex];
            monarchEditForm monarchEdit = new monarchEditForm(monarch);
            monarchEdit.ShowDialog();
            int baseLocMon = historyLoc + heirs[uniqueHeirs[heirBox.SelectedIndex]];

            reader.changeLine(baseLocMon + monarch.getPosition("name="), "\t\t\t\t\tname=" + monarchEdit.name);
            reader.changeLine(baseLocMon + monarch.getPosition("DIP="), "\t\t\t\t\tDIP=" + monarchEdit.dip);
            reader.changeLine(baseLocMon + monarch.getPosition("ADM="), "\t\t\t\t\tADM=" + monarchEdit.adm);
            reader.changeLine(baseLocMon + monarch.getPosition("MIL="), "\t\t\t\t\tMIL=" + monarchEdit.mil);
            //reader.changeLine(baseLocMon + monarch.getPosition("dynasty="), "\t\t\t\t\tdynasty=" + monarchEdit.dynasty);
            //reader.changeLine(baseLocMon + monarch.getPosition("birth_date="), "\t\t\t\t\tbirth_date=" + monarchEdit.birthDate);

            initialize();
        }
    }
}
