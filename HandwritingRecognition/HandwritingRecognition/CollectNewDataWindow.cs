using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;

using HandwritingRecognition.ImageProcessing;
using HandwritingRecognition.Debugging;
using HandwritingRecognition.Classification;
using HandwritingRecognition.Communication;
using HandwritingRecognition.Utils;
using HandwritingRecognition.Resources;

namespace HandwritingRecognition
{
    public partial class CollectNewDataWindow : Form
    {
        bool m_canDraw = false;
        Bitmap m_auxiliaryBitmap;
        ConnectedComponentsTool connectedComponentsTool = new ConnectedComponentsTool();
        List<ConnectedComponent> m_connectedComponents;
        PointF m_previousPoint = new PointF(-1, -1);

        public CollectNewDataWindow()
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
            //m_connectedComponents = connectedComponentsTool.InspectConnectedComponents(m_auxiliaryBitmap);
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

            errorLabel.Text = "Cleared all";
        }

        private String CharacterToHexStringRepresentation(char x)
        {
            String ret = "";
            byte[] aux = { (byte)x };
            ret = BitConverter.ToString(aux);
            ret = ret.ToLower();
            return ret;
        }

        private int GetLastFileNumber(String collectedNewDataFolderPath, char labelCharacter)
        {
            String hexCharacterRepresentation = CharacterToHexStringRepresentation(labelCharacter);
            int lastImageNumber = 0;

            DirectoryInfo hexDirectoryFolder;

            if (!Directory.Exists(collectedNewDataFolderPath + hexCharacterRepresentation))
            {
                lastImageNumber = 0;
                hexDirectoryFolder = Directory.CreateDirectory(collectedNewDataFolderPath + hexCharacterRepresentation);
            }
            else
            {
                hexDirectoryFolder = new DirectoryInfo(collectedNewDataFolderPath + hexCharacterRepresentation);
                FileInfo[] files = hexDirectoryFolder.GetFiles();
                lastImageNumber = 0;
                for (int i = 0; i < files.Length; i++)
                {
                    if (files[i].Extension.ToLower() == ".png")
                    {
                        String extension = files[i].Extension;
                        int lastIndexOfExtension = files[i].Name.LastIndexOf(extension);

                        String fileNameWithoutExtension = files[i].Name.Substring(0, lastIndexOfExtension);
                        int fileNumber = -1;
                        if (Int32.TryParse(fileNameWithoutExtension, out fileNumber))
                        {
                            lastImageNumber = Math.Max(lastImageNumber, fileNumber);
                        }
                    }
                }
            }

            return lastImageNumber;
        }

        private void SaveConnectedComponentsToFileHelper(List<ConnectedComponent> connectedComponentsList)
        {
            char labelCharacter = textBox1.Text[0];

            StreamReader stream = new StreamReader(Paths.CollectedNewDataPath);
            String collectedNewDataFolderPath = stream.ReadLine();
            stream.Close();

            String hexCharacterRepresentation = CharacterToHexStringRepresentation(labelCharacter);

            int lastImageNumber = GetLastFileNumber(collectedNewDataFolderPath, labelCharacter);

            errorLabel.Text = "Saving..";
            for (int i = 0; i < connectedComponentsList.Count; i++)
            {
                errorLabel.Text = "Saving.. " + i.ToString();
                CharacterImage currentCharacterImage = new CharacterImage(connectedComponentsList[i]);

                lastImageNumber++;
                currentCharacterImage.NormalizeAndSave(collectedNewDataFolderPath + hexCharacterRepresentation + "\\", lastImageNumber.ToString());
            }

            errorLabel.Text = "Finished saving.";
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 1)
            {
                errorLabel.Text = "Error: Please set a valid label";
                return;
            }
            m_connectedComponents = connectedComponentsTool.InspectAllConnectedComponentsAtOnce(m_auxiliaryBitmap);
            SaveConnectedComponentsToFileHelper(m_connectedComponents);
        }

        private void saveOneButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 1)
            {
                errorLabel.Text = "Error: Please set a valid label";
                return;
            }
            List<ConnectedComponent> connectedComponentsList = connectedComponentsTool.GetAllConnectedComponentsAsOneUsingBoundingBox(m_auxiliaryBitmap);
            SaveConnectedComponentsToFileHelper(connectedComponentsList);
        }
    }
}
