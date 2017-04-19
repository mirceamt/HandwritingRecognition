using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

using HandwritingRecognition.ImageProcessing;

namespace HandwritingRecognition.ImageProcessing
{
    class CharacterImage
    {
        public CharacterImage(ConnectedComponent connectedComponent)
        {
            Rectangle boundingBox = connectedComponent.GetBoundingBox();

            m_bitmap = new Bitmap(boundingBox.Height, boundingBox.Width);
            for (int i = 0; i < m_bitmap.Height; i++)
            {
                for (int j = 0; j < m_bitmap.Width; j++)
                {
                    m_bitmap.SetPixel(j, i, Color.White);
                }
            }

            for (int i = 0; i < connectedComponent.Points.Count; i++)
            {
                int x = connectedComponent.Points[i].X;
                int y = connectedComponent.Points[i].Y;
                m_bitmap.SetPixel(y, x, Color.Black);
            }
        }

        public Bitmap GetBitmap()
        {
            return m_bitmap;
        }

        private Bitmap m_bitmap = new Bitmap(1, 1);
    }
}
