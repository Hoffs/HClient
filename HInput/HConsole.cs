using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace CoreClient.HInput
{
    public class HConsole
    {
        private TextReader inputReader;
        private TextWriter outputWriter;
        private HInputProcessor hProcessor = new HInputProcessor();
        private HClient _hClient;
        public bool Running { get; set; } = false;

        public HConsole(HClient hClient)
        {
            inputReader = Console.In;
            outputWriter = Console.Out;
            _hClient = hClient;
        }

        public async Task StartReadingInputTask()
        {
            Running = true;
            
            while (Running)
            {
                var input = await inputReader.ReadLineAsync();
                var response = await hProcessor.ProcessMessageTask(input);
                await WriteToConsoleTask(response.Type.ToString());
                await _hClient.GetConnection().SendAync(response);
            }
        }


        public async Task WriteToConsoleTask(string data)
        {
            if (Running)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                await outputWriter.WriteLineAsync("[USER]" + data);
                await outputWriter.FlushAsync();
            }
        }
    }
}
