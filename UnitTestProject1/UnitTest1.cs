using DataAccess;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccess.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        [TestMethod()]
        public void GeneratePaySlipTest()
        {
            Model model = new Model();
            model.Initialise();
            Payslip payslipExpected = new Payslip();
            Payslip payslipActual;

            //Expected data hardcode
            payslipExpected.grossMonthly = 10000;
            payslipExpected.incomeTax = 2696;
            payslipExpected.netMonthly = 7304;
            payslipExpected.superRate = 1000;

            payslipActual = model.GeneratePaySlip(120000, 10);
            Assert.AreEqual(payslipExpected, payslipActual);

        }
    }
}
