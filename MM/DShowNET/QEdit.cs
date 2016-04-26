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
/******************************************************
                  DirectShow .NET
		      netmaster@swissonline.ch
*******************************************************/
//					QEdit
// Extended streaming interfaces, ported from qedit.idl

using System;
using System.Runtime.InteropServices;

namespace DShowNET
{



	[ComVisible(true), ComImport,
	Guid("6B652FFF-11FE-4fce-92AD-0266B5D7C78F"),
	InterfaceType( ComInterfaceType.InterfaceIsIUnknown )]
public interface ISampleGrabber
{
		[PreserveSig]
	int SetOneShot(
		[In, MarshalAs(UnmanagedType.Bool)]				bool	OneShot );

		[PreserveSig]
	int SetMediaType(
		[In, MarshalAs(UnmanagedType.LPStruct)]			AMMediaType	pmt );

		[PreserveSig]
	int GetConnectedMediaType(
		[Out, MarshalAs(UnmanagedType.LPStruct)]		AMMediaType	pmt );

		[PreserveSig]
	int SetBufferSamples(
		[In, MarshalAs(UnmanagedType.Bool)]				bool	BufferThem );

		[PreserveSig]
	int GetCurrentBuffer( ref int pBufferSize, IntPtr pBuffer );

		[PreserveSig]
	int GetCurrentSample( IntPtr ppSample );

		[PreserveSig]
	int SetCallback( ISampleGrabberCB pCallback, int WhichMethodToCallback );
}



	[ComVisible(true), ComImport,
	Guid("0579154A-2B53-4994-B0D0-E773148EFF85"),
	InterfaceType( ComInterfaceType.InterfaceIsIUnknown )]
public interface ISampleGrabberCB
{
		[PreserveSig]
	int SampleCB( double SampleTime, IMediaSample pSample );

		[PreserveSig]
	int BufferCB( double SampleTime, IntPtr pBuffer, int BufferLen );
}



	[StructLayout(LayoutKind.Sequential), ComVisible(false)]
public class VideoInfoHeader		// VIDEOINFOHEADER
{
	public DsRECT	SrcRect;
	public DsRECT	TagRect;
	public int		BitRate;
	public int		BitErrorRate;
	public long					AvgTimePerFrame;
	public DsBITMAPINFOHEADER	BmiHeader;
}


} // namespace DShowNET
