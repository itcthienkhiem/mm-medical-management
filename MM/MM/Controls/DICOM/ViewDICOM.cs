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
using System.Windows.Forms;

// Program to view simple DICOM images.
// Written by Amarnath S, Mahesh Reddy S, Bangalore, India, April 2009.
// Updated along with Harsha T, April 2010 to include Window/Level
// Updated by Amarnath S, July 2010, to include Ultrasound images of 8-bit depth, 3 samples per pixel (RGB).

// Inspired by ImageJ

namespace DicomImageViewer
{
    public enum ImageBitsPerPixel { Eight, Sixteen, TwentyFour };

    /// <summary>
    /// This program reads in a DICOM file and displays it on the screen. 
    /// The functionality for viewer is:
    /// o Open DICOM files created as per DICOM 3.0 standard
    /// o Open files with Explicit VR and Implicit VR Transfer Syntax
    /// o Read those files where image bit depth is 8 or 16 bits (Digital Radiography), 
    ///    or RGB images (from Ultrasound)
    /// o Read a DICOM file with just one image inside it
    /// o Read a DICONDE file also (a DICONDE file is a DICOM file with NDE - Non Destructive   
    ///    Evaluation - tags inside it)
    /// o Perform Window/Level operations on the image.
    /// 
    /// This viewer is not intended to:
    /// o Check whether all mandatory tags are present
    /// o Open files with VR other than Explicit and Implicit - in particular, not to open 
    ///    JPEG Lossy and Lossless files
    /// o Read old DICOM files - requires the preamble and prefix for sure. Earlier DICOM 
    ///    files dont have the preamble and prefix, and just contain the string 1.2.840.10008 
    ///    somewhere in the beginning. For this viewer, the preamble and prefix are necessary
    /// o Read a sequence of images. 
    /// </summary>
    public partial class ViewDicom : Form
    {
        DicomDecoder dd;
        List<byte> pixels8;
        List<ushort> pixels16;
        List<byte> pixels24; // 30 July 2010
        int imageWidth;
        int imageHeight;
        int bitDepth;
        int samplesPerPixel;  // Updated 30 July 2010
        bool imageOpened;
        double winCentre;
        double winWidth;
        bool signedImage;

        public ViewDicom()
        {
            InitializeComponent();
            dd = new DicomDecoder();
            pixels8 = new List<byte>();
            pixels16 = new List<ushort>();
            pixels24 = new List<byte>();
            imageOpened = false;
            signedImage = false;
        }

        private void bnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All DICOM Files(*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (ofd.FileName.Length > 0)
                {
                    Cursor = Cursors.WaitCursor;
                    ReadAndDisplayDicomFile(ofd.FileName, ofd.SafeFileName);
                    imageOpened = true;
                    Cursor = Cursors.Default;
                }
                ofd.Dispose();
            }
        }

        private void ReadAndDisplayDicomFile(string fileName, string fileNameOnly)
        {
            dd.DicomFileName = fileName;
            bool result = dd.dicomFileReadSuccess;
            if (result == true)
            {
                imageWidth = dd.width;
                imageHeight = dd.height;
                bitDepth = dd.bitsAllocated;
                winCentre = dd.windowCentre;
                winWidth = dd.windowWidth;
                samplesPerPixel = dd.samplesPerPixel;
                signedImage = dd.signedImage;

                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                bnSave.Enabled = true;
                bnTags.Enabled = true;
                bnResetWL.Enabled = true;
                label2.Text = imageWidth.ToString() + " X " + imageHeight.ToString();
                if (samplesPerPixel == 1)
                    label4.Text = bitDepth.ToString() + " bit";
                else
                    label4.Text = bitDepth.ToString() + " bit, " + samplesPerPixel +
                        " samples per pixel";

                imagePanelControl.NewImage = true;
                Text = "DICOM Image Viewer: " + fileNameOnly;

                if (samplesPerPixel == 1 && bitDepth == 8)
                {
                    pixels8.Clear();
                    pixels16.Clear();
                    pixels24.Clear();
                    dd.GetPixels8(ref pixels8);

                    if (winCentre == 0 && winWidth == 0)
                    {
                        winWidth = 256;
                        winCentre = 128;
                    }

                    imagePanelControl.SetParameters(ref pixels8, imageWidth, imageHeight,
                        winWidth, winCentre, samplesPerPixel, true, this);
                }

                if (samplesPerPixel == 1 && bitDepth == 16)
                {
                    pixels16.Clear();
                    pixels8.Clear();
                    pixels24.Clear();
                    dd.GetPixels16(ref pixels16);
                    
                    // This is primarily for debugging purposes, 
                    //  to view the pixel values as ascii data.
                    //int ij = 2;
                    //if (ij == 2)
                    //{
                    //    System.IO.StreamWriter file = new System.IO.StreamWriter(
                    //               "c:\\Amar\\imageSigned.txt");

                    //    for (int ik = 0; ik < pixels16.Count; ++ik)
                    //        file.Write(pixels16[ik] + "  ");

                    //    file.Close();
                    //}

                    if (winCentre == 0 && winWidth == 0)
                    {
                        winWidth = 65536;
                        winCentre = 32768;
                    }

                    imagePanelControl.Signed16Image = dd.signedImage;
                    imagePanelControl.SetParameters(ref pixels16, imageWidth, imageHeight,
                        winWidth, winCentre, true, this);
                }

                if (samplesPerPixel == 3 && bitDepth == 8)
                {
                    // This is an RGB colour image
                    pixels8.Clear();
                    pixels16.Clear();
                    pixels24.Clear();
                    dd.GetPixels24(ref pixels24);

                    // This code segment is primarily for debugging purposes, 
                    //    to view the pixel values as ascii data.
                    //int ij = 2;
                    //if (ij == 2)
                    //{
                    //    System.IO.StreamWriter file = new System.IO.StreamWriter(
                    //                      "c:\\Amar\\image24.txt");

                    //    for (int ik = 0; ik < pixels24.Count; ++ik)
                    //        file.Write(pixels24[ik] + "  ");

                    //    file.Close();
                    //}

                    imagePanelControl.SetParameters(ref pixels24, imageWidth, imageHeight,
                        winWidth, winCentre, samplesPerPixel, true, this);                    
                }
            }
            else
            {
                if (dd.dicmFound == false)
                {
                    MessageBox.Show("This does not seem to be a DICOM 3.0 file. Sorry, I can't open this.");
                }
                else if (dd.dicomDir == true)
                {
                    MessageBox.Show("This seems to be a DICOMDIR file, and does not contain an image.");
                }
                else
                {
                    MessageBox.Show("Sorry, I can't read a DICOM file with this Transfer Syntax\n" +
                        "You may view the initial tags instead.");
                }

                // Show a plain grayscale image instead
                pixels8.Clear();
                pixels16.Clear();
                pixels24.Clear();
                samplesPerPixel = 1;

                imageWidth = imagePanelControl.Width - 25;   // 25 is a magic number
                imageHeight = imagePanelControl.Height - 25; // Same magic number
                int iNoPix = imageWidth * imageHeight;

                for (int i = 0; i < iNoPix; ++i)
                {
                    pixels8.Add(240);// 240 is the grayvalue corresponding to the Control colour
                }
                winWidth = 256;
                winCentre = 127;
                imagePanelControl.SetParameters(ref pixels8, imageWidth, imageHeight,
                    winWidth, winCentre, samplesPerPixel, true, this);
                imagePanelControl.Invalidate();
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                bnSave.Enabled = false;
                bnTags.Enabled = false;
                bnResetWL.Enabled = false;
            }
        }

        private void bnTags_Click(object sender, EventArgs e)
        {
            if (imageOpened == true)
            {
                List<string> str = dd.dicomInfo;

                DicomTagsForm dtg = new DicomTagsForm();
                dtg.SetString(ref str);
                dtg.ShowDialog();

                imagePanelControl.Invalidate();
            }
            else
                MessageBox.Show("Load a DICOM file before viewing tags!");
        }

        private void bnSave_Click(object sender, EventArgs e)
        {
            if (imageOpened == true)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PNG Files(*.png)|*.png";

                if (sfd.ShowDialog() == DialogResult.OK)
                    imagePanelControl.SaveImage(sfd.FileName);
            }
            else
                MessageBox.Show("Load a DICOM file before saving!");

            imagePanelControl.Invalidate();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            pixels8.Clear();
            pixels16.Clear();
            imagePanelControl.Dispose();
        }

        private void bnResetWL_Click(object sender, EventArgs e)
        {
            if ((pixels8.Count > 0) || (pixels16.Count > 0) || (pixels24.Count > 0))
            {
                imagePanelControl.ResetValues();
                if (bitDepth == 8)
                {
                    if (samplesPerPixel == 1)
                        imagePanelControl.SetParameters(ref pixels8, imageWidth, imageHeight,
                            winWidth, winCentre, samplesPerPixel, false, this);
                    else // samplesPerPixel == 3
                        imagePanelControl.SetParameters(ref pixels24, imageWidth, imageHeight,
                        winWidth, winCentre, samplesPerPixel, false, this);
                }

                if (bitDepth == 16)
                    imagePanelControl.SetParameters(ref pixels16, imageWidth, imageHeight,
                        winWidth, winCentre, false, this);
            }
            else
                MessageBox.Show("Load a DICOM file before resetting!");
        }

        public void UpdateWindowLevel(int winWidth, int winCentre, ImageBitsPerPixel bpp)
        {
            int winMin = Convert.ToInt32(winCentre - 0.5 * winWidth);
            int winMax = winMin + winWidth;
            //this.windowLevelControl.SetWindowWidthCentre(winMin, winMax, winWidth, winCentre, bpp, signedImage);
        }
    }
}
