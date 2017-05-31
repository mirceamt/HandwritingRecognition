using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

using HandwritingRecognition.ImageProcessing;
using HandwritingRecognition.Debugging;

namespace HandwritingRecognition
{
    public partial class ConnectedComponentsWindow : Form
    {
        private int index = 0;

        Bitmap m_auxiliaryBitmap;

        public ConnectedComponentsWindow()
        {
            InitializeComponent();

            DisplayConnectedComponent(index);
        }

        private void DisplayConnectedComponent(int index)
        {
            if (0 > index || index >= DebugConnectedComponentsTool.ConnectedComponents.Count)
            {
                return;
            }

            ConnectedComponent currentComponent = DebugConnectedComponentsTool.ConnectedComponents[index];
            CharacterImage currentCharacterImage = new CharacterImage(currentComponent);
            if (!currentCharacterImage.IsNormalizedTo32x32())
            {
                currentCharacterImage.NormalizeTo32x32();
            }
            if (!currentCharacterImage.IsMadeOnlyBlackAndWhite())
            {
                currentCharacterImage.MakeOnlyBlackAndWhite();
            }

            m_auxiliaryBitmap = currentCharacterImage.GetBitmap();

            connectedComponentPanel.Refresh();
            connectedComponentPanel.CreateGraphics().DrawImageUnscaled(m_auxiliaryBitmap, new Point(0, 0));
        }

        private void connectedComponentPanel_Paint(object sender, PaintEventArgs e)
        {
            if (m_auxiliaryBitmap == null)
            {
                return;
            }
            e.Graphics.DrawImageUnscaled(m_auxiliaryBitmap, new Point(0, 0));
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            index++;
            if (index >= DebugConnectedComponentsTool.ConnectedComponents.Count)
            {
                index = 0;
            }
            DisplayConnectedComponent(index);
        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            index--;
            if (index < 0)
            {
                index = DebugConnectedComponentsTool.ConnectedComponents.Count - 1;
            }
            DisplayConnectedComponent(index);
        }
    }
}
