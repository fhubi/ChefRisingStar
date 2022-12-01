using System;
using System.Collections.Generic;
using System.Text;

namespace ChefRisingStar.Models
{
    public class IntStringKey
    {
        public int IntValue { get; set; }

        public string StringValue { get; set; }

        public IntStringKey(int intValue, string stringValue)
        {
            IntValue = intValue;
            StringValue = stringValue;
        }   
    }
}
