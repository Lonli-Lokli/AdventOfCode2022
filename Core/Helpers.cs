namespace Core;

public static class Helpers
{
    public static U Pipe<T, U>(this T input, Func<T, U> @operator)
    {
        return @operator(input);
    }

    public static string SanitizeInput(string input) => input.Trim();
    
    public static List<T> IntersectAll<T>(IEnumerable<IEnumerable<T>> lists)
    {
        HashSet<T>? hashSet = null;
        foreach (var list in lists)
        {
            if (hashSet == null)
                hashSet = new HashSet<T>(list);
            else
                hashSet.IntersectWith(list);
        }
        return hashSet == null ? new List<T>() : hashSet.ToList();
    }
    
    public static void ForEachOf<T>(this List<T> input, Action<T, int> action)
    {
        for(int i = 0; i < input.Count; i++)
        {
            action(input[i], i);
        }
    }
    
}