using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace DataAccess
{
    public struct TaxRates
    {
        public int lower;
        public int upper;
        public int baseRate;
        public float additional;
    }

    public struct Payslip
    {
        public double grossMonthly;
        public double incomeTax;
        public double netMonthly;
        public double superRate;
    }
    public class Model
    {
        private SQLiteConnection m_connection = new SQLiteConnection("Data Source=../../../DB/Payroll.db");
        
        public Model()
        {
            
        }
        public void Initialise()
        {
            try
            {
                m_connection.Open();
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }

        private TaxRates GetTaxRates(int annualSalary)
        {
            String sqlCommand = "select * from Tax where Lower <= " + annualSalary + " and Upper >= " + annualSalary;
            SQLiteCommand command = new SQLiteCommand(sqlCommand, m_connection);
            SQLiteDataReader reader = command.ExecuteReader();
            //assuming only one row is retrieved
            TaxRates taxRate = new TaxRates();
            try
            {
                reader.Read();
                taxRate.lower = Convert.ToInt32(reader["Lower"]);
                taxRate.upper = Convert.ToInt32(reader["Upper"]);
                taxRate.baseRate = Convert.ToInt32(reader["Base"]);
                taxRate.additional = Convert.ToSingle(reader["Additional"]);
            }
            catch(Exception ex)
            { 
              return taxRate; //will return empty tax rate in case of database error
            }
            return taxRate;
        }

        public Payslip GeneratePaySlip(int anuualSalaray, int superRate)
        {
            TaxRates taxRates = GetTaxRates(anuualSalaray);
            Payslip payslip = new Payslip();

            payslip.grossMonthly = Math.Round((float)anuualSalaray / 12f, MidpointRounding.AwayFromZero);
            payslip.incomeTax = Math.Round((taxRates.baseRate + ((anuualSalaray - taxRates.lower - 1) * taxRates.additional)) / 12, MidpointRounding.AwayFromZero);
            payslip.netMonthly = payslip.grossMonthly - payslip.incomeTax;
            payslip.superRate = Math.Round(payslip.grossMonthly * superRate / 100, MidpointRounding.AwayFromZero);

            return payslip;
        }
    }
}
