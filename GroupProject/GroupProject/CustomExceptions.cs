﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject
{
    public class InputRequiredException : Exception
    {
        public InputRequiredException(string message) : base(message)
        {
            
        }
    }
}
