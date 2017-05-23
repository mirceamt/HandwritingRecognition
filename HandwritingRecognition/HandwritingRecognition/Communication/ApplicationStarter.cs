using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using HandwritingRecognition.Resources;

namespace HandwritingRecognition.Communication
{
    class ApplicationStarter
    {
        private static Process pythonClientProcess = null;

        public static void StartPythonClientFromExecutable()
        {
            if (pythonClientProcess != null)
            {
                throw new Exception("attempted to start two python clients");
            }
            StreamReader sr = new StreamReader(Paths.PythonClientExecutablePathFile);
            String pythonClientExecutablePath = sr.ReadLine();
            sr.Close();
            pythonClientProcess = Process.Start(pythonClientExecutablePath);
            pythonClientProcess.EnableRaisingEvents = true;
            pythonClientProcess.Exited += pythonClientProcess_Exited;
            // TODO
            // set button for start/stop python client on UI accordingly
        }

        public static void StartPythonClientFromStartingPoint()
        {
            if (pythonClientProcess != null)
            {
                throw new Exception("attempted to start two python clients");
            }
            StreamReader sr = new StreamReader(Paths.PythonClientStartingPointPathFile);
            String pythonClientStartingPoint = sr.ReadLine();
            sr.Close();
            pythonClientProcess = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo



            pythonClientProcess.EnableRaisingEvents = true;
            pythonClientProcess.Exited += pythonClientProcess_Exited;
        }

        public static void StopPythonClient()
        {
            if (pythonClientProcess == null)
            {
                return;
            }
            pythonClientProcess.Kill();
        }

        static void pythonClientProcess_Exited(object sender, EventArgs e)
        {
            pythonClientProcess = null;
            // TODO
            // set button for start/stop python client on UI accordingly
        }
    }
}
