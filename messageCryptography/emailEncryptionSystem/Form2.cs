using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace emailEncryptionSystem
{
    public partial class Form2 : Form
    {
        private string publicKey1, privateKey1,myEmail,myPsw;
        public static string GetKeyString(RSAParameters publicKey)
        {
            var stringWriter = new System.IO.StringWriter();
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            xmlSerializer.Serialize(stringWriter, publicKey);
            return stringWriter.ToString();
        }
        public static string Encrypt(string textToEncrypt, string publicKeyString)
        {
            var bytesToEncrypt = Encoding.UTF8.GetBytes(textToEncrypt);
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    rsa.FromXmlString(publicKeyString.ToString());
                    var encryptedData = rsa.Encrypt(bytesToEncrypt, true);
                    var base64Encrypted = Convert.ToBase64String(encryptedData);
                    return base64Encrypted;
                }
                catch
                {
                    MessageBox.Show("Invalid public key...!");
                    return "";
                    System.Environment.Exit(0);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
        public static string Decrypt(string textToDecrypt, string privateKeyString)
        {
            var bytesToDecrypt = Encoding.UTF8.GetBytes(textToDecrypt);
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    rsa.FromXmlString(privateKeyString);
                    var resultBytes = Convert.FromBase64String(textToDecrypt);
                    var decryptedBytes = rsa.Decrypt(resultBytes, true);
                    var decryptedData = Encoding.UTF8.GetString(decryptedBytes);
                    return decryptedData.ToString();
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
        /*
        private static string GenerateTestString()
        {
            return "custom string";
        }
        */
        private void generateKey()
        {
            var cryptoServiceProvider = new RSACryptoServiceProvider(2048);
            var privateKey = cryptoServiceProvider.ExportParameters(true);
            var publicKey = cryptoServiceProvider.ExportParameters(false);
            publicKey1 = GetKeyString(publicKey);
            privateKey1 = GetKeyString(privateKey);
            /*
            Console.WriteLine("public key: ");
            Console.WriteLine(publicKeyString);
            Console.WriteLine("-----------------------------");
            Console.WriteLine("private key: ");
            Console.WriteLine(privateKeyString);
            Console.WriteLine("-----------------------------");
            string textToEncrypt = GenerateTestString();
            Console.WriteLine("Text to encrypt: ");
            Console.WriteLine(textToEncrypt);
            Console.WriteLine("-----------------------------");
            string encryptedText = Encrypt(textToEncrypt, publicKeyString);
            Console.WriteLine("Encrypted text: ");
            Console.WriteLine(encryptedText);
            Console.WriteLine("-----------------------------");
            string decryptedText = Decrypt(encryptedText, privateKeyString);
            Console.WriteLine("Decrypted text: ");
            Console.WriteLine(decryptedText);

            Console.Read();
            */
        }
        /*
        private string encryptSub(string subject)
        {
            
            return "";
        }
        private string encryptBody(string body)
        {
            return "";
        }
        */
        public Form2(string myEmail,string myPsw)
        {
            this.myEmail = myEmail;
            this.myPsw = myPsw;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox4.Text=="")
            {
                MessageBox.Show("First generate your key...!");
            }
            else
            {

                string encSubject = Encrypt(textBox2.Text,textBox4.Text);
                string encBody = Encrypt(textBox3.Text,textBox4.Text);
                if (Form1.sendMail(myEmail, myPsw, textBox1.Text, encSubject, encBody))
                {
                    MessageBox.Show("Email sent!");
                }
                else
                {
                    MessageBox.Show("Error sending mail");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            generateKey();
            textBox6.Text = "Public key: " + publicKey1 + "\nPrivate key: " + privateKey1 + "\nNote: Don't share the private key with anyone!";
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            myEmail = "";
            myPsw = "";
            this.Hide();
            Form1 form = new Form1();
            form.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                MessageBox.Show("First write your private key!");
            }
            else
            {
                textBox8.Text = Decrypt(textBox8.Text, textBox5.Text);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Form1.sendMail(myEmail, myPsw, textBox7.Text, "[My Public key]", "My public key is: " + publicKey1))
            {
                MessageBox.Show("Email sent!");
            }
            else
            {
                MessageBox.Show("Error sending mail");
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
