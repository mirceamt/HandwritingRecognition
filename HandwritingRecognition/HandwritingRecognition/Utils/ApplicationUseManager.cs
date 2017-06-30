using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace HandwritingRecognition.Utils
{
    public delegate void ApplicationUseEventHandler(object o, ApplicationUseEventArgs e);

    public class ApplicationUseEventArgs : EventArgs
    {
        private string m_eventInfo;
        public ApplicationUseEventArgs()
        {
            m_eventInfo = "";
        }
        public ApplicationUseEventArgs(string s)
        {
            m_eventInfo = s;
        }
        public string GetInfo()
        {
            return m_eventInfo;
        }
    }

    class ApplicationUseManager
    {
        public event ApplicationUseEventHandler ApplicationReady;
        public event ApplicationUseEventHandler ApplicationNotReady;
        private static ApplicationUseManager m_instance = new ApplicationUseManager();
        private static Panel panel = null;
        public PaintEventHandler drawPanelPaintEventHandler = null;
        public MouseEventHandler drawPanelMouseUpEventHandler = null;
        public MouseEventHandler drawPanelMouseMoveEventHandler = null;
        public MouseEventHandler drawPanelMouseDownEventHandler = null;
        private ApplicationUseManager()
        {
            this.ApplicationReady += ApplicationReadyEventHandler;
            this.ApplicationNotReady += ApplicationNotReadyEventHandler;
        }

        public static ApplicationUseManager Instance
        {
            get
            {
                return m_instance;
            }
        }

        public void TriggerApplicationNotReady(string eventInfo = "")
        {
            this.ApplicationNotReady(this, new ApplicationUseEventArgs(eventInfo));
        }
        public void TriggerApplicationReady(string eventInfo = "")
        {
            this.ApplicationReady(this, new ApplicationUseEventArgs(eventInfo));
        }
        

        private void ApplicationReadyEventHandler(object o, ApplicationUseEventArgs e)
        {
            Logger.LogInfo("Application Ready To Use", Color.Green);
            StatusManager.SetStatus("Status: Ready", Color.Green);
            panel.Paint += this.drawPanelPaintEventHandler;
            panel.MouseDown += this.drawPanelMouseDownEventHandler;
            panel.MouseMove += this.drawPanelMouseMoveEventHandler;
            panel.MouseUp += this.drawPanelMouseUpEventHandler;
        }

        public void Initialize(Form1 x)
        {
            panel = x.drawPanelPublic;
            drawPanelPaintEventHandler = x.drawPanelPaintEventHandler;
            drawPanelMouseUpEventHandler = x.drawPanelMouseUpEventHandler;
            drawPanelMouseMoveEventHandler = x.drawPanelMouseMoveEventHandler;
            drawPanelMouseDownEventHandler = x.drawPanelMouseDownEventHandler;
        }

        private void ApplicationNotReadyEventHandler(object o, ApplicationUseEventArgs e)
        {
            Logger.LogError("Application cannot be used. Please start Python Client!", Color.Red);
            StatusManager.SetStatus("Status: Not Ready", Color.Red);
            panel.Paint -= this.drawPanelPaintEventHandler;
            panel.MouseDown -= this.drawPanelMouseDownEventHandler;
            panel.MouseMove -= this.drawPanelMouseMoveEventHandler;
            panel.MouseUp -= this.drawPanelMouseUpEventHandler;
        }
    }
}
