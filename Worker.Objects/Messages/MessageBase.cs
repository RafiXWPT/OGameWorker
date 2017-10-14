using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker.Objects.Messages
{
    public enum MessageType
    {
        Espionage = 0,
        WarRepor = 1,
        Transport = 3,
        Other = 4 
    }

    public enum MessageStatus
    {
        New,
        Readed
    }

    public abstract class MessageBase
    {
        public int MessageId { get; }
        public DateTime MessageDate { get; }
        public MessageType MessageType { get; }
        public MessageStatus MessageStatus { get; set; }

        protected MessageBase(int messageId, DateTime messageDate, MessageType type, MessageStatus status)
        {
            MessageId = messageId;
            MessageDate = messageDate;
            MessageType = type;
            MessageStatus = status;
        }
    }
}
