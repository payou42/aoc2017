using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    public class Day201708 : Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }
        
        private Cpu _cpu;

        private string[] _instructions;

        private Int64 _highest;

        public Day201708()
        {            
            Codename = "2017-08";
            Name = "I Heard You Like Registers";
            _instructions = Input.GetStringVector(this);
            _cpu = new Cpu(-1);
            _cpu.OnExecute += this.Execute;
            _highest = Int64.MinValue;
            CpuState state = CpuState.Running;
            while (state == CpuState.Running)
            {
                state = _cpu.Execute(_instructions);
            }
        }

        public string Run(Part part)
        {
            if (part == Part.Part1)
            {
                return _cpu.Registers.GetLargest().ToString();
            }

            if (part == Part.Part2)
            {
                return _highest.ToString();
            }

            return "";
        }

        private bool CheckCondition(string register, string op, Int64 value)
        {
            Int64 r = _cpu.Registers[register];
            switch (op)
            {
                case "==": return r == value;
                case "!=": return r != value;
                case ">": return r > value;
                case "<": return r < value;
                case ">=": return r >= value;
                case "<=": return r <= value;
                default: return true;
            }
        }

        private void ApplyOperation(string register, string op, Int64 value)
        {
            switch (op)
            {
                case "inc":
                {
                    _cpu.Registers[register] += value;                    
                    return;
                }

                case "dec":
                {
                    _cpu.Registers[register] -= value;
                    return;
                }
            }
        }

        private CpuState Execute(Cpu cpu, string[] instruction)
        {
            if (CheckCondition(instruction[4], instruction[5], Int64.Parse(instruction[6])))
            {
                ApplyOperation(instruction[0], instruction[1], Int64.Parse(instruction[2]));
                _highest = Math.Max(_highest, _cpu.Registers.GetLargest());
            }
            _cpu.Counters[instruction[1]]++;
            _cpu.Registers["ip"]++;
            return CpuState.Running;
        }
    }        
}