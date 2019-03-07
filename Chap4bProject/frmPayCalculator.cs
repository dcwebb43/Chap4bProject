using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/* Commission rate information:
 *  If monthlySales < 10000 Then rate = .05
 *  If monthlySales 10000 to 14999 Then rate = .1
 *  If monthlySales 15000 to 17999 Then rate = .12
 *  If monthlySales 18000 to 21999 Then rate = .14
 *  If monthlySales >= 22000 Then rate = .15
 */

namespace Chap4bProject
{
    public partial class frmPayCalculator : Form
    {
        public frmPayCalculator()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            // declare variables and constants
            decimal decSales, decCommissionRate, decCommission, decNetPay;
            int intAdvPay;
            Boolean blnValid = true;
            string strErr="";
            const decimal decMaxSales = 1000000;
            const int intMaxAdvPay = 10000, intMaxName = 20;
            // validate input
            if (txtLastName.Text.Length == 0 || txtLastName.Text.Length > 20)
            {
                blnValid = false;
                strErr = "Last name must be at least 1 character, and no more than " + 
                    intMaxName.ToString() + " characters long." + "\n";
            }
            if (decimal.TryParse(txtSales.Text, out decSales))
            {
                if (decSales < 0 || decSales > decMaxSales)
                {
                    blnValid = false;
                    strErr += "Monthly sales must be between 0 and " + 
                        decMaxSales.ToString() +"\n";
                }
            }
            else // user entered non-decimal value
            {
                blnValid = false;
                strErr += "Monthly sales must be numeric." + "\n";
            }
            // still need to validate advance pay...
            if (int.TryParse(txtAdvancePay.Text, out intAdvPay))
            {
                if (intAdvPay < 0 || intAdvPay > intMaxAdvPay)
                {
                    blnValid = false;
                    strErr += "Advance pay must be between 0 and " +
                        intMaxAdvPay.ToString() + "\n";
                }
            }
            else // user entered non-integer value
            {
                blnValid = false;
                strErr += "Advance pay must be an integer.";
            }
            // Validation is over. If blnValid is STILL true, then continue
            // with calculations. If blnValid is false, then only show error msg(s).
            if (blnValid)
            {
                // First, determine correct commission rate
                if (decSales < 10000)
                {
                    decCommissionRate = .05m;
                }
                else if (decSales <= 14999)
                {
                    decCommissionRate = .1m;
                }
                else if (decSales <= 17999)
                {
                    decCommissionRate = .12m;
                }
                else if (decSales <= 21999)
                {
                    decCommissionRate = .14m;
                }
                else // decSales > 21999
                {
                    decCommissionRate = .15m;
                }
                // Calculate commission and net pay
                decCommission = decCommissionRate * decSales;
                decNetPay = decCommission - intAdvPay;
                // Display output
                lblCommissionRate.Text = decCommissionRate.ToString("p");
                lblCommission.Text = decCommission.ToString("c");
                lblNetPay.Text = decNetPay.ToString("c");
            }
            else // user entered invalid data
            {
                // erase old output
                lblCommissionRate.Text = "";
                lblCommission.Text = "";
                lblNetPay.Text = "";
                // show error msg(s)
                MessageBox.Show("Please correct the following: " + "\n" + strErr);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtAdvancePay.Clear();
            txtLastName.Clear();
            txtSales.Clear();
            lblCommission.Text = "";
            lblCommissionRate.Text = "";
            lblNetPay.Text = "";
            txtLastName.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
                this.Close();
        }

        private void frmPayCalculator_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dlgReply;
            dlgReply = MessageBox.Show("Do you want to exit?", "Confirm Exit",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgReply == DialogResult.No)
            {
                e.Cancel = true; 
            }
        }
    }
}
