using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Messenger
{
    public interface IMessage
    {
        string Type { get; }
    }

    [JsonObject(ItemTypeNameHandling = TypeNameHandling.Auto)]
    public class MessageContainer
    {
        public IMessage Message { get; set; }

        public MessageContainer(IMessage message)
        {
            Message = message;
        }
    }
}
