using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using Microsoft.CSharp;

namespace Cliente.Scriptings
{
    public sealed class Csharp
    {
        public static void CompileAndRun(string code) //,params object[] parametros)
        {
            System.Threading.Thread trh = new System.Threading.Thread(() =>
            {
                try
                {
                    CompilerParameters CompilerParams = new CompilerParameters();
                    string outputDirectory = Directory.GetCurrentDirectory();

                    CompilerParams.GenerateInMemory = true;
                    CompilerParams.TreatWarningsAsErrors = false;
                    CompilerParams.GenerateExecutable = false;
                    CompilerParams.CompilerOptions = "/optimize";

                    string[] references = {"System.dll", "System.Windows.Forms.dll", "System.Drawing.dll"};
                    CompilerParams.ReferencedAssemblies.AddRange(references);

                    CSharpCodeProvider provider = new CSharpCodeProvider();
                    CompilerResults compile = provider.CompileAssemblyFromSource(CompilerParams, code);

                    if (compile.Errors.HasErrors)
                    {
                        string text = "Compile error: ";
                        foreach (CompilerError ce in compile.Errors)
                        {
                            text += "rn" + ce.ToString();
                        }
                        throw new Exception(text);
                    }

                    //ExpoloreAssembly(compile.CompiledAssembly);

                    Module module = compile.CompiledAssembly.GetModules()[0];
                    Type mt = null;
                    MethodInfo methInfo = null;

                    if (module != null)
                    {
                        mt = module.GetType("Script.Code");
                    }

                    if (mt != null)
                    {
                        methInfo = mt.GetMethod("Main");
                    }

                    if (methInfo != null)
                    {
                        methInfo.Invoke(null, null); //, parametros);
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);

                }
            });
            trh.IsBackground = true;
            trh.Start();
        }
    }
}