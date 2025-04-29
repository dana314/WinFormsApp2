using System;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class NewPasswordForm : Form
    {
        private string sentCode; 

        public NewPasswordForm()
        {
            InitializeComponent();
        }

        private void sendCodeBTN_Click(object sender, EventArgs e)
        {
            Random newCode = new Random();
            sentCode = newCode.Next(1000, 9999).ToString();

            
            MessageBox.Show($"Ваш код подтверждения: {sentCode}", "Код отправлен");// можно потом поменять на смс-симулятор

            // для ввода
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            verificationBTN.Enabled = true;
        }

        private void verificationBTN_Click(object sender, EventArgs e)
        {
            
            if (textBox2.Text == sentCode)
            {
                if (textBox3.Text == textBox4.Text)
                {
                    MessageBox.Show("Пароль изменен");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Пароли не совпадают");
                }
            }
            else
            {
                MessageBox.Show("Неверный код подтверждения");
            }
        }

        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void label4_Click(object sender, EventArgs e)
        {
            
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}