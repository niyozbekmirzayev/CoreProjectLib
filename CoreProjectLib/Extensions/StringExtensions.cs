
namespace CoreProjectLib.Extensions
{
    public static class StringExtensions
    {
        public static string GetDigits(this string input) 
        {
            return new string(input.Where(c => char.IsDigit(c)).ToArray());
        }
    }
}
