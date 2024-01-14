using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BP2ProjekatCornerLibrary.Helpers
{
    public static class ArrowHelper
    {
        public static void DisableAllArrows(List<Image> arrows)
        {
            foreach (Image image in arrows) image.Source = null;
        }
        public static void SetArrow(Image arrow, bool ascending)
        {
            arrow.Source = ascending ? new BitmapImage(new Uri(@"..\..\..\Images\arrow_down.png", UriKind.Relative)) : new BitmapImage(new Uri(@"..\..\..\Images\arrow_up.png", UriKind.Relative));
        }


        //public static List<Image> FindAllArrows(this DependencyObject parent)
        //{
        //    var elements = new List<Image>();

        //    FindElementsRecursive(parent, "img_sort", elements);

        //    return elements;
        //}
        //private static void FindElementsRecursive(DependencyObject parent, string partialName, List<Image> elements)
        //{
        //    if (parent == null)
        //        return;

        //    int childCount = VisualTreeHelper.GetChildrenCount(parent);
        //    for (int i = 0; i < childCount; i++)
        //    {
        //        var child = VisualTreeHelper.GetChild(parent, i) as Image;
        //        if (child != null && child.Name != null && child.Name.Contains(partialName))
        //        {
        //            elements.Add(child);
        //        }

        //        FindElementsRecursive(child, partialName, elements);
        //    }
        //}
    }
}
