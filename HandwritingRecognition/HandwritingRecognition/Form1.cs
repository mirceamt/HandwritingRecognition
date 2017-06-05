using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using HandwritingRecognition.ImageProcessing;
using HandwritingRecognition.Debugging;
using HandwritingRecognition.Classification;
using HandwritingRecognition.Communication;
using HandwritingRecognition.Utils;

namespace HandwritingRecognition
{
    public partial class Form1 : Form
    {
        bool m_canDraw = false;

        PointF m_previousPoint = new PointF(-1, -1);

        Bitmap m_auxiliaryBitmap;

        ConnectedComponentsTool connectedComponentsTool = new ConnectedComponentsTool();

        List<ConnectedComponent> m_connectedComponents;

        public Form1()
        {
            InitializeComponent();
            Logger.Initialize(lastMessageLabel);
            StatusManager.Initialize(statusLabel);
            UIUpdater.Initialize(predictedWordLabel);

            Logger.LogInfo("app started");
            
            this.FormClosed += Form1_FormClosed;

            ApplicationUseManager appUseManagerInstance = ApplicationUseManager.Instance;
            appUseManagerInstance.TriggerApplicationNotReady();

            ConnectionManager.StartListeningToConnections();
            // !!!!!!!!!!!!!!!!!!!!!!!!!  LET THE CLIENT START ALONG WITH THE MAIN APP!!!!!!!!!!!!!!
            ApplicationStarter.StartPythonClientFromStartingPoint();

            m_connectedComponents = new List<ConnectedComponent>();
            this.m_auxiliaryBitmap = new Bitmap(drawPanel.Width, drawPanel.Height, drawPanel.CreateGraphics());
            Graphics.FromImage(m_auxiliaryBitmap).Clear(Color.White);
            connectedComponentsTool.Initialize(m_auxiliaryBitmap);
        }

        void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ConnectionManager.ShutdownConnectionManager();
        }

        private void drawPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_canDraw)
            {
                Graphics panel = Graphics.FromImage(m_auxiliaryBitmap);

                Pen pen = new Pen(Color.Black, 12);

                pen.EndCap = LineCap.Round;
                pen.StartCap = LineCap.Round;

                panel.DrawLine(pen, m_previousPoint, new PointF(e.X, e.Y));

                drawPanel.CreateGraphics().DrawImageUnscaled(m_auxiliaryBitmap, new Point(0, 0));
            }

            m_previousPoint = new PointF(e.X, e.Y);
        }

        private void drawPanel_MouseDown(object sender, MouseEventArgs e)
        {
            PointF currentPoint = new PointF(e.X, e.Y);
            Pen pen = new Pen(Color.Black, 6);
            pen.EndCap = LineCap.Round;
            pen.StartCap = LineCap.Round;

            m_canDraw = true;

            m_previousPoint = currentPoint;

            // -2 is the magic current offset for the drawing of the point

            Graphics.FromImage(m_auxiliaryBitmap).DrawEllipse(pen, currentPoint.X - 2, currentPoint.Y - 2, 6, 6);

            drawPanel.CreateGraphics().DrawImageUnscaled(m_auxiliaryBitmap, new Point(0, 0));
        }

        private void drawPanel_MouseUp(object sender, MouseEventArgs e)
        {
            m_canDraw = false;
            m_connectedComponents = connectedComponentsTool.InspectConnectedComponents(m_auxiliaryBitmap);
            // TODO: sort the connected components according to their position in the panel

            // TODO: refactor this logic
            ConnectedComponent lastConnectedComponent = m_connectedComponents.Last();
            CharacterImage lastImage = new CharacterImage(lastConnectedComponent);
            DisplayImageAsAsciiInConsole(lastImage);
            if (!lastImage.IsNormalizedTo32x32())
            {
                lastImage.NormalizeTo32x32();
            }
            if (!lastImage.IsMadeOnlyBlackAndWhite())
            {
                lastImage.MakeOnlyBlackAndWhite();
            }
            lastImage.ThickenBlackPixels();

            String lastImageLinearizedAsString = lastImage.LinearizeImageToString();
            ConnectionManager.SendLinearizedImageForClassification(lastImageLinearizedAsString, connectedComponentsTool.GetLatestAdjustmentId());
        }

        private void DisplayImageAsAsciiInConsole(CharacterImage image)
        {
            if (!image.IsNormalizedTo32x32())
            {
                image.NormalizeTo32x32();
            }
            if (!image.IsMadeOnlyBlackAndWhite())
            {
                image.MakeOnlyBlackAndWhite();
            }
            String lastImageLinearizedAsString = image.LinearizeImageToString();
            int cnt = 0;
            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    char ch = '0';
                    if (lastImageLinearizedAsString[cnt] == '0')
                        ch = '0';
                    else
                        ch = ' ';
                    cnt++;
                    Console.Write(ch);
                }
                Console.WriteLine();
            }
        }

        private void drawPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(m_auxiliaryBitmap, new Point(0, 0));
        }

        public List<ConnectedComponent> ConnectedComponents
        {
            get
            {
                return m_connectedComponents;
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            Graphics.FromImage(m_auxiliaryBitmap).Clear(Color.White);
            drawPanel.CreateGraphics().DrawImageUnscaled(m_auxiliaryBitmap, new Point(0, 0));
            connectedComponentsTool.Initialize(m_auxiliaryBitmap);
            m_connectedComponents.RemoveRange(0, m_connectedComponents.Count);

            UIUpdater.ResetPredictedWordLabel();
        }

        private void testConnectedComponentsWindowButton_Click(object sender, EventArgs e)
        {
            DebugConnectedComponentsTool.DisplayConnectedComponentsInNewWindow(this.ConnectedComponents);
        }

        private void sendBytesToPythonDebugButton_Click(object sender, EventArgs e)
        {
            byte[] bytesToSend = new byte[10];
            for (int i = 0; i < 10; i++)
            {
                bytesToSend[i] = (byte)i;
            }
            ConnectionManager.SendBytes(bytesToSend);
        }

        private void startPythonClientButton_Click(object sender, EventArgs e)
        {
            ApplicationStarter.StartPythonClientFromStartingPoint();
        }

        private void collectNewDataButton_Click(object sender, EventArgs e)
        {
            CollectNewDataWindow newWindow = new CollectNewDataWindow();
            newWindow.ShowDialog();
        }
    }
}
