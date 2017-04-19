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
        public ConnectedComponent(int index, List<Point> points)
        {
            m_id = index;
            m_points = points;
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

        private List<Point> m_points;
        private int m_id;

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
