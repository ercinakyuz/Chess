using System;
using System.Collections.Generic;

namespace ChessGame.Common
{
    public static class CloneExtensions
    {
        public static Dictionary<TKey, TValue> Clone<TKey, TValue>(this Dictionary<TKey, TValue> source) where TValue : ICloneable
            where TKey : ICloneable
        {
            var destination = new Dictionary<TKey, TValue>(source.Count, source.Comparer);
            foreach (var entry in source)
            {
                destination.Add((TKey)entry.Key.Clone(), (TValue)entry.Value?.Clone());
            }
            return destination;
        }
    }
}
