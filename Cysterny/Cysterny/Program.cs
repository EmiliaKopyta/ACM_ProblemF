namespace Cysterny
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (StreamReader sr = new StreamReader("Cysterny.in"))
            {
                int liczbaZadan = int.Parse(sr.ReadLine());
                if (liczbaZadan <= 0)
                    throw new ArgumentException("Nieprawidłowa liczba zestawów danych.");

                for (int i = 0; i < liczbaZadan; i++)
                {
                    Zadanie z = new Zadanie();
                    if (z.Wczytaj(sr))
                    {
                        z.Rozwiąż();
                        Console.WriteLine($"{z.Komunikat()}");
                    }
                }
            }
        }
    }
}