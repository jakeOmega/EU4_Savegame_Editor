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
    public partial class monarchAbilitiesForm : Form
    {
        fileReader reader;
        List<textBlock> monarchBlocks = new List<textBlock>();
        List<textBlock> heirBlocks = new List<textBlock>();
        int loc;
        int[] monarchs;
        int[] heirs;
        List<int> uniqueHeirs;
        List<int> repeatHeirs;
        public monarchAbilitiesForm(fileReader readerIn, int Loc, string tag)
        {
            List<string> monarchIDs = new List<string>();
            reader = readerIn;
            InitializeComponent();
            reader.goTo(Loc);
            textBlock country = new textBlock('{', '}', reader);
            int historyLoc = country.getPosition("history=");
            reader.goTo(Loc + historyLoc);
            textBlock history = new textBlock('{', '}', reader);
            monarchs = history.findAll("monarch=", 2, '{', '}');
            heirs = history.findAll("heir=", 2, '{', '}');
            uniqueHeirs = new List<int>();
            repeatHeirs = new List<int>();
            loc = Loc+historyLoc;

            int i = monarchs.Length - 1;
            while ( i >= 0)
            {
                reader.goTo(Loc + historyLoc + monarchs[i]);
                textBlock monarch = new textBlock('{', '}', reader);
                string name = monarch.getLine(monarch.getPosition("name=")).Split('=')[1].Replace("\"","").Trim();
                int thing = monarch.getPosition("id=");
                string ID = monarch.getLine(monarch.getPosition("id=",false, 2)).Split('=')[1].Trim();
                monarchBox.Items.Add(name);
                monarchIDs.Add(ID);
                monarchBlocks.Add(monarch);
                i--;
            }

            i = heirs.Length - 1;
            while (i >= 0)
            {
                reader.goTo(Loc + historyLoc + heirs[i]);
                textBlock monarch = new textBlock('{', '}', reader);
                string name = monarch.getLine(monarch.getPosition("name=")).Split('=')[1].Replace("\"", "").Trim();
                string ID = monarch.getLine(monarch.getPosition("id=", false, 2)).Split('=')[1].Trim();
                if ( !monarchIDs.Contains(ID) )
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

        private void onDoneButton(object sender, EventArgs e)
        {
            this.Close();
        }

        private void monarchBox_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            textBlock monarch = monarchBlocks[monarchBox.SelectedIndex];
            monarchEditForm monarchEdit = new monarchEditForm(monarch);
            monarchEdit.ShowDialog();
            int baseLocMon = loc + monarchs[monarchs.Length - 1 - monarchBox.SelectedIndex];
            textBlock heir = new textBlock();

            reader.changeLine(baseLocMon + monarch.getPosition("name="), "\t\t\t\t\tname=\"" + monarchEdit.name+"\"");
            reader.changeLine(baseLocMon + monarch.getPosition("DIP="), "\t\t\t\t\tDIP=" + monarchEdit.dip);
            reader.changeLine(baseLocMon + monarch.getPosition("ADM="), "\t\t\t\t\tADM=" + monarchEdit.adm);
            reader.changeLine(baseLocMon + monarch.getPosition("MIL="), "\t\t\t\t\tMIL=" + monarchEdit.mil);
            //reader.changeLine(baseLocMon + monarch.getPosition("dynasty="), "\t\t\t\t\tdynasty=\"" + monarchEdit.dynasty+"\"");
            //reader.changeLine(baseLocMon + monarch.getPosition("birth_date="), "\t\t\t\t\tbirth_date=" + monarchEdit.birthDate);

            if (repeatHeirs.Contains(monarchBox.SelectedIndex))
            {
                int baseLocHeir = loc + heirs[heirs.Length - 1 - repeatHeirs.IndexOf(monarchBox.SelectedIndex)];
                heir = heirBlocks[repeatHeirs.IndexOf(monarchBox.SelectedIndex)];
                reader.changeLine(baseLocHeir + heir.getPosition("name="), "\t\t\t\t\tname=\"" + monarchEdit.name + "\"");
                reader.changeLine(baseLocHeir + heir.getPosition("DIP="), "\t\t\t\t\tDIP=" + monarchEdit.dip);
                reader.changeLine(baseLocHeir + heir.getPosition("ADM="), "\t\t\t\t\tADM=" + monarchEdit.adm);
                reader.changeLine(baseLocHeir + heir.getPosition("MIL="), "\t\t\t\t\tMIL=" + monarchEdit.mil);
                //reader.changeLine(baseLocHeir + heir.getPosition("dynasty="), "\t\t\t\t\tdynasty=\"" + monarchEdit.dynasty + "\"");
                //reader.changeLine(baseLocHeir + heir.getPosition("birth_date="), "\t\t\t\t\tbirth_date=" + monarchEdit.birthDate);
            }

        }

        private void heirBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBlock monarch = heirBlocks[heirBox.SelectedIndex];
            monarchEditForm monarchEdit = new monarchEditForm(monarch);
            monarchEdit.ShowDialog();
            int baseLocMon = loc + heirs[uniqueHeirs[heirBox.SelectedIndex]];

            reader.changeLine(baseLocMon + monarch.getPosition("name="), "\t\t\t\t\tname=" + monarchEdit.name);
            reader.changeLine(baseLocMon + monarch.getPosition("DIP="), "\t\t\t\t\tDIP=" + monarchEdit.dip);
            reader.changeLine(baseLocMon + monarch.getPosition("ADM="), "\t\t\t\t\tADM=" + monarchEdit.adm);
            reader.changeLine(baseLocMon + monarch.getPosition("MIL="), "\t\t\t\t\tMIL=" + monarchEdit.mil);
            //reader.changeLine(baseLocMon + monarch.getPosition("dynasty="), "\t\t\t\t\tdynasty=" + monarchEdit.dynasty);
            //reader.changeLine(baseLocMon + monarch.getPosition("birth_date="), "\t\t\t\t\tbirth_date=" + monarchEdit.birthDate);
        }
    }
}
