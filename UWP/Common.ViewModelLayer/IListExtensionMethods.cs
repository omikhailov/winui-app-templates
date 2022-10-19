using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModelLayer
{
    public static class IListExtensionMethods
    {
        public static T RemoveSelected<T>(this IList<T> collection, T selectedItem) where T : class
        {
            T result = null;

            var index = collection.IndexOf(selectedItem);

            if (index >= 0)
            {
                collection.RemoveAt(index);

                index = Math.Min(index, collection.Count - 1);

                if (index >= 0) result = collection[index];
            }

            return result;
        }

        public static T RemoveSelected<T>(this IList<T> collection, T selectedItem, Action<T> removingDelegate) where T : class
        {
            T result = null;

            removingDelegate(selectedItem);

            var index = collection.IndexOf(selectedItem);

            if (index >= 0)
            {
                collection.RemoveAt(index);

                index = Math.Min(index, collection.Count - 1);

                if (index >= 0) result = collection[index];
            }

            return result;
        }

        public static T RemoveSelected<T>(this IList<T> collection, T selectedItem, Func<T, bool> removingDelegate) where T : class
        {
            T result = null;

            if (removingDelegate(selectedItem))
            {
                var index = collection.IndexOf(selectedItem);

                if (index >= 0)
                {
                    collection.RemoveAt(index);

                    index = Math.Min(index, collection.Count - 1);

                    if (index >= 0) result = collection[index];
                }
            }

            return result;
        }

        public static async Task<T> RemoveSelectedAsync<T>(this IList<T> collection, T selectedItem, Func<T, Task> removingDelegate) where T : class
        {
            T result = null;

            await removingDelegate(selectedItem);

            var index = collection.IndexOf(selectedItem);

            if (index >= 0)
            {
                collection.RemoveAt(index);

                index = Math.Min(index, collection.Count - 1);

                if (index >= 0) result = collection[index];
            }

            return result;
        }

        public static async Task<T> RemoveSelectedAsync<T>(this IList<T> collection, T selectedItem, Func<T, Task<bool>> removingDelegate) where T : class
        {
            T result = null;

            if (await removingDelegate(selectedItem))
            {
                var index = collection.IndexOf(selectedItem);

                if (index >= 0)
                {
                    collection.RemoveAt(index);

                    index = Math.Min(index, collection.Count - 1);

                    if (index >= 0) result = collection[index];
                }
            }

            return result;
        }
    }
}
