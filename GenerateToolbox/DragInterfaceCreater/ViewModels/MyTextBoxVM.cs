using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Xu.Common;

namespace GenerateToolbox.ViewModels
{
    class MyTextBoxVM : ValidationBase
    {
        TextBoxSetting page;
        MyTextBox myTextBox;
        public MyTextBoxVM(MyTextBox box, TextBoxSetting plugin)
        {
            page = plugin;
            myTextBox = box;
            LoadComboBox();
            IsChecked = myTextBox.IS_API;
            KeyValuePair<string, string> keyValue = new KeyValuePair<string, string>
            (
                myTextBox.BOX_TYPE, myTextBox.BOX_TYPE
            );
            SelectedItem = keyValue;
        }

        private ObservableCollection<KeyValuePair<string, string>> _pairs;
        public ObservableCollection<KeyValuePair<string, string>> pairs
        {
            get
            {
                return _pairs;
            }
            set
            {
                _pairs = value;
                NotifyPropertyChanged("pairs");
            }
        }
        private KeyValuePair<string, string> _SelectedItem;
        public KeyValuePair<string, string> SelectedItem
        {
            get
            {
                return _SelectedItem;
            }
            set
            {
                _SelectedItem = value;
                NotifyPropertyChanged("SelectedItem");
                Save();
            }
        }
        private bool isChecked;
        public bool IsChecked
        {
            get
            {
                return isChecked;
            }
            set
            {
                isChecked = value;
                NotifyPropertyChanged("IsChecked");
            }
        }

        public SimpleCommand CmdSave => new SimpleCommand()
        {
            ExecuteDelegate = x =>
            {
                Save();
                page.Visibility = Visibility.Hidden;
                page.ParentWindow.ccp.Visibility = Visibility.Hidden;
            },
            CanExecuteDelegate = o =>
            {
                return true;
            }
        };

        private void Save()
        {
            myTextBox.NAME_ENG = page.name_eg.Text;
            myTextBox.tblock.Text = page.name_zh.Text;
            myTextBox.IS_API = IsChecked;
            myTextBox.BOX_TYPE = SelectedItem.Value;
            ConfirmControl(myTextBox.BOX_TYPE);
        }

        private void ConfirmControl(string control)
        {
            string[] vs = { "TextBox", "TextBox带弹出框", "Combox",  "DatePicker", "进阶DatePicker", "只读TextBox", "TextBox", "SearchBox"};
            
            foreach (var ds in vs)
            {
                dynamic type;
                switch (ds)
                {
                    case "Combox": type = myTextBox.combox as ComboBox; break;
                    //case "TextBox": type = myTextBox.tb as TextBox; break;
                    case "TextBox带弹出框": type = myTextBox.metrotb as TextBox; break;
                    case "DatePicker": type = myTextBox.datepicker as DatePicker; break;
                    case "进阶DatePicker": type = myTextBox.datetimepicker as MahApps.Metro.Controls.DateTimePicker; break;
                    case "SearchBox": type = myTextBox.searchtb as TextBox; break;
                    default: type = myTextBox.tb as TextBox; break;
                }

                if (ds == control)
                    type.Visibility = Visibility.Visible;
                else
                    type.Visibility = Visibility.Collapsed;
            }
        }

        private void LoadComboBox()
        {
            pairs = new ObservableCollection<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("TextBox", "TextBox"),
                new KeyValuePair<string, string>("TextBox带弹出框", "TextBox带弹出框"),
                new KeyValuePair<string, string>("Combox", "Combox"),
                new KeyValuePair<string, string>("DatePicker", "DatePicker"),
                new KeyValuePair<string, string>("进阶DatePicker", "进阶DatePicker"),
                new KeyValuePair<string, string>("只读TextBox", "只读TextBox"),
                new KeyValuePair<string, string>("SearchBox", "SearchBox")
            };

            SelectedItem = pairs[0];
        }

    }

}
