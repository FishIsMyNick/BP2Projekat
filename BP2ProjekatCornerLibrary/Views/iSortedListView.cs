using BP2ProjekatCornerLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BP2ProjekatCornerLibrary.Views
{
    public interface iSortedListView
    {
        public List<Image> Arrows { get; set; }
        public void DisableAllArrows();
        public void SetArrow(Image arrow, bool ascending);
    }

}
