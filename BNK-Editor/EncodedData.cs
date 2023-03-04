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
            /*
            for (int i = 0; i < _RawData.Count; i++)
            {
                if (i < 4)
                {
                    _Header.Add(_RawData.;// += _RawData[i];
                }
                if (i < 8) 
                { 
                    _ChunkSize.Add()// += _RawData[i];
                }
                else _DataSection.Add()// += _RawData[i];
            }*/
        }

        public string Print()
        {
            string headerName = "";
            foreach (byte headChar in _Header)
            {
                headerName += Convert.ToChar(headChar);
            }

                return " Header = " + headerName
                  +"\nHeader Size = " + _Header.Count
                  +"\n Chunk Size = " + _ChunkSize.Count
                  +"\n  Data Size = " + (_Header.Count + _ChunkSize.Count + _DataSection.Count);
        }

        public string ReadHeader()
        {
            string Header = "";
            foreach (byte b in _Header) { Header += b; }
            return Header;
        }

        public int ReadChunksize()
        {
            string size = Convert.ToHexString(_ChunkSize.ToArray());
            return BinaryPrimitives.ReverseEndianness(int.Parse(size, System.Globalization.NumberStyles.HexNumber));
        }

        public byte[] WriteChunksize(int newSize)
        {
            newSize = BinaryPrimitives.ReverseEndianness(newSize);
            return BitConverter.GetBytes(newSize);
        }

    }
}
