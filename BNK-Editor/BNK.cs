using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNK_Editor
{
    public class BNK
    {
        private Stream _data;
        public List<EncodedData> _headerList = new();

        public void LoadData(Stream data)
        {
            _data = data;

            int currentChunkIndex = 0;
            for (int fileOffset = 0; fileOffset < _data.Length;)
            {
                SplitHeads(ref fileOffset, currentChunkIndex);
                currentChunkIndex++;
            }

            _data.Dispose();
        }

        private void SplitHeads(ref int fileOffset, int currentChunkIndex)
        {
            EncodedData DataChunk = new();

            byte[] buffer;
            
            //Get chunksize
            buffer = new byte[4];
            _data.Position = fileOffset + 4;
            _data.ReadExactly(buffer, 0, 4);
            int currentChunkSize =
                BinaryPrimitives.ReverseEndianness(
                    int.Parse(Convert.ToHexString(buffer),
                              System.Globalization.NumberStyles.HexNumber));

            //parse data per chunk
            buffer = new byte[currentChunkSize + 8];
            _data.Position = fileOffset;
            _data.ReadExactly(buffer, 0, currentChunkSize + 8);

            //Apply parsed data and setup for next parse.
            DataChunk.Load(buffer, currentChunkIndex);
            _headerList.Add(DataChunk);
            fileOffset += currentChunkSize + 8;
        }

        public void Print()
        {
            foreach (var header in _headerList) { header.Print(); }
        }
    }
}

