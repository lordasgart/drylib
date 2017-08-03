using System;

namespace DryLib.Sql.Helper
{
    public class SingleQuoteChecker
    {
        public static bool ShouldUseSingleQuotes<T>()
        {
            var useSingleQuotes = true;

            // All other types should be quoted by now

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    useSingleQuotes = false;
                    break;
            }

            return useSingleQuotes;
        }
    }
}