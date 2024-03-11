using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher
{
    internal class Game
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string Cover { get; set; }
        public string Background { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
