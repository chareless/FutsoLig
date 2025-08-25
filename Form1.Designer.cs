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
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(12, 374);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(400, 128);
            richTextBox1.TabIndex = 4;
            richTextBox1.Text = "";
            // 
            // listBoxEvents
            // 
            listBoxEvents.FormattingEnabled = true;
            listBoxEvents.Location = new Point(12, 45);
            listBoxEvents.Name = "listBoxEvents";
            listBoxEvents.Size = new Size(400, 229);
            listBoxEvents.TabIndex = 5;
            listBoxEvents.SelectedIndexChanged += listBoxEvents_SelectedIndexChanged;
            // 
            // textBoxBearer
            // 
            textBoxBearer.Location = new Point(12, 345);
            textBoxBearer.Name = "textBoxBearer";
            textBoxBearer.Size = new Size(205, 23);
            textBoxBearer.TabIndex = 6;
            textBoxBearer.TextChanged += textBoxBearer_TextChanged;
            // 
            // textBoxEventId
            // 
            textBoxEventId.Location = new Point(12, 300);
            textBoxEventId.Name = "textBoxEventId";
            textBoxEventId.Size = new Size(137, 23);
            textBoxEventId.TabIndex = 7;
            // 
            // listBoxCategories
            // 
            listBoxCategories.FormattingEnabled = true;
            listBoxCategories.Location = new Point(449, 45);
            listBoxCategories.Name = "listBoxCategories";
            listBoxCategories.Size = new Size(400, 229);
            listBoxCategories.TabIndex = 8;
            listBoxCategories.SelectedIndexChanged += listBoxCategories_SelectedIndexChanged;
            // 
            // textBoxCatId
            // 
            textBoxCatId.Location = new Point(275, 300);
            textBoxCatId.Name = "textBoxCatId";
            textBoxCatId.Size = new Size(137, 23);
            textBoxCatId.TabIndex = 9;
            // 
            // listBoxBlocks
            // 
            listBoxBlocks.FormattingEnabled = true;
            listBoxBlocks.Location = new Point(449, 300);
            listBoxBlocks.Name = "listBoxBlocks";
            listBoxBlocks.Size = new Size(400, 199);
            listBoxBlocks.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 27);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 11;
            label1.Text = "Etkinlikler";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(449, 27);
            label2.Name = "label2";
            label2.Size = new Size(64, 15);
            label2.TabIndex = 12;
            label2.Text = "Kategoriler";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(449, 282);
            label3.Name = "label3";
            label3.Size = new Size(43, 15);
            label3.TabIndex = 13;
            label3.Text = "Bloklar";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 282);
            label4.Name = "label4";
            label4.Size = new Size(59, 15);
            label4.TabIndex = 14;
            label4.Text = "Etkinlik ID";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(275, 282);
            label5.Name = "label5";
            label5.Size = new Size(65, 15);
            label5.TabIndex = 15;
            label5.Text = "Kategori ID";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 327);
            label6.Name = "label6";
            label6.Size = new Size(44, 15);
            label6.TabIndex = 16;
            label6.Text = "TOKEN";
            // 
            // LoopButton
            // 
            LoopButton.Location = new Point(275, 345);
            LoopButton.Name = "LoopButton";
            LoopButton.Size = new Size(137, 22);
            LoopButton.TabIndex = 17;
            LoopButton.Text = "Döngüyü Durdur";
            LoopButton.UseVisualStyleBackColor = true;
            LoopButton.Click += Loop_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(880, 545);
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
            Name = "Form1";
            Text = "Form1";
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
    }
}
