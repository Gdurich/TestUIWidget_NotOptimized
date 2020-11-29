using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TestUIwidget.Models
{
    public class GridItem
    {
        public int Row { get; set; }
        public Brush Backgd { get; set; }
        public double EndTime { get; set; }
        public bool IsTransparent { get; set; }
        public bool IsTriangleVisible { get; set; }
        public string Time { get; set; }
        public int Number { get; set; }
        public double StartTime { get; set; }
        public GridItem(double StartTime, int Number)
        {
            this.StartTime = StartTime;
            this.Number = Number;
            IsTriangleVisible = true;
        }
        public GridItem(double StartTime, string Time)
        {
            this.StartTime = StartTime;
            this.Time = Time;
        }
    }
}
