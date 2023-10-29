using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputingModel
{
    static class Instructions
    {
        private static readonly int A = (int)Register.A;

        /// <summary>
        /// Loads the value at the memory location held by register A into register X (It interprets the value in register A as a memory location)
        /// </summary>
        [CommandSymbol(Command.LOD)]
        public static void Load(this Machine m, int x)
        {
            m.Registers[x] = m.Memory[m.Registers[A]];
        }

        /// <summary>
        /// Stores the value in register X at the memory location held by register A
        /// </summary>
        [CommandSymbol(Command.STR)]
        public static void Store(this Machine m, int x)
        {
            m.Memory[m.Registers[A]] = m.Registers[x];
        }

        /// <summary>
        /// Sets the value of register A to the value X
        /// </summary>
        [CommandSymbol(Command.SET)]
        public static void Set(this Machine m, int x)
        {
            m.Registers[A] = x;
        }

        /// <summary>
        /// Adds the value in register X to the value in register A.
        /// </summary>
        [CommandSymbol(Command.ADD)]
        public static void Add(this Machine m, int x)
        {
            m.Registers[A] += m.Registers[x];
        }

        /// <summary>
        /// Multiplies the value in register X with the value in register A and stores the result in register A.
        /// </summary>
        [CommandSymbol(Command.MLT)]
        public static void Mult(this Machine m, int x)
        {
            m.Registers[A] *= m.Registers[x];
        }

        /// <summary>
        /// Compares the value in register X to the value in register A and stores -1, 0, or 1 in register A depending on whether value in A is less than, equal to, or greater than value in X, respectively.
        /// </summary>
        [CommandSymbol(Command.CMP)]
        public static void Comp(this Machine m, int x)
        {
            m.Registers[A] = Math.Sign(m.Registers[A] - m.Registers[x]);
        }

        /// <summary>
        /// Copies the value of register A into register X
        /// </summary>
        [CommandSymbol(Command.CPY)]
        public static void Copy(this Machine m, int x)
        {
            m.Registers[x] = m.Registers[A];
        }

        /// <summary>
        /// Jumps to the instruction location in register X if value in register A is not 0.
        /// </summary>
        [CommandSymbol(Command.JMP)]
        public static int? Jump(this Machine m, int x)
        {
            if (m.Registers[A] == 0)
                return null;
            else
                return m.Registers[x];
        }
    }
}
