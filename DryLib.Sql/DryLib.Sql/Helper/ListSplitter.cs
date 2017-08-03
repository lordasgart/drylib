using System;
using System.Collections.Generic;

namespace DryLib.Sql.Helper
{
    public class ListSplitter
    {
        public static IEnumerable<List<T>> SplitList<T>(List<T> listElements)
        {
            return SplitList(listElements, Settings.MaxParamCountForSingleInOperation);
        }

        private static IEnumerable<List<T>> SplitList<T>(List<T> listElements, int newListSize)
        {
            if (newListSize < 1) throw new ArgumentOutOfRangeException(nameof(listElements));

            for (var i = 0; i < listElements.Count; i += newListSize)
            {
                yield return listElements.GetRange(i, Math.Min(newListSize, listElements.Count - i));
            }
        }
    }
}