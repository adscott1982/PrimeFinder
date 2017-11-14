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

        private static bool IsPrime(this ulong n)
        {
            if (n % 2 == 0) return false; // divisible by two?

            var maxDivisor = n / 2; 

            for (ulong i = 3; i <= maxDivisor; i++)
            {
                if (n % i == 0) return false;
            }

            return true;
        }
    }
}
