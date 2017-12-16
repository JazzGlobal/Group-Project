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
    public partial class TotalForm : Form
    {
        Material_List_Builder mlb; 
        public TotalForm(double total,Material_List_Builder mlb)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedDialog;

            this.mlb = mlb;
            label1.Text = "The total price of your list is $" + total;
            MaximizeBox = false;
        }
        /// <summary>
        /// Sends user back to previous form. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            mlb.Show(); 
        }

        /// <summary>
        /// Sends user back to previous form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, EventArgs e)
        {
            Close(); 
        }
    }
}
