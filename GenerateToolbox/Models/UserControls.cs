using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace GenerateToolbox.Models
{
    public class UserControls
    {
        public static UserControls Control()
        {
            return userControl;
        }
        private static UserControls userControl = new UserControls();
        public List<string> GetFiles { get; set; }
        public UserControls()
        {
            GetFiles = getFiles();
        }

        public void CreateBtn(WrapPanel grid, string btnName, System.Windows.Input.MouseButtonEventHandler handler)
        {
            var sp = new StackPanel();
            sp.Margin = new Thickness(10, 5, 0, 0);
            sp.Width = 100;
            sp.Orientation = Orientation.Horizontal;

            var border = new Border();
            border.Width = 30;
            var url = System.IO.Path.GetDirectoryName(typeof(Project.G.MainWindow).Assembly.Location);
            BitmapImage bitmap = new BitmapImage(new Uri(url + "/img/组件.png", UriKind.RelativeOrAbsolute));
            var Img = new Image();
            Img.Source = bitmap;  // "";
            border.Child = Img;
            sp.Children.Add(border);

            var tlb = new TextBlock();
            tlb.Text = btnName;
            tlb.Margin = new Thickness(10, 0, 0, 0);
            tlb.VerticalAlignment = VerticalAlignment.Center;
            tlb.HorizontalAlignment = HorizontalAlignment.Center;
            sp.Children.Add(tlb);
            sp.MouseLeftButtonDown += handler;
            grid.Children.Add(sp);
        }

        public void CreateProperties(string filename)
        {
            var fullname = GetFiles.FirstOrDefault(x => x.Contains(filename));
            FileStream file = new FileStream(fullname, FileMode.Open);
            StreamReader reader = new StreamReader(file);
            var txt = reader.ReadToEnd();

            var m = Regex.Matches(txt, @"[\{[a-zA-Z\u4e00-\u9fa5]*\}]*");
            var vs = new List<String>();
            foreach (Match c in m)
            {
                vs.Add(c.Value);
            }
            file.Close();
            file.Dispose();
            reader.Close();
            reader.Dispose();

            //添加按钮
            var stackpanel2 = new StackPanel();
            //if(!NewPage.NewPage.PropertiesDic.ContainsKey(filename))
            //    NewPage.NewPage.PropertiesDic[filename] = new List<string>();
            foreach (var ds in vs)
            {
                
                var name = ds.Replace("{", "").Replace("}", "");
                var stackpanel = new StackPanel();
                stackpanel.Orientation = Orientation.Horizontal;
                stackpanel.Margin = new Thickness(5);
                var textblock = new TextBlock();
                textblock.Text = name;
                textblock.Width = 70;
                textblock.Margin = new Thickness(5);
                var textbox = new TextBox();
                textbox.Name = name;
                textbox.Width = 100;
                textbox.Height = 25;
                stackpanel.VerticalAlignment = VerticalAlignment.Center;
                stackpanel.HorizontalAlignment = HorizontalAlignment.Center;
                if (NewPage.NewPage.PropertiesDic.ContainsKey(filename))
                {
                    textbox.Text = NewPage.NewPage.PropertiesDic[filename][vs.IndexOf(ds)];
                }
                stackpanel.Children.Add(textblock);
                stackpanel.Children.Add(textbox);
                stackpanel2.Children.Add(stackpanel);
                txt = txt.Replace(name, vs.IndexOf(ds) + "");
            }


            var button = new Button();
            button.Content = "保存";
            button.Click += (x, y)=> 
            { 
                NewPage.NewPage.CCDic[filename] = txt;
                NewPage.NewPage.PropertiesDic[filename] = new List<string>();
                int t = 0;
                GetTextBoxValue(NewPage.NewPage.customSetting.csgrid.Children, NewPage.NewPage.PropertiesDic[filename]);
                
            };
            button.Width = 100;
            button.Height = 30;
            button.VerticalAlignment = VerticalAlignment.Center;
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.Margin = new Thickness(5);
            button.SetResourceReference(Button.StyleProperty, "SquareButtonStyle");
            stackpanel2.Children.Add(button);
            NewPage.NewPage.customSetting.csgrid.Children.Clear();
            NewPage.NewPage.customSetting.csgrid.Children.Add(stackpanel2);
        }

        private void GetTextBoxValue(UIElementCollection collection, List<string> vs)
        {
            if (collection == null || collection.Count == 0)
                return;
            foreach(var ds in collection)
            {
                if(ds is StackPanel)
                {
                    GetTextBoxValue(((StackPanel)ds).Children, vs);
                }
                if(ds is TextBox)
                {
                    vs.Add(((TextBox)ds).Text);
                }
            }
        }

        private List<string> getFiles()
        {
            var url = System.IO.Path.GetDirectoryName(typeof(Project.G.MainWindow).Assembly.Location);
            var info = new DirectoryInfo(url + "/Components");
            var dirs = info.GetFileSystemInfos();
            var vs = new List<string>();
            foreach(var ds in dirs)
            {
                if (ds.FullName.Contains(".txt"))
                {
                    vs.Add(ds.FullName);
                }
            }
            return vs;
        }
    }
}
