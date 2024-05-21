using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Cysterny
{

    public class Cysterna : Attribute
    {

    }
    abstract internal class Bryła
    {
        abstract internal double ObjNaWys(double zadana); //objętość na zadanej wysokości
        abstract internal void Wczytaj(string[] dane); //dane z inputu
        abstract public double maxH(); //wysokość maksymalna do funkcji rozwiąż
    }

    class Prostopadłościan : Bryła
    {
        double poziom;
        double wysokość;
        double szerokość;
        double głębokość;

        internal override void Wczytaj(string[] dane)
        {
            poziom = double.Parse(dane[1]);
            wysokość = double.Parse(dane[2]);
            szerokość = double.Parse(dane[3]);
            głębokość = double.Parse(dane[4]);
        }

        internal override double ObjNaWys(double poziomWody)
        {
            if (poziomWody < poziom)
                return 0;
            else if (poziomWody >= poziom + wysokość)
                return szerokość * głębokość * wysokość;
            else
                return szerokość * głębokość * (poziomWody - poziom);
        }

        public override double maxH() { return poziom + wysokość; }
    }

    class Kula : Bryła
    {
        double poziom;
        double promień;
        internal override void Wczytaj(string[] dane)
        {
            poziom = double.Parse(dane[1]);
            promień = double.Parse(dane[2]);
        }

        internal override double ObjNaWys(double poziomWody)
        {
            if (poziomWody < poziom)
                return 0;
            else
                return (4.0 / 3.0) * Math.PI * Math.Pow(promień, 3) * (1 - Math.Pow(1 - (poziomWody - poziom) / promień, 3));
        }

        public override double maxH() { return poziom + promień; }
    }

    class Stożek : Bryła
    {
        double poziom;
        double promień;
        double wysokość;
        internal override void Wczytaj(string[] dane)
        {
            poziom = double.Parse(dane[1]);
            promień = double.Parse(dane[2]);
            wysokość = double.Parse(dane[3]);
        }

        internal override double ObjNaWys(double poziomWody)
        {
            if (poziomWody < poziom)
                return 0;
            else
            {
                if (poziomWody >= poziom + wysokość)
                    return (1.0 / 3.0) * Math.PI * Math.Pow(promień, 2) * wysokość;
                else
                    return (1.0 / 3.0) * Math.PI * Math.Pow(promień, 2) * (poziomWody - poziom);
            }
        }

        public override double maxH() { return poziom + wysokość; }
    }

    class StożekOdwrócony : Bryła
    {
        double poziom;
        double promień;
        double wysokość;
        internal override void Wczytaj(string[] dane)
        {
            poziom = double.Parse(dane[1]);
            promień = double.Parse(dane[2]);
            wysokość = double.Parse(dane[3]);
        }

        internal override double ObjNaWys(double poziomWody)
        {
            if (poziomWody < poziom)
                return 0;
            else
            {
                if (poziomWody >= poziom + wysokość)
                    return (1.0 / 3.0) * Math.PI * Math.Pow(promień, 2) * wysokość;
                else
                    return (1.0 / 3.0) * Math.PI * Math.Pow(promień, 2) * (wysokość - (poziomWody - poziom));
            }
        }

        public override double maxH() { return poziom + wysokość; }
    }

    class Walec : Bryła
    {
        double poziom;
        double promień;
        double wysokość;
        internal override void Wczytaj(string[] dane)
        {
            poziom = double.Parse(dane[1]);
            promień = double.Parse(dane[2]);
            wysokość = double.Parse(dane[3]);
        }

        internal override double ObjNaWys(double poziomWody)
        {
            if (poziomWody < poziom)
                return 0;
            else if (poziomWody >= poziom + wysokość)
                return Math.PI * Math.Pow(promień, 2) * wysokość;
            else
                return Math.PI * Math.Pow(promień, 2) * (poziomWody - poziom);
        }

        public override double maxH() { return poziom + wysokość; }
    }

}
