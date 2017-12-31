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
        }

        public async void JoinChannelConfirmHandlerAsync(object sender, LoginArgs args)
        {
            
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
            args.Events.LoginEventHandler -= LoginConfirmHandlerAsync;
            Console.WriteLine("Removed handler");
        }
    }
}