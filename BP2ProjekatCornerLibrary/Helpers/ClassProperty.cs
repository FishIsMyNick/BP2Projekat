using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Helpers
{
    public class ClassPropertyValue
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public ClassPropertyValue(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}
