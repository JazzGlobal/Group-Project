using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace GroupProject
{
    public partial class LoadingForm : Form
    {

        XmlDocument PreviewDocument; //Used to show preview information. 
        List<XmlDocument> XmlList;
        bool closing; 

        public LoadingForm(Form1 form)
        {

            closing = true; 
            InitializeComponent();
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;

            PopulateListBox();
            if(listBox1.Items.Count > 0)
            {
                listBox1.SelectedIndex = 0;
            }
        }
        
        /// <summary>
        /// Fills ListBox with files in the MaterialList Directory. 
        /// </summary>
        private void PopulateListBox()
        {
            listBox1.Items.Clear(); 
            XmlList = new List<XmlDocument>(); 
            for(int i = 0; i < Directory.GetFiles(Directory.GetCurrentDirectory()).Length; i++)
            {
                XmlList.Add(Estimate.ReadXml(Directory.GetFiles(Directory.GetCurrentDirectory())[i]));
            }
            foreach(XmlDocument x in XmlList)
            {
                string s = x.GetElementsByTagName("estimate")[0].Attributes.GetNamedItem("LastName").Value;
                listBox1.Items.Add(s);
            }
        }

        /// <summary>
        /// Updates information labels.
        /// </summary>
        private void UpdateLabels()
        {
            addressLabel.Text = "Address: " + CustomerInformation().Item3; 
            cityStateLabel.Text = "City, State: " + CustomerInformation().Item5 + ", " + CustomerInformation().Item4;
            zipCodeLabel.Text = "Zip Code: " + CustomerInformation().Item6; 
        }

        /// <summary>
        /// Returns the user information from the PreviewDocument in the form of a Tuple object.
        /// </summary>
        /// <returns></returns>
        private Tuple<string,string,string,string,string,string> CustomerInformation()
        {
            PreviewDocument = XmlList[listBox1.SelectedIndex];
            string firstname = PreviewDocument.GetElementsByTagName("estimate")[0].Attributes.GetNamedItem("FirstName").Value;
            string laststname = PreviewDocument.GetElementsByTagName("estimate")[0].Attributes.GetNamedItem("LastName").Value;
            string address = PreviewDocument.GetElementsByTagName("estimate")[0].Attributes.GetNamedItem("Address").Value;
            string state = PreviewDocument.GetElementsByTagName("estimate")[0].Attributes.GetNamedItem("State").Value;
            string city = PreviewDocument.GetElementsByTagName("estimate")[0].Attributes.GetNamedItem("City").Value;
            string zipcode = PreviewDocument.GetElementsByTagName("estimate")[0].Attributes.GetNamedItem("ZipCode").Value;

            return new Tuple<string, string, string, string, string, string>(firstname, laststname, address, state, city, zipcode);
        }

        /// <summary>
        /// Sends user to MaterialListBuilder form with loaded information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if(listBox1.Items.Count > 0)
            {
                closing = false;
                Material_List_Builder mlb = new Material_List_Builder(PreviewDocument);
                mlb.Show();
                Close();
            }
        }

        /// <summary>
        /// Updates label to selected index information. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateLabels(); 
        }

        /// <summary>
        /// Delete button. Only functions if "listBox1" has Items. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if(listBox1.Items.Count > 0)
            {
                File.Delete((string)listBox1.SelectedItem + ".xml");
                Console.WriteLine(listBox1.SelectedItem + ".xml");
                PopulateListBox();
            }
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Closes Application.
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
        /// Sends user back to MainForm. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            closing = false;
            Close();
            Form1.MainForm.Show(); 
        }
    }
}
