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
        private byte[] _data;
        private List<EncodedData> _headerList = new();
        private List<Byte[]> _hierList = new();
        private int _hierIndex = 0;

        public void LoadData(byte[] data) => _data = data;

        public void SplitHeads()
        {
            
        }

        public string WriteData()
        {
            string header = "";
            for (int i = 0; i < 4; i++)
            {
                header += Convert.ToChar(_data[i]);
            }

            string size = Convert.ToHexString(_data, 4, 4);
            if (header == "BKHD")
            {
                int chunksize = BinaryPrimitives.ReverseEndianness(int.Parse(size, System.Globalization.NumberStyles.HexNumber));
                return "HEADER =   " + header + "\nCHUNK SIZE =" + chunksize;
            }
            else
            {
                return "Failed to parse BNK Header";
            }
        }

        private void CreateStructures()
        {

        }
    }
}
