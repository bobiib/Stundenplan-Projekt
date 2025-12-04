using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stundenplan_Projekt;
using System.Collections.Generic;

namespace TestProject1
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {

            // Test 1 Randzeiten prüfen
            Stundenplan plan = new Stundenplan();
            Planungseinstellungen settings = new Planungseinstellungen(10, 0, 0);
            int punkte = plan.BerechnePunkte("3A", "Montag", 0, settings);
            Assert.AreEqual(10, punkte, "Randstunden müssen bestraft werden!");


            // Test 2
        }
    }
}
