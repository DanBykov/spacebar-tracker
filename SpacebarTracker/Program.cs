using System.Runtime.InteropServices;
using System.Diagnostics;
using WindowsInput;
using WindowsInput.Native;

namespace SpacebarTracker
{
    class Program
    {
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vKey);

        const int VK_SPACE = 0x20;
        const int ESCAPE = 0x1B;

        static async Task Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            Console.WindowWidth = 50;
            Console.WindowHeight = 5;

            Console.WriteLine("Press ESC to stop the program.");

            Random random = new Random();

            while (true)
            {
                if (GetAsyncKeyState(ESCAPE) != 0)
                {
                    break;
                }

                if (GetAsyncKeyState(VK_SPACE) != 0)
                {
                    // Catch the app
                    Process[] ps = Process.GetProcessesByName("NWMAIN");
                    Process p = ps?.FirstOrDefault();

                    if (p != null)
                    {
                        InputSimulator isim = new InputSimulator();

                        int counter = random.Next(5, 8);
                        for (int i = 0; i < counter; i++)
                        {
                            isim.Keyboard.KeyPress(VirtualKeyCode.F1);
                            int sleep_ms = random.Next(40, 90);
                            Thread.Sleep(sleep_ms);
                            isim.Mouse.LeftButtonClick();

                            int delay_ms = random.Next(120, 171);
                            await Task.Delay(delay_ms); // Adding a small delay between keypresses to avoid overwhelming the target application
                        }
                    }
                    else
                    {
                        Console.WriteLine("NWMAIN not found.");
                    }
                    await Task.Delay(10); // To avoid high CPU usage
                }
            }
        }
    }
}