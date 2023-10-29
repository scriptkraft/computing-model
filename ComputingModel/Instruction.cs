using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputingModel
{
    struct Instruction
    {
        public Command Command { get; }

        public int Value { get; }

        public Instruction(Command cmd, int val)
        {
            Command = cmd;
            Value = val;
        }
    }
}
