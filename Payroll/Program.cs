﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Payroll
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Program holds the responsibility of creating Model
            //It is passed on as a reference to Payslip(view) class
            DataAccess.Model model = new DataAccess.Model();
            try
            {
                model.Initialise();
            }
            catch(System.Exception ex)
            {
                MessageBox.Show("Error occured " + ex.ToString());
            }
            Application.Run(new Payslip(ref model));
        }
    }
}
