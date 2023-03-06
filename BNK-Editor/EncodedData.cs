﻿using System;
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

        public int NumReleasableHircItem;
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
            Hierarchy hierarchy;
            int chunksize;
            byte[] buffer = new byte[4];
            data = new MemoryStream(_DataSection);

            //Get total number of Hierachy in list
            data.ReadExactly(buffer, 0, 4);
            int ListNumber = Utils.ReadHexAsInt32(buffer);

            for (int index = 0; index < ListNumber; index++)
            {
                hierarchy = new();
                
                //get Size
                data.Position++;
                data.ReadExactly(buffer, 0, 4);
                chunksize = Utils.ReadHexAsInt32(buffer) + 5;
                
                // Reset to beginning of chunk
                data.Position -= 5;

                // send data to hierarchy class
                buffer = new byte[chunksize];
                data.ReadExactly(buffer, 0, chunksize);
                hierarchy.GenNewHirc(buffer);

                // load hierarchy into list
                HIRCList.Add(hierarchy);
                buffer = new byte[4];
            }

            data.Dispose();


        }

        public void Print()
        {
            if (Utils.ReadHexAsText(_Header) != "HIRC") { return; }
            foreach (var item in HIRCList)
            {
                item.ReadAllBytes();
            }
        }

        public int ReadFullChunkSizeWithHeader()
        {
            return _Header.Length + _ChunkSize.Length + _DataSection.Length;
        }

    }
}
