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
        private int         _PropsCount = 0;// # of Properties to edit
        private List<int>   _propsType = new();// Type of property (0x00=Vol, 0x3A=Loops)
        private List<float> _propsValues = new();// Value of property. (Most are Float, Loops is INT)
        //private byte[]      _buffer;

        //Raw Data Bits.
        private byte[] _raw_eHircType = new byte[1];     // decide if important
        private byte[] _raw_dwSectionSize = new byte[4]; // find next header, edit if size changed
        private byte[] _raw_PrePropsData = new byte[32]; // dontgivadamn area
        private byte[] _raw_PropsData;                   // gen new on save.
        private byte[] _raw_PostPropsData;               // dontgiveadamn area 2

        public void GenNewHirc(byte[] rawData)
        {
            MemoryStream data = new(rawData);
            data.ReadExactly(_raw_eHircType, 0, 1);
            data.ReadExactly(_raw_dwSectionSize, 0, 4);
            //catch small packets.
            if (Utils.ReadHexAsInt32(_raw_dwSectionSize) >= 36) { data.ReadExactly(_raw_PrePropsData, 0, 32); }
            else { data.ReadExactly(_raw_PrePropsData, 0, Convert.ToInt32(data.Length - data.Position) - 5); }

            //check first byte for type
            if (_raw_eHircType[0] == 2)
            {
                //if 02, store all data properly
                byte[] _buffer = new byte[4];
                _PropsCount = data.ReadByte();
                //data.Position++;

                for (int i = 0; i < _PropsCount; i++)
                    { _propsType.Add(data.ReadByte()); }

                if (_propsType.Count > 0)
                {
                    foreach (int i in _propsType)
                    {
                        data.ReadExactly(_buffer, 0, 4);
                        _propsValues.Add(Utils.ReadHexAsFloat(_buffer));
                    }
                }

            }


            //load all other info into prepropsdata
            if (Utils.ReadHexAsInt32(_raw_dwSectionSize) >= 36)
            {
                _raw_PostPropsData = new byte[data.Length - data.Position];

                data.ReadExactly(_raw_PostPropsData, 0, _raw_PostPropsData.Length);
            }
                
            data.Dispose();
        }

        public byte[] WriteAllData()
        {
            // combine eHircType, dwSectionSize, PreProp, Prop and PostProp.
            // Return
            return new byte[0];
        }

        public void ReadAllBytes()
        {
            string s = "\nType: " + Convert.ToHexString(_raw_eHircType)
                 + "\nSize: " + Convert.ToHexString(_raw_dwSectionSize)
                 + "\nPrePropData: " + Convert.ToHexString(_raw_PrePropsData);
            if (Utils.ReadHexAsInt32(_raw_dwSectionSize) >= 36)
                { s += "\nPostPropData: " + Convert.ToHexString(_raw_PostPropsData); }
            if (_raw_eHircType[0] == 2)
                { s += "\n\n # of Properties: " + _PropsCount; }
            MessageBox.Show(s);
        }

        public void AddNewProp() { }

        public void RemoveProp() { }

        public void ListProps() { }
    }
}
