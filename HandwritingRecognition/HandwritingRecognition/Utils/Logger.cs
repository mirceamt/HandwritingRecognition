using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace HandwritingRecognition.Utils
{
    class Logger
    {
        private static LoggerDataSource loggerDataSource = new LoggerDataSource();

        public static Label lastMessageLabel = null;

        public static void Initialize(Label label)
        {
            lastMessageLabel = label;
        }

        public static void LogInfo(String message, Color? color = null)
        {
            String newMessage = "[Info] - " + message;
            loggerDataSource.AddMessage(newMessage);
            if (lastMessageLabel.IsHandleCreated)
            {
                lastMessageLabel.Invoke((MethodInvoker)delegate
                {
                    SetLastMessageLabel(newMessage, color ?? Color.Black);
                });
            }
            else
            {
                SetLastMessageLabel(newMessage, color ?? Color.Black);
            }
        }

        public static void LogWarning(String message, Color? color = null)
        {
            String newMessage = "[Warning] - " + message;
            loggerDataSource.AddMessage(newMessage);
            if (lastMessageLabel.IsHandleCreated)
            {
                lastMessageLabel.Invoke((MethodInvoker)delegate
                {
                    SetLastMessageLabel(newMessage, color ?? Color.Red);
                });
            }
            else
            {
                SetLastMessageLabel(newMessage, color ?? Color.Red);
            }
        }

        public static void LogError(String message, Color? color = null)
        {
            String newMessage = "[Error] - " + message;
            loggerDataSource.AddMessage(newMessage);
            if (lastMessageLabel.IsHandleCreated)
            {
                lastMessageLabel.Invoke((MethodInvoker)delegate
                {
                    SetLastMessageLabel(newMessage, color ?? Color.Red);
                });
            }
            else
            {
                SetLastMessageLabel(newMessage, color ?? Color.Red);
            }
        }

        private static void SetLastMessageLabel(String newMessage, Color color)
        {
            lastMessageLabel.Text = newMessage;
            lastMessageLabel.ForeColor = color;
        }

        public static BindingSource DataSource
        {
            get
            {
                return loggerDataSource.DataSource;
            }
        }

        public static BindingList<LogModel> BindingListOfLogs
        {
            get
            {
                return loggerDataSource.BindingListOfLogs;
            }
        }
    }
}
