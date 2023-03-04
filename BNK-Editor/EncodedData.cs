using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BNK_Editor
{
    public class EncodedData
    {
        private List<byte> _RawData = new();
        private List<byte> _Header = new();
        private List<byte> _ChunkSize = new();
        private List<byte> _DataSection = new();

        private int _Index;

        public void Load(List<byte> rawData, int index)
        {
            _RawData = rawData;
            _Index = index;

            int i = 0;
            foreach (byte b in _RawData)
            {
                if (i <= 3) { _Header.Add(b); }
                if (i >= 4 && i <= 7) { _ChunkSize.Add(b); }
                if (i >= 8) { _DataSection.Add(b); }
                i++;
            }
        }

        public string Print()
        {
            return " Header = " + ReadHeaderAsText()
              + "\nHeader Size = " + _Header.Count
              + "\n Chunk Size = " + ReadChunkSizeAsInt32()
              + "\n  Data Size = " + ReadFullChunkSizeWithHeader();
        }

        public int ReadFullChunkSizeWithHeader()
        {
            return _Header.Count + _ChunkSize.Count + _DataSection.Count;
        }

        public string ReadHeaderAsText()
        {
            string Header = "";
            foreach (byte b in _Header) { Header += Convert.ToChar(b); }
            return Header;
        }

        public int ReadChunkSizeAsInt32()
        {
            string size = Convert.ToHexString(_ChunkSize.ToArray());
            return BinaryPrimitives.ReverseEndianness(int.Parse(size, System.Globalization.NumberStyles.HexNumber));
        }

    }
}
