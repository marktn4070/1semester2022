using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1Semprojekt2022_Golf
{
    class Runner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Runner(string name) //igen mangler params
        {
            Name = name;
        }
    }
}
