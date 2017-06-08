using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


using HandwritingRecognition.ImageProcessing;

namespace HandwritingRecognition.Writing
{
    class Word
    {
        class ConnectedComponentComparer : Comparer<int>
        {
            private Word parent;
            public ConnectedComponentComparer(Word parentParam)
            {
                parent = parentParam;
            }
            public override int Compare(int x, int y)
            {
                ConnectedComponent A = parent.m_connectedComponents[x];
                ConnectedComponent B = parent.m_connectedComponents[y];
                PointF globalCenterA = A.GlobalCenter;
                PointF globalCenterB = B.GlobalCenter;
                if (globalCenterA.X < globalCenterB.X)
                {
                    return -1;
                }
                if (globalCenterA.X > globalCenterB.X)
                {
                    return 1;
                }
                return 0;
            }
        }

        private Dictionary<int, List<String>> m_possibleChars = null;
        private Dictionary<int, List<int>> m_positionsOfChosenChars = null; // numbers between [0-61]
        private Dictionary<int, ConnectedComponent> m_connectedComponents = null;
        private List<int> m_orderOfComponents = null;
        
        public Word()
        {
            m_possibleChars = new Dictionary<int, List<String>>();
            m_positionsOfChosenChars = new Dictionary<int, List<int>>();
            m_connectedComponents = new Dictionary<int, ConnectedComponent>();
            m_orderOfComponents = new List<int>();
        }

        public Word(Word oldWord, Dictionary<int, List<int>> newPositionsOfChosenChars)
        {
            this.m_possibleChars = oldWord.m_possibleChars;
            this.m_positionsOfChosenChars = newPositionsOfChosenChars;
            this.m_connectedComponents = oldWord.m_connectedComponents;
            this.m_orderOfComponents = oldWord.m_orderOfComponents;
        }

        public Dictionary<int, List<String>> GetPossibleCharsDictionary()
        {
            return m_possibleChars;
        }

        public Dictionary<int, List<int>> GetPositionsOfChosenCharsDictionary()
        {
            return m_positionsOfChosenChars;
        }

        public void AddConnectedComponent(int id, ConnectedComponent connectedComponent, List<String> possibleChars)
        {
            m_possibleChars.Add(id, possibleChars);
            List<int> positionsOfChosenChars = new List<int>();
            for (int i = 0; i < possibleChars.Count; i++)
            {
                positionsOfChosenChars.Add(0);
            }
            m_positionsOfChosenChars.Add(id, positionsOfChosenChars);
            m_connectedComponents.Add(id, connectedComponent);
            m_orderOfComponents.Add(id);
        }

        public void AddConnectedComponent(int id, ConnectedComponent connectedComponent, List<String> possibleChars, List<int> positionsOfChosenChars)
        {
            m_possibleChars[id] = possibleChars;
            m_positionsOfChosenChars[id] = positionsOfChosenChars;
            m_connectedComponents[id] = connectedComponent;
            if (!m_orderOfComponents.Contains(id))
            {
                m_orderOfComponents.Add(id);
            }
        }

        public void RemoveConnectedComponents(List<ConnectedComponent> connectedComponentsToRemove)
        {
            for (int i = 0; i < connectedComponentsToRemove.Count; i++)
            {
                int id = connectedComponentsToRemove[i].ID;
                m_connectedComponents.Remove(id);
                m_possibleChars.Remove(id);
                m_positionsOfChosenChars.Remove(id);
                m_orderOfComponents.Remove(id);
            }
        }

        public void SortComponents()
        {
            // here we sort the connected components using their global center 
            m_orderOfComponents.Sort(new ConnectedComponentComparer(this));
        }

        public override String ToString()
        {
            SortComponents();
            String ret = "";
            for (int i = 0; i < m_orderOfComponents.Count; i++)
            {
                int currentComponentId = m_orderOfComponents[i];
                List<String> currentPossibleChars = m_possibleChars[currentComponentId];
                List<int> currentPositionsOfChosenChars = m_positionsOfChosenChars[currentComponentId];

                for (int j = 0; j < currentPossibleChars.Count; j++)
                {
                    ret += currentPossibleChars[j][currentPositionsOfChosenChars[j]];
                }
            }

            return ret;
        }

        public int GetGlobalLeft()
        {
            int ret = -1;

            List<ConnectedComponent> allConnectedComponents = m_connectedComponents.Values.ToList();
            for (int i = 0; i < allConnectedComponents.Count; i++)
            {
                if (ret == -1)
                {
                    ret = allConnectedComponents[i].GlobalLeft;
                }
                else
                {
                    ret = Math.Min(ret, allConnectedComponents[i].GlobalLeft);
                }
            }

            return ret;
        }

        public int GetGlobalRight()
        {
            int ret = -1;

            List<ConnectedComponent> allConnectedComponents = m_connectedComponents.Values.ToList();
            for (int i = 0; i < allConnectedComponents.Count; i++)
            {
                if (ret == -1)
                {
                    ret = allConnectedComponents[i].GlobalRight;
                }
                else
                {
                    ret = Math.Max(ret, allConnectedComponents[i].GlobalRight);
                }
            }

            return ret;
        }

        public int GetGlobalUp()
        {
            int ret = -1;

            List<ConnectedComponent> allConnectedComponents = m_connectedComponents.Values.ToList();
            for (int i = 0; i < allConnectedComponents.Count; i++)
            {
                if (ret == -1)
                {
                    ret = allConnectedComponents[i].GlobalUp;
                }
                else
                {
                    ret = Math.Min(ret, allConnectedComponents[i].GlobalUp);
                }
            }

            return ret;
        }

        public int GetGlobalBottom()
        {
            int ret = -1;

            List<ConnectedComponent> allConnectedComponents = m_connectedComponents.Values.ToList();
            for (int i = 0; i < allConnectedComponents.Count; i++)
            {
                if (ret == -1)
                {
                    ret = allConnectedComponents[i].GlobalBottom;
                }
                else
                {
                    ret = Math.Max(ret, allConnectedComponents[i].GlobalBottom);
                }
            }

            return ret;
        }

        public float GetAverageWidthOfLetter()
        {
            int totalWidth = GetGlobalRight() - GetGlobalLeft();
            int totalLetterCount = 0;

            List<List<String>> allPossibleChars = m_possibleChars.Values.ToList();
            for (int i = 0; i < allPossibleChars.Count; i++)
            {
                totalLetterCount += allPossibleChars[i].Count;
            }

            float ret = -1.0f;
            if (totalLetterCount != 0)
            {
                ret = 1.0f * totalWidth / totalLetterCount;
            }

            return ret;
        }
    }
}
