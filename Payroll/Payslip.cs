using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccess;

namespace Payroll
{
    public partial class Payslip : Form
    {
        private Model m_Model;

        public Payslip(ref Model model)
        {
            InitializeComponent();
            m_Model = model;
            Initialise();
            
        }
        public void Initialise()
        {           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                String text = textBox1.Text.ToString();
                String[] values = text.Split(',');
                String fName = values[0];
                String lName = values[1];
                int annualSalary = Convert.ToInt32(values[2]);
                int superRate = int.Parse(values[3].Substring(0, values[3].Length - 1));
                String dates = values[4];

                DataAccess.Payslip payslip = m_Model.GeneratePaySlip(annualSalary, superRate);
                label1.Text = fName + " " + lName + "," + dates + "," + payslip.grossMonthly + "," + payslip.incomeTax + "," + payslip.netMonthly + "," + payslip.superRate;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Invalid input format, please use correct format");
            }
        }
    }
}
