/* Copyright (c) 2016, Cocosoft Inc.
 All rights reserved.
 http://www.Cocosofttech.com

 This file is part of the LIS open source project.

 The LIS  open source project is free software: you can
 redistribute it and/or modify it under the terms of the GNU General Public
 License as published by the Free Software Foundation, either version 3 of the
 License, or (at your option) any later version.

 The ClearCanvas LIS open source project is distributed in the hope that it
 will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
 Public License for more details.

 You should have received a copy of the GNU General Public License along with
 the LIS open source project.  If not, see
 <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

// Written by Amarnath S, Bangalore, India, 2009.
// Updated by Harsha T, Apr 2010.
// Updated by Amarnath, Aug 2010 to incorporate RGB images, with 8 bits per colour. This was suggested 
//  by Guiseppe Marchi and by Vignesh. 

namespace DicomImageViewer
{
    /// <summary>
    /// Image Panel Control
    /// User control to display an 8-bit grayscale image. Comprises of a panel, and two 
    /// scrollbars. Handles the display of an image on its panel, and links up the scrollbars 
    /// to enable image scrolling. This control can be used in any other project. Also does 
    /// Window Levelling.
    /// 
    /// This control exposes three public methods:
    /// 1. SetParameters, which takes in an arraylist of bytes (representing pixel values),
    ///    width, height, and a scroll bar reset boolean parameter. Internally, this method
    ///    creates a bitmap and populates it with the pixels. Then, it displays the bitmap 
    ///    over the panel.
    /// 2. SaveImage, to save the current image.
    /// 3. ResetValues, to reset to the original Window Level values.
    /// </summary>
    public partial class ImagePanelControl : UserControl
    {
        List<byte> pix8; 
        List<ushort> pix16;
        List<byte> pix24; // 30 July 2010
        Bitmap bmp;
        int hOffset;
        int vOffset;
        int hMax;
        int vMax;
        int imgWidth;
        int imgHeight;
        int panWidth;
        int panHeight;
        bool newImage;

        // For Window Level
        int winMin;
        int winMax;
        int winCentre;
        int winWidth;
        int winShr1;
        int deltaX;
        int deltaY;

        Point ptWLDown;
        double changeValWidth;
        double changeValCentre;
        bool rightMouseDown;
        bool imageAvailable;
        bool signed16Image;

        byte[] lut8;
        byte[] lut16;

		byte[] imagePixels8;
        byte[] imagePixels16;
        byte[] imagePixels24; // 30 July 2010
        int sizeImg;
        int sizeImg3;
        ViewDicom mf;

        ImageBitsPerPixel bpp;

        public ImagePanelControl()
        {
            InitializeComponent();
            DoubleBuffered = true;

            pix8 = new List<byte>();
            pix16 = new List<ushort>();
            pix24 = new List<byte>();

            this.hScrollBar.Visible = false;
            this.vScrollBar.Visible = false;

            winMin = 0;
            winMax = 65535;

            ptWLDown = new Point();
            changeValWidth = 0.5;
            changeValCentre = 0.5;
            rightMouseDown = false;
            imageAvailable = false;
            signed16Image = false;

			lut8 = new byte[256];
			lut16 = new byte[65536];

            PerformResize();
        }

        public bool NewImage
        {
            set
            {
                newImage = value;
            }
        }

        public bool Signed16Image
        {
            set { signed16Image = value; }
        }

        public void SetParameters(ref List<byte> arr, int wid, int hei, double windowWidth,
            double windowCentre, int samplesPerPixel, bool resetScroll, ViewDicom mainFrm)
        {
            if (samplesPerPixel == 1)
            {
                bpp = ImageBitsPerPixel.Eight;
                imgWidth = wid;
                imgHeight = hei;
                winWidth = Convert.ToInt32(windowWidth);
                winCentre = Convert.ToInt32(windowCentre);
                changeValWidth = 0.1;
                changeValCentre = 0.1;
                sizeImg = imgWidth * imgHeight;
                sizeImg3 = sizeImg * 3;

                pix8 = arr;
                imagePixels8 = new byte[sizeImg3];

                mf = mainFrm;
                imageAvailable = true;
                if (bmp != null)
                    bmp.Dispose();
                ResetValues();
                ComputeLookUpTable8();
                bmp = new Bitmap(imgWidth, imgHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                CreateImage8();
            }

            if (samplesPerPixel == 3) // 30 July 2010
            {
                bpp = ImageBitsPerPixel.TwentyFour;
                imgWidth = wid;
                imgHeight = hei;
                winWidth = Convert.ToInt32(windowWidth);
                winCentre = Convert.ToInt32(windowCentre);
                changeValWidth = 0.1;
                changeValCentre = 0.1;
                sizeImg = imgWidth * imgHeight;
                sizeImg3 = sizeImg * 3;

                pix24 = arr;
                imagePixels24 = new byte[sizeImg3];

                mf = mainFrm;
                imageAvailable = true;
                if (bmp != null)
                    bmp.Dispose();
                ResetValues();
                ComputeLookUpTable8();
                bmp = new Bitmap(imgWidth, imgHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                CreateImage24();
            }
            if( resetScroll == true ) ComputeScrollBarParameters();
            Invalidate();
        }

        public void SetParameters(ref List<ushort> arr, int wid, int hei, double windowWidth,
            double windowCentre, bool resetScroll, ViewDicom mainFrm)
        {
            bpp = ImageBitsPerPixel.Sixteen;
            imgWidth = wid;
            imgHeight = hei;
            winWidth = Convert.ToInt32(windowWidth);
            winCentre = Convert.ToInt32(windowCentre);

            sizeImg = imgWidth * imgHeight;
            sizeImg3 = sizeImg * 3;
			double sizeImg3By4 = sizeImg3 / 4.0;


            if (signed16Image == true)
                winCentre -= short.MinValue;

            // Modify the 'sensitivity' of the mouse based on the original window width
            if (winWidth < 5000)
            {
                changeValWidth = 2;
                changeValCentre = 2;
            }
            else if (Width > 40000)
            {
                changeValWidth = 50;
                changeValCentre = 50;
            }
            else // it is inbetween 5000 and 40000
            {
                changeValWidth = 25;
                changeValCentre = 25;
            }

            pix16 = arr;
			imagePixels16 = new byte[sizeImg3];

            mf = mainFrm;
            imageAvailable = true;
            if (bmp != null)
                bmp.Dispose();
            ResetValues();
            ComputeLookUpTable16();
            bmp = new Bitmap(imgWidth, imgHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            CreateImage16();
            if (resetScroll == true) ComputeScrollBarParameters();
            Invalidate();
        }

        // Create a bitmap on the fly, using 8-bit grayscale pixel data
        private void CreateImage8()
        {
			BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, imgWidth, imgHeight),
				System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);

			unsafe
			{
				int pixelSize = 3;
				int i, j, j1, i1;
				byte b;

				for (i = 0; i < bmd.Height; ++i)
				{
					byte* row = (byte*)bmd.Scan0 + (i * bmd.Stride);
					i1 = i * bmd.Width;

					for (j = 0; j < bmd.Width; ++j)
					{
						b = lut8[pix8[i * bmd.Width + j]];
						j1 = j * pixelSize;
						row[j1] = b;            // Red
						row[j1 + 1] = b;        // Green
						row[j1 + 2] = b;        // Blue
					}
				}
			}
			bmp.UnlockBits(bmd);
        }

        // Create a bitmap on the fly, using 8-bit RGB pixel data
        private void CreateImage24()
        {
            {
                int numBytes = imgWidth * imgHeight * 3;
                int j;
                int i, i1;

                BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, bmp.Width,
                    bmp.Height), ImageLockMode.WriteOnly, bmp.PixelFormat);

                int width3 = bmd.Width * 3;

                unsafe
                {                    
                    for (i = 0; i < bmd.Height; ++i)
                    {
                        byte* row = (byte*)bmd.Scan0 + (i * bmd.Stride);
                        i1 = i * bmd.Width * 3;

                        for (j = 0; j < width3; j += 3)
                        {
                            // Windows uses little-endian, so the RGB data is 
                            //  actually stored as BGR
                            row[j + 2] = lut8[pix24[i1 + j]];     // Blue
                            row[j + 1] = lut8[pix24[i1 + j + 1]]; // Green
                            row[j]     = lut8[pix24[i1 + j + 2]]; // Red
                        }
                    }
                }
                bmp.UnlockBits(bmd);
            }
        }

        // Create a bitmap on the fly, using 16-bit grayscale pixel data
        private void CreateImage16()
        {
			BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, imgWidth, imgHeight),
			   System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);

			unsafe
			{
				int pixelSize = 3;
				int i, j, j1, i1;
				byte b;

				for (i = 0; i < bmd.Height; ++i)
				{
					byte* row = (byte*)bmd.Scan0 + (i * bmd.Stride);
					i1 = i * bmd.Width;

					for (j = 0; j < bmd.Width; ++j)
					{
						b = lut16[pix16[i*bmd.Width+j]];
						j1 = j * pixelSize;
						row[j1] = b;            // Red
						row[j1 + 1] = b;        // Green
						row[j1 + 2] = b;        // Blue
					}
				}
			}			
            bmp.UnlockBits(bmd);
        }

        private void ComputeScrollBarParameters()
        {
            panWidth = panel.Width;
            panHeight = panel.Height;

            hOffset = (panWidth - imgWidth) / 2;
            vOffset = (panHeight - imgHeight) / 2;

            if (imgWidth < panWidth)
            {
                hScrollBar.Visible = false;
            }
            else
            {
                hScrollBar.Visible = true;
                hScrollBar.Value = (hScrollBar.Maximum + 1 - 
                    hScrollBar.LargeChange - hScrollBar.Minimum) / 2;
            }

            if (imgHeight < panHeight)
            {
                vScrollBar.Visible = false;
            }
            else
            {
                vScrollBar.Visible = true;
                vScrollBar.Value = (vScrollBar.Maximum + 1 - 
                    vScrollBar.LargeChange - vScrollBar.Minimum) / 2;
            }
        }

        private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            int val = vScrollBar.Value;
            vOffset = (panHeight - imgHeight) * (val - vScrollBar.Minimum) /
                    (vMax- vScrollBar.Minimum);
            Invalidate();
        }

        private void hScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            int val = hScrollBar.Value;
            hOffset = (panWidth - imgWidth) * (val - hScrollBar.Minimum) /
                (hMax - hScrollBar.Minimum);
            Invalidate();
        }

        private void ImagePanel_Paint(object sender, PaintEventArgs e)
        {
            if (bpp == ImageBitsPerPixel.Eight)
            {
                if (pix8.Count > 0)
                {
                    Graphics g = Graphics.FromHwnd(panel.Handle);
                    if (newImage == true)
                    {
                        g.Clear(SystemColors.Control);
                        newImage = false;
                    }

                    g.DrawImage(bmp, hOffset, vOffset);
                    g.Dispose();
                }
            }
            else if (bpp == ImageBitsPerPixel.Sixteen)
            {
                if (pix16.Count > 0)
                {
                    Graphics g = Graphics.FromHwnd(panel.Handle);
                    if (newImage == true)
                    {
                        g.Clear(SystemColors.Control);
                        newImage = false;
                    }

                    g.DrawImage(bmp, hOffset, vOffset);
                    g.Dispose();
                }
            }
            else //if (bpp == ImageBitsPerPixel.TwentyFour)  // 30 July 2010
            {
                if (pix24.Count > 0)
                {
                    Graphics g = Graphics.FromHwnd(panel.Handle);
                    if (newImage == true)
                    {
                        g.Clear(SystemColors.Control);
                        newImage = false;
                    }

                    g.DrawImage(bmp, hOffset, vOffset);
                    g.Dispose();
                }
            }
        }

        public void SaveImage(String fileName)
        {
            if( bmp != null )
                bmp.Save(fileName, ImageFormat.Png);
        }

        // We use the linear interpolation method here
        //  Nonlinear methods like sigmoid are also common, but we don't do them here.
        private void ComputeLookUpTable8()
        {
            if (winMax == 0)
                winMax = 255;

            int range = winMax - winMin;
            if (range < 1) range = 1;
            double factor = 255.0 / range;

            for (int i = 0; i < 256; ++i)
            {
                if (i <= winMin)
                    lut8[i] = 0;
                else if (i >= winMax)
                    lut8[i] = 255;
                else
                {
                    lut8[i] = (byte)((i - winMin) * factor);
                }
            }
        }

        // Linear interpolation here too
        private void ComputeLookUpTable16()
        {
            int range = winMax - winMin;
            if (range < 1) range = 1;
            double factor = 255.0 / range;
            int i;

            for (i = 0; i < 65536; ++i)
            {
                if (i <= winMin)
                    lut16[i] = 0;
                else if (i >= winMax)
                    lut16[i] = 255;
                else
                {
                    lut16[i] = (byte)((i - winMin) * factor);
                }
            }
        }
       
        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (imageAvailable == true)
            {
                if (e.Button == MouseButtons.Right)
                {
                    ptWLDown.X = e.X;
                    ptWLDown.Y = e.Y;
                    rightMouseDown = true;
                    Cursor = Cursors.Hand;
                }
            }
        }

        // Mouse-move is made to perform window-level
        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (rightMouseDown == true)
            {
				winShr1 = winWidth >> 1;
                winWidth = winMax - winMin;
                winCentre = winMin + winShr1;

                deltaX = Convert.ToInt32((ptWLDown.X - e.X) * changeValWidth);
                deltaY = Convert.ToInt32((ptWLDown.Y - e.Y) * changeValCentre);

                winCentre -= deltaY;
                winWidth -= deltaX;
                
                if (winWidth < 2) winWidth = 2;
                //if (winWidth > 5000) winWidth = 5000;
                //if (winCentre < -2500) winCentre = -2500;
                //if (winCentre > 2500) winCentre = 2500;
                
                winMin = winCentre - winShr1;
                winMax = winCentre + winShr1;

                if (winMin >= winMax) winMin = winMax - 1;
                if (winMax <= winMin) winMax = winMin + 1;

                ptWLDown.X = e.X;
                ptWLDown.Y = e.Y;

                UpdateMainForm();
                if (bpp == ImageBitsPerPixel.Eight)
                {
                    ComputeLookUpTable8();
                    CreateImage8();
                }
                else if (bpp == ImageBitsPerPixel.Sixteen)
                {
                    ComputeLookUpTable16();
                    CreateImage16();
                }
                else // (bpp == ImageBitsPerPixel.TwentyFour)
                {
                    ComputeLookUpTable8();
                    CreateImage24();
                }

                Invalidate();
            }
        }

        private void panel_MouseUp(object sender, MouseEventArgs e)
        {
            if (rightMouseDown == true)
            {
                rightMouseDown = false;
                Cursor = Cursors.Default;
            }
        }

        // Update the graph control on the main form
        private void UpdateMainForm()
        {
            mf.UpdateWindowLevel(winWidth, winCentre, bpp);
        }

        // Restore original window/level values
        public void ResetValues()
        {
            winMax = Convert.ToInt32(winCentre + 0.5 * winWidth);
            winMin = winMax - winWidth;
            UpdateMainForm();
        }

        private void ImagePanelControl_Resize(object sender, EventArgs e)
        {
            PerformResize();
        }

        private void PerformResize()
        {
            panel.Location = new Point(3, 3);
            panel.Width = ClientRectangle.Width - 24;
            panel.Height = ClientRectangle.Height - 24;

            vScrollBar.Location = new Point(ClientRectangle.Width - 19, 3);
            vScrollBar.Height = panel.Height;

            hScrollBar.Location = new Point(3, ClientRectangle.Height - 19);
            hScrollBar.Width = panel.Width;

            hMax = hScrollBar.Maximum - hScrollBar.LargeChange + hScrollBar.SmallChange;
            vMax = vScrollBar.Maximum - vScrollBar.LargeChange + vScrollBar.SmallChange;
        }
    }
}
