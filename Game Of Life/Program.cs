using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Game_Of_Life
{
    class Program
    {
        private static Grid _grid;

        static void Main(string[] args)
        {

            Init();
            Console.WriteLine("Welcome to my implementation of \"Game of Life\"!!!\n");
            bool pause = false;
            String user_input = "";
            Setup();

            Console.CancelKeyPress += (sender, e) => { e.Cancel = true; pause = true; };

            while (user_input != "Exit")
            {
                if (pause)
                {
                    Display_Options();
                    user_input = Console.ReadLine();
                    switch (user_input)
                    {
                        case "I":
                            Console.Clear();
                            Console.WriteLine(_grid.Iterate_And_Return_State());
                            break;
                        case "R":
                            Setup();
                            pause = false;
                            break;
                        case "P":
                            pause = false;
                            break;
                        case "Exit":
                            break;
                        default:
                            Console.WriteLine("Invalid Input");
                            break;
                    }
                }
                else
                {
                    System.Threading.Thread.Sleep(500);
                    Console.Clear();
                    Console.WriteLine(_grid.Iterate_And_Return_State()) ;
                    Console.WriteLine("Press CTRL+C to Pause");
                }

            }






        }


        private static void Init()
        {
            DisableConsoleQuickEdit.Go();
            DisableConsoleQuickEdit.Maximize();

        }

        /// <summary>
        /// Gets user input and resets the grid.
        /// 
        /// </summary>
        private static void Setup()
        {
            int start_pop;

            Console.WriteLine("Please input the number of starting live cells: ");

            String start_population = Console.ReadLine();

            while (!Int32.TryParse(start_population, out start_pop))
            {
                Console.WriteLine("Please enter a valid integer");
                start_population = Console.ReadLine();
            }

            _grid = new Grid(start_pop);

        }

        private static void Display_Options()
        {
            Console.WriteLine("\nOptions:\n" +
                              "I: Next Iteration\n" +
                              "P: Resume\n" +
                              "R: Restart Game\n" +
                              "Exit: Close Game");
        }
    }





    /// <summary>
    /// Taken from the following link in order to disable "Quick Edit" in the console.
    /// 
    /// https://stackoverflow.com/questions/13656846/how-to-programmatic-disable-c-sharp-console-applications-quick-edit-mode
    /// 
    /// The function to maximise the window size was found here:
    /// 
    /// https://stackoverflow.com/questions/22053112/maximizing-console-window-c-sharp
    /// </summary>
    static class DisableConsoleQuickEdit
    {

        const uint ENABLE_QUICK_EDIT = 0x0040;

        // STD_INPUT_HANDLE (DWORD): -10 is the standard input device.
        const int STD_INPUT_HANDLE = -10;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll")]
        static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);

        internal static void Maximize()
        {
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3); //SW_MAXIMIZE = 3
        }

        internal static bool Go()
        {
 
            IntPtr consoleHandle = GetStdHandle(STD_INPUT_HANDLE);

            // get current console mode
            uint consoleMode;
            if (!GetConsoleMode(consoleHandle, out consoleMode))
            {
                // ERROR: Unable to get console mode.
                return false;
            }

            // Clear the quick edit bit in the mode flags
            consoleMode &= ~ENABLE_QUICK_EDIT;

            // set the new mode
            if (!SetConsoleMode(consoleHandle, consoleMode))
            {
                // ERROR: Unable to set console mode
                return false;
            }

            return true;
        }
    }
}
