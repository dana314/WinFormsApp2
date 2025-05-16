namespace WinFormsApp2
{
    partial class NewPasswordForm
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
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            sendCodeBTN = new Button();
            label5 = new Label();
            textBox3 = new TextBox();
            verificationBTN = new Button();
            repeatPassword = new Label();
            textBox4 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.None;
            pictureBox1.BackgroundImage = Properties.Resources.Авторизация;
            pictureBox1.Location = new Point(189, 190);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(423, 293);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Window;
            label1.Font = new Font("Impact", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(310, 191);
            label1.Name = "label1";
            label1.Size = new Size(183, 28);
            label1.TabIndex = 3;
            label1.Text = "Восстановление ";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.BackColor = SystemColors.Window;
            label2.Font = new Font("Impact", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label2.Location = new Point(351, 218);
            label2.Name = "label2";
            label2.Size = new Size(85, 28);
            label2.TabIndex = 4;
            label2.Text = "пароля";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.BackColor = SystemColors.Window;
            label3.Font = new Font("Impact", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label3.Location = new Point(280, 246);
            label3.Name = "label3";
            label3.Size = new Size(123, 22);
            label3.TabIndex = 5;
            label3.Text = "Введите почту";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.BackColor = SystemColors.Window;
            label4.Font = new Font("Impact", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label4.Location = new Point(295, 316);
            label4.Name = "label4";
            label4.Size = new Size(108, 22);
            label4.TabIndex = 6;
            label4.Text = "Введите код";
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.None;
            textBox1.Location = new Point(407, 242);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(125, 27);
            textBox1.TabIndex = 7;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.None;
            textBox2.Location = new Point(407, 309);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(125, 27);
            textBox2.TabIndex = 8;
            // 
            // sendCodeBTN
            // 
            sendCodeBTN.Anchor = AnchorStyles.None;
            sendCodeBTN.FlatStyle = FlatStyle.Popup;
            sendCodeBTN.Font = new Font("Impact", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            sendCodeBTN.Location = new Point(338, 275);
            sendCodeBTN.Name = "sendCodeBTN";
            sendCodeBTN.Size = new Size(127, 29);
            sendCodeBTN.TabIndex = 9;
            sendCodeBTN.Text = "Отправить код";
            sendCodeBTN.UseVisualStyleBackColor = true;
            sendCodeBTN.Click += sendCodeBTN_Click;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.BackColor = SystemColors.Window;
            label5.Font = new Font("Impact", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label5.Location = new Point(245, 352);
            label5.Name = "label5";
            label5.Size = new Size(157, 22);
            label5.TabIndex = 10;
            label5.Text = "Ваш новый пароль";
            // 
            // textBox3
            // 
            textBox3.Anchor = AnchorStyles.None;
            textBox3.Location = new Point(407, 345);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(125, 27);
            textBox3.TabIndex = 11;
            // 
            // verificationBTN
            // 
            verificationBTN.Anchor = AnchorStyles.None;
            verificationBTN.FlatStyle = FlatStyle.Popup;
            verificationBTN.Font = new Font("Impact", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            verificationBTN.Location = new Point(338, 416);
            verificationBTN.Name = "verificationBTN";
            verificationBTN.Size = new Size(127, 29);
            verificationBTN.TabIndex = 12;
            verificationBTN.Text = "Подтвердить";
            verificationBTN.UseVisualStyleBackColor = true;
            verificationBTN.Click += verificationBTN_Click;
            // 
            // repeatPassword
            // 
            repeatPassword.Anchor = AnchorStyles.None;
            repeatPassword.AutoSize = true;
            repeatPassword.BackColor = SystemColors.Window;
            repeatPassword.Font = new Font("Impact", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            repeatPassword.Location = new Point(249, 383);
            repeatPassword.Name = "repeatPassword";
            repeatPassword.Size = new Size(150, 22);
            repeatPassword.TabIndex = 13;
            repeatPassword.Text = "Повторите пароль";
            // 
            // textBox4
            // 
            textBox4.Anchor = AnchorStyles.None;
            textBox4.Location = new Point(407, 380);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(125, 27);
            textBox4.TabIndex = 14;
            // 
            // NewPasswordForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackgroundImage = Properties.Resources.Фон_регистрация;
            ClientSize = new Size(800, 672);
            Controls.Add(textBox4);
            Controls.Add(repeatPassword);
            Controls.Add(verificationBTN);
            Controls.Add(textBox3);
            Controls.Add(label5);
            Controls.Add(sendCodeBTN);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Name = "NewPasswordForm";
            Text = "Восстановление пароля";
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
        private TextBox textBox1;
        private TextBox textBox2;
        private Button sendCodeBTN;
        private Label label5;
        private TextBox textBox3;
        private Button verificationBTN;
        private Label repeatPassword;
        private TextBox textBox4;
    }
}