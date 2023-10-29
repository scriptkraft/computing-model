using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputingModel
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = Programs.MakeArray(
                new[] {
                    "SET 0",
                    "CPY B",
                    "SET 0"
                },
                Programs.IsBoolEqual
                );
            var m = new Machine(program);
            var a = m.Registers[0];
        }
    }
}
