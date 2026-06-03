using System;
using System.Collections.Generic;
using System.Linq;

public static class ListExtensions
{
    public static T Random<T>(this IList<T> list)
    {
        if (list.Count == 0)
        {
            throw new IndexOutOfRangeException("List needs at least one entry");
        }

        if (list.Count == 1)
        {
            return list[0];
        }

        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    public static T Random<T>(this IReadOnlyCollection<T> collection, Randomizer randomizer)
    {
        if (collection == null || collection.Count == 0)
            throw new System.InvalidOperationException("Collection is empty");

        int index = randomizer.GetRandom(0, collection.Count);

        if (collection is IList<T> list)
            return list[index];

        return collection.ElementAt(index);
    }

    public static T Random<T>(this IReadOnlyCollection<T> collection)
    {
        if (collection == null || collection.Count == 0)
            throw new System.InvalidOperationException("Collection is empty");

        int index = UnityEngine.Random.Range(0, collection.Count);

        if (collection is IList<T> list)
            return list[index];

        return collection.ElementAt(index);
    }

    public static IList<T> Shuffle<T>(this IList<T> list, Randomizer randomizer)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = randomizer.GetRandom(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
        return list;
    }

    public static IList<T> Shuffle<T>(this IList<T> list, int seed)
    {
        var r = new Randomizer(seed);
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = r.GetRandom(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
        return list;
    }

}
