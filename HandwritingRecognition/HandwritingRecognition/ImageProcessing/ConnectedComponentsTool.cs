using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HandwritingRecognition.ImageProcessing
{
    public sealed class ConnectedComponentsTool
    {
        public ConnectedComponentsTool() { } 

        public void Initialize(Bitmap bitmap)
        {
            m_initialized = true;
            m_width = bitmap.Width;
            m_height = bitmap.Height;
            m_verticalResolution = bitmap.VerticalResolution;
            m_horizontalResolution = bitmap.HorizontalResolution;

            m_lastColorMatrix = new Color[m_height, m_width];
            m_connectedComponentsMatrix = new int[m_height, m_width];
            m_visitedCells = new bool[m_height, m_width];
            m_existingConnectedComponents = new List<ConnectedComponent>();

            m_connectedComponentsCounter = 0;
            
            for (int i = 0; i < m_height; i++)
            {
                for (int j = 0; j < m_width; j++)
                {
                    m_lastColorMatrix[i, j] = Color.White;
                    m_connectedComponentsMatrix[i, j] = 0;
                    m_visitedCells[i, j] = false;
                }
            }
        }

        public List<ConnectedComponent> InspectConnectedComponents(Bitmap bmp)
        {
            
            if (!m_initialized)
            {
                throw new Exception("ConnectedComponentsTool not initalized yet!");
            }

            Point startingPoint = new Point(0, 0);
            bool foundStartingPoint = false;

            for (int i = 0; i < m_height; i++)
            {
                for (int j = 0; j < m_width; j++)
                {
                    Color drawnColor = bmp.GetPixel(j, i);
                    int drawnColorArgb = drawnColor.ToArgb();

                    Color lastColor = m_lastColorMatrix[i, j];
                    int lastColorArgb = lastColor.ToArgb();


                    if (drawnColorArgb != lastColorArgb && lastColorArgb == Color.White.ToArgb())
                    {
                        if (!foundStartingPoint)
                        {
                            foundStartingPoint = true;
                            startingPoint.X = i;
                            startingPoint.Y = j;
                        }
                    }

                    m_lastColorMatrix[i, j] = drawnColor;
                }
            }

            if (foundStartingPoint)
            {
                m_connectedComponentsCounter++;
                Queue<Point> connectedComponentQueue = new Queue<Point>();
                HashSet<int> conjoinedConnectedComponents = new HashSet<int>();
                List<Point> newConnectedComponentPointsList = new List<Point>();
                for (int i = 0; i < m_height; i++)
                {
                    for (int j = 0; j < m_width; j++)
                    {
                        m_visitedCells[i, j] = false;
                    }
                }
                connectedComponentQueue.Enqueue(startingPoint);
                m_visitedCells[startingPoint.X, startingPoint.Y] = true;
                m_connectedComponentsMatrix[startingPoint.X, startingPoint.Y] = m_connectedComponentsCounter;
                while (connectedComponentQueue.Count > 0)
                {
                    Point currentPoint = connectedComponentQueue.Dequeue();
                    newConnectedComponentPointsList.Add(currentPoint);

                    // k < 4 in case we want to explore the pixels using 4 directions
                    // and dx8 -> dx4, dy8 -> dy4
                    for (int k = 0; k < 8; k++) 
                    {
                        Point nextPoint = new Point(currentPoint.X + dx8[k], currentPoint.Y + dy8[k]);
                        
                        if (IsInternalPoint(nextPoint) && m_visitedCells[nextPoint.X, nextPoint.Y] == false && bmp.GetPixel(nextPoint.Y, nextPoint.X).ToArgb() != Color.White.ToArgb())
                        {
                            m_visitedCells[nextPoint.X, nextPoint.Y] = true;
                            if (m_connectedComponentsMatrix[nextPoint.X, nextPoint.Y] != 0)
                            {
                                // we are joining 2 connected components:
                                // the m_connectedComponentsMatrix[nextPoint.X, nextPoint.Y]
                                // with m_connectedComponentsCounter
                                conjoinedConnectedComponents.Add(m_connectedComponentsMatrix[nextPoint.X, nextPoint.Y]);
                            }
                            m_connectedComponentsMatrix[nextPoint.X, nextPoint.Y] = m_connectedComponentsCounter;
                            connectedComponentQueue.Enqueue(nextPoint);
                        }
                    }
                } // while (queue != empty)
                this.AdjustExistingComponents(m_connectedComponentsCounter, newConnectedComponentPointsList, conjoinedConnectedComponents);
            }
            return m_existingConnectedComponents;
        }

        private void AdjustExistingComponents(int newComponentIndex, List<Point> newComponentPointsList, HashSet<int> oldConnectedComponentsIDs)
        {
            //List<int> indicesToDelete = new List<int>();
            //for(int i = 0; i < m_existingConnectedComponents.Count; i++)
            //{
            //    ConnectedComponent currentComponent = m_existingConnectedComponents[i];
            //    if (oldConnectedComponentsIDs.Contains(currentComponent.ID))
            //    {
            //        indicesToDelete.Add(i);
            //    }
            //}

            //for (int i = 0; i < indicesToDelete.Count; i++ )
            //{
            //    int indexToDelete = indicesToDelete[i];
            //    m_existingConnectedComponents.RemoveAt(indexToDelete);
            //}


            m_existingConnectedComponents.RemoveAll(x => oldConnectedComponentsIDs.Contains(x.ID));

            ConnectedComponent newComponent = new ConnectedComponent(newComponentIndex, newComponentPointsList);
            newComponent.NormalizeUsingTranslation();
            m_existingConnectedComponents.Add(newComponent);
        }

        public List<ConnectedComponent> GetAllConnectedComponentsAsOneUsingBoundingBox(Bitmap bmp)
        {
            // this method will be used for special characters such as 'i' and 'j' which consist of more than one connected component
            // and will use a bounding box for all the non-white pixels to return a list consisting of only one ConnectedComponent object

            List<ConnectedComponent> ret = new List<ConnectedComponent>();
            List<Point> newConnectedComponentPointsList = new List<Point>();
            m_connectedComponentsCounter++;

            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color drawColor = bmp.GetPixel(j, i);
                    if (drawColor.ToArgb() != Color.White.ToArgb()) // it's a non white pixel, presumably a black one
                    {
                        newConnectedComponentPointsList.Add(new Point(i, j)); // 99% sure it's (i,j) and NOT (j,i)
                    }
                }
            }
            ConnectedComponent newConnectedComponent = new ConnectedComponent(m_connectedComponentsCounter, newConnectedComponentPointsList);
            newConnectedComponent.NormalizeUsingTranslation();
            ret.Add(newConnectedComponent);

            return ret;
        }

        private bool IsInternalPoint(Point p)
        {
            return 0 <= p.X && p.X < m_height && 0 <= p.Y && p.Y < m_width;
        }

        public int Width
        {
            get
            {
                if (!m_initialized)
                {
                    throw new Exception("ConnectedComponentsTool not initalized yet!");
                }
                return m_width;
            }
        }
        public int Height
        {
            get
            {
                if (!m_initialized)
                {
                    throw new Exception("ConnectedComponentsTool not initalized yet!");
                }
                return m_height;
            }
        }

        List<ConnectedComponent> m_existingConnectedComponents = null;

        private Color[,] m_lastColorMatrix = null;
        private int[,] m_connectedComponentsMatrix = null;
        private bool[,] m_visitedCells = null;
        private int m_connectedComponentsCounter;

        private int m_width = 0;
        private int m_height = 0;
        private float m_horizontalResolution = 0.0f;
        private float m_verticalResolution = 0.0f;

        public bool m_initialized = false;

        private int[] dx4 = new int[4] { -1, 0, 1, 0 };
        private int[] dy4 = new int[4] { 0, 1, 0, -1 };
        private int[] dx8 = new int[8] { -1, -1, 0, 1, 1, 1, 0, -1 };
        private int[] dy8 = new int[8] { 0, 1, 1, 1, 0, -1, -1, -1 };
    }    
}
