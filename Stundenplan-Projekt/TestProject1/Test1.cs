using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stundenplan_Projekt;
using System.Collections.Generic;

namespace TestProject1
{
    [TestClass]
    public sealed class Test1
    {
        // Test 1 Randzeiten prüfen
        [TestMethod]
        public void TestMethod1()
        {
            Stundenplan plan = new Stundenplan();
            Planungseinstellungen settings = new Planungseinstellungen(10, 0, 0);
            int punkte = plan.BerechnePunkte("3A", "Montag", 0, settings);
            Assert.AreEqual(10, punkte, "Randstunden müssen bestraft werden!");
        }


        // Test 2 Zwischenstunden
        [TestMethod]
        public void Test_Zwischenstunden_Werden_Bestraft()
        {
            Stundenplan plan = new Stundenplan();
            Planungseinstellungen settings = new Planungseinstellungen(0, 50, 0);

            plan.Eintraege.Add(new StundenplanEintrag { Klasse = "3A", Tag = "Montag", PeriodeIndex = 0 });

            int punkte = plan.BerechnePunkte("3A", "Montag", 2, settings);

            Assert.AreEqual(50, punkte, "Eine Stunde ohne Anschluss (Lücke) muss bestraft werden.");
        }


        // Test 3 Lehrer Doppelbelegung prüfen
        [TestMethod]
        public void Test_Lehrer_Kollision_Erkannt()
        {
            Stundenplan plan = new Stundenplan();
            plan.Eintraege.Add(new StundenplanEintrag
            {
                Lehrer = "Herr Meier",
                Tag = "Montag",
                PeriodeIndex = 0
            });

            bool istBesetzt = plan.IstLehrerBesetzt("Herr Meier", "Montag", 0);

            Assert.IsTrue(istBesetzt, "Lehrer sollte als besetzt markiert sein.");
        }

        // Test 4 Raum Doppelbelegung prüfen
        [TestMethod]
        public void Test_Raum_Kollision_Erkannt()
        {
            Stundenplan plan = new Stundenplan();

            plan.Eintraege.Add(new StundenplanEintrag
            {
                Raum = "101",
                Tag = "Dienstag",
                PeriodeIndex = 3
            });

            bool istBesetzt = plan.IstRaumBesetzt("101", "Dienstag", 3);

            Assert.IsTrue(istBesetzt, "Raum sollte als besetzt gelten.");
        }

        // Test 5 Verfügbarkeit Lehrer
        [TestMethod]
        public void Test_Lehrer_Verfuegbarkeit()
        {
            Lehrperson lehrer = new Lehrperson("TestLehrer", "TST");
            lehrer.AddVerfuegbarkeit("Montag", "08:00", "12:00");

            bool gehtDas = lehrer.IstVerfuegbar("Montag", "09:00", "09:45");
            Assert.IsTrue(gehtDas, "Lehrer sollte Montag früh Zeit haben.");

            bool gehtDasNicht = lehrer.IstVerfuegbar("Montag", "14:00", "14:45");
            Assert.IsFalse(gehtDasNicht, "Lehrer sollte Nachmittags keine Zeit haben.");
        }
    }
}
