using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HandwritingRecognition.ImageProcessing
{
    public class ConnectedComponent
    {
        private List<Point> m_points;
        private int m_id;
        private int m_globalLeft = -1;
        private int m_globalRight = -1;
        private int m_globalUp = -1;
        private int m_globalBottom = -1;
        private PointF m_globalCenter = new PointF();

        // points are stored different from how they are positioned in a bitmap
        // however the global variables such as m_globalRight or m_globalCenter are kept accord to the way a bitmap stores its pixels
        public ConnectedComponent(int index, List<Point> points)
        {
            // this constructor must receive unaltered global points
            m_id = index;
            m_points = points;

            m_globalLeft = m_globalRight = points[0].Y;
            m_globalUp = m_globalBottom = points[0].X;
            for (int i = 1; i < this.m_points.Count; i++)
            {
                m_globalLeft = Math.Min(m_globalLeft, this.m_points[i].Y);
                m_globalRight = Math.Max(m_globalRight, this.m_points[i].Y);
                m_globalUp = Math.Min(m_globalUp, this.m_points[i].X);
                m_globalBottom = Math.Max(m_globalBottom, this.m_points[i].X);
            }
            float globalX = (m_globalLeft + m_globalRight) / 2.0f;
            float globalY = (m_globalUp + m_globalBottom) / 2.0f;
            m_globalCenter = new PointF(globalX, globalY);
        }

        public int GlobalLeft
        {
            get
            {
                return m_globalLeft;
            }
        }

        public int GlobalRight
        {
            get
            {
                return m_globalRight;
            }
        }

        public int GlobalUp
        {
            get
            {
                return m_globalUp;
            }
        }

        public int GlobalBottom
        {
            get
            {
                return m_globalBottom;
            }
        }

        public PointF GlobalCenter
        {
            get
            {
                return m_globalCenter;
            }
        }

        public int ID
        {
            get
            {
                return m_id;
            }
        }

        public List<Point> Points
        {
            get
            {
                return m_points;
            }
        }

        public void NormalizeUsingTranslation()
        {
            int translateAmountX = -1, translateAmountY = -1;
            for (int i = 0; i < this.m_points.Count; i++)
            {
                Point p = this.m_points[i];
                if (translateAmountX == -1 || p.X < translateAmountX)
                {
                    translateAmountX = p.X;
                }
                if (translateAmountY == -1 || p.Y < translateAmountY)
                {
                    translateAmountY = p.Y;
                }
            }
            this.Translate(-translateAmountX, -translateAmountY);
        }

        public void Translate(int amountX, int amountY)
        {
            for (int i = 0; i < m_points.Count; i++)
            {
                int newX = this.m_points[i].X + amountX;
                int newY = this.m_points[i].Y + amountY;
                this.m_points[i] = new Point(newX, newY);
            }
        }

        public Rectangle GetBoundingBox()
        {
            // WARNING: take care of time optimality
            // the work done by this function could be avoided if we update the bounding box accordingly 
            // during the lifetime of this ConnectedComponent
            if (m_points.Count <= 0)
            {
                throw new Exception("the connected component has no points");
            }

            Rectangle ret = new Rectangle();

            int xMin, xMax, yMin, yMax;
            xMin = xMax = m_points[0].X;
            yMin = yMax = m_points[0].Y;


            for (int i = 0; i < m_points.Count; i++)
            {
                int currentX = m_points[i].X;
                int currentY = m_points[i].Y;

                xMin = Math.Min(xMin, currentX);
                xMax = Math.Max(xMax, currentX);

                yMin = Math.Min(yMin, currentY);
                yMax = Math.Max(yMax, currentY);
            }

            ret.X = xMin;
            ret.Y = yMin;

            // WARNING: these dimmensions represent the efective number of pixels 
            // between [xMin, xMax] and [yMin, yMax]
            ret.Width = xMax - xMin + 1;
            ret.Height = yMax - yMin + 1;

            return ret;
        }

    }
}
