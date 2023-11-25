namespace Chess.Core;

public static class Helper
{
    // https://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-overriding-gethashcode
    public static int GenerateHashCode<T>(this T item)
    {
        unchecked
        {
            var hash = 17;
            var mult = 23;

            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var value = property.GetValue(item);
                hash = hash * mult + value.GetHashCode();
            }

            return hash;
        }
    }

    public static bool IsBetweenInclusive(this int number, int low, int high) => low <= number && number <= high;
}