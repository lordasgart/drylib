using System;

namespace DryLib.Sql
{
    public class Settings
    {
        private static int _maxParamCountForSingleInOperation = 1000;

        public static int MaxParamCountForSingleInOperation
        {
            get => _maxParamCountForSingleInOperation;

            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(MaxParamCountForSingleInOperation));

                _maxParamCountForSingleInOperation = value;
            }
        }
    }
}
