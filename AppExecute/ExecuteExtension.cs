using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExecute
{

    public static class ExecuteExtension
    {
        

        public static string executeCommand(string commandType, string commandSentence)
        {

            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "cmd.exe";
            info.Arguments = "/c "+commandSentence;
            info.CreateNoWindow = true;
            info.UseShellExecute = false;

            Process proc = new Process();
            proc.StartInfo = info;
            var test = proc.Start();
            proc.WaitForExit();
            return "Execution du fichier avec succes";


        }
    }
}
