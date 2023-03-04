namespace BNK_Editor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Btn_Open = new System.Windows.Forms.Button();
            this.Btn_Save = new System.Windows.Forms.Button();
            this.Cmb_HeirList = new System.Windows.Forms.ComboBox();
            this.Cmb_PropList = new System.Windows.Forms.ComboBox();
            this.Btn_AddNew = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.Lbl_PropName = new System.Windows.Forms.Label();
            this.Btn_Remove = new System.Windows.Forms.Button();
            this.DBG_Btn_Debug = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // Btn_Open
            // 
            this.Btn_Open.Location = new System.Drawing.Point(12, 12);
            this.Btn_Open.Name = "Btn_Open";
            this.Btn_Open.Size = new System.Drawing.Size(94, 29);
            this.Btn_Open.TabIndex = 1;
            this.Btn_Open.Text = "Open BNK";
            this.Btn_Open.UseVisualStyleBackColor = true;
            this.Btn_Open.Click += new System.EventHandler(this.Btn_Open_Click);
            // 
            // Btn_Save
            // 
            this.Btn_Save.Location = new System.Drawing.Point(112, 12);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(94, 29);
            this.Btn_Save.TabIndex = 2;
            this.Btn_Save.Text = "Save BNK";
            this.Btn_Save.UseVisualStyleBackColor = true;
            this.Btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // Cmb_HeirList
            // 
            this.Cmb_HeirList.FormattingEnabled = true;
            this.Cmb_HeirList.Location = new System.Drawing.Point(12, 47);
            this.Cmb_HeirList.Name = "Cmb_HeirList";
            this.Cmb_HeirList.Size = new System.Drawing.Size(263, 28);
            this.Cmb_HeirList.TabIndex = 3;
            // 
            // Cmb_PropList
            // 
            this.Cmb_PropList.FormattingEnabled = true;
            this.Cmb_PropList.Location = new System.Drawing.Point(12, 81);
            this.Cmb_PropList.Name = "Cmb_PropList";
            this.Cmb_PropList.Size = new System.Drawing.Size(220, 28);
            this.Cmb_PropList.TabIndex = 4;
            // 
            // Btn_AddNew
            // 
            this.Btn_AddNew.Location = new System.Drawing.Point(238, 80);
            this.Btn_AddNew.Name = "Btn_AddNew";
            this.Btn_AddNew.Size = new System.Drawing.Size(37, 29);
            this.Btn_AddNew.TabIndex = 5;
            this.Btn_AddNew.Text = "+";
            this.Btn_AddNew.UseVisualStyleBackColor = true;
            this.Btn_AddNew.Click += new System.EventHandler(this.Btn_AddNew_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(163, 115);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(69, 27);
            this.numericUpDown1.TabIndex = 6;
            // 
            // Lbl_PropName
            // 
            this.Lbl_PropName.AutoSize = true;
            this.Lbl_PropName.Location = new System.Drawing.Point(12, 117);
            this.Lbl_PropName.Name = "Lbl_PropName";
            this.Lbl_PropName.Size = new System.Drawing.Size(119, 20);
            this.Lbl_PropName.TabIndex = 7;
            this.Lbl_PropName.Text = "Property Value =";
            // 
            // Btn_Remove
            // 
            this.Btn_Remove.Location = new System.Drawing.Point(238, 115);
            this.Btn_Remove.Name = "Btn_Remove";
            this.Btn_Remove.Size = new System.Drawing.Size(37, 29);
            this.Btn_Remove.TabIndex = 8;
            this.Btn_Remove.Text = "-";
            this.Btn_Remove.UseVisualStyleBackColor = true;
            this.Btn_Remove.Click += new System.EventHandler(this.Btn_Remove_Click);
            // 
            // DBG_Btn_Debug
            // 
            this.DBG_Btn_Debug.Location = new System.Drawing.Point(212, 12);
            this.DBG_Btn_Debug.Name = "DBG_Btn_Debug";
            this.DBG_Btn_Debug.Size = new System.Drawing.Size(63, 29);
            this.DBG_Btn_Debug.TabIndex = 9;
            this.DBG_Btn_Debug.Text = "DEV";
            this.DBG_Btn_Debug.UseVisualStyleBackColor = true;
            this.DBG_Btn_Debug.Click += new System.EventHandler(this.DBG_Btn_Debug_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 150);
            this.Controls.Add(this.DBG_Btn_Debug);
            this.Controls.Add(this.Btn_Remove);
            this.Controls.Add(this.Lbl_PropName);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.Btn_AddNew);
            this.Controls.Add(this.Cmb_PropList);
            this.Controls.Add(this.Cmb_HeirList);
            this.Controls.Add(this.Btn_Save);
            this.Controls.Add(this.Btn_Open);
            this.Name = "Form1";
            this.Text = "BoiNK -BNK Editor";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button Btn_Open;
        private Button Btn_Save;
        private Button Btn_AddNew;
        private Label Lbl_PropName;
        public ComboBox Cmb_HeirList;
        public ComboBox Cmb_PropList;
        public NumericUpDown numericUpDown1;
        private Button Btn_Remove;
        private Button DBG_Btn_Debug;
    }
}