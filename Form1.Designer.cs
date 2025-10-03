namespace FutsoLig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            richTextBox1 = new RichTextBox();
            listBoxEvents = new ListBox();
            textBoxBearer = new TextBox();
            textBoxEventId = new TextBox();
            listBoxCategories = new ListBox();
            textBoxCatId = new TextBox();
            listBoxBlocks = new ListBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            LoopButton = new Button();
            menuStrip1 = new MenuStrip();
            toolStripUpdate = new ToolStripMenuItem();
            toolStripContact = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(12, 443);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(400, 109);
            richTextBox1.TabIndex = 4;
            richTextBox1.Text = "";
            // 
            // listBoxEvents
            // 
            listBoxEvents.FormattingEnabled = true;
            listBoxEvents.Location = new Point(12, 61);
            listBoxEvents.Name = "listBoxEvents";
            listBoxEvents.Size = new Size(400, 259);
            listBoxEvents.TabIndex = 5;
            listBoxEvents.SelectedIndexChanged += listBoxEvents_SelectedIndexChanged;
            // 
            // textBoxBearer
            // 
            textBoxBearer.Location = new Point(12, 400);
            textBoxBearer.Name = "textBoxBearer";
            textBoxBearer.Size = new Size(205, 23);
            textBoxBearer.TabIndex = 6;
            textBoxBearer.TextChanged += textBoxBearer_TextChanged;
            // 
            // textBoxEventId
            // 
            textBoxEventId.Location = new Point(12, 355);
            textBoxEventId.Name = "textBoxEventId";
            textBoxEventId.Size = new Size(137, 23);
            textBoxEventId.TabIndex = 7;
            textBoxEventId.TextChanged += textBoxEventId_TextChanged;
            // 
            // listBoxCategories
            // 
            listBoxCategories.FormattingEnabled = true;
            listBoxCategories.Location = new Point(449, 61);
            listBoxCategories.Name = "listBoxCategories";
            listBoxCategories.Size = new Size(400, 259);
            listBoxCategories.TabIndex = 8;
            listBoxCategories.SelectedIndexChanged += listBoxCategories_SelectedIndexChanged;
            // 
            // textBoxCatId
            // 
            textBoxCatId.Location = new Point(275, 355);
            textBoxCatId.Name = "textBoxCatId";
            textBoxCatId.Size = new Size(137, 23);
            textBoxCatId.TabIndex = 9;
            // 
            // listBoxBlocks
            // 
            listBoxBlocks.FormattingEnabled = true;
            listBoxBlocks.Location = new Point(449, 353);
            listBoxBlocks.Name = "listBoxBlocks";
            listBoxBlocks.Size = new Size(400, 199);
            listBoxBlocks.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(12, 37);
            label1.Name = "label1";
            label1.Size = new Size(78, 21);
            label1.TabIndex = 11;
            label1.Text = "Etkinlikler";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label2.ForeColor = SystemColors.Control;
            label2.Location = new Point(449, 37);
            label2.Name = "label2";
            label2.Size = new Size(86, 21);
            label2.TabIndex = 12;
            label2.Text = "Kategoriler";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label3.ForeColor = SystemColors.Control;
            label3.Location = new Point(449, 329);
            label3.Name = "label3";
            label3.Size = new Size(58, 21);
            label3.TabIndex = 13;
            label3.Text = "Bloklar";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = SystemColors.Control;
            label4.Location = new Point(12, 337);
            label4.Name = "label4";
            label4.Size = new Size(59, 15);
            label4.TabIndex = 14;
            label4.Text = "Etkinlik ID";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = SystemColors.Control;
            label5.Location = new Point(275, 337);
            label5.Name = "label5";
            label5.Size = new Size(65, 15);
            label5.TabIndex = 15;
            label5.Text = "Kategori ID";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = SystemColors.Control;
            label6.Location = new Point(12, 382);
            label6.Name = "label6";
            label6.Size = new Size(44, 15);
            label6.TabIndex = 16;
            label6.Text = "TOKEN";
            // 
            // LoopButton
            // 
            LoopButton.BackColor = Color.DarkOrange;
            LoopButton.FlatStyle = FlatStyle.Popup;
            LoopButton.ForeColor = SystemColors.ControlText;
            LoopButton.Location = new Point(275, 385);
            LoopButton.Name = "LoopButton";
            LoopButton.Size = new Size(137, 38);
            LoopButton.TabIndex = 17;
            LoopButton.Text = "Döngüyü Durdur";
            LoopButton.UseVisualStyleBackColor = false;
            LoopButton.Click += Loop_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripUpdate, toolStripContact });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(894, 24);
            menuStrip1.TabIndex = 18;
            menuStrip1.Text = "menuStrip1";
            // 
            // toolStripUpdate
            // 
            toolStripUpdate.Name = "toolStripUpdate";
            toolStripUpdate.Size = new Size(141, 20);
            toolStripUpdate.Text = "Güncellemeleri Denetle";
            toolStripUpdate.Click += toolStripUpdate_Click;
            // 
            // toolStripContact
            // 
            toolStripContact.Name = "toolStripContact";
            toolStripContact.Size = new Size(57, 20);
            toolStripContact.Text = "İletişim";
            toolStripContact.Click += toolStripContact_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkRed;
            ClientSize = new Size(894, 593);
            Controls.Add(LoopButton);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(listBoxBlocks);
            Controls.Add(textBoxCatId);
            Controls.Add(listBoxCategories);
            Controls.Add(textBoxEventId);
            Controls.Add(textBoxBearer);
            Controls.Add(listBoxEvents);
            Controls.Add(richTextBox1);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FutsoLig";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private RichTextBox richTextBox1;
        private ListBox listBoxEvents;
        private TextBox textBoxBearer;
        private TextBox textBoxEventId;
        private ListBox listBoxCategories;
        private TextBox textBoxCatId;
        private ListBox listBoxBlocks;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button LoopButton;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripUpdate;
        private ToolStripMenuItem toolStripContact;
    }
}
