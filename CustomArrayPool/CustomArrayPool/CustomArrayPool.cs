using System.Collections.Generic;
using System.Threading;

namespace CustomArrayPool
{
    public class CustomArrayPool<T>
    {
        #region Private fields

        private readonly int _capacity;
        private readonly List<T[]> _arrayPool;
        private int _curentFreeIndex = -1;
        private int _curentUnUsedIndex = -1;

        #endregion

        #region Constructor

        public CustomArrayPool(int capacity, int arrayLength)
        {
            _capacity = capacity - 1;
            _arrayPool = new List<T[]>(capacity);

            for (int i = 0; i <= capacity; i++)
            {
                _arrayPool.Add(new T[arrayLength]);
            }
        }

        #endregion

        #region Public methods

        public T[] Rent()
        {
            Interlocked.CompareExchange(ref _curentFreeIndex, -1, _capacity);

            return _arrayPool[Interlocked.Add(ref _curentFreeIndex, 1)];
        }

        public void UnRent(T[] array)
        {
            Interlocked.CompareExchange(ref _curentUnUsedIndex, -1, _capacity);

            _arrayPool[Interlocked.Add(ref _curentUnUsedIndex, 1)] = array;
        }

        #endregion
    }
}
