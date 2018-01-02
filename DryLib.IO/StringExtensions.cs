namespace DryLib.IO
{
    public static class StringExtensions
    {
        public static string ToCurrentSystemPath(this string p)
        {
            var path = new Path(p);

            return path.Get();
        }
    }
}
