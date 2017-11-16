using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeMaths
{
    public static class Tools
    {
        public static ulong FindNthPrime(uint n)
        {
            if (n == 1) return 2;

            var currentPrimeIndex = 1;
            ulong checkInteger = 2;

            while(currentPrimeIndex < n)
            {
                checkInteger++;
                if (checkInteger.IsPrime()) currentPrimeIndex++;
            }

            return checkInteger;
        }

        public static ulong FindNthPrimeParallel(uint n, int maxThreads = 4)
        {
            var foundPrimes = new List<ulong>();
            foundPrimes.Add(2);
            var tasks = new Task[maxThreads];

            ulong number = 3;

            while (foundPrimes.Count < n)
            {
                for (var i = 0; i < maxThreads; i++)
                {
                    var myNumber = number + (ulong)i;
                    tasks[i] = Task.Run(() =>
                    {
                        if (myNumber.IsPrime()) foundPrimes.Add(myNumber);
                    });
                }

                Task.WaitAll(tasks);
                number += (ulong)maxThreads;
            }

            var orderedPrimes = foundPrimes.OrderBy(p => p);
            return orderedPrimes.ElementAt((int)n - 1);
        }

        private static bool IsPrime(this ulong number)
        {
            //if (n % 2 == 0) return false; // divisible by two?

            //var maxDivisor = n / 2; 

            //for (ulong i = 3; i <= maxDivisor; i++)
            //{
            //    if (n % i == 0) return false;
            //}

            //return true;

            if (number < 2) return false;
            if (number % 2 == 0) return (number == 2);
            ulong root = (ulong)Math.Sqrt(number);
            for (ulong i = 3; i <= root; i += 2)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }
}
