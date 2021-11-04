using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleConsole
{
    public interface ISolver
    {
        public string[] Solve(string[] puzzle);
    }
}
