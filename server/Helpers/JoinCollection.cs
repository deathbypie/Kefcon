﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kefcon.Helpers
{
    // from https://blog.oneunicorn.com/2017/09/25/many-to-many-relationships-in-ef-core-2-0-part-3-hiding-as-icollection/
    public class JoinCollection<T, TJoin> : ICollection<T>
    {
        private readonly ICollection<TJoin> _collection;
        private readonly Func<TJoin, T> _selector;
        private readonly Func<T, TJoin> _creator;

        public JoinCollection(
            ICollection<TJoin> collection,
            Func<TJoin, T> selector,
            Func<T, TJoin> creator)
        {
            _collection = collection;
            _selector = selector;
            _creator = creator;
        }

        public IEnumerator<T> GetEnumerator()
            => _collection.Select(e => _selector(e)).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public void Add(T item)
            => _collection.Add(_creator(item));

        public void Clear()
            => _collection.Clear();

        public bool Contains(T item)
            => _collection.Any(e => Equals(_selector(e), item));

        public void CopyTo(T[] array, int arrayIndex)
            => this.ToList().CopyTo(array, arrayIndex);

        public bool Remove(T item)
            => _collection.Remove(
                _collection.FirstOrDefault(e => Equals(_selector(e), item)));

        public int Count
            => _collection.Count;

        public bool IsReadOnly
            => _collection.IsReadOnly;
    }
}
