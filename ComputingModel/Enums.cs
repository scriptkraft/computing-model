using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputingModel
{
    enum Register
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        [EnumValueAlias("GPRMax")]
        H,
        /// <summary>
        /// Program Counter
        /// </summary>
        PC,
        /// <summary>
        /// Instruction Command
        /// </summary>
        IC,
        /// <summary>
        /// Instruction Value
        /// </summary>
        IV,
    }

    enum Command
    {
        LOD,
        STR,
        SET,
        ADD,
        MLT,
        CMP,
        CPY,
        JMP
    }
}
