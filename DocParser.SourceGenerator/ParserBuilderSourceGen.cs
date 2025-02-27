using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocParser.SourceGenerator
{
    [Generator]
    public class ParserBuilderSourceGen : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            context.RegisterSourceOutput(context.CompilationProvider, (sourceProductionContext, compilation) =>
            {
                var sb = new StringBuilder();
                var sbSet = new StringBuilder();
                sb.Append(@"
                    using System.IO;
                    using System;
                    using System.Collections.Generic;
                    namespace DocParser
                    {
                        public partial class ParserBuilder
                        {
                            public partial IParser Build() => Path.GetExtension(_filePath).Trim('.').ToLower() switch
                            {");
                foreach (var classExts in GetClassesImplementingInterface(compilation))
                {
                    foreach (var ext in classExts.Item2 ?? Array.Empty<string>())
                    {
                        sb.Append('"').Append(ext).Append('"').Append(" => new ").Append(classExts.Item1).Append("(this),");
                        sb.AppendLine();
                        sbSet.Append('"').Append(ext).Append(@""",");
                    }
                }
                sbSet.Length--;
                sb.Append($@"}};
                        }}
                    }}");

                var sourceText = SourceText.From(BeautifyCode(sb.ToString()), Encoding.UTF8);
                sourceProductionContext.AddSource($"ParserBuilder.g.cs", sourceText);
            });
        }

        public static string BeautifyCode(string code)
        {
            // 将代码字符串解析为语法树
            var syntaxTree = CSharpSyntaxTree.ParseText(code);

            // 获取语法树的根节点
            var root = syntaxTree.GetRoot();

            // 格式化代码
            var formattedCode = root.NormalizeWhitespace().ToFullString();

            return formattedCode;
        }

        private static IEnumerable<(string, IEnumerable<string>)> GetClassesImplementingInterface(Compilation compilation)
        {
            // 获取目标接口类型
            var interfaceType = compilation.GetTypeByMetadataName($"DocParser.IParser");
            if (interfaceType == null)
            {
                yield break; // 接口不存在
            }

            // 遍历所有的语法树和语法节点
            foreach (var syntaxTree in compilation.SyntaxTrees)
            {
                var semanticModel = compilation.GetSemanticModel(syntaxTree);
                var classes = syntaxTree.GetRoot()
                                        .DescendantNodes()
                                        .OfType<ClassDeclarationSyntax>();

                foreach (var classDecl in classes)
                {
                    var classSymbol = semanticModel.GetDeclaredSymbol(classDecl) as INamedTypeSymbol;
                    if (classSymbol == null)
                        continue;

                    // 检查是否实现了目标接口
                    if (classSymbol.AllInterfaces.Contains(interfaceType) && !classSymbol.IsAbstract)
                    {
                        yield return (classSymbol.Name, classSymbol.GetAttributes().Where(x => x.AttributeClass.Name == "IncludeExtAttribute").Select(x => x.ConstructorArguments[0].Value?.ToString()).Where(x => !string.IsNullOrEmpty(x)).SelectMany(x => x.Split('|')));
                    }
                }
            }
        }
    }
}