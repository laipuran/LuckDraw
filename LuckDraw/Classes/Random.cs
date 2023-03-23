using PuranLai.Algorithms;

namespace LuckDraw
{
    class RandomNumber
    {
        public static string GetRandomResult(int count, int max, char separator)
        {
            Rand rand = new(count, max);
            int[] array = rand.GetInts();
            return string.Join(separator, array);
        }

        public static string GetRandomResult(int count, int max, char separator, string prefix)
        {
            Rand rand = new(count, max);
            int[] array = rand.GetInts();
            return prefix + separator + string.Join(separator, array);
        }
    }
}