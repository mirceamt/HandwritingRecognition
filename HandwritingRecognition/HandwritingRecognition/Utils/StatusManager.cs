using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace HandwritingRecognition.Utils
{
    class StatusManager
    {
        private static Label statusLabel = null;
        public static void Initialize(Label label)
        {
            statusLabel = label;
        }

        public static void SetStatus(String message, Color color)
        {
            if (statusLabel.IsHandleCreated)
            {
                statusLabel.Invoke((MethodInvoker)delegate
                {
                    SetStatusLabel(message, color);
                });
            }
            else
            {
                SetStatusLabel(message, color);
            }
        }

        private static void SetStatusLabel(String message, Color color)
        {
            statusLabel.Text = message;
            statusLabel.ForeColor = color;
        }

    }
}
