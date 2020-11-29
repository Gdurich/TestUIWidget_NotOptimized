using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TestUIwidget.Models
{
    public class GridRow
    {
        public bool IsEven { get; set; }
        public double TableWidth { get; set; }
        public ObservableCollection<GridItem> RowItems { get; set; }
        public ObservableCollection<bool> Risks { get; set; }
    }
}
