namespace WinFormsApp2
{
    partial class LoginForm
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
            pictureBox1 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            loginBTN = new Button();
            newPassBTN = new Button();
            registrationBTN = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.Авторизация;
            pictureBox1.Location = new Point(186, 175);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(423, 293);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Impact", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(340, 175);
            label1.Name = "label1";
            label1.Size = new Size(90, 35);
            label1.TabIndex = 1;
            label1.Text = "Кино+";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Impact", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label2.Location = new Point(235, 215);
            label2.Name = "label2";
            label2.Size = new Size(140, 22);
            label2.TabIndex = 2;
            label2.Text = "Логин (телефон)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Impact", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label3.Location = new Point(313, 256);
            label3.Name = "label3";
            label3.Size = new Size(66, 22);
            label3.TabIndex = 3;
            label3.Text = "Пароль";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Impact", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label4.Location = new Point(235, 341);
            label4.Name = "label4";
            label4.Size = new Size(140, 22);
            label4.TabIndex = 4;
            label4.Text = "Забыли пароль?";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Impact", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label5.Location = new Point(255, 389);
            label5.Name = "label5";
            label5.Size = new Size(120, 22);
            label5.TabIndex = 5;
            label5.Text = "Нет аккаунта?";
            // 
            // loginBTN
            // 
            loginBTN.FlatStyle = FlatStyle.Popup;
            loginBTN.Font = new Font("Impact", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            loginBTN.Location = new Point(340, 289);
            loginBTN.Name = "loginBTN";
            loginBTN.Size = new Size(94, 29);
            loginBTN.TabIndex = 6;
            loginBTN.Text = "Войти";
            loginBTN.UseVisualStyleBackColor = true;
            loginBTN.Click += loginBTN_Click;
            // 
            // newPassBTN
            // 
            newPassBTN.FlatStyle = FlatStyle.Popup;
            newPassBTN.Font = new Font("Impact", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            newPassBTN.Location = new Point(389, 339);
            newPassBTN.Name = "newPassBTN";
            newPassBTN.Size = new Size(149, 29);
            newPassBTN.TabIndex = 7;
            newPassBTN.Text = "Восстановить";
            newPassBTN.UseVisualStyleBackColor = true;
            newPassBTN.Click += newPassBTN_Click;
            // 
            // registrationBTN
            // 
            registrationBTN.FlatStyle = FlatStyle.Popup;
            registrationBTN.Font = new Font("Impact", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            registrationBTN.Location = new Point(389, 382);
            registrationBTN.Name = "registrationBTN";
            registrationBTN.Size = new Size(149, 29);
            registrationBTN.TabIndex = 8;
            registrationBTN.Text = "Регистрация";
            registrationBTN.UseVisualStyleBackColor = true;
            registrationBTN.Click += registrationBTN_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(389, 214);
            textBox1.MaxLength = 12;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(125, 27);
            textBox1.TabIndex = 9;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(389, 253);
            textBox2.MaxLength = 100;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(125, 27);
            textBox2.TabIndex = 10;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Фон_авторизация;
            ClientSize = new Size(872, 757);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(registrationBTN);
            Controls.Add(newPassBTN);
            Controls.Add(loginBTN);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Name = "LoginForm";
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
        private Button loginBTN;
        private Button newPassBTN;
        private Button registrationBTN;
        private TextBox textBox1;
        private TextBox textBox2;
    }
}
