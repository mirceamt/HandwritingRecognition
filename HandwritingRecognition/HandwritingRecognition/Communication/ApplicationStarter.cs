using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using HandwritingRecognition.Resources;
using HandwritingRecognition.Utils;

namespace HandwritingRecognition.Communication
{
    class ApplicationStarter
    {
        private static Process pythonClientProcess = null;

        public static void StartPythonClientFromExecutable()
        {
            if (pythonClientProcess != null)
            {
                Logger.LogInfo("Python Client already started");
                return;
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
                Logger.LogInfo("Python Client already started");
                return;
            }
            StreamReader sr = new StreamReader(Paths.PythonClientStartingPointPathFile);
            String pythonClientStartingPoint = sr.ReadLine();
            sr.Close();

            String startPythonClientCommand = "python " + "\"" + pythonClientStartingPoint + "\"";

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "python";
            startInfo.Arguments = "\"" + pythonClientStartingPoint + "\"";

            pythonClientProcess = new Process();
            pythonClientProcess.StartInfo = startInfo;
            pythonClientProcess.Start();

            pythonClientProcess.EnableRaisingEvents = true;
            pythonClientProcess.Exited += pythonClientProcess_Exited;
            // TODO
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
            Logger.LogError("Restart Python Client!");
            ApplicationUseManager.Instance.TriggerApplicationNotReady();
            // TODO
            // set button for start/stop python client on UI accordingly
        }
    }
}
