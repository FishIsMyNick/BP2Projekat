using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    public static class ClassPropertyValueFactory
    {
        public static ClassPropertyValue Create(string name, object value)
        {
            return new ClassPropertyValue(name, value);
        }
        public static ClassPropertyValue Create<T>(PropertyInfo propertyInfo, T obj)
        {
            return new ClassPropertyValue(propertyInfo.Name, propertyInfo.GetValue(obj.GetType()));
        }
        public static List<ClassPropertyValue> CreateList<T>(PropertyInfo[] propertyInfos, T obj)
        {
            List<ClassPropertyValue> list = new List<ClassPropertyValue>();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                list.Add(Create(propertyInfo, obj));
            }
            return list;
        }
    }
}
