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
            //d.Reverse();
            return BitConverter.ToSingle(d);
        }
    }
}
