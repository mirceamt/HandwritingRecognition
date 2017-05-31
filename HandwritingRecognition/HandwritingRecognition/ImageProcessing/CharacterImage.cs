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
        private bool normalizedTo32x32 = false;
        private bool madeOnlyBlackAndWhite = false;

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
            normalizedTo32x32 = false;
            madeOnlyBlackAndWhite = false;
        }

        public void NormalizeTo32x32()
        {
            Bitmap resizedBitmap = new Bitmap(m_bitmap, new Size(32, 32));
            m_bitmap = resizedBitmap;
            normalizedTo32x32 = true;
        }

        public void MakeOnlyBlackAndWhite()
        {
            for (int i = 0; i < m_bitmap.Height; i++)
            {
                for (int j = 0; j < m_bitmap.Width; j++)
                {
                    Color newColor = new Color();
                    Color oldColor = m_bitmap.GetPixel(j, i);

                    byte threshold = 127;

                    // there are only gray colors so it is sufficient to compare only the red channel
                    // if the color tends to be white we will transform it into pure white
                    // otherwise we will transform it into pure black
                    if (oldColor.R > threshold)
                    {
                        newColor = Color.White;
                    }
                    else
                    {
                        newColor = Color.Black;
                    }

                    m_bitmap.SetPixel(j, i, newColor);
                }
            }
            madeOnlyBlackAndWhite = true;
        }

        public String LinearizeImageToString()
        {
            if (!normalizedTo32x32)
            {
                NormalizeTo32x32();
            }
            if (!madeOnlyBlackAndWhite)
            {
                MakeOnlyBlackAndWhite();
            }
            String ret = "";
            for (int i = 0; i < m_bitmap.Height; i++)
            {
                for (int j = 0; j < m_bitmap.Width; j++)
                {
                    Color currentColor = m_bitmap.GetPixel(j, i);
                    if (currentColor.R == 255) // white
                    {
                        ret += "1";
                    }
                    else // black
                    {
                        ret += "0";
                    }
                }
            }
            return ret;
        }

        public bool IsMadeOnlyBlackAndWhite()
        {
            return madeOnlyBlackAndWhite;
        }

        public bool IsNormalizedTo32x32()
        {
            return normalizedTo32x32;
        }

        public Bitmap GetBitmap()
        {
            return m_bitmap;
        }

        private Bitmap m_bitmap = new Bitmap(1, 1);
    }
}
