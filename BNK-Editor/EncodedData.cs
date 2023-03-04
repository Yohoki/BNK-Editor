using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNK_Editor
{
    public class EncodedData
    {
        private byte[] _RawData;
        private byte[] _Header;
        private byte[] _ChunkSize;
        private byte[] _DataSection;

        private int _Index;

        public void Load(byte[] rawData, int index)
        {
            _RawData = rawData;
            _Index = index;

            for (int i = 0; i < _RawData.Length; i++)
            {
                if (i < 4) { _Header[i] += _RawData[i]; }
                if (i < 8) { _ChunkSize[i] += _RawData[i]; }
                else _DataSection[i] += _RawData[i];
            }
        }

        public string ReadHeader()
        {
            string Header = "";
            foreach (byte b in _Header) { Header += b; }
            return Header;
        }

        public int ReadChunksize()
        {
            string size = Convert.ToHexString(_ChunkSize);
            return BinaryPrimitives.ReverseEndianness(int.Parse(size, System.Globalization.NumberStyles.HexNumber));
        }

        public byte[] WriteChunksize(int newSize)
        {
            newSize = BinaryPrimitives.ReverseEndianness(newSize);
            return BitConverter.GetBytes(newSize);
        }

    }
}
