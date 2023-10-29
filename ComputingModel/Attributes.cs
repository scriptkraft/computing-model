using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputingModel
{
    [AttributeUsage(AttributeTargets.Method)]
    sealed class CommandSymbolAttribute : Attribute
    {
        public Command Command { get; }

        public CommandSymbolAttribute(Command command)
        {
            Command = command;
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    sealed class EnumValueAliasAttribute : Attribute
    {
        public string Alias { get; }

        public EnumValueAliasAttribute(string alias)
        {
            Alias = alias;
        }
    }
}
