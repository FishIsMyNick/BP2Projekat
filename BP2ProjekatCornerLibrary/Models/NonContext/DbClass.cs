using BP2ProjekatCornerLibrary.Helpers;
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

        public object? GetPropertyValue(string name)
        {
            Type type = this.GetType();
            PropertyInfo propInfo = type.GetProperty(name);
            if (propInfo == null)
                return null;

            object s =  propInfo.GetValue(this);
            return s;
        }
        public _DbClass()
        {

        }

        /// <summary>
        /// Self-setting parametered constructor
        /// </summary>
        /// <param name="args" type="ClassPropertyValue">Name-Value pair class</param>
        //public DbClass(params ClassPropertyValue[] args)
        //{
        //    var props = GetType().GetProperties();
        //    Type type = GetType();

        //    DbClass dbClass;

        //    foreach (ClassPropertyValue arg in args)
        //    {
        //        if (DBHelper.CheckDbNotNull(arg.Value))
        //            type.GetProperty(arg.Name).SetValue(this, arg.Value);

        //    }
        //}
    }
}
