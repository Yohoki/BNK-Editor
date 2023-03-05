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
        internal Stream data;
        private byte[] _RawData;
        private byte[] _Header = new byte[4];
        private byte[] _ChunkSize = new byte[4];
        private byte[] _DataSection;
        public  List<Hierarchy>  HIRCList = new();

        private int _Index;

        public void Load(byte[] rawData, int index)
        {
            _RawData = rawData;
            _Index = index;
            data = new MemoryStream(_RawData);

            data.ReadExactly(_Header, 0, 4);
            data.ReadExactly(_ChunkSize, 0, 4);
            _DataSection = new byte[Utils.ReadHexAsInt32(_ChunkSize)];
            data.ReadExactly(_DataSection, 0, Utils.ReadHexAsInt32(_ChunkSize));

            data.Dispose();

            if (Utils.ReadHexAsText(_Header) == "HIRC")
            {
                GenHierarchyList();
            }
        }

        public void GenHierarchyList()
        {
            Hierarchy hierarchy = new();
            byte[] buffer = new byte[4];
            data = new MemoryStream(_DataSection);

            //Get total number of Hierachy in list
            data.ReadExactly(buffer, 0, 4);
            int ListNumber = Utils.ReadHexAsInt32(buffer);

            for (int index = 0; index < ListNumber; index++)
            {
                // get size
                // reset position to pre-size
                // send data to hierarchy class
                // load hierarchy into list
            }

            data.Dispose();


        }

        public string Print()
        {
            return " Header = " + Utils.ReadHexAsText(_Header)
              + "\nHeader Size = " + _Header.Length
              + "\n Chunk Size = " + Utils.ReadHexAsInt32(_ChunkSize)
              + "\n  Data Size = " + ReadFullChunkSizeWithHeader()
              + "\nIndex Position = " + _Index;
        }

        public int ReadFullChunkSizeWithHeader()
        {
            return _Header.Length + _ChunkSize.Length + _DataSection.Length;
        }

    }
}
