using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MathSnake
{
    class Barrier
    {
        private int _length;
        public int Lenght
        {
            get
            {
                return _length;
            }
            set
            {
                _length = value;
            }
        }
        public Barrier(int length)
        {
            Lenght = length;
        }
    }
}
