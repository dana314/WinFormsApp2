namespace WinFormsApp2
{
    partial class RegistrationForm
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
            pictureBox1 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            button1 = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            dateTimePicker1 = new DateTimePicker();
            cancelBTN = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.Авторизация;
            pictureBox1.Location = new Point(189, 182);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(423, 293);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Impact", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(320, 182);
            label1.Name = "label1";
            label1.Size = new Size(169, 35);
            label1.TabIndex = 2;
            label1.Text = "Регистрация";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.Window;
            label2.Font = new Font("Impact", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label2.Location = new Point(231, 220);
            label2.Name = "label2";
            label2.Size = new Size(151, 21);
            label2.TabIndex = 3;
            label2.Text = "Имя пользователя";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.Window;
            label3.Font = new Font("Impact", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label3.Location = new Point(327, 252);
            label3.Name = "label3";
            label3.Size = new Size(55, 22);
            label3.TabIndex = 4;
            label3.Text = "Почта";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.Window;
            label4.Font = new Font("Impact", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label4.Location = new Point(246, 281);
            label4.Name = "label4";
            label4.Size = new Size(140, 22);
            label4.TabIndex = 5;
            label4.Text = "Логин (телефон)";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.Window;
            label5.Font = new Font("Impact", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label5.Location = new Point(320, 316);
            label5.Name = "label5";
            label5.Size = new Size(66, 22);
            label5.TabIndex = 6;
            label5.Text = "Пароль";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = SystemColors.Window;
            label6.Font = new Font("Impact", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label6.Location = new Point(251, 349);
            label6.Name = "label6";
            label6.Size = new Size(131, 22);
            label6.TabIndex = 7;
            label6.Text = "Дата рождения";
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Impact", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button1.Location = new Point(311, 379);
            button1.Name = "button1";
            button1.Size = new Size(192, 29);
            button1.TabIndex = 8;
            button1.Text = "Зарегистрироваться";
            button1.UseVisualStyleBackColor = true;
            button1.Click += registrationBTN_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(388, 214);
            textBox1.MaxLength = 15;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(155, 27);
            textBox1.TabIndex = 9;
            textBox1.TextChanged += textBox1_TextChanged_1;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(388, 247);
            textBox2.MaxLength = 100;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(155, 27);
            textBox2.TabIndex = 10;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(388, 280);
            textBox3.MaxLength = 12;
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(155, 27);
            textBox3.TabIndex = 11;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(388, 313);
            textBox4.MaxLength = 10;
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(155, 27);
            textBox4.TabIndex = 12;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CustomFormat = "dd.MM.yyyy";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(388, 346);
            dateTimePicker1.MinDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(155, 27);
            dateTimePicker1.TabIndex = 13;
            // 
            // cancelBTN
            // 
            cancelBTN.FlatStyle = FlatStyle.Popup;
            cancelBTN.Font = new Font("Impact", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            cancelBTN.Location = new Point(372, 414);
            cancelBTN.Name = "cancelBTN";
            cancelBTN.Size = new Size(83, 29);
            cancelBTN.TabIndex = 14;
            cancelBTN.Text = "Отмена";
            cancelBTN.UseVisualStyleBackColor = true;
            cancelBTN.Click += cancelBTN_Click;
            // 
            // RegistrationForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Фон_регистрация;
            ClientSize = new Size(733, 745);
            Controls.Add(cancelBTN);
            Controls.Add(dateTimePicker1);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Name = "RegistrationForm";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button button1;
        private Button cancelBTN;
        public TextBox textBox1;
        public TextBox textBox2;
        public TextBox textBox3;
        public TextBox textBox4;
        public DateTimePicker dateTimePicker1;
    }
}