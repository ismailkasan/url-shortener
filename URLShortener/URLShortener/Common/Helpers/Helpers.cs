namespace URLShortener.Common
{
    public static class Helpers
    {
        /// <summary>
        /// Generate uniqe new hashes that have length of size with  Math.Pow(62, 6) possibilities.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static string CreateUniqeStrig(int size)
        {
            /* This custon and random algorith creat uniqe strings.
             * So each character can only be from this range "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".
             * if size is 6, it will create with this formula ==> Math.Pow(62, 6) equals 56.800.235.584. We have only 56.800.235.584 different possibilities to get uniqe segment with this method.
             */
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[size];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }

        /// <summary>
        /// Cretae length of size guid  with given size.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string CreateGuid(int size)
        {
            /* Guid is 128 bit number. And represented in hexadecimal base.
             * So each character can only be from this range [0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F] 
             * So, if size is 6, it will create with this formula ==> Math.Pow(16, 6) equals 16.777.216. We have only 16.777.216 different possibilities to get uniqe segment with this method.
             */
            string newGuid = Guid.NewGuid().ToString("N")[..size];
            return newGuid;
        }
    }
}
