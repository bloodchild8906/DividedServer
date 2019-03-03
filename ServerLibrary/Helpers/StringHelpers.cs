using System.Text;

namespace ServerLibrary.Helpers
{
    public static class StringHelpers
    {
        public static byte[] ToByteArray(this string source)
        {
            return Encoding.UTF8.GetBytes(source);
        }
    }
}
