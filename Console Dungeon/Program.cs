using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Dungeon
{
    class Program
    {
        static void Main(string[] args)
        {
            int cI2 = 0;
            char c2 = new();
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    c2 = (char)cI2;
                    Console.Write($"[{c2}, Cod:{cI2} ]");
                    cI2++;
                }
                Console.WriteLine();
            }
        }

    }
}