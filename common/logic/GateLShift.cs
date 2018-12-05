using System;
using System.Drawing;
using System.Collections.Generic;
using Aoc.Common.Simulators;

namespace Aoc.Common.Logic
{
    public class GateLShift : Gate
    {
        public string A { get; set; }

        public UInt16 B { get; set; }

        public override void Activate(Registers<UInt16> r)
        {
            r[Output] = (UInt16)(r[A] << B);
        }

        public override string[] GetInputs()
        {
            return new string[] { A };
        }
    }
}
