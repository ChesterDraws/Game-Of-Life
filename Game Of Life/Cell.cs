using System;
using System.Collections.Generic;
using System.Text;

namespace Game_Of_Life
{
    class Cell
    {

        public int _num_neighbours { get; }
        private bool _alive;

        private int _x;
        private int _y;


        public Cell(bool alive,int x,int y)
        {
            _alive = alive;
            _x = x;
            _y = y;

        }

        public void Calculate_Neigbours()
        {

        }

        public void Set_Cell_To_Alive()
        {
            _alive = true;
        }
        public void Set_Cell_To_Dead()
        {
            _alive = false;
        }

        public bool Is_Cell_Alive()
        {
            return _alive;
        }

        public int Get_X()
        {
            return _x;
        }
        public int Get_Y()
        {
            return _y;
        }

    }
}
