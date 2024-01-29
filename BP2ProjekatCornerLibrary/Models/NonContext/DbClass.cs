using BP2ProjekatCornerLibrary.Helpers.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models.NonContext
{
    public abstract class _DbClass
    {
        public virtual ClassPropertyValue GetKeyIdentity() { return null; }
        public abstract List<ClassPropertyValue> GetKeyProperties();
        public abstract List<string> GetDbPropertyNames();

        public object? GetPropertyValue(string propertyName)
        {
            Type type = GetType();
            PropertyInfo propInfo = type.GetProperty(propertyName);
            if (propInfo == null)
                return null;

            object val = propInfo.GetValue(this);
            return val;
        }
        public _DbClass() { }
    }
}
