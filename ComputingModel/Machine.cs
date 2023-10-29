using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Reflection;

namespace ComputingModel
{
    class Machine
    {
        private const int MAX_ITERATIONS_DEFAULT = 10000;
        private const int MEMORY_SIZE = ushort.MaxValue;
        private const int PROGRAM_SIZE_INDEX = 0;

        static Machine()
        {
            MethodByCommand = typeof(Instructions)
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Select(m => (m, a: m.GetCustomAttribute<CommandSymbolAttribute>()))
                .Where(t => t.a != null)
                .ToDictionary(t => t.a.Command, t => t.m);
            GPRMax = (Register)typeof(Register)
                .GetFields()
                .First(f => f.GetCustomAttribute<EnumValueAliasAttribute>()?.Alias == "GPRMax")
                .GetValue(null);
        }

        public Machine(string[] program, int maxIterations = MAX_ITERATIONS_DEFAULT)
        {
            MaxIterations = maxIterations;
            Memory = new int[MEMORY_SIZE];
            Registers = new int[Enum.GetValues(typeof(Register)).Length];
            for (int i = 0; i < Registers.Length; i++)
            {
                Registers[i] = 0;
            }
            Parse(program);
            Execute();
        }

        public int[] Registers { get; set; }

        public int[] Memory { get; set; }

        private static readonly Dictionary<Command, MethodInfo> MethodByCommand;
        private static readonly Register GPRMax;

        private readonly int MaxIterations;


        #region Low Level Operations
        private int ProgramSize
        {
            get
            {
                return Memory[PROGRAM_SIZE_INDEX];
            }
            set
            {
                Memory[PROGRAM_SIZE_INDEX] = value;
            }
        }

        private int ProgramCounter
        {
            get
            {
                return Registers[(int)Register.PC];
            }
            set
            {
                Registers[(int)Register.PC] = value;
            }
        }

        private void CreateInstruction(int index, Command command, int value)
        {
            Memory[PROGRAM_SIZE_INDEX + 2 * index + 1] = (int)command;
            Memory[PROGRAM_SIZE_INDEX + 2 * index + 2] = value;
        }
        private void LoadInstruction()
        {
            Registers[(int)Register.IC] = Memory[PROGRAM_SIZE_INDEX + 2 * ProgramCounter + 1];
            Registers[(int)Register.IV] = Memory[PROGRAM_SIZE_INDEX + 2 * ProgramCounter + 2];
        }
        private Instruction ReadInstruction => new Instruction((Command)Registers[(int)Register.IC], Registers[(int)Register.IV]);
        #endregion

        private void Execute()
        {
            var iteration = 0;
            ProgramCounter = 0;
            while (ProgramCounter < ProgramSize && iteration < MaxIterations)
            {
                LoadInstruction();
                var jump = ExecuteInstruction();
                ProgramCounter = jump ?? ProgramCounter + 1;
                iteration++;
            }
        }

        private void Parse(string[] program)
        {
            ProgramSize = program.Length;
            for (int i = 0; i < program.Length; i++)
            {
                var split = program[i].ToUpper().Split(' ');
                var (command, value) = ((Command)Enum.Parse(typeof(Command), split[0]), split[1]);
                int numVal;
                if (command == Command.SET || command == Command.JMP)
                    numVal = int.Parse(value);
                else
                {
                    numVal = (int)Enum.Parse(typeof(Register), value);
                    if (numVal < (int)Register.A || numVal > (int)GPRMax)
                        throw new Exception("Invalid Register value.");
                }
                CreateInstruction(i, command, numVal);
            }
        }

        private int? ExecuteInstruction()
        {
            var method = MethodByCommand[ReadInstruction.Command];
            return (int?)method.Invoke(null, new object[] { this, ReadInstruction.Value });
        }
    }
}
