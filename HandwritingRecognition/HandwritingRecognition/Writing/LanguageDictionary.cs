using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HandwritingRecognition.Resources;

namespace HandwritingRecognition.Writing
{
    class Node
    {
        Node[] sons = new Node[128];
        bool m_isFinalWord = false;
        char m_charInNode = (char)0;

        public Node(char charInNodeParam = '\0', bool isFinalWordParam = false)
        {
            m_charInNode = charInNodeParam;
            m_isFinalWord = isFinalWordParam;
        }

        public bool IsFinalWord
        {
            get
            {
                return m_isFinalWord;
            }
            set
            {
                m_isFinalWord = value;
            }
        }

        public char CharInNode
        {
            get
            {
                return m_charInNode;
            }
            set
            {
                m_charInNode = value;
            }
        }

        public Node[] Sons
        {
            get
            {
                return sons;
            }
            set
            {
                sons = value;
            }
        }
    }


    class LanguageDictionary
    {
        private Node root = new Node();

        public LanguageDictionary()
        {

        }

        private void AddWordInTrie(Node nod, string s, int k)
        {
            if (k == s.Length)
            {
                nod.IsFinalWord = true;
                return;
            }

            int sonNumber = (int)s[k];
            if (nod.Sons[sonNumber] == null)
            {
                nod.Sons[sonNumber] = new Node(s[k], false);
            }

            AddWordInTrie(nod.Sons[sonNumber], s, k + 1);
        }

        private void AddWordInTrie(string s)
        {
            AddWordInTrie(root, s, 0);
        }

        private void LoadFromFile(string fileName)
        {
            StreamReader reader = new StreamReader(fileName);
            string currentLine = "";
            while ((currentLine = reader.ReadLine()) != null)
            {
                AddWordInTrie(currentLine);
            }

            reader.Close();
        }

        public void Load(string language = "english", string typeOfWords = "alpha")
        {
            StreamReader reader = new StreamReader(Paths.LanguageDictionaryPath);

            String currentLine = "";
            bool found = false;

            while ((currentLine = reader.ReadLine()) != null)
            {
                if (currentLine == language)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                throw new Exception("Could not locate language! Language parameter: " + language);
            }

            string fileName = language;
            if (typeOfWords != null)
            {
                fileName = fileName + "_" + typeOfWords;
            }
            fileName.ToLower();

            found = false;
            while ((currentLine = reader.ReadLine()) != null)
            {
                if (currentLine.Contains(fileName))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                throw new Exception("Could not locate fileName! FileName parameter: " + fileName);
            }

            reader.Close();

            LoadFromFile(currentLine);
        }

        public bool ExistsPrefix(Node nod, String s, int k)
        {
            if (k == s.Length)
            {
                return true;
            }

            int sonIndex = (int)s[k];
            if (nod.Sons[sonIndex] == null)
            {
                return false;
            }
            return ExistsPrefix(nod.Sons[sonIndex], s, k + 1);
        }

        public bool ExistsPrefix(String s)
        {
            return ExistsPrefix(root, s, 0);
        }
    }
}
