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
        public enum NormalizeTo32x32Type
        {
            UnbiasedRatio,
            BiasedRatio
        }

        public static readonly int[] dx4 = { -1, 0, 1, 0 };
        public static readonly int[] dy4 = { 0, 1, 0, -1 };

        public static readonly int[] dx8 = { -1, -1, 0, 1, 1, 1, 0, -1 };
        public static readonly int[] dy8 = { 0, 1, 1, 1, 0, -1, -1, -1 };

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

        public void NormalizeTo32x32(NormalizeTo32x32Type opType)
        {
            Bitmap resizedBitmap = null;
            switch(opType)
            {
                case NormalizeTo32x32Type.BiasedRatio:
                    resizedBitmap = new Bitmap(m_bitmap, new Size(32, 32));
                    m_bitmap = resizedBitmap;
                    normalizedTo32x32 = true;
                    break;

                case NormalizeTo32x32Type.UnbiasedRatio:
                    int nrColumns = m_bitmap.Width;
                    int nrLines = m_bitmap.Height;
                    int maxDimension = Math.Max(nrLines, nrColumns);
                    resizedBitmap = new Bitmap(maxDimension, maxDimension);

                    Graphics.FromImage(resizedBitmap).Clear(Color.White);

                    int startDrawX = 0, startDrawY = 0;
                    if (nrLines >= nrColumns)
                    {
                        int stanga = 0;
                        int dreapta = 1;
                        int side = stanga;
                        for (int i = nrColumns + 1; i <= nrLines; i++)
                        {
                            if (side == stanga)
                            {
                                startDrawX++;
                            }
                            else
                            {
                                // nothing
                            }
                            side = 1 - side;
                        }
                    }
                    else
                    {
                        // prea putine linii
                        int sus = 0;
                        int jos = 1;
                        int side = sus;
                        for (int i = nrLines + 1; i <= nrColumns; i++)
                        {
                            if (side == sus)
                            {
                                startDrawY++;
                            }
                            else
                            {
                                // nothing
                            }
                            side = 1 - side;
                        }
                    }

                    Graphics.FromImage(resizedBitmap).DrawImage(m_bitmap, new Point(startDrawX, startDrawY));
                    resizedBitmap = new Bitmap(resizedBitmap, new Size(32, 32));
                    m_bitmap = resizedBitmap;
                    normalizedTo32x32 = true;
                    break;
            }
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

        public String LinearizeImageToString(NormalizeTo32x32Type normalizeTo32x32OperationType)
        {
            if (!normalizedTo32x32)
            {
                NormalizeTo32x32(normalizeTo32x32OperationType);
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

        public void ThickenBlackPixels()
        {
            Bitmap newImg = new Bitmap(m_bitmap);
            

            for (int i = 0; i < m_bitmap.Height; i++)
            {
                for (int j = 0; j < m_bitmap.Width; j++)
                {
                    Color currentColor = m_bitmap.GetPixel(j, i);
                    if (currentColor.ToArgb() == Color.Black.ToArgb())
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            int newX = j + dx4[k], newY = i + dy4[k];
                            GraphicsUnit graphicsUnit = GraphicsUnit.Pixel;
                            RectangleF rectangleImg = newImg.GetBounds(ref graphicsUnit);
                            if (rectangleImg.Contains(new PointF(newX, newY)))
                            {
                                newImg.SetPixel(newX, newY, Color.Black);
                            }
                        }
                    }
                }
            }
            m_bitmap = newImg;
        }

        public void NormalizeAndSave(String pathForSaving, String imageName)
        {
            if (!IsNormalizedTo32x32())
            {
                //NormalizeTo32x32(NormalizeTo32x32Type.BiasedRatio);
                NormalizeTo32x32(NormalizeTo32x32Type.UnbiasedRatio);
            }
            if (!IsMadeOnlyBlackAndWhite())
            {
                MakeOnlyBlackAndWhite();
            }
            ThickenBlackPixels();
            
            Save(pathForSaving, imageName);
        }

        public void Save(String pathForSaving, String imageName)
        {
            m_bitmap.Save(pathForSaving + imageName + ".png", ImageFormat.Png);
        }
    }
}
