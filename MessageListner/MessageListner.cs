using System;

namespace MessageListner
{
    public class MessageListner
    {
        public static void Main()
        {
            var listener = new MSMQListener(@".\Private$\myqueue");
            listener.MessageReceived += new MessageReceivedEventHandler(listener_MessageReceived);
            listener.Start();
            Console.WriteLine("Read Message");
            Console.ReadLine();
            listener.Stop();
        }

        public static void listener_MessageReceived(object sender, MessageEventArgs args)
        {
            Console.WriteLine(args.MessageBody);
        }
    }
}
