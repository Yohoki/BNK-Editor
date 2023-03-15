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
        public byte        eHircType;          // we really only care about 0x02(Sound)... just store the rest...
        public int         dwSectionSize;      // Needed to find next Hierachy
      //public int         ulID;               // Display this? Really only used internally...
        public int         sourceID;           // Which WEM file is edited
        public int         PropsCount = 0;     // # of Properties to edit
        public List<int>   propsType = new();  // Type of property (0x00=Vol, 0x3A=Loops)
        public List<float> propsValues = new();// Value of property. (Most are Float, Loops is INT)
        //public int         propValueLoops = 0; // Loops needs to use INT.
        public int         uPluginID; //!!-----// IMPORTANT Check for 0x65 and add +4 offset to props list if true.

        public int         _index;

        //Raw Data Bits.
        private byte[] _raw_eHircType = new byte[1];     // decide if important
        private byte[] _raw_dwSectionSize = new byte[4]; // find next header, edit if size changed
        private byte[] _raw_PrePropsData = new byte[32]; // dontgivadamn area
        private byte[]? _raw_PropsData = new byte[0];    // gen new on save.
        private byte[]? _raw_PostPropsData= new byte[0];              // dontgiveadamn area 2

        public void GenNewHirc(byte[] rawData, int index)
        {
            _index = index;
            MemoryStream data = new(rawData);
            
            data.ReadExactly(_raw_eHircType, 0, 1);
            eHircType = _raw_eHircType[0];

            data.ReadExactly(_raw_dwSectionSize, 0, 4);
            dwSectionSize = Utils.ReadHexAsInt32(_raw_dwSectionSize);

            //check Codec info
            data.Position += 6;
            uPluginID = data.ReadByte();
            data.Position -= 7;

            //catch small packets.
            if (Utils.ReadHexAsInt32(_raw_dwSectionSize) >= 36) { data.ReadExactly(_raw_PrePropsData, 0, 32); }
            else
            {
                _raw_PrePropsData = new byte[data.Length - data.Position];
                data.ReadExactly(_raw_PrePropsData, 0, Convert.ToInt32(data.Length - data.Position));
            }
            //MessageBox.Show(BitConverter.ToString(_raw_PrePropsData),"DEBUG PREPROPS ID " + _index);
            //check first byte for type
            if (eHircType == 0x02)
            {
                GetSourceID();

                if (uPluginID == 101) data.Position += 4;

                //if 02, store all data properly
                byte[] _buffer = new byte[4];
                PropsCount = data.ReadByte();
                //data.Position++;

                for (int i = 0; i < PropsCount; i++)
                    { propsType.Add(data.ReadByte()); }

                if (propsType.Count > 0)
                {
                    foreach (int i in propsType)
                    {
                        data.ReadExactly(_buffer, 0, 4);
                        if (i == 0x3A) { propsValues.Add((float)Utils.ReadHexAsInt32(_buffer)); }
                        else propsValues.Add(Utils.ReadHexAsFloat(_buffer));
                    }
                }

            }


            //load all other info into prepropsdata
            if (Utils.ReadHexAsInt32(_raw_dwSectionSize) >= 36)
            {
                _raw_PostPropsData = new byte[data.Length - data.Position];

                data.ReadExactly(_raw_PostPropsData, 0, _raw_PostPropsData.Length);
            }
            //MessageBox.Show(BitConverter.ToString(_raw_PostPropsData), "DEBUG POSTPROPS ID " + _index);

            data.Dispose();
        }

        public byte[] MakeHirc()
        {
            GenPropsData();
            byte[] RawData = _raw_eHircType;
            _raw_dwSectionSize = Utils.ToHex(_raw_PrePropsData.Length +_raw_PostPropsData.Length + _raw_PropsData.Length);

            /*MessageBox.Show(BitConverter.ToString(_raw_eHircType
                    .Concat(_raw_dwSectionSize)
                    .Concat(_raw_PrePropsData)
                    .Concat(_raw_PropsData)
                    .Concat(_raw_PostPropsData).ToArray()).Replace("-",""), "DEBUG: ChunkID " + _index);
            */
            return _raw_eHircType
                    .Concat(_raw_dwSectionSize)
                    .Concat(_raw_PrePropsData)
                    .Concat(_raw_PropsData)
                    .Concat(_raw_PostPropsData).ToArray();
        }

        public void ReadAllBytes()
        {
            string s = "\nType: " + TypeDef.getHircType(eHircType)
                 + "\nSize: " + Utils.ReadHexAsInt32(_raw_dwSectionSize) + "Bytes"
                 + "\nPrePropData: " + Convert.ToHexString(_raw_PrePropsData);
            if (Utils.ReadHexAsInt32(_raw_dwSectionSize) >= 36)
                { s += "\nPostPropData: " + Convert.ToHexString(_raw_PostPropsData); }
            if (_raw_eHircType[0] == 2)
            {
                s += "\nFileName: " + sourceID + ".wem\n\n";
                foreach (string P in ListProps()) { s += P; }
            }
            MessageBox.Show(s);
        }

        public void GetSourceID()
        {
            MemoryStream data = new(_raw_PrePropsData);
            data.Position = 9;
            byte[] buffer = new byte[4];
            data.ReadExactly(buffer, 0, 4);
            sourceID = Utils.ReadHexAsInt32(buffer);
            data.Dispose();
        }

        public List<string> GetPropsList()
        {
            List<string> props = new();
            if (PropsCount == 0) props.Add("No Properties Available");
            foreach (int prop in propsType)
            { props.Add( TypeDef.getPropType(prop) ); }
            return props;
        }

        public override string ToString()
        {
            return TypeDef.getHircType(eHircType) + $"[{_index}]" + (eHircType == 2 ? $" - {sourceID}.wem" : "");
        }

        public void AddNewProp() { }

        public void RemoveProp() { }

        public void GenPropsData() 
        {
            if (eHircType != 0x02) return;
            if (PropsCount == 0) { _raw_PropsData = new byte[1]; return; }
            byte[] propsT = new byte[PropsCount];
            byte[] propsV = new byte[0];

            for (int i=0; i < PropsCount; i++)
            {
                propsT[i] = Convert.ToByte(propsType[i]);
                if (propsType[i] == 0x3A || propsType[i] == 0x07) // Loops need to be int32, not float
                { propsV = propsV.Concat(Utils.ToHex(Convert.ToInt32(propsValues[i]))).ToArray(); }
                else propsV = propsV.Concat(Utils.ToHex(propsValues[i])).ToArray();
            }
            _raw_PropsData = new byte[1];
            _raw_PropsData[0] = Utils.ToHexByte(PropsCount);
            _raw_PropsData = _raw_PropsData.Concat(propsT).Concat(propsV).ToArray();

            //_raw_PropsData = 
            //      Utils.ToHex(PropsCount)
            //    + Utils.ToHex;
            //_raw_PropsData = new byte[0];
        }

        public string[] ListProps()
        {
            string[] props = new string[PropsCount];

            if (PropsCount == 0) props = new string[] { "\n\nNo Properties stored" };
            for (int i = 0; i < PropsCount; i++)
            {
                props[i] = $"\n\nProp Type:{TypeDef.getPropType(propsType[i])}\nProp Value:{propsValues[i]}";
            }

            //get source id//

            return props;
        }
    }
}
