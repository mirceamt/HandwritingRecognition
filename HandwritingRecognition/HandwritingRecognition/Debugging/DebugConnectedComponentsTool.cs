using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HandwritingRecognition.ImageProcessing;


namespace HandwritingRecognition.Debugging
{
    class DebugConnectedComponentsTool
    {
        private static List<ConnectedComponent> m_connectedComponents;
        public static List<ConnectedComponent> ConnectedComponents
        {
            get
            {
                return m_connectedComponents;
            }
        }
        public static void DisplayConnectedComponentsInNewWindow(List<ConnectedComponent> list)
        {
            m_connectedComponents = list;
            ConnectedComponentsWindow form2 = new ConnectedComponentsWindow();
            form2.ShowDialog();
        }
    }
}
