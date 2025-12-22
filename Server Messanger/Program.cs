using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using static Server;

class Server
{
    public static void Main(string[] args)
    {
        
        using (UdpClient server = new UdpClient(55555))
        {
            Console.WriteLine("Start");
            string jsonm;
            //создаём конечную точку
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
            byte[] data;
            Pols? person;
            bool IsFirs = true;
            while (true)
            {

                    data = server.Receive(ref remoteEP);
                    //переделываем переданые биты в стринги
                    jsonm = Encoding.UTF8.GetString(data);
                    //распаковываем json пакетик в объект
                    person = JsonSerializer.Deserialize<Pols>(jsonm);
                    person.Pr(); //DELETE
                    //проверка при входе на наличие пользователя в бд и правильность пароля
                    if (!person.IsReg)
                    { 
                        byte[] responseData = Encoding.UTF8.GetBytes(IsUser(person.Tag, person.Password));
                        server.Send(responseData, responseData.Length, remoteEP);
                    }
                    if(person.IsReg)
                    {
                        byte[] responseData = Encoding.UTF8.GetBytes(IsNewUser(person.Tag, person.Password));
                        server.Send(responseData, responseData.Length, remoteEP);
                    }
                    IsFirs = false;

                
            }
        }

        
    }
    //функция для проверки наличия пользователя и правильность пароля
    //код "1" - успех, код "01" - неправильный tag, код "02" - неправильный пароль, код "03" - неправильный токен
    private static string IsUser(string tag, string passwordHash)
    {
        return "1";
    }

    //функция для проверки новизны пользователя
    //код "1" - успех, код "04" - такой пользователь уже существует
    private static string IsNewUser(string tag, string passwordhash)
    {
        return "1";
    }
    //класс как шаблон для json пакетиков
    public class Pols
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Password { get; set; }
        public int Token { get; set; }
        public bool IsReg { get; set; }

        public void Pr()
        {
            Console.WriteLine(Name + "\n" + Tag + "\n" + Password + "\n" + Token + "\n" + IsReg);
        }
    }
        //data = server.Receive(ref remoteEP);
                            ////переделываем переданые биты в стринги
                            //jsonm = Encoding.UTF8.GetString(data);
                            ////распаковываем json пакетик в объект
                            //person = JsonSerializer.Deserialize<Pols>(jsonm);
                            //person.Pr(); //DELETE
}
//public async Task Main(string[] args)
//{
//    string jsonm;
//    using (UdpClient server = new UdpClient(55555))
//    {
//        Console.WriteLine("Start");

//        while (true)
//        {
//            UdpReceiveResult result = await server.ReceiveAsync();
//            //
//            jsonm = Encoding.UTF8.GetString(result.Buffer);
//            //распаковываем json пакетик в объект
//            Pols person = JsonSerializer.Deserialize<Pols>(jsonm);
//            person.Pr();
//            //проверка при входе на наличие пользователя в бд и правильность пароля
//            if (!person.IsReg)
//            {
//                byte[] responseData = Encoding.UTF8.GetBytes(IsUser(person.Tag, person.Password));
//                await server.SendAsync(responseData, responseData.Length, result.RemoteEndPoint);
//            }
//        }
//    }


//}
