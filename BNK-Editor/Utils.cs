using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNK_Editor
{
    public class Utils
    {

        public static string ReadHexAsText(byte[] d)
        {
            string s = "";
            foreach (byte b in d) { s += Convert.ToChar(b); }
            return s;
        }

        public static int ReadHexAsInt32(byte[] d)
        {
            string s = Convert.ToHexString(d);
            return BinaryPrimitives.ReverseEndianness(int.Parse(s, System.Globalization.NumberStyles.HexNumber));
        }

        public static float ReadHexAsFloat(byte[] d)
        {
            return BitConverter.ToSingle(d);
        }

        public static byte[] ToHex(float d)
        {
            return BitConverter.GetBytes(d);
        }

        public static byte[] ToHex(int d)
        {
            return BitConverter.GetBytes(d);
        }

        public static byte[] ToHex(string d)
        {
            return Encoding.UTF8.GetBytes(d);
        }

        public static byte ToHexByte(int d)
        {
            return (byte)(16 * (d / 10) + (d % 10));
        }
    }
}
