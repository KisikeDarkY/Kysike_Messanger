using BCrypt.Net;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using CommunicationLib;

namespace Messenger
{
    public partial class Form1 : Form
    {
        public static Random random = new Random();
        private string json; //json пакетик
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
                var n = new RegPols
                {
                    Name = Name_TextBox.Text,
                    Tag = Tag_TextBox.Text,
                    Password = Pasword_TextBox.Text

                };

                byte[] data = MessageHandler.ConvertMessage(n);

                MessageBox.Show(Encoding.UTF8.GetString(data));

                using (UdpClient udpClient = new UdpClient())
                { 
                    //отправляем пакетик
                    udpClient.Send(data, data.Length, serverEP);

                    bool ServNotAnswer = true;

                    IMessage ans = null;
                    while (ServNotAnswer)
                    {
                        ans = MessageHandler.HandleMessage(udpClient.Receive(ref responceEP));
                        ServNotAnswer = false;
                    }
                    if(ans is AnswerServer)
                    {
                        AnswerServer ansSr = (AnswerServer)ans;
                        if(ansSr.Code == "1")
                        {
                            udpClient.Close();
                            MessageBox.Show("Вы успешно создали и вошли в аккаунт");
                            //Код для перехода в сам месенджер
                            Window_Messager messager = new Window_Messager(ansSr.Token, Tag_TextBox.Text, serverEP, responceEP);
                            messager.FormClosed += (s, args) => this.Show();
                            this.Hide();
                            Name_TextBox.Text = "";
                            Pasword_TextBox.Text = "";
                            Tag_TextBox.Text = "";
                            Input_tagTB.Text = "";
                            Input_PasswordTB.Text = "";
                            messager.Show();
                        }
                        else if( ansSr.Code == "04")
                        {
                            udpClient.Close();
                            MessageBox.Show("Пользователь уже существует!");
                            Application.Restart();
                        }
                        else
                        {
                            MessageBox.Show("Некорректный ответ от сервера");
                            udpClient.Close();
                            Application.Restart();
                        }
                    }
                }
            }
        }
        private void Input_button_Click(object sender, EventArgs e)
        {
            if(ValidateFormInp())
            {
                var n = new InsPols
                {
                    Tag = Input_tagTB.Text,
                    Password = Input_PasswordTB.Text
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
                        ans = MessageHandler.HandleMessage(udpClient.Receive(ref responceEP));
                        ServNotAnswer = false;
                    }
                    if (ans is AnswerServer)
                    {
                        AnswerServer ansSr = (AnswerServer)ans;
                        if (ansSr.Code == "1")
                        {
                            udpClient.Close();
                            MessageBox.Show("Вы успешно вошли в аккаунт");
                            //Код для перехода в сам месенджер
                            Window_Messager messager = new Window_Messager(ansSr.Token, Input_tagTB.Text, serverEP, responceEP);
                            messager.FormClosed += (s, args) => this.Show();
                            this.Hide();
                            Name_TextBox.Text = "";
                            Pasword_TextBox.Text = "";
                            Tag_TextBox.Text = "";
                            Input_tagTB.Text = "";
                            Input_PasswordTB.Text = "";
                            messager.Show();
                        }
                        else if (ansSr.Code == "01")
                        {
                            udpClient.Close();
                            MessageBox.Show("Не правильный Tag");
                            Application.Restart();
                        }
                        else if (ansSr.Code == "02")
                        {
                            udpClient.Close();
                            MessageBox.Show("Не правильный пароль");
                            Application.Restart();
                        }
                        else if (ansSr.Code == "03")
                        {
                            udpClient.Close();
                            MessageBox.Show("Некоректный токен");
                            Application.Restart();
                        }
                        else
                        {
                            MessageBox.Show("Некорректный ответ от сервера");
                            udpClient.Close();
                            Application.Restart();
                        }
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
}

