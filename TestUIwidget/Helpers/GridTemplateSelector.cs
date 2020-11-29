using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TestUIwidget.Models;

namespace TestUIwidget.Helpers
{
    public class GridTemplateSelector : DataTemplateSelector
    {
        public DataTemplate FirstTemplate { get; set; }
        public DataTemplate SecondTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var template = item as GridItem;
            if (template != null && template.IsTriangleVisible)
                return FirstTemplate;
            return SecondTemplate;
        }

    }
}
