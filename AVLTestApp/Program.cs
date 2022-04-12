using System;
using Lab2;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AVLTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            AVL<int, object> avl = new AVL<int, object>();
            SortedDictionary<int, int> sdict = new SortedDictionary<int, int>();

            var randomIntegers = GenerateRandomIntegers(10000);

            Stopwatch stopwatch = Stopwatch.StartNew();
            foreach (var item in randomIntegers)
                avl.Insert(item, 0);
            stopwatch.Stop();

            Console.WriteLine("AVL.Insert: " + stopwatch.ElapsedTicks);

            stopwatch.Restart();
            foreach (var item in randomIntegers)
                sdict.Add(item, 0);
            stopwatch.Stop();

            Console.WriteLine("SDICT.Add: " + stopwatch.ElapsedTicks);

            Console.WriteLine();

            stopwatch.Restart();
            for (int i = 5000; i <= 7000; i++) {
                avl.Delete(randomIntegers[i]);
            }
            stopwatch.Stop();

            Console.WriteLine("AVL.Remove: " + stopwatch.ElapsedTicks);

            stopwatch.Restart();
            for (int i = 5000; i <= 7000; i++) {
                sdict.Remove(randomIntegers[i]);
            }
            stopwatch.Stop();

            Console.WriteLine("SDICT.Remove: " + stopwatch.ElapsedTicks);

            Console.ReadKey();
        }

        private static List<int> GenerateRandomIntegers(int count)
        {
            List<int> randomIntegers = new List<int>();
            Random random = new Random(DateTime.Now.Millisecond);

            while (randomIntegers.Count < count) {
                int newRandomInteger = random.Next(-2 * count, 2 * count);

                if (!randomIntegers.Contains(newRandomInteger))
                    randomIntegers.Add(newRandomInteger);
            }

            Console.WriteLine("Integers generated");

            return randomIntegers;
        }
    }
}
