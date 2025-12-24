using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Messenger
{
    public static class MessageHandler
    {
        public static byte[] ConvertMessage(IMessage message)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };

            string json = JsonConvert.SerializeObject(new MessageContainer(message), settings);

            return Encoding.UTF8.GetBytes(json);
        }

        public static IMessage HandleMessage(byte[] bytes)
        {
            var json = Encoding.UTF8.GetString(bytes);

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };

            return JsonConvert.DeserializeObject<MessageContainer>(json, settings).Message;
        }
    }
}
