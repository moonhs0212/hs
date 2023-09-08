﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.item
{
    public class Integer
    {
        public int Value { get; set; }


        public Integer() { }
        public Integer(int value) { Value = value; }


        // Custom cast from "int":
        public static implicit operator Integer(Int32 x) { return new Integer(x); }

        // Custom cast to "int":
        public static implicit operator Int32(Integer x) { return x.Value; }


        public override string ToString()
        {
            return string.Format("Integer({0})", Value);
        }
    }
}
