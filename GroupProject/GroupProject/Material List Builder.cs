using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace GroupProject
{
    public partial class Material_List_Builder : Form
    {
        XmlDocument xmlDoc;
        BuildingList buildingList;
        bool closing;
        double total = 0;


        public Material_List_Builder(XmlDocument xmlDoc)
        {
            closing = true; 
            InitializeComponent();
            this.xmlDoc = xmlDoc;
            if (xmlDoc != null)
            {
                Console.WriteLine("Xml Load Completed...");
            }
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            materialListView.GridLines = true;
            materialListView.FullRowSelect = true;
            buildingList = new BuildingList(xmlDoc);

            //Adds initial items to list. 
            for(int i = 0; i < buildingList.MaterialList.Count; i++)
            {
                materialListView.Items.Add(buildingList.MaterialList[i].Item1);
                materialListView.Items[i].SubItems.Add(buildingList.MaterialList[i].Item2);
                materialListView.Items[i].SubItems.Add(buildingList.MaterialList[i].Item3);
            }
        }   
        /// <summary>
        /// Clears and repopulated the listView control.
        /// </summary>
        private void RefreshItems()
        {
            materialListView.Items.Clear();
            for (int i = 0; i < buildingList.MaterialList.Count; i++)
            {
                materialListView.Items.Add(buildingList.MaterialList[i].Item1);
                materialListView.Items[i].SubItems.Add(buildingList.MaterialList[i].Item2);
                materialListView.Items[i].SubItems.Add(buildingList.MaterialList[i].Item3);
            }
        }

        /// <summary>
        /// Reloads the current XML document. 
        /// </summary>
        private void ReloadXmlDocument()
        {
            xmlDoc = Estimate.ReadXml(xmlDoc.Attributes.GetNamedItem("LastName").Value);
        }

        public Material_List_Builder()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
        }

        /// <summary>
        /// Performs various checks to decide if the Material should be added to the list. See comments for details. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //AddItem button
            double x = 0;
            double y = 0; 
            if(nameTextBox.Text != "" && priceTextBox.Text != "" && unitTextBox.Text != "") //Checks to make sure the boxes aren't filled with empty text. 
            {
                var t = new Tuple<string, string, string>(nameTextBox.Text, priceTextBox.Text, unitTextBox.Text);
                bool addToList = false;
                if(IsDigitsOnly(t.Item2) && IsDigitsOnly(t.Item3)) //Checks to make sure the price and quantity text boxes only contain digits between 0 and 9, and decimal points. 
                {
                    if (Double.TryParse(t.Item2, out x) && Double.TryParse(t.Item3, out y)) //Checks to make sure the price and quantity text boxes can be parsed to a correct double. 
                    {
                        addToList = true; 
                    }
                }
                if (addToList) //If all of these are true 
                {
                    buildingList.MaterialList.Add(t); //Add Material to list and...
                    total += x * y; // ... Increase the total by price*quantity. 
                }
                RefreshItems(); 
            }
            ClearTextBoxes();
        }

        /// <summary>
        /// Clears the input textboxes.
        /// </summary>
        private void ClearTextBoxes()
        {
            nameTextBox.Clear();
            priceTextBox.Clear();
            unitTextBox.Clear();
        }

        /// <summary>
        /// Deletes the selected material from the listView and MaterialList.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //Deletes the selected Material from the list.

            var selecteditems = materialListView.SelectedItems; 
            if(selecteditems.Count > 0)
            {
                buildingList.MaterialList.RemoveAt(materialListView.SelectedIndices[0]);
                materialListView.Items.RemoveAt(materialListView.SelectedIndices[0]);
                buildingList.PrintInformation(); 
                
            }
        }

        /// <summary>
        /// Writes a new XML file with the filename of 'buildingList.LastName.xml'. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, EventArgs e)
        { 
            Estimate.WriteXml(buildingList.FirstName,buildingList.LastName,buildingList.Address,buildingList.State,buildingList.City,buildingList.ZipCode,buildingList.MaterialList); 
        }

        /// <summary>
        /// Sends the user back to the LoadingForm.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            closing = false; 
            Close();
            var x = new LoadingForm(Form1.MainForm);
            x.Show();
        }

        /// <summary>
        /// Closes the Application.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (closing)
            {
                base.OnFormClosing(e);
                Application.Exit();
            }

        }

        /// <summary>
        /// Creates a new TotalForm and sends the user to it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            var form = new TotalForm(total,this);
            form.Show();
            this.Hide(); 
        }

        /// <summary>
        /// Returns true if only digits 0 through 9 and Decimal points are located in the string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private bool IsDigitsOnly(string str)
        {
            foreach(char c in str)
            {
                if (c < '0' && c != '.' || c > '9' && c != '.')
                {
                    return false;
                }
            }
            return true; 
        }

        /// <summary>
        /// Clears the Material List. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearButton_Click(object sender, EventArgs e)
        {
            buildingList.MaterialList.Clear();
            ClearTextBoxes();
            RefreshItems();
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
