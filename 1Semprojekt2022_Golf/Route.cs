using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1Semprojekt2022_Golf
{
    class Route
    {
        public int Length { get; set; }
        public string Name { get; set; }
        public Route(int length, string name)//måske flere params? 
        {
            Length = length;
            Name = name;
        }
    }
}
