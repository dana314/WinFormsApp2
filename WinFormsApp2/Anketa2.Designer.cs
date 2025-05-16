namespace WinFormsApp2
{
    partial class Anketa2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            comboBox4 = new ComboBox();
            comboBox3 = new ComboBox();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            button1 = new Button();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label1 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(comboBox4);
            panel1.Controls.Add(comboBox3);
            panel1.Controls.Add(comboBox2);
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label5);
            panel1.Location = new Point(158, 142);
            panel1.Name = "panel1";
            panel1.Size = new Size(571, 482);
            panel1.TabIndex = 0;
            // 
            // comboBox4
            // 
            comboBox4.Anchor = AnchorStyles.Top;
            comboBox4.FormattingEnabled = true;
            comboBox4.Location = new Point(293, 197);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(151, 28);
            comboBox4.TabIndex = 9;
            // 
            // comboBox3
            // 
            comboBox3.Anchor = AnchorStyles.Top;
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(293, 149);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(151, 28);
            comboBox3.TabIndex = 8;
            // 
            // comboBox2
            // 
            comboBox2.Anchor = AnchorStyles.Top;
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(293, 98);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(151, 28);
            comboBox2.TabIndex = 7;
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.Top;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(293, 48);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 6;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Impact", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button1.Location = new Point(143, 300);
            button1.Name = "button1";
            button1.Size = new Size(301, 49);
            button1.TabIndex = 5;
            button1.Text = "К рекомендациям";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label2.AutoSize = true;
            label2.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label2.Location = new Point(211, 46);
            label2.Name = "label2";
            label2.Size = new Size(58, 25);
            label2.TabIndex = 1;
            label2.Text = "Жанр";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label3.AutoSize = true;
            label3.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label3.Location = new Point(196, 96);
            label3.Name = "label3";
            label3.Size = new Size(73, 25);
            label3.TabIndex = 2;
            label3.Text = "Страна";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label4.AutoSize = true;
            label4.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label4.Location = new Point(143, 147);
            label4.Name = "label4";
            label4.Size = new Size(126, 25);
            label4.TabIndex = 3;
            label4.Text = "Десятилетие";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label5.AutoSize = true;
            label5.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label5.Location = new Point(41, 200);
            label5.Name = "label5";
            label5.Size = new Size(228, 25);
            label5.TabIndex = 4;
            label5.Text = "Возрастное ограничение";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.FlatStyle = FlatStyle.Popup;
            label1.Font = new Font("Impact", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(175, 83);
            label1.Name = "label1";
            label1.Size = new Size(542, 42);
            label1.TabIndex = 0;
            label1.Text = "Расскажите о своих предпочтениях";
            // 
            // Anketa2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackgroundImage = Properties.Resources.Фон_авторизация;
            ClientSize = new Size(847, 748);
            Controls.Add(label1);
            Controls.Add(panel1);
            Name = "Anketa2";
            Text = "Предпочтения";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Button button1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label1;
        private ComboBox comboBox4;
        private ComboBox comboBox3;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
    }
}