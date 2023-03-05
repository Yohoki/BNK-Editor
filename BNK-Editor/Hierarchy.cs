using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNK_Editor
{
    public class Hierarchy
    {
        // Converted Bits and Bobs.
        private byte        _eHircType;     // we really only care about 0x02(Sound)... just store the rest...
        private int         _dwSectionSize; // Needed to find next Hierachy
        private int         _ulID;          // Display this? Really only used internally...
        private int         _sourceID;      // Which WEM file is edited
        private int         _PropsCount;    // # of Properties to edit
        private List<int>   _propsType;     // Type of property (0x00=Vol, 0x3A=Loops)
        private List<float> _propsValues;   // Value of property. (Most are Float, Loops is INT)
        private byte[]      _buffer;

        //Raw Data Bits.
        private byte[] _raw_eHircType = new byte[1];     // decide if important
        private byte[] _raw_dwSectionSize = new byte[4]; // find next header, edit if size changed
        private byte[] _raw_PrePropsData;                // dontgivadamn area
        private byte[] _raw_PropsData;                   // gen new on save.
        private byte[] _raw_PostPropsData;               // dontgiveadamn area 2

        public void GenNewHirc(byte[] rawData)
        {
            MemoryStream data = new (rawData);
            data.ReadExactly(_raw_eHircType, 0, 0);
            data.ReadExactly(_raw_dwSectionSize, 0, 4);

            //check first byte for type
            //if !02, save size and load all other info into prepropsdata
            //if 02, store all data properly

            _raw_PostPropsData = new byte[Utils.ReadHexAsInt32(_raw_dwSectionSize)];
            data.ReadExactly(_raw_PostPropsData, 0, Utils.ReadHexAsInt32(_raw_dwSectionSize));

            data.Dispose();
        }

        public byte[] WriteAllData()
        {
            // combine eHircType, dwSectionSize, PreProp, Prop and PostProp.
            // Return
            return new byte[0];
        }

        public void AddNewProp() { }

        public void RemoveProp() { }

        public void ListProps() { }
    }
}
