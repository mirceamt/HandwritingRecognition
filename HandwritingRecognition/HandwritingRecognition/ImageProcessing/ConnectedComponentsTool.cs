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
            m_connectedComponentsMatrixForAtOnce = new int[m_height, m_width];

            m_visitedCells = new bool[m_height, m_width];
            m_existingConnectedComponents = new List<ConnectedComponent>();
            m_latestRemovedConnectedComponents = new List<ConnectedComponent>();
            m_latestAddedConnectedComponent = null;

            m_adjustmentsDoneCounter = 0;
            m_adjustmentsDone = new Dictionary<int, Tuple<List<ConnectedComponent>, ConnectedComponent>>();

            m_connectedComponentsCounter = 0;
            
            for (int i = 0; i < m_height; i++)
            {
                for (int j = 0; j < m_width; j++)
                {
                    m_lastColorMatrix[i, j] = Color.White;
                    m_connectedComponentsMatrix[i, j] = 0;
                    m_connectedComponentsMatrixForAtOnce[i, j] = 0;
                    m_visitedCells[i, j] = false;
                }
            }
        }

        private ConnectedComponent GetConnectedComponent(int contor, Point startingPoint, Bitmap bmp)
        {
            List<Point> newConnectedComponentListOfPoints = new List<Point>();
            Queue<Point> queue = new Queue<Point>();

            m_visitedCells[startingPoint.X, startingPoint.Y] = true;
            queue.Enqueue(startingPoint);
            m_connectedComponentsMatrixForAtOnce[startingPoint.X, startingPoint.Y] = contor;

            while(queue.Count > 0)
            {
                Point currentPoint = queue.Dequeue();
                newConnectedComponentListOfPoints.Add(currentPoint);

                for (int k = 0; k < 8; k++)
                {
                    Point nextPoint = new Point(currentPoint.X + dx8[k], currentPoint.Y + dy8[k]);

                    if (IsInternalPoint(nextPoint) && m_visitedCells[nextPoint.X, nextPoint.Y] == false && bmp.GetPixel(nextPoint.Y, nextPoint.X).ToArgb() != Color.White.ToArgb())
                    {
                        m_visitedCells[nextPoint.X, nextPoint.Y] = true;
                        m_connectedComponentsMatrixForAtOnce[nextPoint.X, nextPoint.Y] = contor;
                        queue.Enqueue(nextPoint);
                    }
                }
            }

            ConnectedComponent ret = new ConnectedComponent(contor, newConnectedComponentListOfPoints);
            return ret;
        }

        public List<ConnectedComponent> InspectAllConnectedComponentsAtOnce(Bitmap bmp)
        {
            //use this method to get all the connected components from the bitmap using only one traversal of the bitmap
            // this method is particularly useful when gathering examples for training because repeated calls
            // to InspectConnectedComponents delay the app

            if (!m_initialized)
            {
                throw new Exception("ConnectedComponentsTool not initialized yet!");
            }

            for (int i = 0; i < m_height; i++)
            {
                for (int j = 0; j < m_width; j++)
                {
                    m_visitedCells[i, j] = false;
                    m_connectedComponentsMatrixForAtOnce[i, j] = 0;
                }
            }
            List<ConnectedComponent> ret = new List<ConnectedComponent>();
            int contor = 0;
            for (int i = 0; i < m_height; i++)
            {
                for (int j = 0; j < m_width; j++)
                {
                    Color drawnColor = bmp.GetPixel(j, i);
                    int drawnColorArgb = drawnColor.ToArgb();

                    if (drawnColorArgb != Color.White.ToArgb() && m_visitedCells[i, j] == false)
                    {
                        contor++;
                        ConnectedComponent currentConnectedComponent = GetConnectedComponent(contor, new Point(i, j), bmp);
                        currentConnectedComponent.NormalizeUsingTranslation();
                        ret.Add(currentConnectedComponent);
                    }
                }
            }

            return ret;
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
            // check if newComponentPointsList is a point
            if (true) //this if can be removed
            {
                int stepsLimit = 0;
                if (CheckIfListOfPointsIsAPoint(newComponentPointsList))
                {
                    stepsLimit = 50;
                }
                else
                {
                    stepsLimit = 18;
                }
                // check the pixels below this points.
                Point leftPoint = new Point();
                Point rightPoint = new Point();
                GetLeftAndRightExtremes(newComponentPointsList, ref leftPoint, ref rightPoint);
                int connectedComponentIndexOfPoint = newComponentIndex;
                int connectedComponentIndexSearched = -1;
                Point aPointInSearchedComponent = new Point();
                bool stop = false;
                for (int i = Math.Max(leftPoint.X, rightPoint.X), steps = 1; !stop && steps <= stepsLimit; i++, steps++)
                {
                    if (i >= m_height)
                    {
                        stop = true;
                        break;
                    }
                    for (int j = leftPoint.Y - 1; j <= rightPoint.Y + 1; j++) // -3, +2 = a little extension for search area below point
                    {
                        if (!IsInternalPoint(new Point(i, j)))
                        {
                            continue;
                        }
                        if (m_connectedComponentsMatrix[i, j] == 0 || m_connectedComponentsMatrix[i, j] == connectedComponentIndexOfPoint)
                        {
                            continue;
                        }
                        connectedComponentIndexSearched = m_connectedComponentsMatrix[i, j];
                        stop = true;
                        aPointInSearchedComponent = new Point(i, j);
                        break;
                    }
                }
                if (connectedComponentIndexSearched != -1)
                {
                    AddTheSearchedComponentToThePoint(aPointInSearchedComponent, connectedComponentIndexSearched, connectedComponentIndexOfPoint, newComponentPointsList);
                    oldConnectedComponentsIDs.Add(connectedComponentIndexSearched);
                }
            }

            m_latestRemovedConnectedComponents = m_existingConnectedComponents.FindAll(x => oldConnectedComponentsIDs.Contains(x.ID));
            ObtainTheNewPointsForTheNewComponent(ref newComponentPointsList, oldConnectedComponentsIDs); // COMMENT IF BAD THINGS HAPPEN
            m_existingConnectedComponents.RemoveAll(x => oldConnectedComponentsIDs.Contains(x.ID));

            ConnectedComponent newComponent = new ConnectedComponent(newComponentIndex, newComponentPointsList);
            newComponent.NormalizeUsingTranslation();

            m_latestAddedConnectedComponent = newComponent;

            m_adjustmentsDoneCounter++;
            m_adjustmentsDone.Add(m_adjustmentsDoneCounter, new Tuple<List<ConnectedComponent>, ConnectedComponent>(m_latestRemovedConnectedComponents, m_latestAddedConnectedComponent));

            m_existingConnectedComponents.Add(newComponent);
        }

        private void ObtainTheNewPointsForTheNewComponent(ref List<Point> newComponentPointsList, HashSet<int> oldConnectedComponentsIDs)
        {
            if (oldConnectedComponentsIDs.Count == 0)
            {
                return;
            }
            List<ConnectedComponent> oldComponents = m_existingConnectedComponents.FindAll(x => oldConnectedComponentsIDs.Contains(x.ID));
            HashSet<Point> newPoints = new HashSet<Point>();
            for (int i = 0; i < newComponentPointsList.Count; i++)
            {
                newPoints.Add(newComponentPointsList[i]);
            }

            for (int i = 0; i < oldComponents.Count; i++)
            {
                List<Point> aux = oldComponents[i].GetGlobalPoints();
                for (int j = 0; j < aux.Count; j++)
                {
                    newPoints.Add(aux[j]);
                }
            }
            newComponentPointsList = newPoints.ToList();
        }

        private void AddTheSearchedComponentToThePoint(Point aPointInSearchedComponent, int connectedComponentIndexSearched, int connectedComponentIndexOfPoint, List<Point> newComponentPointsList)
        {
            Queue<Point> queue = new Queue<Point>();
            queue.Enqueue(aPointInSearchedComponent);
            m_connectedComponentsMatrix[aPointInSearchedComponent.X, aPointInSearchedComponent.Y] = connectedComponentIndexOfPoint;
            while(queue.Count > 0)
            {
                Point currentPoint = queue.Dequeue();
                newComponentPointsList.Add(currentPoint);
                for (int i = 0; i < 8; i++)
                {
                    Point nextPoint = new Point(currentPoint.X + dx8[i], currentPoint.Y + dy8[i]);
                    if (IsInternalPoint(nextPoint) && m_connectedComponentsMatrix[nextPoint.X, nextPoint.Y] == connectedComponentIndexSearched)
                    {
                        m_connectedComponentsMatrix[nextPoint.X, nextPoint.Y] = connectedComponentIndexOfPoint;
                        queue.Enqueue(nextPoint);
                    }
                }
            }
        }

        private void GetLeftAndRightExtremes(List<Point> pointsList, ref Point leftPoint, ref Point rightPoint)
        {
            leftPoint = pointsList[0];
            rightPoint = pointsList[0];
            for (int i = 0; i < pointsList.Count; i++)
            {
                if (pointsList[i].Y < leftPoint.Y)
                {
                    leftPoint = pointsList[i];
                }
                if (pointsList[i].Y > rightPoint.Y)
                {
                    rightPoint = pointsList[i];
                }
            }
        }

        private bool CheckIfListOfPointsIsAPoint(List<Point> newComponentPointsList)
        {
            //return true; // COMMENT IF BAD THINGS HAPPEN
            ConnectedComponent auxComponent = new ConnectedComponent(-1, newComponentPointsList);
            auxComponent.NormalizeUsingTranslation();

            CharacterImage chrImg = new CharacterImage(auxComponent);
            chrImg.NormalizeTo32x32(CharacterImage.NormalizeTo32x32Type.UnbiasedRatio);
            chrImg.MakeOnlyBlackAndWhite();
            chrImg.ThickenBlackPixels();

            return chrImg.CheckIfIsPoint();
            //return false;
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

        ConnectedComponent GetConnectedComponentsById(int idParam)
        {
            for (int i = 0; i < m_existingConnectedComponents.Count; i++)
            {
                if (m_existingConnectedComponents[i].ID == idParam)
                {
                    return m_existingConnectedComponents[i];
                }
            }
            return null;
        }

        public int GetLatestAdjustmentId()
        {
            return m_adjustmentsDoneCounter;
        }

        public Tuple<List<ConnectedComponent>, ConnectedComponent> GetAdjustmentById(int id)
        {
            if (m_adjustmentsDone.ContainsKey(id))
            {
                return m_adjustmentsDone[id];
            }
            return null;
        }

        List<ConnectedComponent> GetLatestRemovedConnectedComponents()
        {
            return m_latestRemovedConnectedComponents;
        }

        ConnectedComponent GetLatestAddedConnectedComponent()
        {
            return m_latestAddedConnectedComponent;
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
        List<ConnectedComponent> m_latestRemovedConnectedComponents = null;
        ConnectedComponent m_latestAddedConnectedComponent = null;

        Dictionary<int, Tuple<List<ConnectedComponent>, ConnectedComponent>> m_adjustmentsDone = null;
        int m_adjustmentsDoneCounter = 0;

        private Color[,] m_lastColorMatrix = null;
        private int[,] m_connectedComponentsMatrix = null;
        private int[,] m_connectedComponentsMatrixForAtOnce = null;
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
