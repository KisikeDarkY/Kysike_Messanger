using BCrypt.Net;
using Messenger;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Messenger
{
    public partial class Form1 : Form
    {
        public static Random random = new Random();
        private string json; //json пакетик
        private int token = random.Next(10000); //создаем рандомный токен
        private IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 55555); //создаем конечную точку сервера
        private IPEndPoint responceEP = new IPEndPoint(IPAddress.Any, 0); //создаем конечную точку для получения данных
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Reg_button_Click(object sender, EventArgs e)
        {
           
           if(ValidateFormReg())
           {
                var n = new Pols
                {
                    Name = Name_TextBox.Text,
                    Tag = Tag_TextBox.Text,
                    Password = BCrypt.Net.BCrypt.HashPassword(Pasword_TextBox.Text, workFactor: 12),
                    Token = token,
                    IsReg = true
                };

                byte[] data = MessageHandler.ConvertMessage(n);

                using (UdpClient udpClient = new UdpClient())
                { 
                    //отправляем пакетик
                    udpClient.Send(data, data.Length, serverEP);

                    bool ServNotAnswer = true;

                    IMessage ans = null;
                    while (ServNotAnswer)
                    {
                        byte[] responseData = udpClient.Receive(ref responceEP);
                        ans = MessageHandler.HandleMessage(responseData);
                        ServNotAnswer = false;
                    }

                    if (ans is Pols)
                    {
                        Pols pols = (Pols)ans;
                    }
                    else if ()
                    {

                    }

                    //успешный случай
                    if (answer == "1")
                    {
                        udpClient.Close();
                        MessageBox.Show("Вы успешно создали и вошли в аккаунт");
                        //Код для перехода в сам месенджер
                        Window_Messager messager = new Window_Messager(token, "n.Tag", serverEP, responceEP);
                        messager.FormClosed += (s, args) => this.Show();
                        this.Hide();
                        Name_TextBox.Text = "";
                        Pasword_TextBox.Text = "";
                        Tag_TextBox.Text = "";
                        Input_tagTB.Text = "";
                        Input_PasswordTB.Text = "";
                        messager.Show();
                    }
                    //проверка кодов ошибок
                    else if (answer == "04")
                    {
                        MessageBox.Show("Такой пользователь уже существует");
                        udpClient.Close();
                        Application.Restart();
                    }
                    //если ответ от сервера был странный
                    else
                    {
                        MessageBox.Show("Некорректный ответ от сервера");
                        udpClient.Close();
                        Application.Restart();
                    }
                }
            }
        }
        private void Input_button_Click(object sender, EventArgs e)
        {
            if(ValidateFormInp())
            {
                var n = new Pols
                {
                    Tag = Input_tagTB.Text,
                    Password = BCrypt.Net.BCrypt.HashPassword(Input_PasswordTB.Text, workFactor: 12),
                    Token = token,
                    IsReg = false
                }; 
                json = JsonSerializer.Serialize(n);//так же json пакетик

                using (UdpClient udpClient = new UdpClient())
                {
                    byte[] data = Encoding.UTF8.GetBytes(json);
                    //отправляем пакетик
                    udpClient.Send(data, data.Length, serverEP);

                    //ждём ответ от сервера
                    bool ServNotAnswer = true;
                    while (ServNotAnswer)
                    {
                        byte[] responseData = udpClient.Receive(ref responceEP);
                        answer = Encoding.UTF8.GetString(responseData);
                        ServNotAnswer = false;
                    }

                    //успешный случай
                    if(answer == "1")
                    {
                      udpClient.Close();
                      MessageBox.Show("Вы успешно вошли в аккаунт");
                      //Код для перехода в сам месенджер
                      Window_Messager messager = new Window_Messager(token, "n.Tag", serverEP, responceEP);
                      messager.FormClosed += (s, args) => this.Show();
                      this.Hide();
                      Name_TextBox.Text = "";
                      Pasword_TextBox.Text = "";
                      Tag_TextBox.Text = "";
                      Input_tagTB.Text = "";
                      Input_PasswordTB.Text = "";
                      messager.Show();
                    }
                    //проверка кодов ошибок
                    else if (answer == "01") 
                    {
                        MessageBox.Show("Неправильный Tag");
                        udpClient.Close();
                        Application.Restart();
                    }
                    else if (answer == "02")
                    {
                        MessageBox.Show("Неправильный Пароль");
                        udpClient.Close();
                        Application.Restart();
                    }
                    else if (answer == "03")
                    {
                        MessageBox.Show("Это не вы");
                        udpClient.Close();
                        Application.Restart();
                    }
                    //если ответ от сервера был странный
                    else
                    {
                        MessageBox.Show("Некорректный ответ от сервера");
                        udpClient.Close();
                        Application.Restart();
                    }  
                }
            }
        }


        private bool ValidateFormReg()
        {
            if (string.IsNullOrWhiteSpace(Name_TextBox.Text))
            {
                MessageBox.Show("Введите имя");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Tag_TextBox.Text))
            {
                MessageBox.Show("Введите Tag");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Pasword_TextBox.Text))
            {
                MessageBox.Show("Введите пароль");
                return false;
            }

            return true;
        } //проверка пустоты полей на регистрации
        private bool ValidateFormInp()
        {
            if (string.IsNullOrWhiteSpace(Input_tagTB.Text))
            {
                MessageBox.Show("Введите Tag");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Input_PasswordTB.Text))
            {
                MessageBox.Show("Введите пароль");
                return false;
            }

            return true;
        } //такая же проверка но для полей входа
    }
    //класс для json пакетиков
    public class Pols : IMessage
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Password { get; set; }
        public int Token { get; set; }
        public bool IsReg { get; set; }
    }
}
//private async Task Input_button_Click(object sender, EventArgs e)
//{
//    if (ValidateFormInp())
//    {
//        var n = new Pols
//        {
//            Tag = Input_tagTB.Text,
//            Password = BCrypt.Net.BCrypt.HashPassword(Input_PasswordTB.Text, workFactor: 12),
//            Token = token,
//            IsReg = false
//        };
//        json = JsonSerializer.Serialize(n);//так же json пакетик

//        using (UdpClient udpClient = new UdpClient())
//        {
//            byte[] data = Encoding.UTF8.GetBytes(json);
//            //отправляем пакетик
//            await udpClient.SendAsync(data, data.Length, serverEP);

//            //ждём ответ от сервера
//            bool ServNotAnswer = true;
//            while (ServNotAnswer)
//            {
//                UdpReceiveResult result = await udpClient.ReceiveAsync();
//                answer = Encoding.UTF8.GetString(result.Buffer);
//                ServNotAnswer = false;
//            }

//            //успешный случай
//            if (answer == "1")
//            {
//                udpClient.Close();
//                MessageBox.Show("Вы успешно вошли в аккаунт");
//                /*
//                 *  Код для перехода в сам месенджер
//                 */
//            }
//            //проверка кодов ошибок
//            if (answer == "01")
//            {
//                MessageBox.Show("Неправильный Tag");
//                udpClient.Close();
//                Application.Restart();
//            }
//            if (answer == "02")
//            {
//                MessageBox.Show("Неправильный Пароль");
//                udpClient.Close();
//                Application.Restart();
//            }
//            if (answer == "03")
//            {
//                MessageBox.Show("Это не вы");
//                udpClient.Close();
//                Application.Restart();
//            }
//            //если ответ от сервера был странный
//            MessageBox.Show("Некорректный ответ от сервера");
//            udpClient.Close();
//            Application.Restart();
//        }
//    }
//}
