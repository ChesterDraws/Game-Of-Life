using System;
using System.Collections.Generic;
using System.Text;

namespace Game_Of_Life
{
    class Grid
    {
        private int _generation_number;
        private Cell[,] _cells;
        private int _grid_size;
        private static int GRID_SIZE = 40;


        public Grid(int initial_live_cells)
        {


            _generation_number = 0;
            if (initial_live_cells > GRID_SIZE * GRID_SIZE)
                initial_live_cells = GRID_SIZE * GRID_SIZE;

            this._grid_size = GRID_SIZE;
            Create_Grid();
            Generate_Initial_Live_Cells(initial_live_cells);


        }

        /// <summary>
        /// Computes the next generation and returns the state of the grid in a string.
        /// 
        /// </summary>
        /// <returns></returns>
        public string Iterate_And_Return_State()
        {
            Process_Grid_Iteration();
            _generation_number++;
            return Draw_Grid() + "\nGeneration Number: " + _generation_number;
        }


        private void Create_Grid()
        {
            this._cells = new Cell[this._grid_size, this._grid_size];

            for (int i = 0; i < this._grid_size; i++)
            {
                for (int j = 0; j < this._grid_size; j++)
                {
                    this._cells[i, j] = new Cell(false, i, j);
                }
            }



        }

        private void Generate_Initial_Live_Cells(int initial_live_cells)
        {
            for (int i = 0; i < initial_live_cells; i++)
            {
                Position_At_Random();
            }
        }

        private void Position_At_Random()
        {
            Random rnd = new Random();
            Cell cell = _cells[rnd.Next(0, _cells.GetLength(0)), rnd.Next(0, _cells.GetLength(1))];
            if (cell.Is_Cell_Alive())
            {
                Position_At_Random();
            }
            else
            {
                cell.Set_Cell_To_Alive();
            }



        }

        private void Process_Grid_Iteration()
        {
            Grid temp_grid = Copy_Cells();

            for (int i = 0; i < _grid_size; i++)
            {
                for (int j = 0; j < _grid_size; j++)
                {
                    int y = Calculate_Cell_Neighbours(_cells[i, j]);
                    Determine_Cell_Action(temp_grid._cells[i, j], y);
                }
            }
            this._cells = temp_grid._cells;
        }

        /// <summary>
        /// Copys the cells from one Grid object to another.
        /// 
        /// </summary>
        /// <returns></returns>
        private Grid Copy_Cells()
        {
            Grid temp = new Grid(0);

            for (int i = 0; i < _grid_size; i++)
            {
                for (int j = 0; j < _grid_size; j++)
                {
                    temp._cells[i, j] = new Cell(_cells[i, j].Is_Cell_Alive(), _cells[i, j].Get_X(), _cells[i, j].Get_Y());
                }
            }

            return temp;
        }



        /// <summary>
        /// Checks where or not a position is within the grid
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool InBounds(int x, int y)
        {
            return x >= 0 && y >= 0 && x < _grid_size && y < _grid_size;

        }



        /// <summary>
        /// Calculates the number of a neighbours a given cell has
        /// 
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private int Calculate_Cell_Neighbours(Cell cell)
        {
            int neighbours = 0;

            int cell_x = cell.Get_X();
            int cell_y = cell.Get_Y();



            for (int i = cell_x - 1; i < cell_x + 2; i++)
            {
                for (int j = cell_y - 1; j < cell_y + 2; j++)
                {
                    if (InBounds(i, j))
                    {
                        if (_cells[i, j].Is_Cell_Alive() && !_cells[i, j].Equals(cell))
                            neighbours++;
                    }
                }
            }

            return neighbours;
        }


        /// <summary>
        /// Returns a string of the current state of the grid.
        /// 
        /// </summary>
        private String Draw_Grid()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this._grid_size; i++)
            {
                for (int j = 0; j < this._grid_size; j++)
                {
                    if (this._cells[i, j].Is_Cell_Alive())
                    {
                        sb.Append("[\u2022]");
                    }
                    else
                    {
                        sb.Append("[ ]");

                    }
                }
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }


        /// <summary>
        /// Switch statement to determine the action of the cell.
        /// 
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="neighbours"></param>
        private void Determine_Cell_Action(Cell cell, int neighbours)
        {
            switch (neighbours)
            {

                case 0:
                case 1:
                    // Underpopulation
                    cell.Set_Cell_To_Dead();
                    break;
                case 2:
                    // Survival
                    break;
                case 3:
                    // Creation of Life
                    if (!cell.Is_Cell_Alive())
                        cell.Set_Cell_To_Alive();

                    // Survival
                    break;
                case var x when (neighbours > 3):
                    // Overcrowding
                    if (cell.Is_Cell_Alive())
                        cell.Set_Cell_To_Dead();
                    break;



            }
        }



    }
}
