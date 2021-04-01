using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckLibrary
{
    internal interface IMove
    {
        void Move(Cell to);
        void CutDown(Cell to);
        bool CanMove(Cell to);
        bool CanCutDown(Cell to);
    }
}
