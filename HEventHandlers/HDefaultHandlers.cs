using System;
using System.Linq;
using System.Threading.Tasks;
using ChatProtos.Data;
using ChatProtos.Networking;
using ChatProtos.Networking.Messages;
using CoreClient.HMessageArgs;
using Google.Protobuf;

namespace CoreClient.HEventHandlers
{
    public class HDefaultHandlers
    {
        public void RegisterHandlers(HEvents events)
        {
            events.LoginEventHandler += LoginConfirmHandlerAsync;
            events.LoginEventHandler += JoinLoginConfirmHandlerAsync;
            events.LoginEventHandler += LoginTest5000;
            events.LoginEventHandler += LoginTest2000;
        }

        private async void LoginTest2000(object sender, LoginArgs args)
        {
            Console.WriteLine("EventHandler on LoginEvent two");
            await Task.Delay(2000);
            Console.WriteLine("EventHandler on LoginEvent after sleep 2000 two");
        }

        private async void LoginTest5000(object sender, LoginArgs args)
        {
            Console.WriteLine("EventHandler on LoginEvent one");
            await Task.Delay(5000);
            Console.WriteLine("EventHandler on LoginEvent after sleep 5000 one");
        }

        public async void LoginConfirmHandlerAsync(object sender, LoginArgs args)
        {
            await Task.Yield();
            Console.WriteLine("EventHandler on LoginEvent async ");
            Random rand = new Random();
            int[] array = Enumerable.Repeat(0, 100).Select(i => rand.Next(0, 10000)).ToArray();
            foreach (var numb in array)
            {
                Console.WriteLine(numb);
            }
            Console.WriteLine("EventHandler on LoginEvent after async ");
            args.Handlers.LoginEventHandler -= LoginConfirmHandlerAsync;
            Console.WriteLine("Removed handler");
        }

        public async void JoinLoginConfirmHandlerAsync(object sender, LoginArgs args)
        {
            await Task.Yield();
            var message = new RequestMessage
            {
                Type = RequestType.ChatMessage,
                Message = new ChatMessageRequest
                {
                    ChannelId = "0",
                    Message = new ChatMessage
                    {
                        Text = "Test!"
                    }
                }.ToByteString()
            };
            await args.Connection.SendAync(message);
            args.Handlers.LoginEventHandler -= JoinLoginConfirmHandlerAsync;
        }
    }
}