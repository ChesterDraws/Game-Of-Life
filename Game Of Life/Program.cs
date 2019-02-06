using System;

namespace Game_Of_Life
{
    class Program
    {
        private static Grid _grid;
        private static bool pause = false;
        private static string user_input = "";
        static void Main(string[] args)
        {

            Init();
            Setup();

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
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            ConsoleHelperFunctions.Disable_Quick_Edit();
            
            ConsoleHelperFunctions.Maximize();

            Console.CancelKeyPress += (sender, e) => { e.Cancel = true; pause = true; };


        }

        /// <summary>
        /// Gets user input and resets the grid.
        /// 
        /// </summary>
        private static void Setup()
        {
            int start_pop;

            Console.WriteLine("Welcome to my implementation of \"Game of Life\"!!!\n");
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





}
