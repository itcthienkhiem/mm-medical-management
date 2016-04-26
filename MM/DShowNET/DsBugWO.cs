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
//           WORKAROUND FOR DS BUGs

using System;
using System.Text;
using System.Runtime.InteropServices;

namespace DShowNET
{

public class DsBugWO
{
	/*
	works:
		CoCreateInstance( CLSID_CaptureGraphBuilder2, ..., IID_ICaptureGraphBuilder2, ...);
	doesn't (E_NOTIMPL):
		CoCreateInstance( CLSID_CaptureGraphBuilder2, ..., IID_IUnknown, ...);
	thus .NET 'Activator.CreateInstance' fails
	*/

	public static object CreateDsInstance( ref Guid clsid, ref Guid riid )
	{
		IntPtr ptrIf;
		int hr = CoCreateInstance( ref clsid, IntPtr.Zero, CLSCTX.Inproc, ref riid, out ptrIf );
		if( (hr != 0) || (ptrIf == IntPtr.Zero) )
			Marshal.ThrowExceptionForHR( hr );

		Guid iu = new Guid( "00000000-0000-0000-C000-000000000046" );
		IntPtr ptrXX;
		hr = Marshal.QueryInterface( ptrIf, ref iu, out ptrXX );

		object ooo = System.Runtime.Remoting.Services.EnterpriseServicesHelper.WrapIUnknownWithComObject( ptrIf );
		int ct = Marshal.Release( ptrIf );
		return ooo;
	}

	[DllImport("ole32.dll") ]
	private static extern int CoCreateInstance(	ref Guid clsid, IntPtr pUnkOuter, CLSCTX dwClsContext, ref Guid iid, out IntPtr ptrIf );
}

[Flags]
internal enum CLSCTX
	{
	Inproc					= 0x03,
	Server					= 0x15,
	All						= 0x17,
	}


} // namespace DShowNET
