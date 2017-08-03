using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DryLib.Sql.Helper;

namespace DryLib.Sql
{
    public class Arguments
    {
        public static string Where<T>(string column, IEnumerable<T> enumerable)
        {
            return GetInCmdText("WHERE", column, enumerable);
        }

        public static string And<T>(string column, IEnumerable<T> enumerable)
        {
            return GetInCmdText("AND", column, enumerable);
        }

        public static string GetInCmdText<T>(string keyword, string column, IEnumerable<T> enumerable)
        {
            var cmdText = new StringBuilder();

            cmdText.AppendLine();
            cmdText.Append(keyword);
            cmdText.AppendLine();

            var inOperator = In(column, enumerable);

            cmdText.Append(inOperator);
            cmdText.AppendLine();

            return cmdText.ToString();
        }

        public static string In<T>(string column, IEnumerable<T> enumerable)
        {
            var ilist = enumerable as IList<T> ?? enumerable.ToList();
            if (!ilist.Any()) throw new ArgumentOutOfRangeException(nameof(enumerable));

            var useSingleQuotes = SingleQuoteChecker.ShouldUseSingleQuotes<T>();

            var enumerableOfLists = ListSplitter.SplitList(ilist.ToList());

            var currentListCounter = 0;

            var cmdText = new StringBuilder();

            cmdText.AppendLine();
            cmdText.Append("(");
            cmdText.AppendLine();

            var lists = enumerableOfLists as IList<List<T>> ?? enumerableOfLists.ToList();

            foreach (var list in lists)
            {
                cmdText.Append(column);
                cmdText.Append(" IN (");

                if (useSingleQuotes)
                {
                    var joinedList = string.Join("','", list);
                    cmdText.Append("'");
                    cmdText.Append(joinedList);
                    cmdText.Append("'");
                }
                else
                {
                    var joinedList = string.Join(",", list);
                    cmdText.Append(joinedList);
                }

                cmdText.Append(")");

                currentListCounter++;

                if (currentListCounter == lists.Count) continue;

                cmdText.AppendLine();
                cmdText.Append("OR");
                cmdText.AppendLine();
            }

            cmdText.AppendLine();
            cmdText.Append(")");
            cmdText.AppendLine();

            return cmdText.ToString();
        }
    }
}
