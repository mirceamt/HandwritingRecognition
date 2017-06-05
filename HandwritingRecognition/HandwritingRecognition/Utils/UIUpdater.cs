using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;

using HandwritingRecognition.Writing;

namespace HandwritingRecognition.Utils
{
    class UIUpdater
    {
        private static TextBox predictedWords = null;
        private static Form1 m_form = null;
        public static void Initialize(Form1 form, TextBox textBox)
        {
            predictedWords = textBox;
            m_form = form;
        }

        public static void UpdatePredictedWord(String s)
        {
            if (predictedWords.IsHandleCreated)
            {
                try
                {
                    predictedWords.Invoke((MethodInvoker)delegate
                    {
                        AddLetterToLastPredictedWord(s[0]);
                    });
                }
                catch (ObjectDisposedException)
                {

                }
            }
            else
            {
                AddLetterToLastPredictedWord(s[0]);
            }
        }

        public static void AddLetterToLastPredictedWord(char ch)
        {
            String existingText = predictedWords.Text;
            existingText += ch;
            predictedWords.Text = existingText;
        }

        public static void ResetPredictedWordLabel()
        {
            if (predictedWords.IsHandleCreated)
            {
                try
                {
                    predictedWords.Invoke((MethodInvoker)delegate
                    {
                        predictedWords.Text = "";
                    });
                }
                catch (ObjectDisposedException)
                {

                }
            }
            else
            {
                predictedWords.Text = "";
            }
        }

        internal static void SetPredictedWordsText(string allPredictedWords)
        {
            if (predictedWords.IsHandleCreated)
            {
                try
                {
                    predictedWords.Invoke((MethodInvoker)delegate
                    {
                        predictedWords.Text = allPredictedWords;
                    });
                }
                catch (ObjectDisposedException)
                {

                }
            }
            else
            {
                predictedWords.Text = allPredictedWords;
            }
        }

        private static void AddLabelToForm(Label label)
        {
            if (m_form.IsHandleCreated)
            {
                try
                {
                    m_form.Invoke((MethodInvoker)delegate
                    {
                        m_form.Controls.Add(label);
                    });
                }
                catch(ObjectDisposedException)
                { }
            }
            else
            {
                m_form.Controls.Add(label);
            }
        }

        public static void CreateLabelsForCandidateWords(List<Word> candidateWords)
        {
            Label lastLabel = null;
            for (int i = 0; i < candidateWords.Count; i++)
            {
                Label label = new Label();
                label.Text = candidateWords[i].ToString();
                if (lastLabel == null)
                {
                    label.Location = new Point(162, 30);
                }
                else
                {
                    label.Location = new Point(lastLabel.Location.X + lastLabel.Size.Width, 30);
                }
                
                AddLabelToForm(label);
                lastLabel = label;
            }
        }
    }
}
