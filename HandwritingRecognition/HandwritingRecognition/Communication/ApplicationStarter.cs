using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace HandwritingRecognition.Communication
{
    class ApplicationStarter
    {
        private static Process pythonClientProcess = null;

        public static void StartPythonClient()
        {
            if (pythonClientProcess != null)
            {
                throw new Exception("attempted to start two python clients");
            }
            StreamReader sr = new StreamReader(@"..\..\Resources\pythonClientPath.txt");
            String pythonClientExecutablePath = sr.ReadLine();
            sr.Close();
            pythonClientProcess = Process.Start(pythonClientExecutablePath);
            pythonClientProcess.EnableRaisingEvents = true;
            pythonClientProcess.Exited += pythonClientProcess_Exited;
            // set button for start/stop python client on UI accordingly
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
            // set button for start/stop python client on UI accordingly
        }
    }
}
