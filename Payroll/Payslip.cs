using System;
using System.Windows.Forms;
using DataAccess;

namespace Payroll
{
    //This is the view class, it takes user input and provides to model and model returns with the formatted data to display
    public partial class Payslip : Form
    {
        private Model m_Model;

        //reference to Model passed by main application
        public Payslip(ref Model model)
        {
            InitializeComponent();
            m_Model = model;            
        }        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                String text = textBox1.Text.ToString();
                String[] values = text.Split(',');
                String fName = values[0];
                String lName = values[1];
                double annualSalary = Convert.ToDouble(values[2]);
                double superRate = double.Parse(values[3].Substring(0, values[3].Length - 1));
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
