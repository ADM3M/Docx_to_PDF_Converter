using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PdfMaker
{
    public class FilePresentation
    {
        public string Name { get; }

        public string Extension { get; }

        public string Path { get; }

        public FilePresentation(string path)
        {
            Regex reg = new(@"(\w+)(\.)([a-z]+)$");
            Match groups = reg.Match(path);

            Name = groups.Groups[1].Value;
            Extension = groups.Groups[3].Value;
            Path = path.Remove(groups.Index, groups.Length);
        }

        public string GetFullPath()
        {
            return string.Concat(Path, Name, ".", Extension);
        }
        public string GetFullPath(string newExtension)
        {
            return string.Concat(Path, Name, ".", newExtension);
        }
    }
}
