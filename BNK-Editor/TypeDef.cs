using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNK_Editor
{
    public class TypeDef
    {
        public static string getHircType(byte b)
        {
            switch (b)
            {
                case 0x02:
                    return "Sound";
                case 0x03:
                    return "Action";
                case 0x04:
                    return "Event";
                case 0x07:
                    return "Actor-Mixer";
                case 0x0E:
                    return "Attenuation";
                case 0x14:
                    return "Envelope";

            }
            return b + " - Unknown type";
        }
        public static string getPropType(int b)
        {
            switch (b)
            {
                case 0:
                    return "00 - Volume";
                case 58:
                    return "3A - Loop";
            }
            return b + "Unknown Property";
        }
    }
}
