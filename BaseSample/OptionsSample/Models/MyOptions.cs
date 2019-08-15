﻿using Microsoft.Extensions.Options;

namespace OptionsSample.Models
{
    public class MyOptions
    {
        public MyOptions()
        {
            Option1 = "value1_from_ctor";
        }
        
        public string Option1 { get; set; }

        public int Option2 { get; set; } = 5;
    }
}