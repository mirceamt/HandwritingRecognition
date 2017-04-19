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

using HandwritingRecognition.ImageProcessing;
using HandwritingRecognition.Debugging;

namespace HandwritingRecognition
{
    public partial class Form1 : Form
    {
        /*
        Graphics m_graphics;
        bool m_startPaint = false;
        Point m_oldPoint = new Point(-1, -1);
        public Form1()
        {
            InitializeComponent();
            m_graphics = drawPanel.CreateGraphics();
        }

        private void drawPanel_MouseDown(object sender, MouseEventArgs e)
        {
            m_startPaint = true;
        }

        private void drawPanel_MouseUp(object sender, MouseEventArgs e)
        {
            m_startPaint = false;
            m_oldPoint.X = -1;
            m_oldPoint.Y = -1;
        }

        private void drawPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_startPaint == true)
            {
                Pen pen = new Pen(Color.Black, 4);
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                Point currentPoint = new Point(e.X, e.Y);
                if (m_oldPoint.X == -1 && m_oldPoint.Y == -1)
                {
                    this.DrawLineWithMiddlePoint(currentPoint, currentPoint);
                }
                else
                {
                    //Point[] points = new Point[2];
                    //points[0] = m_oldPoint;
                    //points[1] = currentPoint;
                    //byte[] pointTypes = new byte[2];
                    //pointTypes[0] = (byte)PathPointType.Start;
                    //pointTypes[1] = (byte)PathPointType.Line;
                    //GraphicsPath path = new GraphicsPath(points, pointTypes);

                    this.DrawLineWithMiddlePoint(m_oldPoint, currentPoint);
                }
                m_oldPoint = currentPoint;
            }
        }

        private void DrawLine(PointF A, PointF B)
        {
            Brush brush = new SolidBrush(Color.Black);
            Pen pen = new Pen(brush, 10);
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            m_graphics.DrawLine(pen, A, B);
        }

        private void DrawLineWithMiddlePoint(PointF A, PointF B)
        {
            PointF middlePoint = new PointF((A.X + B.X) / 2, (A.Y + B.Y) / 2);
            this.DrawLine(A, middlePoint);
            this.DrawLine(middlePoint, B);
        }

        private void drawPanel_Paint(object sender, PaintEventArgs e)
        {
            Console.Out.Write("yea");
        }

        private void drawLineButton_Click(object sender, EventArgs e)
        {
            PointF startPoint = new PointF(float.Parse(x1TextBox.Text), float.Parse(y1TextBox.Text));
            PointF endPoint = new PointF(float.Parse(x2TextBox.Text), float.Parse(y2TextBox.Text));
            this.DrawLine(startPoint, endPoint);
            
        }
        

        */

        bool m_canDraw = false;

        PointF m_previousPoint = new PointF(-1, -1);

        Bitmap m_auxiliaryBitmap;

        ConnectedComponentsTool connectedComponentsTool = ConnectedComponentsTool.Instance;

        List<ConnectedComponent> m_connectedComponents;

        public Form1()
        {
            InitializeComponent();
            m_connectedComponents = new List<ConnectedComponent>();
            this.m_auxiliaryBitmap = new Bitmap(drawPanel.Width, drawPanel.Height, drawPanel.CreateGraphics());
            Graphics.FromImage(m_auxiliaryBitmap).Clear(Color.White);
            connectedComponentsTool.Initialize(m_auxiliaryBitmap);
        }

        private void drawPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_canDraw)
            {
                Graphics panel = Graphics.FromImage(m_auxiliaryBitmap);

                Pen pen = new Pen(Color.Black, 10);

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

            // TODO: move the classification below to another thread

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
        }

        private void testConnectedComponentsWindowButton_Click(object sender, EventArgs e)
        {
            DebugConnectedComponentsTool.DisplayConnectedComponentsInNewWindow(this.ConnectedComponents);
        }


    }
}
