using GenerateToolbox.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GenerateToolbox
{
    /// <summary>
    /// TextBoxSetting.xaml 的交互逻辑
    /// </summary>
    public partial class TextBoxSetting : Page, INotifyPropertyChanged
    {
        private string name;
        MyTextBox mytextbox;
        MyTextBoxVM vm;
        public TextBoxSetting(FrameworkElement name)
        {
            InitializeComponent();
            mytextbox = name as MyTextBox;
            this.name = name.Name;
            name_eg.Text = mytextbox.NAME_ENG as string;
            name_zh.Text = mytextbox.tblock.Text as string;
            //LoadComboBox();
            vm = new MyTextBoxVM(mytextbox, this);
            DataContext = vm;
        }

        public NewPage.NewPage ParentWindow { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mytextbox.NAME_ENG = name_eg.Text;
            mytextbox.tblock.Text = name_zh.Text;
            this.Visibility = Visibility.Hidden;
            ParentWindow.ccp.Visibility = Visibility.Hidden;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

       
    }
}
