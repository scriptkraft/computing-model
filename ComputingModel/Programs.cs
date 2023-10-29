using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputingModel
{
    /// <summary>
    /// Holds a fixed and immutable list of instructions to be executed by the machine when it starts. Size limited by size of a register. Meaning the number of instructions the program can have is limited to what a register's value can be.
    /// </summary>
    class Programs
    {
        public static string[] MakeArray(params string[][] arrays)
        {
            return arrays.SelectMany(a => a).ToArray();
        }

        public static readonly string[] AbsoluteValue = new[] {
            "CPY B",
            "SET 0",
            "CPY C",
            "ADD B",
            "CMP C",
            "MLT B"
        };

        /// <summary>
        /// Puts the boolean equivalent 0 or 1 of the value in register A back into register A
        /// </summary>
        public static readonly string[] MakeBool = new[] {
            "CPY B",
            "SET 0",
            "CMP B",
            "CPY B",
            "SET 0",
            "ADD B",
            "MLT B"
        };

        /// <summary>
        /// Given a 0 or 1 in register A, inverts it in A
        /// </summary>
        public static readonly string[] NegateBool = new[]
        {
            "CPY B",
            "SET -1",
            "ADD B",
            "CPY B",
            "MLT B"
        };

        /// <summary>
        /// Given a 0 or 1 in register A and B, determines their equality it in A
        /// </summary>
        public static readonly string[] IsBoolEqual = MakeArray(
            new[]
            {
                "CMP B",
                "CPY B",
                "MLT B"
            },
            NegateBool
            );
    };
}
