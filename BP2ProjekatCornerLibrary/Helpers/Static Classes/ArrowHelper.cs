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

    }
}
