using Bitlush;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TestUIwidget.Models;
using TestUIwidget.Views;

namespace TestUIwidget.ViewModels
{
    class MainWindowViewModel : Base.ViewModel
    {
        public ObservableCollection<bool> Risks { get; set; }
        public ObservableCollection<RulerItem> RulerItems { get; set; }
        public ObservableCollection<GridRow> GridRows { get; set; }
        AvlTree<int, GridItem> Items;
        public ICommand GenerateScheduleCommand { get; set; }
        private int Rows = 0;
        private int Period = 100;
        private int ItemCount;
        public MainWindowViewModel(Window window) : base(window)
        {
            GenerateScheduleCommand = new Command((o)=>
            {
                GenerateSchedulePopUp generateSchedulePopUp = new GenerateSchedulePopUp();
                generateSchedulePopUp.UseLayoutRounding = true;
                generateSchedulePopUp.Owner = window;
                if (generateSchedulePopUp.ShowDialog() == true)
                {
                    GenerateSettings result = generateSchedulePopUp.GetResult();
                    Rows = result.RowCount;
                    Period = result.Period;
                    ItemCount = result.ItemCount;
                    CreateItems();
                    CreateTable();
                }
            });
            Items = new AvlTree<int, GridItem>();
            RulerItems = new ObservableCollection<RulerItem>();
            GridRows = new ObservableCollection<GridRow>();
            Risks = new ObservableCollection<bool>();
        }
        void CreateItems()
        {
            Random random = new Random();
            for (int i = 0; i < ItemCount; i++)
            {
                GridItem gridItem;
                if (random.Next(0, 3) != 0)
                {
                    gridItem = new GridItem(random.Next(10, (int)(Period * 10) - 50), 1234567890);
                    gridItem.IsTransparent = Convert.ToBoolean(random.Next(0, 2));
                    switch (random.Next(0, 3))
                    {
                        case 0:
                            gridItem.Backgd = (Brush)new BrushConverter().ConvertFrom("#50a31d");
                            break;
                        case 1:
                            gridItem.Backgd = (Brush)new BrushConverter().ConvertFrom("#c79300");
                            break;
                        case 2:
                            gridItem.Backgd = (Brush)new BrushConverter().ConvertFrom("#9493b7");
                            break;
                    }
                }
                else gridItem = new GridItem(random.Next(10, (int)(Period * 10) - 50), "4h 15m");
                gridItem.Row = random.Next(0, Rows);
                gridItem.EndTime = random.Next(50, 150);
                Items.Insert(i, gridItem);
            }
        }
        void CreateTable()
        {
            RulerItems.Clear();
            for (int i = 0; i < Period + 6; i += 5)
                RulerItems.Add(new RulerItem { Number = i.ToString() });
            Random random = new Random();
            GridRows.Clear();

            for (int i = 0; i < Rows; i++)
            {
                GridRow gridRow = new GridRow();
                gridRow.IsEven = (i % 2) == 1 ? true : false;
                gridRow.TableWidth = (Period * 10) > Window.Width - 20 ? Period * 11 : Window.Width - 20;
                gridRow.RowItems = new ObservableCollection<GridItem>();
                gridRow.Risks = new ObservableCollection<bool>();
                int t = (int)gridRow.TableWidth / 20;
                for (int d = 0; d < t - 3; d++)
                    gridRow.Risks.Add(true);
                var list = Items.ToList();
                var curList = list.Where(x => x.Value.Row == i).OrderBy(x => x.Value.StartTime);
                foreach (var item in curList)
                    gridRow.RowItems.Add(item.Value);
                //var temp = Items.GetEnumerator();
                //while (true)
                //{
                //    GridItem gridItem;
                //    if (!Items.Search(i, out gridItem))
                //        break;
                //    gridRow.RowItems.Add(gridItem);
                //}
                //for (int j = 0; j < ItemCount / Rows; j++)
                //{
                //    int x = random.Next(10, (int)(Period * 10) - 50);
                //    GridItem gridItem;
                //    if (random.Next(0, 3) != 0)
                //    {
                //        gridItem = new GridItem(x, 1234567890);
                //        gridItem.IsTransparent = Convert.ToBoolean(random.Next(0, 2));
                //        switch (random.Next(0,3))
                //        {
                //            case 0:
                //                gridItem.Backgd = (Brush)new BrushConverter().ConvertFrom("#50a31d");
                //                break;
                //            case 1:
                //                gridItem.Backgd = (Brush)new BrushConverter().ConvertFrom("#c79300");
                //                break;
                //            case 2:
                //                gridItem.Backgd = (Brush)new BrushConverter().ConvertFrom("#9493b7");
                //                break;
                //        }
                //    }
                //    else gridItem = new GridItem(x, "4h 15m");
                //    gridItem.EndTime = random.Next(50, 150);
                //    gridRow.RowItems.Add(gridItem);
                //}
                if(gridRow.RowItems.Count == 0)
                    gridRow.RowItems.Add(new GridItem(0, ""));
                GridRows.Add(gridRow);
            }
            //var enumerator = Items.GetEnumerator();
            //enumerator.MoveNext();
            //while (true)
            //{
            //    try
            //    {
            //        GridRows[enumerator.Current.Key].RowItems.Add(enumerator.Current.Value);
            //    }
            //    catch
            //    {

            //    }
            //    if (!enumerator.MoveNext()) break;
            //}
            if (GridRows.Count < 13)
            {
                for (int i = 0; i < 2; i++)
                {
                    GridRow gridRow = new GridRow();
                    gridRow.IsEven = (i % 2) == 1 ? true : false;
                    gridRow.TableWidth = (Period * 11) > Window.Width - 20 ? Period * 11 : Window.Width - 20;
                    gridRow.RowItems = new ObservableCollection<GridItem>();
                    gridRow.RowItems.Add(new GridItem(0, ""));
                    gridRow.Risks = new ObservableCollection<bool>();
                    int t = (int)gridRow.TableWidth / 20;
                    for (int d = 0; d < t - 3; d++)
                        gridRow.Risks.Add(true);
                    GridRows.Add(gridRow);
                }
            }
        }
    }
}
