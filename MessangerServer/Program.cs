using CommunicationLib;
using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

class Server
{
    public static void Main(string[] args)
    {
        string token = GenerateUserToken();
        using (UdpClient server = new UdpClient(55555))
        {
            Console.WriteLine("Start");
            //создаём конечную точку
            IPEndPoint responceEP = new IPEndPoint(IPAddress.Any, 0);
            IMessage ansCl = null;
            AnswerServer retCl = null;

            while (true)
            {
                ansCl = MessageHandler.HandleMessage(server.Receive(ref responceEP));
                if (ansCl is RegPols)
                {
                    RegPols newr = (RegPols)ansCl;
                    newr.Pr();
                    string Code = IsNewUser(newr.Tag);
                    if (Code == "1")
                    {
                        string hashPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(newr.Password, BCrypt.Net.HashType.SHA384); 
                        string userToken = GenerateUserToken(); // Уникальный токен
                        AddPolsBD(newr.Name, newr.Tag, hashPassword, userToken);
                        retCl = new AnswerServer
                        {
                          Code = "1",
                          Token = userToken
                        };
                        byte[] data = MessageHandler.ConvertMessage(retCl);
                        server.Send(data, data.Length, responceEP);
                    }
                    else
                    {
                        retCl = new AnswerServer
                        {
                            Code = Code
                        };
                        byte[] data = MessageHandler.ConvertMessage(retCl);
                        server.Send(data, data.Length, responceEP);
                    }
                    
                }
                if (ansCl is InsPols)
                {
                    InsPols insp = (InsPols)ansCl;
                    insp.Pr();
                    string hashPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(insp.Password, BCrypt.Net.HashType.SHA384); 
                    string Code = IsUser(insp.Tag, hashPassword);
                    UpdateToken(token, insp.Tag);
                    if (Code == "1")
                    {
                        retCl = new AnswerServer
                        {
                            Code = "1",
                            Token = token
                        };
                        byte[] data = MessageHandler.ConvertMessage(retCl);
                        server.Send(data, data.Length, responceEP);
                    }
                    else
                    {
                        retCl = new AnswerServer
                        {
                            Code = Code
                        };
                        byte[] data = MessageHandler.ConvertMessage(retCl);
                        server.Send(data, data.Length, responceEP);
                    }
                }
                if(ansCl is UserMessage)
                {
                    UserMessage message = (UserMessage)ansCl;
                    Console.WriteLine($"{message.Tag} \n{message.Token} \n{message.Message}");
                    string Code = IsEnUser(message.Tag, message.Token);
                    if (Code == "1")
                    {
                        AddMessageDB(message.Tag, message.Message);
                        retCl = new AnswerServer
                        {
                            Code = "1",
                            Token = token
                        };
                        byte[] data = MessageHandler.ConvertMessage(retCl);
                        server.Send(data, data.Length, responceEP);
                    }
                    else
                    {
                        retCl = new AnswerServer
                        {
                            Code = Code
                        };
                        byte[] data = MessageHandler.ConvertMessage(retCl);
                        server.Send(data, data.Length, responceEP);
                    }
                }
            }
            
        }


    }
    private static SqlConnection GetConnection()
    {
        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=MessangerDb;Trusted_Connection=true;TrustServerCertificate=true;";
        // или для SQL Auth: "Server=localhost;Database=YourDatabase;User Id=user;Password=pass;";
        return new SqlConnection(connectionString);
    }
    //функция для проверки наличия пользователя и правильность пароля
    //код "1" - успех, код "01" - неправильный tag, код "02" - неправильный пароль, код "03" - неправильный токен
    private static string IsUser(string tag, string passwordHash)
    {
        try
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string query = @"
                SELECT HashPassword, EndlesToken 
                FROM users 
                WHERE Tag = @Tag";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Tag", tag);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                            return "01"; // Пользователь не найден

                        reader.Read();
                        string dbPasswordHash = reader["HashPassword"].ToString();

                        // Безопасное сравнение паролей
                        if (BCrypt.Net.BCrypt.EnhancedVerify(passwordHash, dbPasswordHash, BCrypt.Net.HashType.SHA384))
                        {
                           Console.WriteLine(passwordHash);
                            Console.WriteLine(dbPasswordHash);
                            return "02"; // Неправильный пароль
                        }

                        return "1"; // Успех
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Ошибка БД в IsUser: {ex.Message}");
            return "00"; // Ошибка БД
        }
    }

    private static string UpdateToken(string newToken, string Tag)
    {
        try
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = @"
                UPDATE users 
                SET EndlesToken = @NewToken 
                WHERE Tag = @Tag";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NewToken", newToken);
                    command.Parameters.AddWithValue("@Tag", Tag);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0 ? "1" : "01";
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Ошибка БД при обновлении токена: {ex.Message}");
            return "00";
        }
    }

    // 05
    private static string IsEnUser(string tag, string token)
    {
        using (SqlConnection connection = GetConnection())
        {
            connection.Open();

            string query = @"
            SELECT EndlesToken 
            FROM users 
            WHERE Tag = @Tag";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Tag", tag);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                        return "01"; // Пользователь не найден

                    reader.Read();
                    string dbToken = reader["EndlesToken"]?.ToString();

                    // Проверка токена
                    if (dbToken != token)
                        return "03"; // Неправильный токен

                    return "1"; // Успех
                }
            }
        }
    }

    //функция для проверки новизны пользователя
    //код "1" - успех, код "04" - такой пользователь уже существует
    private static string IsNewUser(string tag)
    {
        using (SqlConnection connection = GetConnection())
        {
            connection.Open();

            string query = @"
            SELECT COUNT(*) as UserCount 
            FROM users 
            WHERE Tag = @Tag";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Tag", tag);

                int count = (int)command.ExecuteScalar();

                return count > 0 ? "04" : "1"; // "04" - существует, "1" - новый
            }
        }
    }

    private static void AddPolsBD(string name, string tag, string hashPassword, string endlessToken)
    {
        using (SqlConnection connection = GetConnection())
        {
            connection.Open();

            string query = @"
            INSERT INTO users (username, Tag, HashPassword, EndlesToken) 
            VALUES (@Username, @Tag, @HashPassword, @EndlesToken)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", name);
                command.Parameters.AddWithValue("@Tag", tag);
                command.Parameters.AddWithValue("@HashPassword", hashPassword);
                command.Parameters.AddWithValue("@EndlesToken", endlessToken);

                command.ExecuteNonQuery();
            }
        }
    }
    public static void AddMessageDB(string tag, string message)
    {
        // Сначала получаем ID пользователя по тегу
        int userId = GetUserIdByTag(tag);

        if (userId == -1)
        {
            Console.WriteLine("Пользователь не найден");
            return;
        }

        using (SqlConnection connection = GetConnection())
        {
            connection.Open();

            string query = @"
            INSERT INTO message (user_id, Tag, Message) 
            VALUES (@UserId, @Tag, @Message)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@Tag", tag);
                command.Parameters.AddWithValue("@Message", message);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                    Console.WriteLine("Сообщение добавлено: " + message);
                else
                    Console.WriteLine("Ошибка добавления сообщения");
            }
        }
    }

    // Вспомогательный метод для получения ID пользователя
    private static int GetUserIdByTag(string tag)
    {
        using (SqlConnection connection = GetConnection())
        {
            connection.Open();

            string query = "SELECT id FROM users WHERE Tag = @Tag";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Tag", tag);

                object result = command.ExecuteScalar();

                return result != null ? Convert.ToInt32(result) : -1;
            }
        }
    }

    private static string GenerateUserToken()
    {
        return Guid.NewGuid().ToString("N") + "-" +
               BitConverter.ToString(SHA256.Create().ComputeHash(Guid.NewGuid().ToByteArray()))
               .Replace("-", "").Substring(0, 32);
    }
}
