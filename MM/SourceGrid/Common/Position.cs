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

namespace SourceGrid2
{
	/// <summary>
	/// Represents a cell position (Row, Col). Once created connot be modified
	/// </summary>
	public struct Position
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="p_Row"></param>
		/// <param name="p_Col"></param>
		public Position(int p_Row, int p_Col)
		{
			m_Row = p_Row;
			m_Col = p_Col;
		}

		static Position()
		{
			Empty = new Position(c_EmptyIndex, c_EmptyIndex);
		}

		private int m_Row;

		private int m_Col;

		/// <summary>
		/// Row
		/// </summary>
		public int Row
		{
			get{return m_Row;}
		}
		/// <summary>
		/// Column
		/// </summary>
		public int Column
		{
			get{return m_Col;}
		}

		/// <summary>
		/// Empty position
		/// </summary>
		public readonly static Position Empty;

		/// <summary>
		/// Returns true if the current struct is empty
		/// </summary>
		/// <returns></returns>
		public bool IsEmpty()
		{
			return this.Equals(Empty);
		}

		/// <summary>
		/// An empty index constant
		/// </summary>
		public const int c_EmptyIndex = -1;

		/// <summary>
		/// GetHashCode
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return Row;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_Position"></param>
		/// <returns></returns>
		public bool Equals(Position p_Position)
		{
			return (m_Col == p_Position.m_Col && m_Row == p_Position.m_Row);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			return Equals((Position)obj);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Left"></param>
		/// <param name="Right"></param>
		/// <returns></returns>
		public static bool operator == (Position Left, Position Right)
		{
			return Left.Equals(Right);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Left"></param>
		/// <param name="Right"></param>
		/// <returns></returns>
		public static bool operator != (Position Left, Position Right)
		{
			return !Left.Equals(Right);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return Row.ToString() + ";" + Column.ToString();
		}

		/// <summary>
		/// Returns a position with the smaller Row and the smaller column
		/// </summary>
		/// <param name="p_Position1"></param>
		/// <param name="p_Position2"></param>
		/// <returns></returns>
		public static Position MergeMinor(Position p_Position1, Position p_Position2)
		{
			int l_Row, l_Col;
			if (p_Position1.Row < p_Position2.Row)
				l_Row = p_Position1.Row;
			else
				l_Row = p_Position2.Row;
			if (p_Position1.Column < p_Position2.Column)
				l_Col = p_Position1.Column;
			else
				l_Col = p_Position2.Column;
			return new Position(l_Row, l_Col);
		}
		/// <summary>
		/// Returns a position with the bigger Row and the bigger column
		/// </summary>
		/// <param name="p_Position1"></param>
		/// <param name="p_Position2"></param>
		/// <returns></returns>
		public static Position MergeMajor(Position p_Position1, Position p_Position2)
		{
			int l_Row, l_Col;
			if (p_Position1.Row > p_Position2.Row)
				l_Row = p_Position1.Row;
			else
				l_Row = p_Position2.Row;
			if (p_Position1.Column > p_Position2.Column)
				l_Col = p_Position1.Column;
			else
				l_Col = p_Position2.Column;
			return new Position(l_Row, l_Col);
		}
	}


}
