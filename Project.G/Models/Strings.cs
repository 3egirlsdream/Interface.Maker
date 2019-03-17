using Model.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xu.Common;

namespace Project.G.Models
{
    public class Strings
    {
        public static string LoadJson(string path)
        {
            try
            {
                FileStream file = new FileStream(path, FileMode.Open);
                StreamReader sr = new StreamReader(file, Encoding.Default);
                string json = sr.ReadToEnd();
                //MessageBox.Show(json);
                file.Close();
                return json;
            }
            catch (IOException)
            {
                return null;
                //Console.WriteLine(e.ToString());
            }
        }


        public static string GetCsproj(string ProjectName, string Complier = "", string Page = "", string Extend = "", string Link = "")
        {
            string csproject = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
                "<Project ToolsVersion=\"15.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">\r\n  " +
                "<Import Project=\"$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props\" Condition=\"Exists('$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props')\" />\r\n  " +
                "<PropertyGroup>\r\n    <Configuration Condition=\" '$(Configuration)' == '' \">Debug</Configuration>\r\n    " +
                "<Platform Condition=\" '$(Platform)' == '' \">AnyCPU</Platform>\r\n    <ProjectGuid>{04C6C65A-8C05-4C4B-9F82-CEB1C218DEA5}" +
                "</ProjectGuid>\r\n    " +
                "<OutputType>Library</OutputType>\r\n    " +
                "<AppDesignerFolder>Properties</AppDesignerFolder>\r\n    " +
                "<RootNamespace>"+ProjectName+"</RootNamespace>\r\n    " +
                "<AssemblyName>"+ProjectName+"</AssemblyName>\r\n    " +
                "<TargetFrameworkVersion>v4.0</TargetFrameworkVersion>\r\n    " +
                "<FileAlignment>512</FileAlignment>\r\n    " +
                "<Deterministic>false</Deterministic>\r\n  " +
                "</PropertyGroup>\r\n  " +
                "<PropertyGroup>\r\n    " +
                "<StartupObject />\r\n  " +
                "</PropertyGroup>\r\n  " +
                "<PropertyGroup Condition=\"'$(Configuration)|$(Platform)' == 'Debug|AnyCPU'\">\r\n    " +
                "<DebugSymbols>true</DebugSymbols>\r\n    " +
                "<DebugType>full</DebugType>\r\n    " +
                "<Optimize>false</Optimize>\r\n    " +
                "<OutputPath>X:\\CLIENT\\plugins\\</OutputPath>\r\n    " +
                "<DefineConstants>\r\n    </DefineConstants>\r\n    " +
                "<ErrorReport>prompt</ErrorReport>\r\n    " +
                "<WarningLevel>4</WarningLevel>\r\n  " +
                "</PropertyGroup>\r\n  " +
                "<PropertyGroup Condition=\"'$(Configuration)|$(Platform)' == 'Release|AnyCPU'\">\r\n    " +
                "<DebugType>pdbonly</DebugType>\r\n    " +
                "<Optimize>true</Optimize>\r\n    " +
                "<OutputPath>bin\\Release\\</OutputPath>\r\n    " +
                "<DefineConstants>TRACE</DefineConstants>\r\n    " +
                "<ErrorReport>prompt</ErrorReport>\r\n    " +
                "<WarningLevel>4</WarningLevel>\r\n  " +
                "</PropertyGroup>\r\n  " +
                "<ItemGroup>\r\n    " +
                "<Reference Include=\"Creative.Plugin.Common\">\r\n      " +
                "<HintPath>..\\..\\Reference Assemblies\\Creative.Plugin.Common.dll</HintPath>\r\n    " +
                "</Reference>\r\n    <Reference Include=\"DAF.Plugin.Common\">\r\n      " +
                "<HintPath>..\\..\\Reference Assemblies\\DAF.Plugin.Common.dll</HintPath>\r\n    " +
                "</Reference>\r\n    <Reference Include=\"MES.Plugin.Common\">\r\n      " +
                "<HintPath>..\\..\\Reference Assemblies\\MES.Plugin.Common.dll</HintPath>\r\n    " +
                "</Reference>\r\n    " +
                "<Reference Include=\"Newtonsoft.Json, Version=4.5.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed, processorArchitecture=MSIL\">\r\n      " +
                "<SpecificVersion>False</SpecificVersion>\r\n      " +
                "<HintPath>..\\..\\Reference Assemblies\\Newtonsoft.Json.dll</HintPath>\r\n    " +
                "</Reference>\r\n    " +

                "<Reference Include=\"NPOI, Version=2.2.1.0, Culture=neutral, PublicKeyToken=0df73ec7942b34e1, processorArchitecture=MSIL\">\r\n      " +
                "<SpecificVersion>False</SpecificVersion>\r\n      " +
                "<HintPath>..\\..\\Reference Assemblies\\NPOI.dll</HintPath>\r\n    " +
                "</Reference>\r\n    " +
                "<Reference Include=\"NPOI.OOXML, Version=2.2.1.0, Culture=neutral, PublicKeyToken=0df73ec7942b34e1, processorArchitecture=MSIL\">\r\n      " +
                "<SpecificVersion>False</SpecificVersion>\r\n      " +
                "<HintPath>..\\..\\Reference Assemblies\\NPOI.OOXML.dll</HintPath>\r\n    " +
                "</Reference>\r\n    <Reference Include=\"NPOI.OpenXml4Net, Version=2.2.1.0, Culture=neutral, PublicKeyToken=0df73ec7942b34e1, processorArchitecture=MSIL\">\r\n      " +
                "<SpecificVersion>False</SpecificVersion>\r\n      " +
                "<HintPath>..\\..\\Reference Assemblies\\NPOI.OpenXml4Net.dll</HintPath>\r\n    " +
                "</Reference>\r\n    <Reference Include=\"NPOI.OpenXmlFormats, Version=2.2.1.0, Culture=neutral, PublicKeyToken=0df73ec7942b34e1, processorArchitecture=MSIL\">\r\n      " +
                "<SpecificVersion>False</SpecificVersion>\r\n      " +
                "<HintPath>..\\..\\Reference Assemblies\\NPOI.OpenXmlFormats.dll</HintPath>\r\n    " +
                "</Reference>"+

                "<Reference Include=\"System\" />\r\n    " +
                "<Reference Include=\"System.ComponentModel.DataAnnotations\" />\r\n    " +
                "<Reference Include=\"System.Data\" />\r\n    " +
                "<Reference Include=\"System.Xml\" />\r\n    " +
                "<Reference Include=\"Microsoft.CSharp\" />\r\n    " +
                "<Reference Include=\"System.Core\" />\r\n    " +
                "<Reference Include=\"System.Xml.Linq\" />\r\n    " +
                "<Reference Include=\"System.Data.DataSetExtensions\" />\r\n    " +
                "<Reference Include=\"System.Xaml\">\r\n      " +
                "<RequiredTargetFramework>4.0</RequiredTargetFramework>\r\n    " +
                "</Reference>\r\n    " +
                "<Reference Include=\"WindowsBase\" />\r\n    " +
                "<Reference Include=\"PresentationCore\" />\r\n    " +
                "<Reference Include=\"PresentationFramework\" />\r\n  " +
                "</ItemGroup>\r\n  " +


                "<ItemGroup>\r\n   " +
                Page +
                "<Page Include=\"Views\\IndexPage.xaml\">\r\n      " +
                "<Generator>MSBuild:Compile</Generator>\r\n      " +
                "<SubType>Designer</SubType>\r\n    " +
                "</Page>\r\n    " +
                
                
                "<Page Include=\"Resources\\Strings.zh-CN.xaml\">\r\n      " +
                "<Generator>MSBuild:Compile</Generator>\r\n      " +
                "<SubType>Designer</SubType>\r\n    " +
                "</Page>\r\n  " +
                "</ItemGroup>\r\n  " +


                "<ItemGroup>\r\n " +
                "<Compile Include=\"..\\..\\Reference Assemblies\\Release\\GlobalAssemblyInfo.cs\">\r\n      " +
                "<Link>Properties\\GlobalAssemblyInfo.cs</Link>\r\n    " +
                "</Compile>\r\n\r\n    " +
                Link +
                "<Compile Include=\"Services.cs\" />" +
                "<Compile Include=\"ViewModels\\IndexPageVM.cs\" />\r\n    " +
                "<Compile Include=\"Models\\Model.cs\" />\r\n\r\n    " +
                "<Compile Include=\"Models\\ComboxModel.cs\" />\r\n\r\n    " +
                "<Compile Include=\"Views\\IndexPage.xaml.cs\">\r\n      " +
                "<DependentUpon>IndexPage.xaml</DependentUpon>\r\n      " +
                "<SubType>Code</SubType>\r\n    " +
                "</Compile>\r\n    " +
                Complier + 
                "<Compile Include=\"Properties\\AssemblyInfo.cs\">\r\n      " +
                "<SubType>Code</SubType>\r\n    " +
                "</Compile>\r\n    " +
                "<AppDesigner Include=\"Properties\\\" />\r\n  " +
                "</ItemGroup>\r\n   " +


                "<Import Project=\"$(MSBuildToolsPath)\\Microsoft.CSharp.targets\" />\r\n"+Extend+"\r\n" +
                "</Project>";
            return csproject;
        }


        public static string ModelLink(string ShareModel, List<string> names, bool flag = false)
        {
            string s = "";
            foreach (var name in names)
            {
                if (!flag)
                {
                    string tmp = "<Compile Include=\"..\\..\\SharedModels\\" + name.ToUpper() + ".cs\" >\r\n      " +
                                "<Link>Models\\" + name.ToUpper() + ".cs</Link>\r\n    " +
                                "</Compile>\r\n\r\n    ";
                    s += tmp;
                }
                else if (flag && File.Exists(ShareModel + "\\" + name.ToUpper() + ".cs"))
                {
                    string tmp = "<Compile Include=\"..\\..\\SharedModels\\" + name.ToUpper() + ".cs\" >\r\n      " +
                                "<Link>Models\\" + name.ToUpper() + ".cs</Link>\r\n    " +
                                "</Compile>\r\n\r\n    ";
                    s += tmp;
                } 
            }
            return s;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjectName">项目名</param>
        /// <param name="Extend">拓展内容</param>
        /// <returns></returns>
        public static string GetResource(string ProjectName, string Extend)
        {
            string resource = "<ResourceDictionary\r\nxmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"\r\n" +
                "xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"\r\n" +
                "xmlns:sys=\"clr-namespace:System;assembly=mscorlib\">\r\n" +
                "<sys:String x:Key=\"Title\">"+ProjectName+"</sys:String>\r\n" +
                "<sys:String x:Key=\"Title_Add\">新增</sys:String>\r\n" +
                "<sys:String x:Key=\"Title_Edit\">编辑</sys:String>\r\n" +
                "<sys:String x:Key=\"ErrorMsg\">错误原因</sys:String>\r\n" +
                "<sys:String x:Key=\"BillNo\">单据号</sys:String>\r\n" +
                "<sys:String x:Key=\"Submit\">提交</sys:String>\r\n" +
                "<sys:String x:Key=\"ModifiedBy\">修改人</sys:String>\r\n" +
                "<sys:String x:Key=\"Export\">导出</sys:String>\r\n" +
                "<sys:String x:Key=\"Refresh\">刷新</sys:String>\r\n" +
                "<sys:String x:Key=\"Abort\">放弃</sys:String>\r\n" +
                "<sys:String x:Key=\"DateEnd\">交易结束时间</sys:String>\r\n" +
                "<sys:String x:Key=\"Processing\">处理中</sys:String>\r\n" +
                "<sys:String x:Key=\"transDate\">事务日期</sys:String>\r\n" +
                "<sys:String x:Key=\"Status\">状态</sys:String>\r\n" +
                "<sys:String x:Key=\"To\">至</sys:String>\r\n" +
                "<sys:String x:Key=\"Success\">成功</sys:String>\r\n" +
                "<sys:String x:Key=\"Fail\">失败</sys:String>\r\n" +
                "<sys:String x:Key=\"Waiting\">等待</sys:String>\r\n" +
                "<sys:String x:Key=\"Save\">保存</sys:String>\r\n" +
                "<sys:String x:Key=\"Cancel\">取消</sys:String>\r\n"+
                "<sys:String x:Key=\"Print\">打印</sys:String>\r\n" +
                "<sys:String x:Key=\"Import\">导入</sys:String>\r\n" +
                "<sys:String x:Key=\"Create\">生成</sys:String>\r\n" +
                "<sys:String x:Key=\"Edit\">编辑</sys:String>\r\n" +
                "<sys:String x:Key=\"Add\">新增</sys:String>\r\n" +
                "<sys:String x:Key=\"Delete\">删除</sys:String>\r\n" +
                "<sys:String x:Key=\"Forbiden\">禁用</sys:String>\r\n" +
                "<sys:String x:Key=\"SelectFile\">选择导入文件</sys:String>\r\n" +
                "<sys:String x:Key=\"DownloadTemplate\">下载导入文件模板</sys:String>\r\n" +
                "<sys:String x:Key=\"InvaliableColume\">不合法的列数</sys:String>\r\n" +
                "<!--自动生成-->\r\n" +
                Extend +"\r\n</ResourceDictionary>";
            return resource;
        }

        public static string GetIndexPage(string ProjectName, string Button, string SearchContent, string DataGrid)
        {
            string xaml = "<common:PagePlugin\r\n    x:Class=\""+ ProjectName + ".IndexPage\"\r\n    " +
                "xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"\r\n    " +
                "xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"\r\n    " +
                "xmlns:common=\"http://schemas.creative.com/plugin\"\r\n    " +
                "xmlns:controls=\"http://schemas.creative.com/controls\"\r\n    " +
                "xmlns:d=\"http://schemas.microsoft.com/expression/blend/2008\"\r\n    " +
                "xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\"\r\n    " +
                "Title=\"{DynamicResource Title}\"\r\n    d:DesignHeight=\"768\"\r\n    " +
                "d:DesignWidth=\"1024\"\r\n    mc:Ignorable=\"d\">\r\n    " +
                "<Page.Resources>\r\n        " +
                "<ResourceDictionary>\r\n            <ResourceDictionary.MergedDictionaries>\r\n                " +
                "<ResourceDictionary Source=\"/" + ProjectName + ";component/Resources/Strings.zh-CN.xaml\" />\r\n  " +
                "<ResourceDictionary Source=\"/Creative.Plugin.Common;component/Themes/Generic.xaml\" />\r\n            " +
                "</ResourceDictionary.MergedDictionaries>\r\n        " +
                "</ResourceDictionary>\r\n    " +
                "</Page.Resources>\r\n    " +
                "<Grid>\r\n        " +
                "<Grid.RowDefinitions>\r\n\r\n            " +
                "<RowDefinition Height=\"Auto\"/>\r\n            " +
                "<RowDefinition Height=\"Auto\"/>\r\n            " +
                "<RowDefinition Height=\"Auto\"/>\r\n            " +
                "<RowDefinition Height=\"Auto\"/>\r\n            " +
                "<RowDefinition Height=\"Auto\"/>\r\n            " +
                "<RowDefinition Height=\"Auto\"/>\r\n            " +
                "<RowDefinition Height=\"*\" />\r\n            " +
                "<RowDefinition Height=\"Auto\"/>\r\n        " +
                "</Grid.RowDefinitions>\r\n        " +
                "<Border \r\n            Grid.Row=\"0\"\r\n            Margin=\"0,0,0,0\"\r\n            Background=\"{DynamicResource PanelBrush}\">\r\n            " +
                "<Grid Margin=\"{DynamicResource ContainerPadding}\">\r\n                \r\n            " +
                Button +
                "</Grid>\r\n        " +
                "</Border>\r\n       " +
                " <Border\r\n            Grid.Row=\"1\"\r\n            Margin=\"0,0,0,0\"\r\n            " +
                "Background=\"{DynamicResource PanelBrush}\">\r\n            " +
                "<Border BorderBrush=\"#D8D8D9\" BorderThickness=\"1\" Margin=\"{DynamicResource ContainerPadding}\" Padding=\"5\">" +
                "<Grid>\r\n                " +
                
                "<Grid.RowDefinitions>\r\n                    " +
                "<RowDefinition Height=\"Auto\" />\r\n                    " +
                "<RowDefinition Height=\"Auto\" />\r\n                    " +
                "</Grid.RowDefinitions>\r\n                \r\n            " +
                SearchContent +
                "</Grid>\r\n        " +
                "</Border>\r\n" +
                "</Border>\r\n        " +
                "<Border Grid.Row=\"6\" Margin=\"0,0,0,0\" Background=\"{DynamicResource PanelBrush}\">\r\n            " +
                "<Grid Margin=\"{DynamicResource ContainerPadding}\">\r\n                " +
                "<DataGrid  ItemsSource=\"{Binding DataSource}\" SelectedItem=\"{Binding SelectedRow}\">\r\n                    " +
                "<DataGrid.Columns>\r\n\r\n                    " +
                DataGrid+
                "</DataGrid.Columns>\r\n                " +
                "</DataGrid>\r\n            " +
                "</Grid>\r\n        " +
                "</Border>\r\n        " +
                "<controls:DataPager\r\n            Grid.Row=\"7\"\r\n                    " +
                "PageSize=\"{Binding PageSize, Mode=TwoWay}\"\r\n                    " +
                "TotalCount=\"{Binding TotalCount, Mode=TwoWay}\"\r\n                    " +
                "PageIndex=\"{Binding PageIndex, Mode=TwoWay}\"/>\r\n    " +
                "</Grid>\r\n</common:PagePlugin>\r\n";
            return xaml;
        }

        public static string GetIndexXamlCs(string ProjectName)
        {
            string cs = "using System;\r\nusing System.Globalization;\r\nusing DAF.Plugin.Common;\r\nusing System.Windows;\r\n\r\nnamespace " + ProjectName + "\r\n{\r\n    /// <summary>\r\n    /// IndexPage.xaml 的交互逻辑\r\n    /// </summary>\r\n    public partial class IndexPage : PagePlugin\r\n    {\r\n\r\n        #region 基类方法\r\n        IndexPageVM vm;\r\n        public IndexPage()\r\n        {\r\n            InitializeComponent();\r\n        }\r\n\r\n        " +
                "public override void PluginLoadCompleted()\r\n        " +
                "{\r\n      try{ \r\n     vm = new IndexPageVM(this);\r\n            " +
                "this.DataContext = vm;\r\n  }\r\n\t\t\t catch(Exception ex)\r\n\t\t\t{\r\n\t\t\t\t throw ex;\r\n\t\t\t}\r\n     " +
                "}\r\n\r\n        public override void SetFocus()\r\n        {\r\n\r\n        }\r\n\r\n        /// <summary>\r\n        /// 程序中多语言实现代码放在此事件中。\r\n        /// </summary>\r\n        /// <param name=\"culture\"></param>\r\n        public override void OnCultureUpdated(CultureInfo culture)\r\n        {\r\n            base.OnCultureUpdated(culture);\r\n            //切换本地化语言资源文件。\r\n            string strResourceName = string.Format(@\"pack://application:,,,/" + ProjectName+";component/Resources/Strings.zh-CN.xaml\", culture.Name);\r\n            ResourceDictionary languageResDic = new ResourceDictionary();\r\n            languageResDic.Source = new Uri(strResourceName, UriKind.RelativeOrAbsolute);\r\n            this.Resources.MergedDictionaries.Add(languageResDic);\r\n\r\n            PageContainer.OnPluginUpdated(new PluginUpdatedEventArg { category = PluginUpdateCategory.Title, value = this[\"Title\"] });\r\n        }\r\n        #endregion\r\n    }\r\n}\r\n\r\n";
            return cs;
        }
    
        public static string GetIndexVM(string _namespace, string Extend, string LoadData)
        {
            string vm = "using DAF.Plugin.Common;\r\nusing System;\r\n" +
                "using System.Collections.Generic;\r\n" +
                "using System.Linq;\r\nusing System.Text;\r\n" +
                "using Newtonsoft.Json;\r\n" +
                "using System.Windows.Input;\r\n" +
                "using System.Windows.Controls;\r\n" +
                "using System.Collections.ObjectModel;\r\n" +
                "using MES.Plugin.Common;\r\n\r\nnamespace "+_namespace+"\r\n{\r\n    " +
                "public class IndexPageVM : ViewModelBase\r\n    {\r\n       \r\n        " +
                "public IndexPageVM(IDAFPlugin plugin) : base(plugin) \r\n        " +
                "{\r\n            LoadData();\r\n        }\r\n\r\n        " +
                "#region 分页\r\n\r\n        private int _pageIndex = 1;\r\n       " +
                " public int PageIndex\r\n        {\r\n            get { return _pageIndex; }\r\n            set\r\n            {\r\n                if (_pageIndex != value)\r\n                {\r\n                    _pageIndex = value;\r\n                    NotifyPropertyChanged(\"PageIndex\");\r\n                    LoadData();\r\n                }\r\n            }\r\n        }\r\n\r\n        " +
                "private int _pageSize = 20;\r\n        public int PageSize\r\n        {\r\n            get { return _pageSize; }\r\n            set\r\n            {\r\n                _pageSize = value;\r\n                NotifyPropertyChanged(\"PageSize\");\r\n                LoadData();\r\n            }\r\n        }\r\n\r\n\r\n        private int _totalCount;\r\n        public int TotalCount\r\n        {\r\n            get { return _totalCount; }\r\n            set\r\n            {\r\n                if (_totalCount != value)\r\n                {\r\n                    _totalCount = value;\r\n                    NotifyPropertyChanged(\"TotalCount\");\r\n                }\r\n            }\r\n        }\r\n\r\n        #endregion\r\n\r\n\r\n        private bool _isSelectedAll;\r\n        /// <summary>\r\n        /// 是否全部勾选\r\n        /// </summary>\r\n        public bool IsSelectedAll\r\n        {\r\n            get { return _isSelectedAll; }\r\n            set\r\n            {\r\n                _isSelectedAll = value;\r\n                foreach(var ds in DataSource)\r\n                {\r\n                    ds.IsChecked = value;\r\n                }\r\n                NotifyPropertyChanged(\"IsSelectedAll\");\r\n            }\r\n        }\r\n\r\n        /// <summary>\r\n        /// 主数据\r\n        /// </summary>\r\n        private List<Model> _DataSource;\r\n        public List<Model> DataSource\r\n        {\r\n            get\r\n            {\r\n                return _DataSource;\r\n            }\r\n            set\r\n            {\r\n                _DataSource = value;\r\n                NotifyPropertyChanged(\"DataSource\");\r\n            }\r\n        }\r\n\r\n\r\n\r\n\r\n\r\n \tprivate Model _SelectedRow;\r\n        public Model SelectedRow\r\n        {\r\n            get\r\n            {\r\n                return _SelectedRow;\r\n            }\r\n            set\r\n            {\r\n                _SelectedRow = value;\r\n                NotifyPropertyChanged(\"SelectedRow\");\r\n            }\r\n \t}\r\n " + Extend+"      #region 方法\r\n        " +
                "public void LoadData()\r\n        " +
                "{\r\n\r\n        " +
                LoadData +
                "}\r\n        #endregion\r\n\r\n  }\r\n}\r\n";
            return vm;
        }

        public const string resx = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<root>\r\n  <!-- \r\n    Microsoft ResX Schema \r\n    \r\n    Version 2.0\r\n    \r\n    The primary goals of this format is to allow a simple XML format \r\n    that is mostly human readable. The generation and parsing of the \r\n    various data types are done through the TypeConverter classes \r\n    associated with the data types.\r\n    \r\n    Example:\r\n    \r\n    ... ado.net/XML headers & schema ...\r\n    <resheader name=\"resmimetype\">text/microsoft-resx</resheader>\r\n    <resheader name=\"version\">2.0</resheader>\r\n    <resheader name=\"reader\">System.Resources.ResXResourceReader, System.Windows.Forms, ...</resheader>\r\n    <resheader name=\"writer\">System.Resources.ResXResourceWriter, System.Windows.Forms, ...</resheader>\r\n    <data name=\"Name1\"><value>this is my long string</value><comment>this is a comment</comment></data>\r\n    <data name=\"Color1\" type=\"System.Drawing.Color, System.Drawing\">Blue</data>\r\n    <data name=\"Bitmap1\" mimetype=\"application/x-microsoft.net.object.binary.base64\">\r\n        <value>[base64 mime encoded serialized .NET Framework object]</value>\r\n    </data>\r\n    <data name=\"Icon1\" type=\"System.Drawing.Icon, System.Drawing\" mimetype=\"application/x-microsoft.net.object.bytearray.base64\">\r\n        <value>[base64 mime encoded string representing a byte array form of the .NET Framework object]</value>\r\n        <comment>This is a comment</comment>\r\n    </data>\r\n                \r\n    There are any number of \"resheader\" rows that contain simple \r\n    name/value pairs.\r\n    \r\n    Each data row contains a name, and value. The row also contains a \r\n    type or mimetype. Type corresponds to a .NET class that support \r\n    text/value conversion through the TypeConverter architecture. \r\n    Classes that don't support this are serialized and stored with the \r\n    mimetype set.\r\n    \r\n    The mimetype is used for serialized objects, and tells the \r\n    ResXResourceReader how to depersist the object. This is currently not \r\n    extensible. For a given mimetype the value must be set accordingly:\r\n    \r\n    Note - application/x-microsoft.net.object.binary.base64 is the format \r\n    that the ResXResourceWriter will generate, however the reader can \r\n    read any of the formats listed below.\r\n    \r\n    mimetype: application/x-microsoft.net.object.binary.base64\r\n    value   : The object must be serialized with \r\n            : System.Serialization.Formatters.Binary.BinaryFormatter\r\n            : and then encoded with base64 encoding.\r\n    \r\n    mimetype: application/x-microsoft.net.object.soap.base64\r\n    value   : The object must be serialized with \r\n            : System.Runtime.Serialization.Formatters.Soap.SoapFormatter\r\n            : and then encoded with base64 encoding.\r\n\r\n    mimetype: application/x-microsoft.net.object.bytearray.base64\r\n    value   : The object must be serialized into a byte array \r\n            : using a System.ComponentModel.TypeConverter\r\n            : and then encoded with base64 encoding.\r\n    -->\r\n  <xsd:schema id=\"root\" xmlns=\"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">\r\n    <xsd:element name=\"root\" msdata:IsDataSet=\"true\">\r\n      <xsd:complexType>\r\n        <xsd:choice maxOccurs=\"unbounded\">\r\n          <xsd:element name=\"metadata\">\r\n            <xsd:complexType>\r\n              <xsd:sequence>\r\n                <xsd:element name=\"value\" type=\"xsd:string\" minOccurs=\"0\" />\r\n              </xsd:sequence>\r\n              <xsd:attribute name=\"name\" type=\"xsd:string\" />\r\n              <xsd:attribute name=\"type\" type=\"xsd:string\" />\r\n              <xsd:attribute name=\"mimetype\" type=\"xsd:string\" />\r\n            </xsd:complexType>\r\n          </xsd:element>\r\n          <xsd:element name=\"assembly\">\r\n            <xsd:complexType>\r\n              <xsd:attribute name=\"alias\" type=\"xsd:string\" />\r\n              <xsd:attribute name=\"name\" type=\"xsd:string\" />\r\n            </xsd:complexType>\r\n          </xsd:element>\r\n          <xsd:element name=\"data\">\r\n            <xsd:complexType>\r\n              <xsd:sequence>\r\n                <xsd:element name=\"value\" type=\"xsd:string\" minOccurs=\"0\" msdata:Ordinal=\"1\" />\r\n                <xsd:element name=\"comment\" type=\"xsd:string\" minOccurs=\"0\" msdata:Ordinal=\"2\" />\r\n              </xsd:sequence>\r\n              <xsd:attribute name=\"name\" type=\"xsd:string\" msdata:Ordinal=\"1\" />\r\n              <xsd:attribute name=\"type\" type=\"xsd:string\" msdata:Ordinal=\"3\" />\r\n              <xsd:attribute name=\"mimetype\" type=\"xsd:string\" msdata:Ordinal=\"4\" />\r\n            </xsd:complexType>\r\n          </xsd:element>\r\n          <xsd:element name=\"resheader\">\r\n            <xsd:complexType>\r\n              <xsd:sequence>\r\n                <xsd:element name=\"value\" type=\"xsd:string\" minOccurs=\"0\" msdata:Ordinal=\"1\" />\r\n              </xsd:sequence>\r\n              <xsd:attribute name=\"name\" type=\"xsd:string\" use=\"required\" />\r\n            </xsd:complexType>\r\n          </xsd:element>\r\n        </xsd:choice>\r\n      </xsd:complexType>\r\n    </xsd:element>\r\n  </xsd:schema>\r\n  <resheader name=\"resmimetype\">\r\n    <value>text/microsoft-resx</value>\r\n  </resheader>\r\n  <resheader name=\"version\">\r\n    <value>2.0</value>\r\n  </resheader>\r\n  <resheader name=\"reader\">\r\n    <value>System.Resources.ResXResourceReader, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>\r\n  </resheader>\r\n  <resheader name=\"writer\">\r\n    <value>System.Resources.ResXResourceWriter, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>\r\n  </resheader>\r\n</root>";

        public static string GetAssembly(string ProjectName)
        {
            return "using System.Reflection;\r\nusing System.Resources;\r\nusing System.Runtime.CompilerServices;\r\nusing System.Runtime.InteropServices;\r\nusing System.Windows;\r\n\r\n// 有关程序集的常规信息通过以下\r\n// 特性集控制。更改这些特性值可修改\r\n// 与程序集关联的信息。\r\n[assembly: AssemblyTitle(\""+ProjectName+"\")]\r\n[assembly: AssemblyDescription(\"\")]\r\n//[assembly: AssemblyConfiguration(\"\")]\r\n//[assembly: AssemblyCompany(\"\")]\r\n//[assembly: AssemblyCopyright(\"Copyright ©  2016\")]\r\n//[assembly: AssemblyTrademark(\"\")]\r\n//[assembly: AssemblyCulture(\"\")]\r\n\r\n// 将 ComVisible 设置为 false 使此程序集中的类型\r\n// 对 COM 组件不可见。  如果需要从 COM 访问此程序集中的类型，\r\n// 则将该类型上的 ComVisible 特性设置为 true。\r\n[assembly: ComVisible(false)]\r\n\r\n//若要开始生成可本地化的应用程序，请在 \r\n//<PropertyGroup> 中的 .csproj 文件中\r\n//设置 <UICulture>CultureYouAreCodingWith</UICulture>。  例如，如果您在源文件中\r\n//使用的是美国英语，请将 <UICulture> 设置为 en-US。  然后取消\r\n//对以下 NeutralResourceLanguage 特性的注释。  更新\r\n//以下行中的“en-US”以匹配项目文件中的 UICulture 设置。\r\n\r\n//[assembly: NeutralResourcesLanguage(\"en-US\", UltimateResourceFallbackLocation.Satellite)]\r\n\r\n\r\n[assembly: ThemeInfo(\r\n    ResourceDictionaryLocation.None, //主题特定资源词典所处位置\r\n    //(在页面或应用程序资源词典中 \r\n    // 未找到某个资源的情况下使用)\r\n    ResourceDictionaryLocation.SourceAssembly //常规资源词典所处位置\r\n    //(在页面、应用程序或任何主题特定资源词典中\r\n    // 未找到某个资源的情况下使用)\r\n)]\r\n\r\n\r\n// 程序集的版本信息由下面四个值组成: \r\n//\r\n//      主版本\r\n//      次版本 \r\n//      生成号\r\n//      修订号\r\n//\r\n// 可以指定所有这些值，也可以使用“生成号”和“修订号”的默认值，\r\n// 方法是按如下所示使用“*”: \r\n// [assembly: AssemblyVersion(\"1.0.*\")]\r\n//[assembly: AssemblyVersion(\"1.0.0.1\")]\r\n//[assembly: AssemblyFileVersion(\"1.0.0.1\")]\r\n";
        }

        public static string GetAddPageXaml(string ProjectName, string Extend, string Height)
        {
            return "<common:WindowPlugin\r\n    " +
                "x:Class=\"" + ProjectName + ".Add\"\r\n    " +
                "xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"\r\n    " +
                "xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"\r\n    " +
                "xmlns:common=\"http://schemas.creative.com/plugin\"\r\n    " +
                "xmlns:controls=\"http://schemas.creative.com/controls\"\r\n    " +
                "xmlns:d=\"http://schemas.microsoft.com/expression/blend/2008\"\r\n    " +
                "xmlns:local=\"clr-namespace:" + ProjectName + "\"\r\n    " +
                "xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\"\r\n    " +
                "Title=\"{DynamicResource Title_Add}\"\r\n    Width=\"440\"\r\n    Height=\"500\"\r\n    " +
                "mc:Ignorable=\"d\">\r\n    " +
                "<Window.Resources>\r\n        " +
                "<ResourceDictionary>\r\n            " +
                "<ResourceDictionary.MergedDictionaries>\r\n                " +
                "<ResourceDictionary Source=\"/Creative.Plugin.Common;component/Themes/Generic.xaml\" />\r\n                " +
                "<ResourceDictionary Source=\"/" + ProjectName + ";component/Resources/Strings.zh-CN.xaml\" />\r\n            " +
                "</ResourceDictionary.MergedDictionaries>\r\n        </ResourceDictionary>\r\n    " +
                "</Window.Resources>\r\n    " +
                "<Grid>\r\n        " +
                "<Grid.RowDefinitions>\r\n            " +
                "<RowDefinition Height=\"" + Height + "\" />\r\n            " +
                "<RowDefinition Height=\"" + Height + "\" />\r\n            " +
                "<RowDefinition Height=\"" + Height + "\" />\r\n            " +
                "<RowDefinition Height=\"" + Height + "\" />\r\n            " +
                "<RowDefinition Height=\"" + Height + "\" />\r\n            " +
                "<RowDefinition Height=\"" + Height + "\" />\r\n            " +
                "<RowDefinition Height=\"" + Height + "\" />\r\n            " +
                "<RowDefinition Height=\"" + Height + "\" />\r\n            " +
                "<RowDefinition Height=\"" + Height + "\" />\r\n            " +
                "<RowDefinition Height=\"*\" />\r\n        " +
                "</Grid.RowDefinitions>\r\n        \r\n        " +
                Extend +
                "<Border\r\n            " +
                "Grid.Row=\"10\"\r\n            " +
                "Grid.Column=\"0\"\r\n            " +
                "Grid.ColumnSpan=\"3\"\r\n            " +
                "Style=\"{DynamicResource BottomControlPanelStyle}\">\r\n            " +
                "<StackPanel\r\n                Height=\"auto\"\r\n                HorizontalAlignment=\"Right\"\r\n                Orientation=\"Horizontal\">\r\n                " +
                "<Button\r\n                    Command=\"{Binding CmdSave}\"\r\n                    Content=\"{DynamicResource Submit}\"\r\n                    Style=\"{DynamicResource HighLightButtonStyle}\" />\r\n                " +
                "<Button\r\n                    Margin=\"{StaticResource BtnMargin}\"\r\n                    Content=\"{DynamicResource Cancel}\"\r\n                    IsCancel=\"True\" />\r\n            " +
                "</StackPanel>\r\n        " +
                "</Border>\r\n    " +
                "</Grid>\r\n" +
                "</common:WindowPlugin>\r\n";
        }

        public static string GetAddPageXamlCs(string ProjectName)
        {
            return "using DAF.Plugin.Common;\r\n" +
                "using "+ProjectName+".ViewModels;\r\n" +
                "using System;\r\nusing System.Windows;\r\n\r\n" +
                "namespace "+ProjectName+"\r\n" +
                "{\r\n    /// <summary>\r\n    /// Add.xaml 的交互逻辑\r\n    /// </summary>\r\n    " +
                "public partial class Add : WindowPlugin\r\n    {\r\n\r\n        " +
                "public Add(string parameters) : base(parameters)\r\n        " +
                "{\r\n            InitializeComponent();\r\n            " +
                "this.DataContext = this;\r\n        }\r\n        " +
                "AddVM vm;\r\n\r\n        " +
                "public override void PluginLoadCompleted()\r\n        {\r\n            " +
                "base.PluginLoadCompleted();\r\n            try\r\n            " +
                "{\r\n                vm = new AddVM(this);\r\n                " +
                "this.DataContext = vm;\r\n            }\r\n            " +
                "catch (Exception ex)\r\n            {\r\n                " +
                "MessageBox.Show(ex.Message);\r\n            " +
                "}\r\n\r\n        }\r\n        \r\n    " +
                "}\r\n}\r\n";
        }

        public static string GetAddVM(string ProjectName, string Extend, string PostData = "", string IsLegal = "true")
        {
            string s = "using DAF.Plugin.Common;\r\n" +
                "using System.Collections.ObjectModel;\r\n" +
                "using System.Collections.Generic;\r\n" +
                "namespace " + ProjectName + ".ViewModels\r\n{\r\n    " +
                "class AddVM : WindowViewModelBase\r\n    {\r\n        \r\n        " +
                "public AddVM(IWindowPlugin plugin) : base(plugin)\r\n        " +
                "{\r\n            LoadData();\r\n        }\r\n\r\n        " +
                "#region 分页\r\n\r\n        " +
                "private int _pageIndex = 1;\r\n        " +
                "public int PageIndex\r\n        {\r\n            " +
                "get { return _pageIndex; }\r\n            " +
                "set\r\n            {\r\n                " +
                "if (_pageIndex != value)\r\n                {\r\n                    " +
                "_pageIndex = value;\r\n                    " +
                "NotifyPropertyChanged(\"PageIndex\");\r\n                    " +
                "LoadData();\r\n                }\r\n            }\r\n        }\r\n\r\n        " +
                "private int _pageSize = 20;\r\n        " +
                "public int PageSize\r\n        {\r\n            get { return _pageSize; }\r\n            " +
                "set\r\n            {\r\n                _pageSize = value;\r\n                " +
                "NotifyPropertyChanged(\"PageSize\");\r\n                " +
                "LoadData();\r\n            }\r\n        }\r\n\r\n\r\n        " +
                "private int _totalCount;\r\n        " +
                "public int TotalCount\r\n        {\r\n            " +
                "get { return _totalCount; }\r\n            set\r\n            {\r\n                " +
                "if (_totalCount != value)\r\n                {\r\n                    " +
                "_totalCount = value;\r\n                    " +
                "NotifyPropertyChanged(\"TotalCount\");\r\n                }\r\n            }\r\n        }\r\n\r\n        " +
                "#endregion\r\n\r\n        " +
                "/// <summary>\r\n        /// 主数据\r\n        /// </summary>\r\n        " +
                "private List<Model> _DataSource;\r\n        " +
                "public List<Model> DataSource\r\n        {\r\n            get\r\n            {\r\n                " +
                "return _DataSource;\r\n            }\r\n            set\r\n            {\r\n                " +
                "_DataSource = value;\r\n                " +
                "NotifyPropertyChanged(\"DataSource\");\r\n            }\r\n        }\r\n\r\n        " +
                "private Model _SelectedRow;\r\n        " +
                "public Model SelectedRow\r\n        {\r\n            get\r\n            {\r\n                " +
                "return _SelectedRow;\r\n            }\r\n            set\r\n            {\r\n                " +
                "_SelectedRow = value;\r\n                " +
                "NotifyPropertyChanged(\"SelectedRow\");\r\n            }\r\n        }\r\n\r\n        " +
                Extend +
                "public SimpleCommand CmdSave => new SimpleCommand()\r\n        {\r\n            " +
                "ExecuteDelegate = x =>\r\n            {\r\n                " +
                "if (IsLegal())\r\n                " +
                "{\r\n                    " +
                "plugin.Framework.PostData(Services.url_add, new{model = PostData()});\r\n"+
                "this.plugin.DialogResult = true;\r\n                }\r\n            },\r\n            " +
                "CanExecuteDelegate = o =>\r\n            {\r\n                " +
                "return IsLegal();\r\n            }\r\n        };\r\n\r\n        #region 方法\r\n        " +
                "/// <summary>\r\n        /// 初始化数据\r\n        /// </summary>\r\n        " +
                "private void LoadData()\r\n        {\r\n            \r\n        }\r\n\r\n        " +
                "/// <summary>\r\n        /// 保存数据\r\n        /// </summary>\r\n        " +
                "private List<Model> PostData()\r\n        " +
                "{\r\n\r\n        " +
                PostData +
                "}\r\n\r\n        " +
                "/// <summary>\r\n        /// 校验数据\r\n        /// </summary>\r\n        /// <returns></returns>\r\n        " +
                "private bool IsLegal()\r\n        " +
                "{\r\n            " +
                "return "+
                IsLegal +
                ";\r\n        " +
                "}\r\n        " +
                "#endregion\r\n\r\n    }\r\n}\r\n";
            return s;
        }

        public static string GetEditPageXaml(string ProjectName, string Extend, string Height)
        {
            return "<common:WindowPlugin\r\n    " +
                "x:Class=\"" + ProjectName + ".Edit\"\r\n    " +
                "xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"\r\n    " +
                "xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"\r\n    " +
                "xmlns:common=\"http://schemas.creative.com/plugin\"\r\n    " +
                "xmlns:controls=\"http://schemas.creative.com/controls\"\r\n    " +
                "xmlns:d=\"http://schemas.microsoft.com/expression/blend/2008\"\r\n    " +
                "xmlns:local=\"clr-namespace:" + ProjectName + "\"\r\n    " +
                "xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\"\r\n    " +
                "Title=\"{DynamicResource Title_Edit}\"\r\n    Width=\"440\"\r\n    Height=\"500\"\r\n    " +
                "mc:Ignorable=\"d\">\r\n    " +
                "<Window.Resources>\r\n        " +
                "<ResourceDictionary>\r\n            " +
                "<ResourceDictionary.MergedDictionaries>\r\n                " +
                "<ResourceDictionary Source=\"/Creative.Plugin.Common;component/Themes/Generic.xaml\" />\r\n                " +
                "<ResourceDictionary Source=\"/" + ProjectName + ";component/Resources/Strings.zh-CN.xaml\" />\r\n            " +
                "</ResourceDictionary.MergedDictionaries>\r\n        </ResourceDictionary>\r\n    " +
                "</Window.Resources>\r\n    " +
                "<Grid>\r\n        " +
                "<Grid.RowDefinitions>\r\n            " +
                "<RowDefinition Height=\"" + Height + "\" />\r\n            " +
                "<RowDefinition Height=\"" + Height + "\" />\r\n            " +
                "<RowDefinition Height=\"" + Height + "\" />\r\n            " +
                "<RowDefinition Height=\"" + Height + "\" />\r\n            " +
                "<RowDefinition Height=\"" + Height + "\" />\r\n            " +
                "<RowDefinition Height=\"" + Height + "\" />\r\n            " +
                "<RowDefinition Height=\"" + Height + "\" />\r\n            " +
                "<RowDefinition Height=\"" + Height + "\" />\r\n            " +
                "<RowDefinition Height=\"" + Height + "\" />\r\n            " +
                "<RowDefinition Height=\"*\" />\r\n        " +
                "</Grid.RowDefinitions>\r\n        \r\n        " +
                Extend +
                "<Border\r\n            " +
                "Grid.Row=\"10\"\r\n            " +
                "Grid.Column=\"0\"\r\n            " +
                "Grid.ColumnSpan=\"3\"\r\n            " +
                "Style=\"{DynamicResource BottomControlPanelStyle}\">\r\n            " +
                "<StackPanel\r\n                Height=\"auto\"\r\n                HorizontalAlignment=\"Right\"\r\n                Orientation=\"Horizontal\">\r\n                " +
                "<Button\r\n                    Command=\"{Binding CmdSave}\"\r\n                    Content=\"{DynamicResource Submit}\"\r\n                    Style=\"{DynamicResource HighLightButtonStyle}\" />\r\n                " +
                "<Button\r\n                    Margin=\"{StaticResource BtnMargin}\"\r\n                    Content=\"{DynamicResource Cancel}\"\r\n                    IsCancel=\"True\" />\r\n            " +
                "</StackPanel>\r\n        " +
                "</Border>\r\n    " +
                "</Grid>\r\n" +
                "</common:WindowPlugin>\r\n";
        }

        public static string GetEditPageXamlCs(string ProjectName)
        {
            return "using DAF.Plugin.Common;\r\n" +
                "using " + ProjectName + ".ViewModels;\r\n" +
                "using System;\r\nusing System.Windows;\r\n\r\n" +
                "namespace " + ProjectName + "\r\n" +
                "{\r\n    /// <summary>\r\n    /// Edit.xaml 的交互逻辑\r\n    /// </summary>\r\n    " +
                "public partial class Edit : WindowPlugin\r\n    {\r\n\r\n        string json;\r\n" +
                "public Edit(string parameters) : base(parameters)\r\n        " +
                "{\r\n            json = parameters;\r\n" +
                "InitializeComponent();\r\n            " +
                "this.DataContext = this;\r\n        }\r\n        " +
                "EditVM vm;\r\n\r\n        " +
                "public override void PluginLoadCompleted()\r\n        {\r\n            " +
                "base.PluginLoadCompleted();\r\n            try\r\n            " +
                "{\r\n                vm = new EditVM(this, json);\r\n                " +
                "this.DataContext = vm;\r\n            }\r\n            " +
                "catch (Exception ex)\r\n            {\r\n                " +
                "MessageBox.Show(ex.Message);\r\n            " +
                "}\r\n\r\n        }\r\n        \r\n    " +
                "}\r\n}\r\n";
        }

        public static string GetEditVM(string ProjectName, string Extend, string PostData = "", string IsLegal = "true", string LoadData = "")
        {
            string s = "using DAF.Plugin.Common;\r\n" +
                "using Newtonsoft.Json;\r\n"+
                "using System.Collections.ObjectModel;\r\n" +
                "using System.Collections.Generic;\r\n" +
                "namespace " + ProjectName + ".ViewModels\r\n{\r\n    " +
                "class EditVM : WindowViewModelBase\r\n    {\r\n        \r\n        string json;" +
                "public EditVM(IWindowPlugin plugin, string js) : base(plugin)\r\n        " +
                "{\r\n            json = js;\r\n" +
                "LoadData();\r\n        }\r\n\r\n        " +
                "#region 分页\r\n\r\n        " +
                "private int _pageIndex = 1;\r\n        " +
                "public int PageIndex\r\n        {\r\n            " +
                "get { return _pageIndex; }\r\n            " +
                "set\r\n            {\r\n                " +
                "if (_pageIndex != value)\r\n                {\r\n                    " +
                "_pageIndex = value;\r\n                    " +
                "NotifyPropertyChanged(\"PageIndex\");\r\n                    " +
                "LoadData();\r\n                }\r\n            }\r\n        }\r\n\r\n        " +
                "private int _pageSize = 20;\r\n        " +
                "public int PageSize\r\n        {\r\n            get { return _pageSize; }\r\n            " +
                "set\r\n            {\r\n                _pageSize = value;\r\n                " +
                "NotifyPropertyChanged(\"PageSize\");\r\n                " +
                "LoadData();\r\n            }\r\n        }\r\n\r\n\r\n        " +
                "private int _totalCount;\r\n        " +
                "public int TotalCount\r\n        {\r\n            " +
                "get { return _totalCount; }\r\n            set\r\n            {\r\n                " +
                "if (_totalCount != value)\r\n                {\r\n                    " +
                "_totalCount = value;\r\n                    " +
                "NotifyPropertyChanged(\"TotalCount\");\r\n                }\r\n            }\r\n        }\r\n\r\n        " +
                "#endregion\r\n\r\n        " +
                "/// <summary>\r\n        /// 主数据\r\n        /// </summary>\r\n        " +
                "private List<Model> _DataSource;\r\n        " +
                "public List<Model> DataSource\r\n        {\r\n            get\r\n            {\r\n                " +
                "return _DataSource;\r\n            }\r\n            set\r\n            {\r\n                " +
                "_DataSource = value;\r\n                " +
                "NotifyPropertyChanged(\"DataSource\");\r\n            }\r\n        }\r\n\r\n        " +
                "private Model _SelectedRow;\r\n        " +
                "public Model SelectedRow\r\n        {\r\n            get\r\n            {\r\n                " +
                "return _SelectedRow;\r\n            }\r\n            set\r\n            {\r\n                " +
                "_SelectedRow = value;\r\n                " +
                "NotifyPropertyChanged(\"SelectedRow\");\r\n            }\r\n        }\r\n\r\n        " +
                Extend +
                "public SimpleCommand CmdSave => new SimpleCommand()\r\n        {\r\n            " +
                "ExecuteDelegate = x =>\r\n            {\r\n                " +
                "if (IsLegal())\r\n                " +
                "{\r\n                    " +
                "plugin.Framework.PostData(Services.url_edit, new{model = PostData()});\r\n" +
                "this.plugin.DialogResult = true;\r\n                }\r\n            },\r\n            " +
                "CanExecuteDelegate = o =>\r\n            {\r\n                " +
                "return IsLegal();\r\n            }\r\n        };\r\n\r\n        #region 方法\r\n        " +
                "/// <summary>\r\n        /// 初始化数据\r\n        /// </summary>\r\n        " +
                "private void LoadData()\r\n        {\r\n        " +
                LoadData +
                "    \r\n        }\r\n\r\n        " +
                "/// <summary>\r\n        /// 保存数据\r\n        /// </summary>\r\n        " +
                "private List<Model> PostData()\r\n        " +
                "{\r\n\r\n        " +
                PostData +
                "}\r\n\r\n        " +
                "/// <summary>\r\n        /// 校验数据\r\n        /// </summary>\r\n        /// <returns></returns>\r\n        " +
                "private bool IsLegal()\r\n        " +
                "{\r\n            " +
                "return " +
                IsLegal +
                ";\r\n        " +
                "}\r\n        " +
                "#endregion\r\n\r\n    }\r\n}\r\n";
            return s;
        }

        public static string GetBoxesXaml(string ProjectName, string BoxName, string Extend, string Search = "")
        {
            string s = "<common:WindowPlugin\r\n    " +
                "x:Class=\""+ ProjectName + "."+ BoxName + "\"\r\n    " +
                "xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"\r\n    " +
                "xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"\r\n    " +
                "xmlns:common=\"http://schemas.creative.com/plugin\"\r\n    " +
                "xmlns:controls=\"http://schemas.creative.com/controls\"\r\n    " +
                "xmlns:d=\"http://schemas.microsoft.com/expression/blend/2008\"\r\n    " +
                "xmlns:local=\"clr-namespace:"+ ProjectName + "\"\r\n    " +
                "xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\"\r\n    " +
                "Title=\"{DynamicResource Title_"+ BoxName + "}\"\r\n    " +
                "Width=\"700\"\r\n    Height=\"480\"\r\n    mc:Ignorable=\"d\">\r\n    " +
                "<Window.Resources>\r\n        " +
                "<ResourceDictionary>\r\n            " +
                "<ResourceDictionary.MergedDictionaries>\r\n                " +
                "<ResourceDictionary Source=\"/Creative.Plugin.Common;component/Themes/Generic.xaml\" />\r\n                " +
                "<ResourceDictionary Source=\"/" + ProjectName + ";component/Resources/Strings.zh-CN.xaml\" />\r\n            " +
                "</ResourceDictionary.MergedDictionaries>\r\n        " +
                "</ResourceDictionary>\r\n    " +
                "</Window.Resources>\r\n    " +
                "<Grid>\r\n        " +
                "<Grid.RowDefinitions>\r\n            " +
                "<RowDefinition Height=\"Auto\" />\r\n            " +
                "<RowDefinition />\r\n            " +
                "<RowDefinition Height=\"Auto\" />\r\n        " +
                "<RowDefinition Height=\"Auto\" />\r\n        " +
                "</Grid.RowDefinitions>\r\n        " +
                "<Grid\r\n            Grid.Row=\"0\"\r\n            Margin=\"5,10,0,0\"\r\n            " +
                "VerticalAlignment=\"Center\">\r\n            " +
                "<Grid.ColumnDefinitions>\r\n                " +
                "<ColumnDefinition Width=\"Auto\" />\r\n                " +
                "</Grid.ColumnDefinitions>\r\n            " +
                "<TextBox\r\n                " +
                "Grid.Column=\"0\"\r\n                Margin=\"{StaticResource BtnMargin}\"\r\n                " +
                "controls:TextBoxHelper.ButtonCommand=\"{Binding CmdSearch}\"\r\n                " +
                "controls:TextBoxHelper.Watermark=\"{DynamicResource "+ Search + "_Watermark}\"\r\n                " +
                "Text=\"{Binding Text, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}\"\r\n                " +
                "Style=\"{DynamicResource SearchMetroTextBox}\" />\r\n        " +
                "</Grid>\r\n        " +
                "<Border\r\n            Grid.Row=\"1\"\r\n            Margin=\"0,10,0,0\"\r\n            " +
                "Background=\"{DynamicResource PanelBrush}\">\r\n            " +
                "<Grid Margin=\"{DynamicResource ContainerPadding}\">\r\n                " +
                "<DataGrid\r\n                    IsReadOnly=\"True\"\r\n                    " +
                "ItemsSource=\"{Binding DataSource}\"\r\n                    " +
                "SelectedItem=\"{Binding SelectedRow, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}\">\r\n                    " +
                "<DataGrid.Columns>\r\n                        \r\n                    " +
                Extend +
                "</DataGrid.Columns>\r\n                </DataGrid>\r\n            " +
                "</Grid>\r\n        </Border>\r\n        " +
                "<Border\r\n            Grid.Row=\"3\"\r\n            " +
                "Grid.Column=\"0\"\r\n            Grid.ColumnSpan=\"3\"\r\n            " +
                "Style=\"{DynamicResource BottomControlPanelStyle}\">\r\n            " +
                "<StackPanel\r\n                Height=\"auto\"\r\n                " +
                "HorizontalAlignment=\"Right\"\r\n                " +
                "Orientation=\"Horizontal\">\r\n                " +
                "<Button\r\n                    " +
                "Content=\"{DynamicResource Submit}\"\r\n                    " +
                "Style=\"{DynamicResource HighLightButtonStyle}\" />\r\n                " +
                "<Button\r\n                    Margin=\"{StaticResource BtnMargin}\"\r\n                    " +
                "Content=\"{DynamicResource Cancel}\"\r\n                    IsCancel=\"True\" />\r\n            " +
                "</StackPanel>\r\n        </Border>\r\n    " +
                "<controls:DataPager Grid.Row=\"2\" PageSize=\"{Binding PageSize, Mode=TwoWay}\" TotalCount=\"{Binding TotalCount, Mode=TwoWay}\" PageIndex=\"{Binding PageIndex, Mode=TwoWay}\"/>"+
                "</Grid>\r\n" +
                "</common:WindowPlugin>\r\n";
            return s;
        }

        public static string GetBoxesVM(string ProjectName, string BoxName, string Extend = "", string url = "")
        {
            string s = "using DAF.Plugin.Common;\r\n" +
                "using MES.Plugin.Common;\r\nusing System;\r\n" +
                "using System.Collections.Generic;\r\n" +
                "using System.Collections.ObjectModel;\r\n" +
                "using System.Data;\r\nu" +
                "sing System.Linq;\r\n" +
                "using System.Text;\r\n" +
                "using System.Threading.Tasks;\r\n\r\n" +
                "namespace "+ ProjectName + ".ViewModels\r\n{\r\n    " +
                "class "+ BoxName + "VM : WindowViewModelBase\r\n    {\r\n        " +
                "public " + BoxName + "VM(IWindowPlugin plugin) : base(plugin)\r\n        {\r\n            " +
                "LoadData();\r\n        }\r\n\r\n        " +
                "#region 分页\r\n\r\n        private int _pageIndex = 1;\r\n        " +
                "public int PageIndex\r\n        {\r\n            get { return _pageIndex; }\r\n            " +
                "set\r\n            {\r\n                if (_pageIndex != value)\r\n                " +
                "{\r\n                    _pageIndex = value;\r\n                    " +
                "NotifyPropertyChanged(\"PageIndex\");\r\n                    " +
                "LoadData();\r\n                }\r\n            }\r\n        }\r\n\r\n        " +
                "private int _pageSize = 20;\r\n        public int PageSize\r\n        {\r\n            " +
                "get { return _pageSize; }\r\n            set\r\n            {\r\n                " +
                "_pageSize = value;\r\n                NotifyPropertyChanged(\"PageSize\");\r\n                " +
                "LoadData();\r\n            }\r\n        }\r\n\r\n\r\n        private int _totalCount;\r\n        " +
                "public int TotalCount\r\n        {\r\n            get { return _totalCount; }\r\n            " +
                "set\r\n            {\r\n                if (_totalCount != value)\r\n                {\r\n                    " +
                "_totalCount = value;\r\n                    NotifyPropertyChanged(\"TotalCount\");\r\n                " +
                "}\r\n            }\r\n        }\r\n\r\n        #endregion\r\n\r\n        " +
                "private List<Model> _DataSource;\r\n        public List<Model> DataSource\r\n        " +
                "{\r\n            get\r\n            {\r\n                return _DataSource;\r\n            " +
                "}\r\n            set\r\n            {\r\n                _DataSource = value;\r\n                " +
                "NotifyPropertyChanged(\"DataSource\");\r\n            }\r\n        }\r\n        \r\n        " +
                "private Model _SelectedRow;\r\n        public Model SelectedRow\r\n        {\r\n            " +
                "get\r\n            {\r\n                return _SelectedRow;\r\n            }\r\n            " +
                "set\r\n            {\r\n                _SelectedRow = value;\r\n                " +
                "NotifyPropertyChanged(\"SelectedRow\");\r\n            }\r\n        }\r\n        \r\n        " +
                "private string _Text;\r\n        public string Text\r\n        {\r\n            " +
                "get { return _Text; }\r\n            set\r\n            {\r\n                " +
                "_Text = value;\r\n                NotifyPropertyChanged(\"Text\");\r\n            " +
                "}\r\n        }\r\n\r\n" +
                Extend +
                "        #region Command\r\n        " +
                "public SimpleCommand CmdSearch => new SimpleCommand\r\n        {\r\n            " +
                "ExecuteDelegate = x =>\r\n            {\r\n                LoadData();\r\n            },\r\n            " +
                "CanExecuteDelegate = x =>\r\n            {\r\n                return true;\r\n            }\r\n        };\r\n        \r\n        public SimpleCommand CmdSave => new SimpleCommand()\r\n        {\r\n            ExecuteDelegate = x => {\r\n                this.plugin.Data = SelectedRow;\r\n            },\r\n            CanExecuteDelegate = o => {\r\n                return SelectedRow != null;\r\n            }\r\n        };\r\n        #endregion\r\n\r\n        #region 方法\r\n        " +
                "private void LoadData()\r\n        " +
                "{\r\n\r\n        " +
                 url +
                "}\r\n        #endregion\r\n    }\r\n}\r\n";
            return s;
        }

        public static string GetBoxesXamlCs(string ProjectName, string BoxName, string Extend = "")
        {
            string s = "using DAF.Plugin.Common;\r\n" +
                "using "+ProjectName+".ViewModels;\r\n" +
                "using System;\r\nusing System.Windows;\r\n\r\n" +
                "namespace " + ProjectName + "\r\n" +
                "{\r\n    /// <summary>\r\n    /// Add.xaml 的交互逻辑\r\n    /// </summary>\r\n    " +
                "public partial class "+BoxName+" : WindowPlugin\r\n    {\r\n        " +
                "public " + BoxName + "(string parameters) : base(parameters)\r\n        {\r\n            " +
                "InitializeComponent();\r\n            this.DataContext = this;\r\n        }\r\n\r\n        " +
                "" + BoxName + "VM vm;\r\n\r\n        public override void PluginLoadCompleted()\r\n        " +
                "{\r\n            base.PluginLoadCompleted();\r\n            try\r\n            {\r\n                " +
                "vm = new " + BoxName + "VM(this);\r\n                this.DataContext = vm;\r\n            }\r\n            " +
                "catch (Exception ex)\r\n            {\r\n\r\n                MessageBox.Show(ex.Message);\r\n            " +
                "}\r\n   " +
                Extend +
                "     }\r\n    }\r\n}";
            return s;
        }

        public static string GetServices(string ProjectName, string Extend = "")
        {
            string s = "namespace "+ProjectName + "\r\n" +
                "{\r\n\tstatic class Services\r\n\t{" +
                Extend +
                "\r\n\t}\r\n}";
            return s;
        }

        public static string GetImportXaml(string ProjectName, string Extend)
        {
            string s = "<common:WindowPlugin x:Class=\""+ ProjectName+ ".ImportPage\" \r\n                     " +
                "xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" \r\n                     " +
                "xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" \r\n                     " +
                "xmlns:common=\"http://schemas.creative.com/plugin\" \r\n                     " +
                "xmlns:d=\"http://schemas.microsoft.com/expression/blend/2008\" \r\n                     " +
                "xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\" \r\n                     " +
                "xmlns:controls=\"http://schemas.creative.com/controls\" \r\n                     " +
                "Title=\"{DynamicResource Import}\" Width=\"800\" Height=\"500\" AllowDrop=\"True\" \r\n                     " +
                "Background=\"{DynamicResource BackgroundBrush}\" \r\n                     " +
                "WindowStartupLocation=\"CenterScreen\" mc:Ignorable=\"d\">\r\n  " +
                "<common:WindowPlugin.Resources>\r\n    <ResourceDictionary>\r\n      " +
                "<ResourceDictionary.MergedDictionaries>\r\n        " +
                "<ResourceDictionary Source=\"/Creative.Plugin.Common;component/Themes/Generic.xaml\" />\r\n        " +
                "<ResourceDictionary Source=\"/" + ProjectName + ";component/Resources/Strings.zh-CN.xaml\" />\r\n      " +
                "</ResourceDictionary.MergedDictionaries>\r\n    </ResourceDictionary>\r\n  " +
                "</common:WindowPlugin.Resources>\r\n  " +
                "<Grid>\r\n    <Grid.RowDefinitions>\r\n      " +
                "<RowDefinition Height=\"10\" />\r\n      " +
                "<RowDefinition Height=\"40\" />\r\n      " +
                "<RowDefinition Height=\"*\" />\r\n      " +
                "<RowDefinition Height=\"auto\" />\r\n    " +
                "</Grid.RowDefinitions>\r\n    " +
                "<Grid.ColumnDefinitions>\r\n      " +
                "<ColumnDefinition Width=\"180\" />\r\n      " +
                "<ColumnDefinition Width=\"180\" />\r\n      " +
                "<ColumnDefinition Width=\"180\" />\r\n      " +
                "<ColumnDefinition Width=\"*\" />\r\n    " +
                "</Grid.ColumnDefinitions>\r\n    " +
                "<Button Grid.Row=\"1\" Grid.Column=\"0\" Width=\"130\" Margin=\"{StaticResource BtnMargin}\" HorizontalAlignment=\"Left\" Command=\"{Binding CmdImport}\" Content=\"{DynamicResource SelectFile}\" />\r\n    " +
                "<Button Grid.Row=\"1\" Grid.Column=\"1\" Margin=\"{StaticResource BtnMargin}\" HorizontalAlignment=\"Left\" Command=\"{Binding CmdDownTemp}\" Content=\"{DynamicResource DownloadTemplate}\" />\r\n    " +
                "<DataGrid Name=\"dgRepairOrder\" Grid.Row=\"2\" Grid.Column=\"0\" Grid.ColumnSpan=\"8\" Margin=\"10,0,10,0\" AutoGenerateColumns=\"False\" CanUserAddRows=\"False\" CanUserDeleteRows=\"False\" CanUserReorderColumns=\"True\" CanUserSortColumns=\"True\" IsReadOnly=\"True\" ItemsSource=\"{Binding DataSource, Mode=TwoWay}\" SelectionMode=\"Single\">\r\n      " +
                "<DataGrid.Columns>\r\n        \r\n      " +
                Extend +
                "</DataGrid.Columns>\r\n    " +
                
                "</DataGrid>\r\n    " +
                "<Border Grid.Row=\"6\" Grid.Column=\"0\" Grid.ColumnSpan=\"5\" FlowDirection=\"RightToLeft\" Style=\"{DynamicResource BottomControlPanelStyle}\">\r\n      <StackPanel Orientation=\"Horizontal\">\r\n        " +
                "<Button Content=\"{DynamicResource Cancel}\" IsCancel=\"True\" />\r\n        <Button Margin=\"{DynamicResource BtnMargin}\" Command=\"{Binding CmdSave}\" Content=\"{DynamicResource Import}\" Style=\"{DynamicResource HighLightButtonStyle}\" />\r\n      " +
                "</StackPanel>\r\n    </Border>\r\n  </Grid>\r\n" +
                "</common:WindowPlugin>";
            return s;
        }

        public static string GetImportXamlCs(string ProjectName)
        {
            string s = "using DAF.Plugin.Common;\r\n" +
                "using "+ ProjectName + ".ViewModel;\r\n" +
                "using System;\r\n" +
                "using System.Windows;\r\n\r\n" +
                "namespace " + ProjectName + "\r\n{\r\n    /// <summary>\r\n    /// Import.xaml 的交互逻辑\r\n    /// </summary>\r\n    " +
                "public partial class ImportPage : WindowPlugin\r\n    {\r\n        ImportPageVM vm;\r\n\r\n        " +
                "public ImportPage(string paramters)\r\n        {\r\n            InitializeComponent();\r\n        }\r\n\r\n        " +
                "public override void PluginLoadCompleted()\r\n        {\r\n            base.PluginLoadCompleted();\r\n            try\r\n            {\r\n                vm = new ImportPageVM(this);\r\n                this.DataContext = vm;\r\n            }\r\n            catch (Exception ex)\r\n            {\r\n                MessageBox.Show(ex.Message);\r\n            }\r\n        }\r\n    }\r\n}\r\n";
            return s;
        }

        public static string GetImprotVM(string ProjectName, ImportClass import, string Xss, string EmptyCode, string RepeatCode, string Function, string CheckImportData)
        {
            string s = "using System;\r\n" +
                "using System.Collections.Generic;\r\n" +
                "using System.IO;\r\nusing System.Linq;\r\n" +
                "using System.Net;\r\nusing System.Windows;\r\n" +
                "using DAF.Plugin.Common;\r\nusing Microsoft.Win32;\r\n" +
                "using MES.Plugin.Common;\r\nusing NPOI.XSSF.UserModel;\r\n\r\n" +
                "namespace "+ ProjectName + ".ViewModel\r\n{\r\n    " +
                "public class ImportPageVM : WindowViewModelBase\r\n    {\r\n\r\n        " +
                "public ImportPageVM(WindowPlugin plugin) : base(plugin)\r\n        {\r\n\r\n        }\r\n\r\n\r\n        private List<Model> _DataSource;\r\n        public List<Model> DataSource\r\n        {\r\n            get\r\n            {\r\n                return _DataSource;\r\n            }\r\n            set\r\n            {\r\n                _DataSource = value;\r\n                NotifyPropertyChanged(\"DataSource\");\r\n            }\r\n        }\r\n\r\n\r\n\r\n\r\n        /// <summary>\r\n        /// 下载模板\r\n        /// </summary>\r\n        public SimpleCommand CmdDownTemp => new SimpleCommand\r\n        {\r\n            ExecuteDelegate = (o) =>\r\n            {\r\n                DownTemp();\r\n            }\r\n        };\r\n\r\n        public SimpleCommand CmdImport => new SimpleCommand\r\n        {\r\n            ExecuteDelegate = (o) =>\r\n            {\r\n                Import();\r\n            }\r\n        };\r\n\r\n        public SimpleCommand CmdSave => new SimpleCommand\r\n        {\r\n            ExecuteDelegate = (o) =>\r\n            {\r\n                List<Model> models = new List<Model>();\r\n                foreach(var ds in DataSource)\r\n                {\r\n                    if (ds.IsChecked) models.Add(ds);\r\n                }\r\n                plugin.Framework.PostData(Services.url_add, new\r\n                {\r\n                    model = models\r\n                });\r\n                plugin.DialogResult = true;\r\n            },\r\n            CanExecuteDelegate = x =>\r\n            {\r\n                return DataSource != null;\r\n            }\r\n        };\r\n\r\n\r\n        public void DownTemp()\r\n        {\r\n            string fileName = string.Format(\"{0}{1}\",Translator.Get(\"Title\"), \".xlsx\");\r\n            string allPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;\r\n            string path = allPath + \"\\\\Templates\\\\Import\\\\\" + fileName;\r\n            WebClient webClient = new WebClient();\r\n            SaveFileDialog dlg = new SaveFileDialog();\r\n            dlg.FileName = fileName;\r\n            if (dlg.ShowDialog() == true)\r\n            {\r\n                //获取要保存文件名的完整路径\r\n                string filename = dlg.FileName;\r\n                try\r\n                {\r\n                    webClient.DownloadFile(path, filename);\r\n                }\r\n                catch (Exception)\r\n                {\r\n                    throw;\r\n                }\r\n            }\r\n        }\r\n\r\n        " +
                "public void Import()\r\n        {\r\n            try\r\n            {\r\n                " +
                "OpenFileDialog open = new OpenFileDialog();\r\n                " +
                "open.Filter = \"Excel (*.XLSX)|*.xlsx|*.XLS|*.xls\";\r\n                " +
                "if ((bool)(open.ShowDialog()))\r\n                {\r\n\r\n                    //上传成功后获取文件进行读取\r\n                    XSSFWorkbook xssfWorkbook;\r\n                    " +
                "using (FileStream fs = new FileStream(open.FileName, FileMode.Open, FileAccess.Read))\r\n                    {\r\n                        xssfWorkbook = new XSSFWorkbook(fs);\r\n                    }\r\n\r\n                    " +
                "List<Model> models = new List<Model>();\r\n                    " +
                "XSSFSheet sheet = (XSSFSheet)xssfWorkbook.GetSheetAt(0);//SheetAt 索引从0开始                    \r\n                    \r\n                    " +
                "if(sheet.GetRow(0).Cells.Count != "+ import.Body.Count() +")\r\n                    {\r\n                        MessageBox.Show(Translator.Get(\"InvaliableColume\"));\r\n                    }\r\n                    \r\n                    " +
                "for(int i = 0; i <= sheet.LastRowNum; i++)\r\n                    {\r\n                        " +
                "Model model = new Model();\r\n                        " +
                "if (sheet.GetRow(i) == null)\r\n                            continue;\r\n                        " +
                Xss +
                EmptyCode +
                RepeatCode +
                "models.Add(CheckImportData(model, models));\r\n                    }\r\n\r\n                    DataSource = models;\r\n                }\r\n            }\r\n        catch (Exception ex)\r\n            {\r\n                MessageBox.Show(\"请删除结尾空行！\\n\" + ex.Message);\r\n            }\r\n        }\r\n\r\n       \r\n\r\n        /// <summary>\r\n        /// 重复返回1，反之返回0\r\n        /// </summary>\r\n        /// <param name=\"code\"></param>\r\n        /// <returns></returns>\r\n        " +
                Function +
                "\r\n            var rs = res.GetData<string>();\r\n            if (rs == \"0\")\r\n                return false;\r\n            else\r\n                return true;\r\n        }\r\n\r\n " +
                "       /// <summary>\r\n        /// 前台判断重复\r\n        /// </summary>\r\n        /// <param name=\"model\"></param>\r\n        /// <param name=\"models\"></param>\r\n        /// <returns></returns>\r\n        " +
                "private Model CheckImportData(Model model, List<Model> models)\r\n        {\r\n            if(models.FirstOrDefault(x => "+CheckImportData+") != null)\r\n            {\r\n                model.IsChecked = false;\r\n            }\r\n            return model;\r\n        }\r\n\r\n\r\n    }\r\n}\r\n";
            return s;
        }

       

        #region 扩展方法

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string PostData(List<Excel> model)
        {
            string s = 
                        "List <Model> models = new List<Model>();\r\n" +
                        "Model model = new Model();\r\n";
            foreach(var ds in model)
            {
                string tmp = "";
                if(ds.CONTROL_CODE == "Combox")
                {
                    tmp = "model." + ds.SEARCH_CODE + " = Filter_" + ds.SEARCH_CODE + "?.Value;\r\n";
                }
                else 
                    tmp = "model." + ds.SEARCH_CODE + " = " + ds.SEARCH_CODE + ";\r\n";
                s += tmp;
            }
            s += "models.Add(model);\r\n return models;";
            return s;
        }

        /// <summary>
        /// 新增校验
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string IsLegal(List<Excel> model)
        {
            string s = "!string.IsNullOrEmpty(" + model[0].SEARCH_CODE + ")";
            for(int i = 1; i < model.Count; i++)
            {
                string tmp = "";
                if(model[i].CONTROL_CODE != "Combox")
                    tmp += "&& !string.IsNullOrEmpty(" + model[i].SEARCH_CODE + ")";
                else
                    tmp += "&& !string.IsNullOrEmpty(Filter_" + model[i].SEARCH_CODE + "?.ToString())";
                s += tmp;
            }
            return s + ";";
        }

        /// <summary>
        /// 新增的url
        /// </summary>
        /// <param name="ProjectName"></param>
        /// <param name="models"></param>
        /// <returns></returns>
        public static string CreateAddUrl(string ProjectName)
        {
            var ls = ProjectName.Split('.');
            string s = "\r\n\t\tpublic const string url_add = \"/api/" + ls.Last().ToLower() +"/" + ls.Last().ToLower() + "/add\";";
            return s;
        }

        public static string CreateEditUrl(string ProjectName)
        {
            var ls = ProjectName.Split('.');
            string s = "\r\n\t\tpublic const string url_edit = \"/api/" + ls.Last().ToLower() + "/" + ls.Last().ToLower() + "/edit\";";
            return s;
        }

        /// <summary>
        /// 生成getall的url
        /// </summary>
        /// <param name="ProjectName"></param>
        /// <param name="models"></param>
        /// <returns></returns>
        public static string CreateGetallUrl(string ProjectName, List<Excel> models, int t = 0)
        {
            models = models.Where(x => x.IsApi).ToList();
            var ls = ProjectName.Split('.');
            string s = "\r\n\t\tpublic const string url_getall = \"/api/" + ls.Last().ToLower() + "/" + ls.Last().ToLower() + "/getall?";
            for(int i = 0; i < models.Count(); i++)
            {
                string tmp = "";
                if (i > 0) tmp += "&";
                tmp += models[i].SEARCH_CODE + "={";
                if (t == 1) tmp += i.ToString() + "}";
                else tmp += models[i].SEARCH_CODE + "}";
                s += tmp;
            }
            if(t == 1)
            {
                s += "&Start={"+ Convert.ToInt32(models.Count())+ "}&Length={" + (Convert.ToInt32(models.Count()) + 1) + "}";
            }
            else s += "&Start={Start}&Length={Length}";
            return s + "\";";
        }

        public static string CreateDeleteUrl(string ProjectName)
        {
            var ls = ProjectName.Split('.');
            string s = "\r\n\t\tpublic const string url_delete = \"/api/" + ls.Last().ToLower() + "/" + ls.Last().ToLower() + "/delete\";";
            return s;
        }

        /// <summary>
        /// 通用弹出框url
        /// </summary>
        /// <param name="ProjectName"></param>
        /// <param name="boxName"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string CreateBoxUrl(string ProjectName, string boxName, string code)
        {
            var ls = ProjectName.Split('.');
            string s = "string url_"+boxName.ToLower()+" = \"/api/" + ls.Last().ToLower() + "/" + ls.Last().ToLower() + "/"+boxName.ToLower()+"_search?" + code +"={" + code + "}\";";
            return s;
        }

        /// <summary>
        /// 更新url
        /// </summary>
        /// <param name="ProjectName"></param>
        /// <returns></returns>
        public static string CreateUpdateUrl(string ProjectName)
        {
            var ls = ProjectName.Split('.');
            string s = "\r\n\t\tpublic const string url_update = \"/api/" + ls.Last().ToLower() + "/" + ls.Last().ToLower() + "/update\";";
            return s;
        }


        public static string CreateLoadData(List<Excel> models)
        {
            models = models.Where(x => x.IsApi).ToList();
            string s = "try{\r\n//务必重载(如果有)下拉框模型的ToString()函数,以保证传参正确\r\n" +
                "var res = plugin.Framework.GetData(Services.url_getall,";
            string tmp = models[0].SEARCH_CODE;
            for (int i = 1; i < models.Count(); i++)
            {
                if (models[i].CONTROL_CODE == "Combox")
                    tmp += ", Filter_" + models[i].SEARCH_CODE + "?.Value";
                else
                    tmp += ", " + models[i].SEARCH_CODE;
            }
            s += tmp;
            s += ", PageIndex * (PageIndex - 1), PageSize);\nif (res.success)\n{\nDataSource = JsonConvert.DeserializeObject<List<Model>>(Convert.ToString(res.data.data));\nTotalCount = res.data.TotalCount;\n}}catch(Exception ex){}\n";

            return s;
        }



        /// <summary>
        /// 生成编辑初始化数据的函数
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public static string CreateEditLoadData(List<Excel> models)
        {
            
            string s = "var model = JsonConvert.DeserializeObject<Model>(json);\r\n";
            foreach(var ds in models)
            {
                string tmp = "";
                if (ds.CONTROL_CODE == "Combox")
                    continue;
                else
                    tmp += ds.SEARCH_CODE + "= model." + ds.SEARCH_CODE + ";\r\n";
                s += tmp;
            }
            return s;
        }
        
        #endregion
    }
}
