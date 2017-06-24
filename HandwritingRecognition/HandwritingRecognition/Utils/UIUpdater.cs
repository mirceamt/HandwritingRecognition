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
        private static List<Control> m_addedCandidateWordsLabels = new List<Control>();

        private static WritingObserver m_writingObserver = null;

        public static void Initialize(Form1 form, TextBox textBox, WritingObserver wo)
        {
            predictedWords = textBox;
            m_form = form;
            m_writingObserver = wo;
        }

        public static void Clear()
        {
            ResetPredictedWordsLabel();
            RemoveLabelsForCandidateWordsFromForm();
        }

        public static void UpdatePredictedWord(String s)
        {
            // obsolete
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
                { }
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

        public static void ResetPredictedWordsLabel()
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
                { }
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
                { }
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

        public static void RemoveLabelsForCandidateWordsFromFormHelper()
        {
            for(int i = 0; i < m_addedCandidateWordsLabels.Count; i++)
            {
                m_form.Controls.Remove(m_addedCandidateWordsLabels[i]);
            }
        }

        public static void RemoveLabelsForCandidateWordsFromForm()
        {
            if (m_form.IsHandleCreated)
            {
                try
                {
                    m_form.Invoke((MethodInvoker)delegate
                    {
                        RemoveLabelsForCandidateWordsFromFormHelper();
                    });
                }
                catch (ObjectDisposedException)
                { }
            }
            else
            {
                RemoveLabelsForCandidateWordsFromFormHelper();
            }
            m_addedCandidateWordsLabels.Clear();
        }

        public static int GetWindowWidth()
        {
            int ret = 0;
            if (m_form.IsHandleCreated)
            {
                try
                {
                    m_form.Invoke((MethodInvoker)delegate
                    {
                        ret = m_form.Width;
                    });
                }
                catch (ObjectDisposedException)
                { }
            }
            else
            {
                ret = m_form.Width;
            }
            return ret;
        }

        public static void CreateLabelsForCandidateWords(List<Word> candidateWords)
        {
            RemoveLabelsForCandidateWordsFromForm();
            Label lastLabel = null;
            int padding = 8;

            int cnt = 0;
            for (int i = 0; i < candidateWords.Count; i++)
            {
                Label label = new Label();
                label.AutoSize = true;
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Padding = new Padding(padding);
                label.BorderStyle = BorderStyle.FixedSingle;
                
                label.MouseEnter += label_MouseEnter;
                label.MouseLeave += label_MouseLeave;
                label.MouseDown += label_MouseDown;
                label.MouseClick += label_MouseClick;

                label.Name = "label_candidateWord_" + cnt.ToString();
                cnt++;
                label.Text = candidateWords[i].ToString();
                if (lastLabel == null)
                {
                    label.Location = new Point(162, 30);
                }
                else
                {
                    label.Location = new Point(lastLabel.Location.X + lastLabel.Size.Width + padding, lastLabel.Location.Y);
                    int windowWidth = GetWindowWidth();
                    int locationX = label.Location.X;
                    int labelWidth = label.Size.Width;

                    if (locationX + labelWidth > windowWidth)
                    {
                        label.Location = new Point(162, lastLabel.Location.Y + lastLabel.Height + padding);
                    }
                }
                
                AddLabelToForm(label);
                m_addedCandidateWordsLabels.Add(label);

                lastLabel = label;
            }
        }

        static void label_MouseClick(object sender, MouseEventArgs e)
        {
            Label label = (Label)sender;
            String textOnLabel = label.Text;
            m_writingObserver.FinishCurrentWord(textOnLabel);
        }

        static void label_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = (Label)sender;
            label.BackColor = Color.GreenYellow;
        }

        static void label_MouseLeave(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            label.BackColor = Color.Transparent;
            label.Cursor = Cursors.Default;
        }

        static void label_MouseEnter(object sender, EventArgs e)
        {
            Label label = (Label)sender; 
            label.BackColor = Color.White;
            label.Cursor = Cursors.Hand;
        }
    }
}
