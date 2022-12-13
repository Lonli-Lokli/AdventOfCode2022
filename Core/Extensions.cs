using System.Collections;

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
        for (int i = 0; i < input.Count; i++)
        {
            action(input[i], i);
        }
    }

    public static IEnumerable<T> Tap<T>(this IEnumerable<T> source, Action<T> action)
    {
        if (source is null)
        {
            throw new InvalidOperationException("Cannot tap on null source");
        }

        if (action is null)
        {
            throw new InvalidOperationException("Cannot tap with null action");
        }

        return source.Select(item =>
        {
            action(item);
            return item;
        });
    }

    public static string JoinAll(this IEnumerable<string> source, string? delimeter = null) =>
        string.Join(delimeter ?? Environment.NewLine, source);

    public static IEnumerable<string> JoinAll(this IEnumerable<IEnumerable<string>> source, string? delimeter = null) =>
        source.Select(items => string.Join(delimeter ?? string.Empty, items));

    public static IEnumerable<T> RemoveFirst<T>(this IEnumerable<T> list, Func<T, bool> predicate) => new RemoveFirstEnumerable<T>(list, predicate);

    public static IEnumerable<T> RemoveFirst<T>(this IEnumerable<T> list, T item) => RemoveFirst(list, x => Object.Equals(x, item));

    private class RemoveFirstEnumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _source;
        private readonly Func<T, bool> _predicate;

        public RemoveFirstEnumerable(IEnumerable<T> source, Func<T, bool> predicate)
        {
            _source = source;
            _predicate = predicate;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new RemoveFirstEnumerator(_source, _predicate);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new RemoveFirstEnumerator(_source, _predicate);
        }

        private class RemoveFirstEnumerator : IEnumerator<T>
        {
            private readonly IEnumerator<T> _enumerator;
            private readonly Func<T, bool> _predicate;
            bool _alreadyRemoved = false;

            public RemoveFirstEnumerator(IEnumerable<T> source, Func<T, bool> predicate)
            {
                _enumerator = source.Where(WherePredicate).GetEnumerator();
                _predicate = predicate;
            }

            bool WherePredicate(T current) => _alreadyRemoved || !(_alreadyRemoved = _predicate(current));

            public T Current => _enumerator.Current;

            public bool MoveNext() => _enumerator.MoveNext();


            public void Reset()
            {
                _enumerator.Reset();
            }

            public void Dispose()
            {
                _enumerator.Dispose();
            }

            object IEnumerator.Current => _enumerator.Current;
        }
    }
}