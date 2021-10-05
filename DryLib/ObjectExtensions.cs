using Newtonsoft.Json;
using System;

namespace DryLib
{
    public static class ObjectExtensions
    {
        public static T Dump<T>(this T o)
        {
            if (o is string)
            {
                Console.WriteLine(o);                
            }
            else
            {
                var s = JsonConvert.SerializeObject(o, Formatting.Indented);
                Console.WriteLine(s);
            }
            return o;
        }
    }
}