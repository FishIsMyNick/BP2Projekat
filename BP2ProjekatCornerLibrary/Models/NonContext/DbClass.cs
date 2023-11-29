using BP2ProjekatCornerLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models.NonContext
{
    public class _DbClass
    {
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
