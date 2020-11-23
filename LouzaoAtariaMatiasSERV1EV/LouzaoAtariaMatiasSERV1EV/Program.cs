//Matias Louzao Ataria 54505421K
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LouzaoAtariaMatiasSERV1EV
{
    class Program
    {
        public static int NOnions = 0;
        public static int NPotatoes = 0;
        public static int NOmelette = 0;
        public static bool running = true;//cocinando, mientras no haya 10 tortillas.
        public static Object l = new object();

        static void Main(string[] args)
        {
            Thread tPotato= new Thread(() => Ingredient("Onion", ref NOnions));
            Thread tOnion= new Thread(() => Ingredient("Potato", ref NPotatoes));
            Thread tOmelette= new Thread(Omelette);
            tPotato.Start();
            tOnion.Start();
            tOmelette.Start();
            tOmelette.Join();
            Console.WriteLine("The End, Pon Wins. No onion rules!");
            Console.ReadKey();
        }

        public static void Ingredient(string ingredient,ref int cont)
        {
            while (running)
            {
                lock (l)
                {
                    if (running)
                    {
                        if (NOmelette < 10)
                        {
                            cont++;
                            if (cont >= 5)
                            {
                                lock (l)
                                {
                                    Monitor.Pulse(l);
                                }
                            }
                            Console.WriteLine("Ingredient: {0,7} {1,3}",ingredient,cont);
                        }
                    }
                }
            }
        }

        public static void Omelette()
        {
            while (running)
            {
                lock (l)
                {
                    if (running)
                    {
                        if (NOmelette < 10)
                        {
                            if (NOnions >= 5 && NPotatoes >= 5)
                            {
                                NOmelette++;
                                NOnions -= 5;
                                NPotatoes -= 5;
                                Console.WriteLine("Omelet: {0,3}, Onion: {1,3}, Potato: {2,3}",NOmelette,NOnions,NPotatoes);
                            }
                            else
                            {
                                Console.WriteLine("Waiting...");
                                lock (l)
                                {
                                    Monitor.Wait(l);
                                }
                            }
                        }
                        else 
                        {
                            running = false;
                        }
                    }
                }
            }
        }
    }
}
