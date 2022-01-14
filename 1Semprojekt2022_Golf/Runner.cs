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
        public Runner(int id) //igen mangler params
        {
            Id = id;
        }
    }
}
