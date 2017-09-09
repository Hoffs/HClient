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
        private HConsoleProcessor hProcessor = new HConsoleProcessor();
        public bool Running { get; set; } = false;

        public HConsole()
        {
            inputReader = Console.In;
            outputWriter = Console.Out;
        }

        public async Task StartReadingInputTask()
        {
            Running = true;
            
            while (Running)
            {
                var input = await inputReader.ReadLineAsync();
                var response = await hProcessor.ProcessMessageTask(input);
                await WriteToConsoleTask(response);
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
