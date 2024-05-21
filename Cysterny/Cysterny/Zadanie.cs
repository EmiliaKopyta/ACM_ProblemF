using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cysterny
{
    internal class Zadanie
    {
        private Bryła[] bryły;
        private double zadanaIlośćWody;
        private double optymalnaIlośćWody;
        private bool overflow;

        public bool Wczytaj(TextReader tr)
        {
            string linia = tr.ReadLine();
            if (linia == null)
                throw new ArgumentException("Brak danych o ilości cystern w zestawie.");

            int ilośćCystern = int.Parse(linia);

            bryły = new Bryła[ilośćCystern];

            for (int i = 0; i < ilośćCystern; i++)
            {
                string[] daneBryły = tr.ReadLine().Split();
                string rodzajBryły = daneBryły[0]; //założenie do zmiany: pierwszy element w linii z danymi cysterny to jest typ bryły

                switch (rodzajBryły)
                {
                    case "p":
                        bryły[i] = new Prostopadłościan();
                        break;
                    case "s":
                        bryły[i] = new Stożek();
                        break;
                    case "o":
                        bryły[i] = new StożekOdwrócony();
                        break;
                    case "k":
                        bryły[i] = new Kula();
                        break;
                    case "w":
                        bryły[i] = new Walec();
                        break;
                    default:
                        throw new ArgumentException($"Nieznany rodzaj bryły: {rodzajBryły}");
                }

                bryły[i].Wczytaj(daneBryły);
            }

            zadanaIlośćWody = double.Parse(tr.ReadLine());
            return true;
        }

        //Rozwiązanie zadania też do cysterny.dll, aplikacja konsolowa będzie zwyczajnie wywoływać te funkcje po wczytaniu odpowiednio dll (ten główny jest załączony więc już i tak będzie załadowany)


        internal double granicaGórna()
        {
            double hmax = 0;
            for (int i = 0; i < bryły.Length; i++)
            {
                hmax = Math.Max(hmax, bryły[i].maxH());
            }
            return hmax;
        }

        internal double ObjNaWys(double wysokość)
        {
            double v = 0;
            for (int i = 0; i < bryły.Length; i++)
            {
                v += bryły[i].ObjNaWys(wysokość);
            }
            return v;
        }

        internal bool Overflow(double hmax)
        {
            double vmax = ObjNaWys(hmax);
            if (vmax <= zadanaIlośćWody)
            {
                return true;
            }
            return false;  
        }

        internal void Rozwiąż()
        {
            double hmin = 0;
            double hmax = granicaGórna();

            overflow = Overflow(hmax);
            if (overflow)
                return;
            
            while (hmax - hmin > 0.001)
            {
                double hpol = (hmin + hmax) / 2;
                double vpol = ObjNaWys(hpol);

                if (vpol >= zadanaIlośćWody)
                {
                    hmax = hpol;
                }
                else
                {
                    hmin = hpol;
                }
            }
            optymalnaIlośćWody = Math.Round(hmin, 2);
        }

        internal string Komunikat()
        {
            if (overflow)
                return"OVERFLOW";

            return $"{optymalnaIlośćWody}";
        }
    }
}
