using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using BCrypt.Net;

namespace Messenger
{
    public partial class Window_Messager : Form
    {
        private string json;
        private string answer;
        private int token; //токен
        private string tag; //Tag
        private IPEndPoint serverEP;
        private IPEndPoint responceEP;
        public Window_Messager(int Token, string Tag, IPEndPoint ServerEP, IPEndPoint ResponceEP)
        {
            InitializeComponent();
            token = Token;
            tag = Tag;
            serverEP = ServerEP;
            responceEP = ResponceEP;
        }

        private void Window_Messager_Load(object sender, EventArgs e)
        {

        }

        private void Send_button_Click(object sender, EventArgs e)
        {
            var m = new Wor
            {
                Tag = tag,
                Token = token,
                Message = UsingRTB.Text
            };
            json = JsonSerializer.Serialize(m);

            using(UdpClient  udpClient = new UdpClient())
            {
                byte[] data = Encoding.UTF8.GetBytes(json);
                //отправляем пакетик
                udpClient.Send(data, data.Length, serverEP);

                bool ServNotAnswer = true;
                while (ServNotAnswer)
                {
                    byte[] responseData = udpClient.Receive(ref responceEP);
                    answer = Encoding.UTF8.GetString(responseData);
                    ServNotAnswer = false;
                }
                if(answer == "1")
                {
                   
                }
                else if(answer == "03") 
                {
                  
                }
                else
                {
                   
                }
            }
        }

        private void Exit_button_Click(object sender, EventArgs e)
        {

        }
    }
    public class Wor
    {
        public string Tag { get; set; }
        public int Token { get; set; }
        public string Message { get; set; }
    }
}
