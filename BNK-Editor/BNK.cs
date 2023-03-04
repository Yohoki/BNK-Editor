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
        private List<byte> _data = new();
        public List<EncodedData> _headerList = new();
        private List<List<byte>> _hierList = new();
        private int _hierIndex = 0;

        public void LoadData(byte[] data) => _data = data.ToList<byte>();

        public void SplitHeads()
        {

        }

        public void WriteData()
        {
            //string header = "";
            EncodedData Temp = new();
            /*for (int i = 0; i < 4; i++)
            {
                header += Convert.ToChar(_data[i]);
            }*/

            string size;

            List<byte> currentChunk = new();
            EncodedData DataChunk;
            int currentChunkSize;
            int currentChunkIndex = 0;
            
            for (int fileOffset = 0; fileOffset < _data.Count;)
            {
                //Init new parse
                DataChunk = new();
                size = Convert.ToHexString(_data.ToArray(), fileOffset + 4, 4);
                currentChunk.Clear();
                currentChunkSize = BinaryPrimitives.ReverseEndianness(int.Parse(size, System.Globalization.NumberStyles.HexNumber));

                //parse data per chunk
                for (int chunkOffset = 0; chunkOffset < currentChunkSize + 8; chunkOffset++)
                {
                    currentChunk.Add(_data[fileOffset + chunkOffset]);
                }

                //Apply parsed data and setup for next parse.
                DataChunk.Load(currentChunk, currentChunkIndex);
                _headerList.Add(DataChunk);
                fileOffset += currentChunkSize + 8;
                currentChunkIndex++;
            }
        }

        private void CreateStructures()
        {

        }
    }
}
