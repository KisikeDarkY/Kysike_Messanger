using BCrypt.Net;
using CommunicationLib;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Messenger
{
    public partial class Window_Messager : Form
    {
        
        private string token;
        private string tag; 
        private IPEndPoint serverEP;
        private IPEndPoint responceEP;
        public Window_Messager(string Token, string Tag, IPEndPoint ServerEP, IPEndPoint ResponceEP)
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
            IMessage ans = null;
            var m = new UserMessage
            {
                Tag = tag,
                Token = token,
                Message = UsingRTB.Text
            };
            byte[] data = MessageHandler.ConvertMessage(m);

            MessageBox.Show(Encoding.UTF8.GetString(data));

            using (UdpClient  udpClient = new UdpClient())
            {
                State_messageCB.Checked = false;
                //отправляем пакетик
                udpClient.Send(data, data.Length, serverEP);

                bool ServNotAnswer = true;
                while (ServNotAnswer)
                {
                    ans = MessageHandler.HandleMessage(udpClient.Receive(ref responceEP));
                    ServNotAnswer = false;
                }
                State_messageCB.Checked = false;
                if (ans is AnswerServer)
                {
                    AnswerServer ansSr = (AnswerServer)ans;
                    if (ansSr.Code == "1")
                    {
                        State_messageCB.Checked = true;
                        OutputRTB.Text = $"{OutputRTB.Text} \n <{tag}> {UsingRTB.Text}";
                    }
                    else if (ansSr.Code == "01")
                    {
                        MessageBox.Show("01");
                    }
                    else if (ansSr.Code == "03")
                    {
                        MessageBox.Show($"03: {token}");
                    }
                    else
                    {
                        MessageBox.Show("-1");
                    }
                }
            }
        }

        private void Exit_button_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
