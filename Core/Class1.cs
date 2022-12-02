namespace Core;

public static class Helpers
{
    public static U Pipe<T, U>(this T input, Func<T, U> @operator)
    {
        return @operator(input);
    }
}