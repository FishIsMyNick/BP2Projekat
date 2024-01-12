using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BP2ProjekatCornerLibrary.Helpers
{
    public static class Sorter
    {
        static object GetPropValue(object obj, string propName)
        {
            // Use reflection to get the property value
            PropertyInfo propInfo = obj.GetType().GetProperty(propName);
            if (propInfo != null)
            {
                return propInfo.GetValue(obj, null);
            }
            else
            {
                return null; // Property not found
            }
        }

        public static List<T> SortText<T>(List<T> toSort, string property, bool ascending)
        {
            if (toSort.Count == 0) return null;
            if ((GetPropValue(toSort[0], property) as string) == null) return null;

            T[] arr = toSort.ToArray();
            T temp;

            if (ascending)
            {
                for (int i = 0; i < toSort.Count - 1; i++)
                {
                    for (int j = i + 1; j < toSort.Count; j++)
                    {
                        if ((GetPropValue(arr[i], property) as string).CompareTo(GetPropValue(arr[j], property) as string) > 0)
                        {
                            temp = arr[i];
                            arr[i] = arr[j];
                            arr[j] = temp;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < toSort.Count - 1; i++)
                {
                    for (int j = i + 1; j < toSort.Count; j++)
                    {
                        if ((GetPropValue(arr[i], property) as string).CompareTo(GetPropValue(arr[j], property) as string) < 0)
                        {
                            temp = arr[i];
                            arr[i] = arr[j];
                            arr[j] = temp;
                        }
                    }
                }
            }
            return arr.ToList<T>();
        }
        public static List<T> SortInt<T>(List<T> toSort, string property, bool ascending)
        {
            if (toSort.Count == 0) return null;
            if ((GetPropValue(toSort[0], property) as int?) == null) return null;

            T[] arr = toSort.ToArray();
            T temp;

            if (ascending)
            {
                for (int i = 0; i < toSort.Count - 1; i++)
                {
                    for (int j = i + 1; j < toSort.Count; j++)
                    {
                        if ((GetPropValue(arr[i], property) as int?) > (GetPropValue(arr[j], property) as int?))
                        {
                            temp = arr[i];
                            arr[i] = arr[j];
                            arr[j] = temp;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < toSort.Count - 1; i++)
                {
                    for (int j = i + 1; j < toSort.Count; j++)
                    {
                        if ((GetPropValue(arr[i], property) as int?) < (GetPropValue(arr[j], property) as int?))
                        {
                            temp = arr[i];
                            arr[i] = arr[j];
                            arr[j] = temp;
                        }
                    }
                }
            }
            return arr.ToList<T>();
        }
        public static List<T> SortDecimal<T>(List<T> toSort, string property, bool ascending)
        {
            if (toSort.Count == 0) return null;
            if ((GetPropValue(toSort[0], property) as decimal?) == null) return null;

            T[] arr = toSort.ToArray();
            T temp;

            if (ascending)
            {
                for (int i = 0; i < toSort.Count - 1; i++)
                {
                    for (int j = i + 1; j < toSort.Count; j++)
                    {
                        if ((GetPropValue(arr[i], property) as decimal?) > (GetPropValue(arr[j], property) as decimal?))
                        {
                            temp = arr[i];
                            arr[i] = arr[j];
                            arr[j] = temp;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < toSort.Count - 1; i++)
                {
                    for (int j = i + 1; j < toSort.Count; j++)
                    {
                        if ((GetPropValue(arr[i], property) as decimal?) < (GetPropValue(arr[j], property) as decimal?))
                        {
                            temp = arr[i];
                            arr[i] = arr[j];
                            arr[j] = temp;
                        }
                    }
                }
            }
            return arr.ToList<T>();
        }
        public static List<T> SortDate<T>(List<T> toSort, string property, bool ascending)
        {
            if (toSort.Count == 0) return null;
            if ((GetPropValue(toSort[0], property) as DateTime?) == null) return null;

            T[] arr = toSort.ToArray();
            T temp;

            if (ascending)
            {
                for (int i = 0; i < toSort.Count - 1; i++)
                {
                    for (int j = i + 1; j < toSort.Count; j++)
                    {
                        if ((GetPropValue(arr[i], property) as DateTime?) > (GetPropValue(arr[j], property) as DateTime?))
                        {
                            temp = arr[i];
                            arr[i] = arr[j];
                            arr[j] = temp;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < toSort.Count - 1; i++)
                {
                    for (int j = i + 1; j < toSort.Count; j++)
                    {
                        if ((GetPropValue(arr[i], property) as DateTime?) < (GetPropValue(arr[j], property) as DateTime?))
                        {
                            temp = arr[i];
                            arr[i] = arr[j];
                            arr[j] = temp;
                        }
                    }
                }
            }
            return arr.ToList<T>();
        }
        public static List<T> SortDateString<T>(List<T> toSort, string property, bool ascending)
        {
            if (toSort.Count == 0) return null;
            if ((GetPropValue(toSort[0], property) as string) == null) return null;

            DateTime? tempDate;
            List<T> sortable = new List<T>();
            List<T> na = new List<T>();
            foreach (T t in toSort)
            {
                if (DateConverter.TryToDateTime((GetPropValue(t, property) as string)) != null)
                    sortable.Add(t);
                else
                    na.Add(t);
            }
            T temp;
            T[] arr = sortable.ToArray();

            if (ascending)
            {
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    for (int j = i + 1; j < arr.Length; j++)
                    {
                        if ((GetPropValue(arr[i], property) as DateTime?) > (GetPropValue(arr[j], property) as DateTime?))
                        {
                            temp = arr[i];
                            arr[i] = arr[j];
                            arr[j] = temp;
                        }
                    }
                }
                return SortText<T>(na, property, ascending).Concat<T>(arr).ToList<T>();
            }
            else
            {
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    for (int j = i + 1; j < arr.Length; j++)
                    {
                        if ((GetPropValue(arr[i], property) as DateTime?) < (GetPropValue(arr[j], property) as DateTime?))
                        {
                            temp = arr[i];
                            arr[i] = arr[j];
                            arr[j] = temp;
                        }
                    }
                }
                return arr.ToList<T>().Concat<T>(SortText<T>(na, property, ascending)).ToList<T>();
            }
        }
        public static List<T> SortGodina<T>(List<T> toSort, string property, bool ascending)
        {
            if (toSort.Count == 0) return null;
            if ((GetPropValue(toSort[0], property) as string) == null) return null;

            List<T> sortable = new List<T>();
            List<T> na = new List<T>();
            foreach (T t in toSort)
            {
                if (int.TryParse((GetPropValue(t, property) as string).Trim('.'), out _))
                    sortable.Add(t);
                else
                    na.Add(t);
            }
            T temp;
            T[] arr = sortable.ToArray();

            if (ascending)
            {
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    for (int j = i + 1; j < arr.Length; j++)
                    {
                        if (int.Parse((GetPropValue(arr[i], property) as string).Trim('.')) > int.Parse((GetPropValue(arr[j], property) as string).Trim('.')))
                        {
                            temp = arr[i];
                            arr[i] = arr[j];
                            arr[j] = temp;
                        }
                    }
                }
                return SortText<T>(na, property, ascending).Concat<T>(arr).ToList<T>();
            }
            else
            {
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    for (int j = i + 1; j < arr.Length; j++)
                    {
                        if (int.Parse((GetPropValue(arr[i], property) as string).Trim('.')) <
                            int.Parse((GetPropValue(arr[j], property) as string).Trim('.')))
                        {
                            temp = arr[i];
                            arr[i] = arr[j];
                            arr[j] = temp;
                        }
                    }
                }
                return arr.ToList<T>().Concat<T>(SortText<T>(na, property, ascending)).ToList<T>();
            }
        }
        public static List<T> SortKolicina<T>(List<T> toSort, string property, bool ascending)
        {
            if (toSort.Count == 0) return null;
            if ((GetPropValue(toSort[0], property) as string) == null) return null;

            List<T> na = new List<T>();
            List<T> sortable = new List<T>();
            foreach (T t in toSort)
            {
                if (!int.TryParse(GetPropValue(t, property) as string, out _))
                    na.Add(t);
                else
                    sortable.Add(t);
            }
            T[] arrSort = sortable.ToArray();
            T temp;

            if (ascending)
            {
                for (int i = 0; i < arrSort.Length - 1; i++)
                {
                    for (int j = i + 1; j < arrSort.Length; j++)
                    {
                        if ((GetPropValue(arrSort[i], property) as string).CompareTo(GetPropValue(arrSort[j], property) as string) > 0)
                        {
                            temp = arrSort[i];
                            arrSort[i] = arrSort[j];
                            arrSort[j] = temp;
                        }
                    }
                }

                return na.Concat<T>(arrSort.ToList<T>()).ToList<T>();

            }
            else
            {
                for (int i = 0; i < arrSort.Length - 1; i++)
                {
                    for (int j = i + 1; j < arrSort.Length; j++)
                    {
                        if ((GetPropValue(arrSort[i], property) as string).CompareTo(GetPropValue(arrSort[j], property) as string) < 0)
                        {
                            temp = arrSort[i];
                            arrSort[i] = arrSort[j];
                            arrSort[j] = temp;
                        }
                    }
                }

                return arrSort.ToList<T>().Concat<T>(na).ToList<T>();
            }
        }
        public static List<T> SortBool<T>(List<T> toSort, string property, bool ascending)
        {
            if (toSort.Count == 0) return null;
            if ((GetPropValue(toSort[0], property) as bool?) == null) return null;

            List<T> tVal = new List<T>();
            List<T> fVal = new List<T>();
            bool? temp;
            foreach (T t in toSort)
            {
                temp = GetPropValue(t, property) as bool?;
                if (temp == true) tVal.Add(t);
                else fVal.Add(t);
            }

            if (ascending) return tVal.Concat<T>(fVal).ToList<T>();
            else return fVal.Concat<T>(tVal).ToList<T>();
        }
        public static List<T> SortBoolInt<T>(List<T> toSort, string property, bool ascending)
        {
            if (toSort.Count == 0) return null;
            if ((GetPropValue(toSort[0], property) as int?) == null) return null;

            List<T> tVal = new List<T>();
            List<T> fVal = new List<T>();
            int? temp;
            foreach (T t in toSort)
            {
                temp = GetPropValue(t, property) as int?;
                if (temp != null && temp != 0) tVal.Add(t);
                else fVal.Add(t);
            }

            if (ascending) return tVal.Concat<T>(fVal).ToList<T>();
            else return fVal.Concat<T>(tVal).ToList<T>();
        }
    }
}
