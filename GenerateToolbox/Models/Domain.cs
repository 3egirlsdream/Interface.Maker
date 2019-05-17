using Model.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.G.Models
{
    public class Domains
    {
        public static string GetCsproj(string ProjectName, string Link = "")
        {
            var ls = ProjectName.Split('.');
            string s = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<Project ToolsVersion=\"14.0\" DefaultTargets=\"Build\" " +
                "xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">\r\n  " +
                "<Import Project=\"$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props\" Condition=\"Exists('$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props')\" />\r\n  <PropertyGroup>\r\n    <Configuration Condition=\" '$(Configuration)' == '' \">Debug</Configuration>\r\n    <Platform Condition=\" '$(Platform)' == '' \">AnyCPU</Platform>\r\n    <ProjectGuid>{6363F4E5-6A68-4112-AC41-B2D86C2151A0}</ProjectGuid>\r\n    <OutputType>Library</OutputType>\r\n    <AppDesignerFolder>Properties</AppDesignerFolder>\r\n    " +
                "<RootNamespace> " + ProjectName + "</RootNamespace>\r\n    " +
                "<AssemblyName>" + ProjectName + "</AssemblyName>\r\n    " +
                "<TargetFrameworkVersion>v4.0</TargetFrameworkVersion>\r\n    " +
                "<FileAlignment>512</FileAlignment>\r\n  " +
                "</PropertyGroup>\r\n  " +
                "<PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' \">\r\n    " +
                "<DebugSymbols>true</DebugSymbols>\r\n    " +
                "<DebugType>full</DebugType>\r\n    " +
                "<Optimize>false</Optimize>\r\n    " +
                "<OutputPath>X:\\MIDDLE SERVER\\Plugins\\</OutputPath>\r\n    " +
                "<DefineConstants>DEBUG;TRACE</DefineConstants>\r\n    " +
                "<ErrorReport>prompt</ErrorReport>\r\n    " +
                "<WarningLevel>4</WarningLevel>\r\n    " +
                "<PlatformTarget>x86</PlatformTarget>\r\n  " +
                "</PropertyGroup>\r\n  " +
                "<PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' \">\r\n    <DebugType>pdbonly</DebugType>\r\n    " +
                "<Optimize>true</Optimize>\r\n    <OutputPath>X:\\MIDDLE SERVER\\Plugins\\</OutputPath>\r\n    " +
                "<DefineConstants>TRACE</DefineConstants>\r\n    <ErrorReport>prompt</ErrorReport>\r\n    " +
                "<WarningLevel>4</WarningLevel>\r\n  </PropertyGroup>\r\n  <ItemGroup>\r\n    " +
                "<Reference Include=\"Creative.ODA, Version=1.0.0.0, Culture=neutral, processorArchitecture=x86\">\r\n      " +
                "<SpecificVersion>False</SpecificVersion>\r\n      " +
                "<HintPath>..\\Reference Assemblies\\Creative.ODA.dll</HintPath>\r\n    </Reference>\r\n    " +
                "<Reference Include=\"Creative.ServerPlugin.Common, Version=2017.4.18.1, Culture=neutral, processorArchitecture=x86\">\r\n      " +
                "<SpecificVersion>False</SpecificVersion>\r\n      " +
                "<HintPath>..\\Reference Assemblies\\Creative.ServerPlugin.Common.dll</HintPath>\r\n    </Reference>\r\n    " +
                "<Reference Include=\"Newtonsoft.Json, Version=4.5.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed, processorArchitecture=MSIL\">\r\n      " +
                "<SpecificVersion>False</SpecificVersion>\r\n      <HintPath>..\\Reference Assemblies\\Newtonsoft.Json.dll</HintPath>\r\n    </Reference>\r\n    " +
                "<Reference Include=\"System\" />\r\n    <Reference Include=\"System.Core\" />\r\n    <Reference Include=\"System.Web\" />\r\n    " +
                "<Reference Include=\"System.Xml.Linq\" />\r\n    <Reference Include=\"System.Data.DataSetExtensions\" />\r\n    " +
                "<Reference Include=\"Microsoft.CSharp\" />\r\n    <Reference Include=\"System.Data\" />\r\n    " +
                "<Reference Include=\"System.Xml\" />\r\n  </ItemGroup>\r\n  <ItemGroup>\r\n    " +
                "<Compile Include=\"..\\..\\SharedAssemblyFiles\\GlobalAssemblyVersion.cs\">\r\n      " +
                "<Link>Properties\\GlobalAssemblyVersion.cs</Link>\r\n    </Compile>\r\n    " +
                Link +
                "<Compile Include=\"Domains\\"+ls.Last().ToLower()+"Domain.cs\" />\r\n    " +
                "<Compile Include=\"Properties\\AssemblyInfo.cs\" />\r\n    " +
                "<Compile Include=\"Services\\" + ls.Last().ToLower() + "Service.cs\" />\r\n  </ItemGroup>\r\n  <ItemGroup>\r\n    " +
                "<Folder Include=\"Models\\\" />\r\n  </ItemGroup>\r\n  <Import Project=\"$(MSBuildToolsPath)\\Microsoft.CSharp.targets\" />\r\n  " +
                "<!-- To modify your build process, add your task inside one of the targets below and uncomment it. \r\n       Other similar extension points exist, see Microsoft.Common.targets.\r\n  <Target Name=\"BeforeBuild\">\r\n  </Target>\r\n  <Target Name=\"AfterBuild\">\r\n  </Target>\r\n  -->\r\n</Project>";
            return s;
        }

        public static string GetAssembly(string ServerName, string ChineseName)
        {
            Random rnd = new Random((int)DateTime.Now.Ticks);
            int t = rnd.Next((int)DateTime.Now.Ticks < 0 ? -(int)DateTime.Now.Ticks : (int)DateTime.Now.Ticks);
            t = t % 100;
            var ls = ServerName.Split('.');
            string s = "using Creative.ServerPlugin.Common;\r\n" +
                "using System.Reflection;\r\n\r\n// 有关程序集的一般信息由以下\r\n// 控制。更改这些特性值可修改\r\n// 与程序集关联的信息。\r\n" +
                "[assembly: AssemblyTitle(\"" + ServerName + "\")]\r\n" +
                "[assembly: AssemblyVersion(\"2.0.0.*\")]\r\n\r\n" +
                "[assembly: AssemblyDescription(\"\")]\r\n\r\n" +
                "[assembly: RESTful(Code = \"" + t + ls.Last()[0].ToString().ToUpper() + "\", UriTemplate = \"/api/" + ls.Last().ToLower() + "\", Description = \"" + ChineseName + "\")]\r\n";
            return s;
        }
        
        public static string GetDomain(string ProjectName, string getall_url, string Hasword = "", string HaswordFunction = "", string GetAllFunction = "")
        {
            var ls = ProjectName.Split('.');
            string s = "using Creative.ODA;\r\nusing MeiCloud.DataAccess;\r\nusing Newtonsoft.Json.Linq;\r\nusing Newtonsoft.Json;\r\nusing System;\r\nusing System.Collections.Generic;\r\nusing System.Linq;\r\n\r\nnamespace MeiCloud.Middleware.Services\r\n{\r\n    /// <summary>\r\n    /// 交易控制台服务\r\n    /// </summary>\r\n    " +
                "internal class " + ls.Last() + "Domain\r\n    {\r\n        " +
                "public static object GetAll("+getall_url+ ",int Start, int Length, string orgId, string enterpriseId, string user)\r\n        {\r\n    " +
                GetAllFunction +
                "        return null;\r\n        }\r\n\r\n        " +
                "public static void Delete(JToken jt)\r\n        {\r\n\r\n " +
                "var models = JsonConvert.DeserializeObject<List<string>>(jt[\"ids\"].ToString());\r\n" +
                "var ctx = ODAContext.NoTransContext;\r\n" +
                "foreach (var ds in models)\r\n" +
                "{\r\n" +
                "var cmd = ctx.GetCmd<>();\r\n" +
                "cmd.Where(cmd.ColId == ds).Delete();\r\n" +
                "}\r\n" +
                "       }\r\n\r\n        " +
                "public static void Update(JToken jt, string orgId, string enterpriseId, string user)\r\n        {\r\n\r\n  " +
                "            var models = JsonConvert.DeserializeObject<List<dynamic>>(jt[\"model\"].ToString());\r\n" +
                "            var ctx = ODAContext.NoTransContext;\r\n" +
                "            foreach (var ds in models)\r\n" +
                "            {\r\n" +
                "                var cmd = ctx.GetCmd<>();\r\n" +
                "                ds.DATETIME_MODIFIED = ctx.DBDatetime;\r\n" +
                "                ds.USER_MODIFIED = user;\r\n" +
                "                cmd.Where(cmd.ColId == ds.ID).Update(ds);\r\n" +
                "            }\r\n" +
                "      }\r\n\r\n        " +
                "public static void Add(JToken jt, string orgId, string enterpriseId, string user)\r\n        {\r\n\r\n " +
                            "var models = JsonConvert.DeserializeObject<List<dynamic>>(jt[\"model\"].ToString());\r\n" +
                "var ctx = new ODAContext();\r\n" +
                "try\r\n" +
                "{\r\n" +
                "    ctx.BeginTransaction();\r\n" +
                "    foreach (var ds in models)\r\n" +
                "    {\r\n" +
                "        var cmd = ctx.GetCmd<>();\r\n" +
                "        ds.DATETIME_CREATED = ctx.DBDatetime;\r\n" +
                "        ds.USER_CREATED = user;\r\n" +
                "        ds.ID = Guid.NewGuid().ToString(\"N\").ToUpper();\r\n" +
                "        ds.STATE = \"A\";\r\n" +
                "        ds.ORG_ID = orgId;\r\n" +
                "        ds.ENTERPRISE_ID = enterpriseId;\r\n" +
                "        cmd.Insert(ds);\r\n" +
                "    }\r\n" +
                "    ctx.Commit();\r\n" +
                "}\r\n" +
                "catch(Exception ex)\r\n" +
                "{\r\n" +
                "    ctx.RollBack();\r\n" +
                "    throw ex;\r\n" +
                "}\r\n" +
                "       }\r\n\r\n";
            if(Hasword != "")
            {
                s += "public static object HasWord("+Hasword+ ", string orgId, string enterpriseId, string user)\r\n        {\r\n\r\n " +
                    HaswordFunction +
                    "       }\r\n\r\n    }\r\n}\r\n";
            }
            return s + "}\r\n}";
        }

        public static string GetService(string ProjectName, string getall_url_header, string getall_url_body, string getall_url_param, string ChineseName, string HasWord1 = "", string HasWord2 = "", string HasWord3 = "")
        {
            var ls = ProjectName.Split('.');
            string s = "using Creative.ServerPlugin.Common;\r\nusing MeiCloud.Middleware.Common;\r\nusing Newtonsoft.Json;\r\nusing Newtonsoft.Json.Linq;\r\nusing System;\r\nusing System.Collections.Generic;\r\n\r\n" +
                "namespace MeiCloud.Middleware.Services\r\n{\r\n    " +
                "[ServiceDomain(UriTemplate = \"/api/"+ls.Last().ToLower()+ "/" + ls.Last().ToLower() + "\", Name = \""+ ChineseName + "\", Description = \"" + ChineseName + "\")]\r\n    " +
                "[RESTful(UriTemplate = \"/" + ls.Last().ToLower() + "\", Code = \"11\")]\r\n    " +
                "public class " + ls.Last() + "Service : ServiceBase\r\n    {\r\n        /// <summary>\r\n        /// 获取所有数据\r\n        /// </summary>\r\n        " +
                "[RESTful(UriTemplate = \"/"+getall_url_header+"\", Method = RequestMethod.GET, Code = \"001\")]\r\n        " +
                "public object GetAll(" + getall_url_body + ",int Start, int Length)\r\n        {\r\n            " +
                "return "+ls.Last()+"Domain.GetAll("+ getall_url_param + "Start, Length, this.OrgId, this.EnterpriseId, this.CurrentUserName);\r\n        }\r\n\r\n        /// <summary>\r\n        /// 更新\r\n        /// </summary>\r\n        " +
                "[RESTful(UriTemplate = \"/update\", Method = RequestMethod.POST, Code = \"002\")]\r\n        " +
                "public void Update(JToken jt)\r\n        {\r\n            " + ls.Last() + "Domain.Update(jt, this.OrgId, this.EnterpriseId, this.CurrentUserName);\r\n        }\r\n\r\n        /// <summary>\r\n        /// 删除\r\n        /// </summary>\r\n        " +
                "[RESTful(UriTemplate = \"/delete\", Method = RequestMethod.POST, Code = \"003\")]\r\n        " +
                "public void Delete(JToken jt)\r\n        {\r\n            " + ls.Last() + "Domain.Delete(jt);\r\n        }\r\n\r\n        /// <summary>\r\n        /// 新增\r\n        /// </summary>\r\n        " +
                "[RESTful(UriTemplate = \"/add\", Method = RequestMethod.POST, Code = \"004\")]\r\n        " +
                "public void Add(JToken jt)\r\n        {\r\n            " + ls.Last() + "Domain.Add(jt, this.OrgId, this.EnterpriseId, this.CurrentUserName);\r\n        }\r\n";
            if(HasWord1 != "")
            {
                 s += "//判断导入数据是否存在\r\n\t\t[RESTful(UriTemplate = \""+HasWord1+"\", Method = RequestMethod.GET, Code = \"005\")]\r\n        " +
                "public object HasWord("+HasWord2+")\r\n        {\r\n            return " + ls.Last() + "Domain.HasWord("+HasWord3+" this.OrgId, this.EnterpriseId, this.CurrentUserName);\r\n        }\r\n    }\r\n}\r\n";
            }
            return s + "}\r\n}";
        }

        public static string GetAllUrlHeader(string ProjectName, List<Excel> models)
        {
            models = models.Where(x => x.IsApi).ToList();
            var ls = ProjectName.Split('.');
            string s = "getall?" + models[0].SEARCH_CODE + "={" + models[0].SEARCH_CODE + "}";
            for (int i = 1; i < models.Count(); i++)
            {
                string tmp = "&" + models[i].SEARCH_CODE + "={";
                tmp += models[i].SEARCH_CODE + "}";
                s += tmp;
            }
            s += "&Start={Start}&Length={Length}";
            return s;
        }

        /// <summary>
        /// t=1生成声明，其他生成传参
        /// </summary>
        /// <param name="models"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GetAllUrlBody(List<Excel> models, int t = 0)
        {
            models = models.Where(x => x.IsApi).ToList();
            string s = "";
            if(t == 1) s += "string " + models[0].SEARCH_CODE;
            else s += models[0].SEARCH_CODE + ", ";
            for (int i = 1; i < models.Count(); i++)
            {
                string tmp = "";
                if (t == 1) tmp += ", string " + models[i].SEARCH_CODE;
                else tmp += models[i].SEARCH_CODE + ", ";
                s += tmp;
            }
            return s;
        }

        //生成检测重复的url
        public static string GetHasWordUrl(ImportClass import, int t = 0)
        {
            if (import == null || (import.REPEAT_CODE.Count() == 0 && import.EMPTY_CODE.Count() == 0 && import.Body.Count() == 0))
                return "";
            if(t == 0)//带/Hasword
            {
                string s = "/hasword?";
                for (int i = 0; i < import.REPEAT_CODE.Count(); i++)
                {
                    ModelHelper model = new ModelHelper();
                    string tmp = "";
                    if (i > 0) tmp += "&";
                    tmp += model.Col(import.REPEAT_CODE[i]).Replace("Col", "") + "={" + model.Col(import.REPEAT_CODE[i]).Replace("Col", "") + "}";
                    s += tmp;
                }
                return s;
            }
            if(t == 1)//带string 
            {
                ModelHelper model = new ModelHelper();
                string s = "string " + model.Col(import.REPEAT_CODE[0]).Replace("Col", "");
                for (int i = 1; i < import.REPEAT_CODE.Count(); i++)
                {
                    string tmp = ", string " + model.Col(import.REPEAT_CODE[i]).Replace("Col", "");
                    s += tmp;
                }
                return s;
            }
            else //不带string 
            {
                string s = "";
                for (int i = 0; i < import.REPEAT_CODE.Count(); i++)
                {
                    ModelHelper model = new ModelHelper();
                    string tmp = model.Col(import.REPEAT_CODE[i]).Replace("Col", "") + ", ";
                    s += tmp;
                }
                return s;
            }
        }

        //生成函数体
        public static string GetHasWrodFunction(ImportClass import)
        {
            string s = "";
            foreach (var ds in import.REPEAT_CODE)
            {
                ModelHelper model = new ModelHelper();
                string tmp = "            cmd." + model.Col(ds) + " == " + model.Col(ds).Replace("Col", "") + ", \r\n";
                s += tmp;
            }

            string ss =
                "            var ctx = ODAContext.NoTransContext;\r\n" +
                "            var cmd = ctx.GetCmd<>();\r\n" +
                "            var dt = cmd.Where(\r\n" +
                s +
                "            cmd.ColOrgId == orgId,\r\n" +
                "            cmd.ColEnterpriseId == enterpriseId).Count();\r\n" +  
                "            if (dt > 0)\r\n" +
                "                return 1;\r\n" +
                "            else" +
                "                return 0;\r\n";
            return ss;
        }

        //生成GetAll()函数体
        public static string GetAllFunction(string Table, List<Excel> models)
        {
            string tbs = "";
            if (!string.IsNullOrEmpty(Table))
            {
                string[]  tables = Table.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                ModelHelper model = new ModelHelper();
                foreach (var ds in tables)
                {
                    tbs += "            var cmd"+ model.Col(ds).Replace("Col", "") +" = ctx.GetCmd<" + model.GetCmd(ds.ToUpper()) + ">();\r\n";
                }
            }
            models = models.Where(x => x.IsApi).ToList();
            string ss = "";
            foreach (var ds in models)
            {
                ss +=  "if (!string.IsNullOrEmpty("+ds.SEARCH_CODE+"))\r\n" +
                       "{\r\n" +
                       "cmd.Where(cmd.Col"+ds.SEARCH_CODE+" == "+ds.SEARCH_CODE+");"+     
                       "}\r\n";
            }

            

            string s = "            var ctx = ODAContext.NoTransContext;\r\n" +
                       tbs +
                       ss;

            return s;
        }

    }
}
