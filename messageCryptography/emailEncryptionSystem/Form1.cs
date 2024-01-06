using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace emailEncryptionSystem
{
    public partial class Form1 : Form
    {
        public static bool sendMail(string email,string psw,string to,string subject,string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(email);
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(email, psw);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);

                Form2 form = new Form2(email,psw);
                form.Show();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox1.Focus();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(sendMail(textBox1.Text, textBox2.Text, "saifullaharshad999@gmail.com", "Testing email", "This is a test email"))
            {
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid credentials...!");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        
    }
}
