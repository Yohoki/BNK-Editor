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
                case 0x01:
                    return "State / Settings";
                case 0x02:
                    return "Sound";
                case 0x03:
                    return "Action";
                case 0x04:
                    return "Event";
                case 0x05:
                    return "Random/Sequence Container";
                case 0x06:
                    return "Switch Container";
                case 0x07:
                    return "Actor-Mixer";
                case 0x08:
                    return "Audio Bus";
                case 0x09:
                    return "Blend Container";
                case 0x0A:
                    return "Music Segment";
                case 0x0B:
                    return "Music Track";
                case 0x0C:
                    return "Music Switch Container";
                case 0x0D:
                    return "Music Playlist Container";
                case 0x0E:
                    return "Attenuation";
                case 0x0F:
                    return "Dialogue Event";
                case 0x10:
                    return "Motion Bus";
                case 0x11:
                    return "FX Custom";
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
                    return "Volume";
                case 0x02:
                    return "Pitch";
                case 0x03:
                    return "Low-Pass Filter";
                case 0x05:
                    return "Playback Priority";
                case 0x06:
                    return "Playback Priority: Offset";
                case 0x07:
                    return "Loop"; //Maybe?
                case 0x08:
                    return "Motion Volume Offset";
                case 0x0B:
                    return "2D: Panner X-coordinate";
                case 0x0C:
                    return "2D: Panner Y-coordinate"; //Maybe flipped?
                case 0x0D:
                    return "Center %";
                case 0x12:
                    return "User-Defined Aux Sends: Bus #0 Volume";
                case 0x13:
                    return "User-Defined Aux Sends: Bus #1 Volume";
                case 0x14:
                    return "User-Defined Aux Sends: Bus #2 Volume";
                case 0x15:
                    return "User-Defined Aux Sends: Bus #3 Volume";
                case 0x16:
                    return "Game-Defined Aux Sends: Volume";
                case 0x17:
                    return "Output Bus: Volume";
                case 0x18:
                    return "Output Bus: Low-Pass Filter";
                case 0x3A:
                    return "Loop";
            }
            return b + " - Unknown Property";
        }
    }
}
