using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Xu.Common;

namespace GenerateToolbox.ViewModels
{
    class MyDataGridVM : ValidationBase
    {
        MyDataGrid control;
        DataGridSetting grid;
        public MyDataGridVM(FrameworkElement obj, DataGridSetting grid)
        {
            this.grid = grid;
            control = obj as MyDataGrid;
            foreach(var ds in control.grid.Columns)
            {
                GridModel tmp = new GridModel
                {
                    NAME_ZH = ds.Header?.ToString(),
                    NAME_ENG = ds?.SortMemberPath
                };
                DataSource.Add(tmp);
            }
        }

        /// <summary>
        ///
        /// </summary>
        private List<GridModel> _DataSource =  new List<GridModel>();
        public List<GridModel> DataSource
        {
            get
            {
                return _DataSource;
            }
            set
            {
                _DataSource = value;
                NotifyPropertyChanged("DataSource");
            }
        }


        public SimpleCommand CmdSave => new SimpleCommand()
        {
            ExecuteDelegate = x => {
                control.grid.Columns.Clear();
                foreach(var ds in DataSource)
                {
                    DataGridTextColumn column = new DataGridTextColumn();
                    System.Windows.Data.Binding binding = new System.Windows.Data.Binding(ds.NAME_ENG);
                    column.Header = ds.NAME_ZH;
                    column.Binding = binding;
                    control.grid.Columns.Add(column);
                }
                this.grid.Visibility = Visibility.Hidden;
            },
            CanExecuteDelegate = o => {
                return true;
            }
        };



    }

    class GridModel
    {
        public string NAME_ZH { get; set; }
        public string NAME_ENG { get; set; }
    }
}
