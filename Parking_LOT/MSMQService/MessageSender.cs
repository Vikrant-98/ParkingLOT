using Experimental.System.Messaging;

namespace Parking_LOT.MSMQService
{
    public class MessageSender
    {
        /// <summary>
        /// SMTP service to send message through 
        /// messageQueue
        /// </summary>
        /// <param name = "MessageToBeSend" ></ param >
        public void Message(string emailMessage)
        {
            MessageQueue MyQueue;
            if (MessageQueue.Exists(@".\Private$\myqueue"))
            {
                MyQueue = new MessageQueue(@".\Private$\myqueue");
            }
            else
            {
                MyQueue = MessageQueue.Create(@".\Private$\myqueue");
            }
            Message MyMessage = new Message
            {
                Formatter = new BinaryMessageFormatter(),
                Body = emailMessage,
                Label = "Registration",
                Priority = MessagePriority.Normal
            };
            MyQueue.Send(MyMessage);
        }
    }
}
