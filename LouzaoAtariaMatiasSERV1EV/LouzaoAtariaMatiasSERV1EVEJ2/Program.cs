//Matias Louzao Ataria 54505421K
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace LouzaoAtariaMatiasSERV1EVEJ2
{
    class Program
    {
        public static Random r = new Random();
        public delegate void MyDelegate();
        static void Main(string[] args)
        {
            ExceptionControl(RandomInfo);
            FileInfo f = new FileInfo(Environment.GetEnvironmentVariable("SYSTEMROOT")+"\\");
            Console.WriteLine("Archivos en {0}:", Environment.GetEnvironmentVariable("SYSTEMROOT"));
            foreach (FileInfo file in f.Directory.GetFiles())
            {
                Console.WriteLine(file.Name);
            }
            //Array.ForEach(f.Directory.GetFiles(),Console.WriteLine);
            Console.ReadKey();
        }

        public static void RandomInfo()
        {
            Process[] pSistem = Process.GetProcesses();
            //Process p = Process.GetProcessById(4220);
            Process p = Process.GetProcessById(r.Next(pSistem.Length));
            string str = "";
            str += string.Format("Process name: {0}\n\r",p.ProcessName);
            foreach (ProcessModule module in p.Modules)
            {
                str += string.Format("{0}\r\n",module.ModuleName);
            }
            Console.WriteLine(str);
            using (StreamWriter writer = new StreamWriter(Environment.GetEnvironmentVariable("USERPROFILE")+"\\examen.txt"))
            {
                writer.Write(str);
            }
        }

        public static void ExceptionControl(MyDelegate deleg)
        {
            try
            {
                deleg();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Panic Error!!");
            }
        }
    }
}
