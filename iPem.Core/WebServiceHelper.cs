using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Net;
using System.Reflection;
using System.Web.Services.Description;

namespace iPem.Core {
    /// <summary>
    /// WebService帮助类
    /// </summary>
    public abstract class WebServiceHelper {
        public static object InvokeWebService(string url, string methodname, object[] args) {
            return InvokeWebService(url, null, methodname, args);
        }

        public static object InvokeWebService(string url, string classname, string methodname, object[] args) {
            var @namespace = "iPem.Core.WebService";
            if(string.IsNullOrWhiteSpace(classname))
                classname = GetWsClassName(url);

            try {
                if(!url.EndsWith("wsdl", StringComparison.CurrentCultureIgnoreCase))
                    url = string.Format("{0}?{1}", url, "wsdl");

                //获取wsdl
                var wc = new WebClient();
                var stream = wc.OpenRead(url);

                var sd = ServiceDescription.Read(stream);
                var sdi = new ServiceDescriptionImporter();
                sdi.AddServiceDescription(sd, "", "");

                var cn = new CodeNamespace(@namespace);
                var ccu = new CodeCompileUnit();
                ccu.Namespaces.Add(cn);
                sdi.Import(cn, ccu);
                
                //设定编译参数
                var cplist = new CompilerParameters();
                cplist.GenerateExecutable = false;
                cplist.GenerateInMemory = true;
                cplist.ReferencedAssemblies.Add("System.dll");
                cplist.ReferencedAssemblies.Add("System.XML.dll");
                cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
                cplist.ReferencedAssemblies.Add("System.Data.dll");

                //编译代理类 
                var icc = new CSharpCodeProvider();
                var cr = icc.CompileAssemblyFromDom(cplist, ccu);
                if(true == cr.Errors.HasErrors) {
                    var sb = new System.Text.StringBuilder();
                    foreach(CompilerError ce in cr.Errors) {
                        sb.AppendLine(ce.ToString());
                    }
                    throw new Exception(sb.ToString());
                }

                //生成代理实例，并调用方法  
                var assembly = cr.CompiledAssembly;
                var t = assembly.GetType(@namespace + "." + classname, true, true);
                var obj = Activator.CreateInstance(t);
                var mi = t.GetMethod(methodname);
                return mi.Invoke(obj, args);
            } catch(Exception ex) {
                throw new Exception(ex.InnerException.Message, new Exception(ex.InnerException.StackTrace));
            }
        }

        private static string GetWsClassName(string wsUrl) {
            var parts = wsUrl.Split('/');
            var pps = parts[parts.Length - 1].Split('.');
            if(pps[0].Contains("?"))
                return pps[0].Split('?')[0];

            return pps[0];
        }
    }
}
