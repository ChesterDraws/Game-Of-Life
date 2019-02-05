using System;

namespace Game_Of_Life
{
    class Program
    {
        private static Grid _grid;

        static void Main(string[] args)
        {
            bool pause = false;
            String user_input = "";
            Setup();
            _grid.Iterate();

            Console.CancelKeyPress += (sender, e) =>{pause = true;};

            while (user_input != "Exit")
            {
                if (pause)
                {
                    Console.CancelKeyPress += (sender, e) => { pause = false; };
                    user_input = Console.ReadLine();
                    switch (user_input)
                    {
                        case "I":
                            _grid.Iterate();
                            break;
                        case "R":
                            Setup();
                            _grid.Iterate();
                            break;
                        case "Exit":
                            break;
                        default:
                            Console.WriteLine("Invalid Input");
                            break;


                    }
                else
                {
                   
                    }
                }
            }

            Display_Options();
           

            switch (user_input)
            {
                case "I":
                    _grid.Iterate();
                    break;
                case "R":
                    Setup();
                    _grid.Iterate();
                    break;
                case "Exit":
                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    break;
            }

        }

        private static void Setup()
        {
            int start_pop;
            int start_size;

            Console.WriteLine("Welcome to the game of life!!\n" +
                              "Please input the size of the grid: ");

            String grid_size = Console.ReadLine();

            while (!Int32.TryParse(grid_size, out start_size))
            {
                Console.WriteLine("Please enter a valid integer");
                grid_size = Console.ReadLine();

            }

            Console.WriteLine("Please input the number of starting live cells.\n" +
                              "(If this number is greater than the size of the grid times 2," +
                              "the entire grid will be populated). ");

            String start_population = Console.ReadLine();

            while (!Int32.TryParse(start_population, out start_pop))
            {
                Console.WriteLine("Please enter a valid integer");
                start_population = Console.ReadLine();
            }

            _grid = new Grid(start_size, start_pop);

        }

        private static void Display_Options()
        {
            Console.WriteLine("\nOptions:\n" +
                              "I: Next Iteration\n" +
                              "R: Restart Game\n" +
                              "Exit: Close Game");
        }
    }
}
