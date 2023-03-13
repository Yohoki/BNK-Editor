using System.Buffers.Binary;
using System.DirectoryServices.ActiveDirectory;
using System.Text;

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
            //DEBUG Start
            if (DBG_DebugMode) { Filepath = DBG_FilePath + DBG_FileName; }
            if (!DBG_DebugMode)
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {

                    openFileDialog.Filter = "WWise SoundBank files (*.bnk)|*.bnk|All files (*.*)|*.*";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Filepath = openFileDialog.FileName;
                    }
                    else return;
                }
            }

            //DEBUG End
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
            NumPropValue.Enabled = true;

            Hierarchy Hirc = (Hierarchy)Cmb_HeirList.Items[Cmb_HeirList.SelectedIndex];
            NumPropValue.Value = Convert.ToDecimal(Hirc._propsValues[Cmb_PropList.SelectedIndex]);
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {

        }

        private void Btn_AddNew_Click(object sender, EventArgs e)
        {

        }

        private void Btn_Remove_Click(object sender, EventArgs e)
        {

        }

        private void DBG_Btn_Debug_Click(object sender, EventArgs e)
        {
            foreach (var i in BankFile._headerList)
            {
                i.Print();
            }
        }

        private void Cmb_HeirList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cmb_PropList.Items.Clear();
            Cmb_PropList.Enabled = false;
            LoadPropsCMBList();
        }
    }
}