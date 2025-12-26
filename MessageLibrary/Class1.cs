using Newtonsoft.Json;
using System;
using System.Text;

namespace CommunicationLib
{
    [JsonObject(ItemTypeNameHandling = TypeNameHandling.Auto)]
    public interface IMessage
    {

    }
    public class MessageContainer
    {
        public IMessage Meaasge { get; set; }
    }
    public static class MessageHandler
    {
        public static byte[] ConvertMessage(IMessage message)
        {
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Auto,
            };

            string json = JsonConvert.SerializeObject(new MessageContainer { Meaasge = message }, settings);

            return Encoding.UTF8.GetBytes(json);
        }

        public static IMessage HandleMessage(byte[] bytes)
        {
            var json = Encoding.UTF8.GetString(bytes);

            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Auto,
            };

            return JsonConvert.DeserializeObject<MessageContainer>(json, settings).Meaasge;
        }
    }

    public class RegPols : IMessage
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Password { get; set; }
        public void Pr()
        {
            Console.WriteLine(Name + "\n" + Tag + "\n" + Password + "\n");
        }
    }
    public class InsPols : IMessage
    {
        public string Tag { get; set; }
        public string Password { get; set; }
        public void Pr()
        {
            Console.WriteLine(Tag + "\n" + Password + "\n");
        }
    }
    public class AnswerServer : IMessage
    {
        public string Code { get; set; }
        public string Token { get; set; }
    }
    public class UserMessage : IMessage
    {
        public string Tag { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}
