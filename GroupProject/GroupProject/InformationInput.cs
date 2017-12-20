using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroupProject
{
    public partial class InformationInput : CustomForm
    {
        public Form1 Form;
        private string address;
        private string firstName;
        private string lastName;
        private string city;
        private string state;
        private string zipCode;
        bool closing; 
        public InformationInput(Form1 form)
        {
            this.Form = form; 
            InitializeComponent();
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            closing = true; 
        }
        /// <summary>
        /// Writes XML Data to file and sends user to Material_List_Builder Form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            SetVariables();
            List<Tuple<string, string, string>> list = new List<Tuple<string, string, string>>();
        
            Estimate.WriteXml(firstName, lastName, address, state, city, zipCode, list,false);
            Material_List_Builder mlb = new Material_List_Builder(Estimate.ReadXml(lastName + ".xml"));
            mlb.Show(); 
        }

        /// <summary>
        /// Sets initial data input from user.
        /// </summary>
        private void SetVariables()
        {
            address = addressTextBox.Text;
            firstName = firstNameTextBox.Text;       
            lastName = lastNameTextBox.Text;
            if(lastName == "")
            {
                lastName = "nolastname"; 
            }
            city = cityTextBox.Text;
            state = stateTextBox.Text;
            zipCode = zipCodeTextBox.Text;
        }

        private void InformationInput_Load(object sender, EventArgs e)
        {

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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Sends user back to MainForm.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            closing = false; 
            Close(); 
            Form.Show();
        }
    }
}
