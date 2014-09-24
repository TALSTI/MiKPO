using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace FuncTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "..\\..\\..\\..\\MiKPO Lab 1\\MiKPO Lab 1\\bin\\Debug\\";
            DirectoryInfo d = new DirectoryInfo(path + "in");
            Process proc = new Process();
            proc.StartInfo.FileName = path + "MiKPO Lab 1.exe";
            foreach (FileInfo f in d.GetFiles())
            {
                proc.StartInfo.Arguments = "\"" + f.FullName + "\" \"" + path + "out\\" + f.Name + "\"";
                proc.Start();
                proc.Close();
            }
        }
    }
}
