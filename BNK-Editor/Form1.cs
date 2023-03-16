using System.Buffers.Binary;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Text;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BNK_Editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        public bool DBG_DebugMode = false; //DEBUG
        public string DBG_FilePath = "D:\\BNK_Mod\\"; //DEBUG
        public string DBG_FileName = "spells_perk_shadowblink_akb.bnk"; //DEBUG
        public Stream StreamBankFile;
        public string Filepath;
        public BNK BankFile = new();

        private void Btn_Open_Click(object sender, EventArgs e)
        {
            BankFile = new();

            if (DBG_DebugMode) { Filepath = DBG_FilePath + DBG_FileName; } //DEBUG

            if (!DBG_DebugMode)
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {

                    openFileDialog.Filter = "WWise SoundBank File (*.bnk, *.bnk_)|*.bnk; *.bnk_|BNK Backup (*.bnk_)|*.bnk_|All files (*.*)|*.*";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Filepath = openFileDialog.FileName;
                    }
                    else return;
                }
            }

            StreamBankFile = File.OpenRead(Filepath);
            BankFile.LoadData(StreamBankFile);
            LoadHircCMBList();
            LoadPropsCMBList();

        }

        public void LoadHircCMBList()
        {
            Cmb_HeirList.Items.Clear();

            foreach (var item in BankFile.GetList()) { Cmb_HeirList.Items.Add(item); }
            Cmb_HeirList.SelectedIndex= 0;
            LoadPropsCMBList();
        }

        public void LoadPropsCMBList()
        {
            Cmb_PropList.Enabled= false;
            NumPropValue.Enabled= false;
            Cmb_PropList.Items.Clear();

            foreach (Hierarchy H in Cmb_HeirList.Items)
            {
                if (H._index == Cmb_HeirList.SelectedIndex)
                {
                    foreach (var prop in H.GetPropsList()) Cmb_PropList.Items.Add(prop);
                }
            }
            Cmb_PropList.SelectedIndex = 0;
            if (Cmb_PropList.Items[0] == "No Properties Available") return;
            Cmb_PropList.Enabled = true;
            LoadPropValue();
        }

        public void LoadPropValue()
        {
            if (Cmb_PropList.Items[0] == "No Properties Available") return;
            NumPropValue.Enabled = true;

            Hierarchy Hirc = (Hierarchy)Cmb_HeirList.Items[Cmb_HeirList.SelectedIndex];
            NumPropValue.Value = Convert.ToDecimal(Hirc.propsValues[Cmb_PropList.SelectedIndex]);
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "WWise SoundBank File (*.bnk)|*.bnk|BNK Backup (*.bnk_)|*.bnk_|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    //File.Create(saveFileDialog.FileName);
                    foreach (EncodedData D in BankFile._headerList)
                    {
                        myStream.Write(D.MakeDataChunks());
                    }
                    // Code to write the stream goes here.
                    myStream.Close();
                    MessageBox.Show("Save Completed");
                }
            }

            /*//...
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "WWise SoundBank|*.bnk";
            saveFileDialog.Title = "Save WWise SoundBank";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                try
                {
                    using var writer = new BinaryWriter(File.OpenWrite(saveFileDialog.FileName));

                    if (File.Exists(saveFileDialog.FileName))
                    {
                        File.Delete(saveFileDialog.FileName);
                    }

                    File.Create(saveFileDialog.FileName);
                    foreach (EncodedData D in BankFile._headerList)
                    {
                        writer.Write(D.MakeDataChunks());
                    }
                }
                catch { MessageBox.Show("Error while saving file."); return; }
                MessageBox.Show("File saved!");
            }*/
        }


        private void Btn_AddNew_Click(object sender, EventArgs e)
        {
            Hierarchy Hirc = (Hierarchy)Cmb_HeirList.Items[Cmb_HeirList.SelectedIndex];
            if (Hirc.eHircType != 0x02) return;
            foreach (byte b in TypeDef.returnPropTypes())
            {
                if (!Hirc.propsType.Contains(b))
                {
                    Hirc.propsType.Add(b);
                    Hirc.propsValues.Add(0);
                }
                Hirc.PropsCount = Hirc.propsType.Count;
            }
            LoadPropsCMBList();
        }

        private void Btn_Remove_Click(object sender, EventArgs e)
        {
            Hierarchy Hirc = (Hierarchy)Cmb_HeirList.Items[Cmb_HeirList.SelectedIndex];
            if (Hirc.eHircType != 0x02) return;

            try
            {
                Hirc.propsValues[Cmb_PropList.SelectedIndex] = (float)0.00;
                for (int i = 0; i < BankFile._headerList.Count; i++)
                {
                    for (int h = 0; h < BankFile._headerList[i].HIRCList.Count; h++)
                    {
                        if (BankFile._headerList[i].HIRCList[h]._index == Hirc._index) BankFile._headerList[i].HIRCList[h] = Hirc;
                    }
                }
            }
            catch { }
            LoadPropValue();
        }

        //DEBUG Start
        private void DBG_Btn_Debug_Click(object sender, EventArgs e)
        {
            foreach (EncodedData D in BankFile._headerList)
            {
                D.MakeDataChunks();
            }
        } //DEBUG End

        private void Cmb_HeirList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cmb_PropList.Items.Clear();
            Cmb_PropList.Enabled = false;
            LoadPropsCMBList();
        }

        private void Cmb_PropList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPropValue();
        }

        private void NumPropValue_ValueChanged(object sender, EventArgs e)
        {
            Hierarchy Hirc = (Hierarchy)Cmb_HeirList.Items[Cmb_HeirList.SelectedIndex];
            if (Hirc.eHircType != 0x02) return;

            try
            {
                Hirc.propsValues[Cmb_PropList.SelectedIndex] = (float)NumPropValue.Value;
                for (int i = 0; i < BankFile._headerList.Count; i++)
                {
                    for (int h = 0; h < BankFile._headerList[i].HIRCList.Count; h++)
                    {
                        if (BankFile._headerList[i].HIRCList[h]._index == Hirc._index) BankFile._headerList[i].HIRCList[h] = Hirc;
                    }
                }
            }
            catch { }
        }
    }
}