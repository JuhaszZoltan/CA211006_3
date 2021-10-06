using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA211006_3
{
    interface IVersenyenLeveoCucc
    {
        string GetInfo { get; }
    }

    class Hal : IVersenyenLeveoCucc
    {
        public string Fajta { get; set; }
        public float Suly { get; set; }
        public (int Min, int Max) UszasiSav { get; set; }

        public string GetInfo =>
            $"{Fajta} {Suly} Kg [{UszasiSav.Min}-{UszasiSav.Max}] m\n";

    }
    class Horgasz : IVersenyenLeveoCucc
    {
        public string Nev { get; set; }
        public DateTime Szul { get; set; }
        public List<Hal> KifogottHalak { get; set; } = new List<Hal>();

        public string GetInfo
        {
            get
            {
                string info = $"{Nev} ({DateTime.Now.Year - Szul.Year} éves)\n";
                foreach (var hal in KifogottHalak)
                {
                    info += $"\t{hal.GetInfo}";
                }
                return info;
            }
        }
    }


    class Program
    {
        static List<Hal> to = new List<Hal>()
        {
            new Hal() { Fajta = "pisztráng", Suly = 12.5F, UszasiSav = (20, 100)},
            new Hal() { Fajta = "harcsa", Suly = 22.6F, UszasiSav = (10, 110)},
            new Hal() { Fajta = "keszeg", Suly = 55.4F, UszasiSav = (40, 70)},
            new Hal() { Fajta = "ponty", Suly = 31.1F, UszasiSav = (50, 80)},
            new Hal() { Fajta = "lazac", Suly = 43.4F, UszasiSav = (0, 120)},
        };
        static List<Horgasz> versenyzok = new List<Horgasz>()
        {
            new Horgasz() { Nev = "Erzsébet", Szul = new DateTime(2003, 03, 11), },
            new Horgasz() { Nev = "István", Szul = new DateTime(1971, 06, 07), },
            new Horgasz() { Nev = "Ferenc", Szul = new DateTime(1973, 05, 22), },
        };


        static Random rnd = new Random();
        static void Main()
        {
            versenyzok[0].KifogottHalak.Add(to[0]);
            to.Remove(versenyzok[0].KifogottHalak[0]);
            versenyzok[2].KifogottHalak.Add(to[3]);
            to.Remove(versenyzok[2].KifogottHalak[0]);
            versenyzok[0].KifogottHalak.Add(to[0]);
            to.Remove(versenyzok[0].KifogottHalak[1]);

            var logList = new List<IVersenyenLeveoCucc>();
            logList.AddRange(to);
            logList.AddRange(versenyzok);

            logList = logList.OrderBy(x => rnd.Next()).ToList();

            foreach (var cucc in logList)
            {
                Console.Write(cucc.GetInfo);
            }

            Console.WriteLine("done!");
            Console.ReadKey();
        }
    }
}
