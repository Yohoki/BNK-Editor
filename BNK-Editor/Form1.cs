using System.Buffers.Binary;
using System.Text;

namespace BNK_Editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        public bool DBG_DebugMode = true; //DEBUG
        public string DBG_FilePath = "D:\\BNK_Mod\\"; //DEBUG
        public string DBG_FileName = "spells_perk_shadowblink_akb.bnk"; //DEBUG
        public string Filepath;
        public BNK BankFile = new();

        private void Btn_Open_Click(object sender, EventArgs e)
        {
            //DEBUG Start
            if (DBG_DebugMode) { Filepath = DBG_FilePath + DBG_FileName; }
            //DEBUG End
            BankFile.LoadData(File.ReadAllBytes(Filepath));

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
            BankFile.LoadData();
            foreach (var i in BankFile._headerList)
            {
                MessageBox.Show(i.Print());
            }
        }
    }
}