using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastersign.DisplayManager
{
    static class ConsoleFormat
    {
        public static void WriteList<T>(Func<IEnumerable<T>> enumerator)
        {
            foreach (var item in enumerator())
            {
                Console.WriteLine(Indent(item));
            }
        }

        public static string FormatArray<T>(IEnumerable<T> items)
            => string.Join(Environment.NewLine, items.Select(i => Indent(i)));

        public static string Indent(object x, int level, bool isArrayItem = true)
        {
            return Indent(x,
                new string(' ', level * 2) + (isArrayItem ? "- " : "  "),
                new string(' ', level * 2 + 2));
        }

        public static string Indent(object x, string firstIndent = "- ", string indent = "  ")
        {
            var text = x != null ? x.ToString() : "null";
            return firstIndent + text.Replace(Environment.NewLine, Environment.NewLine + indent);
        }
    }
}
