using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Keys = System.Windows.Forms.Keys;

namespace paradoxSaveEditor
{
    /// <summary>
    /// Class for the main form for the save game editor
    /// </summary>
    public partial class EU4SaveGameEditor : Form
    {
        int currentState; //0 - main menu, 1-9 various functions
        fileReader reader;
        string inputFile;
        /// <summary>
        /// Initialize a main menu form
        /// </summary>
        /// <param name="saveFile">The save file to edit</param>
        public EU4SaveGameEditor(string saveFile)
        {
            currentState = 0;
            InitializeComponent();
            inputFile = saveFile;
            reader = new fileReader(saveFile);
            instructionText.AppendText("Save game successfully read. Please select an option below.\r\n");
            aggExpButton.Visible = true;
            techLevelButton.Visible = true;
            monarchAbilitiesButton.Visible = true;
            countryResourcesButton.Visible = true;
            provinceButton.Visible = true;
            writeButton.Visible = true;
            inputTextRight.Enabled = false;
        }

        private void inputTextRight_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handle user entered input based on our current state
        /// </summary>
        private void inputEntered(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (currentState == 1) //Agg Exp
                {
                    inputTextRight.Enabled = false;
                    string inputText = inputTextRight.Text;
                    saveEditFn.alterAggExp(reader, inputText);
                    currentState = 0;
                    instructionText.AppendText("Edit complete. Please select an option below.\r\n");
                }
                else if (currentState == 2) //Tech level
                {
                    inputTextRight.Enabled = false;
                    string inputText = inputTextRight.Text;
                    saveEditFn.alterTechLevel(reader, inputText);
                    currentState = 0;
                }
                else if (currentState == 3) //Monarch Abilities
                {
                    inputTextRight.Enabled = false;
                    string inputText = inputTextRight.Text;
                    instructionText.AppendText("The monarchs and heirs of a selected tag will be displayed in reverse cronological order. The current monarch and the current heir (if there is one) will be the first displayed in their respective catagories.\r\n");
                    saveEditFn.changeMonarchAbilities(reader, inputText);
                    currentState = 0;
                }
                else if (currentState == 4) //Country Resources
                {
                    inputTextRight.Enabled = false;
                    string inputText = inputTextRight.Text;
                    saveEditFn.changeCountyResouces(reader, inputText);
                    currentState = 0;
                }
                inputTextRight.Text = "";
            }
        }

        //Handle the various button presses for the buttons on the main menu by changing state and asking the user for further input
        private void aggExpButton_Click(object sender, EventArgs e)
        {
            instructionText.AppendText("Enter the tag(s) of the country or countries you want to remove aggressive expansion for (multiple tags should be seperated by commas).\r\n");
            currentState = 1;
            inputTextRight.Enabled = true;
            this.ActiveControl = inputTextRight;
        }

        private void techLevel_click(object sender, EventArgs e)
        {
            instructionText.AppendText("Enter the tag(s) of the country or countries you want to alter tech level for (multiple tags should be seperated by commas)\r\n");
            currentState = 2;
            inputTextRight.Enabled = true;
            this.ActiveControl = inputTextRight;
        }

        private void monarchAbilitiesButton_Click(object sender, EventArgs e)
        {
            instructionText.AppendText("Enter the tag(s) of the country or countries you want to alter monarch abilities for (multiple tags should be seperated by commas)\r\n");
            currentState = 3;
            inputTextRight.Enabled = true;
            this.ActiveControl = inputTextRight;
        }

        private void countryResourcesButton_Click(object sender, EventArgs e)
        {
            instructionText.AppendText("Enter the tag(s) of the country or countries you want to alter country resources for (multiple tags should be seperated by commas)\r\n");
            currentState = 4;
            inputTextRight.Enabled = true;
            this.ActiveControl = inputTextRight;
        }

        private void provinceButton_Click(object sender, EventArgs e)
        {
            provinceEditor provEditor = new provinceEditor(reader);
            provEditor.ShowDialog();
        }

        private void writeButton_click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = System.Environment.CurrentDirectory;
            if ( saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK )
            {
                string outputFile = saveFile.FileName;
                saveEditFn.writeFile(reader, outputFile);
                instructionText.AppendText("Write successful!");
            }
            
        }

    }
}
