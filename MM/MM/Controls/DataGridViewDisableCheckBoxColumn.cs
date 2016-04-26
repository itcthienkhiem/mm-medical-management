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
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace MM.Controls
{
    public class DataGridViewDisableCheckBoxColumn : DataGridViewCheckBoxColumn
    {
        public DataGridViewDisableCheckBoxColumn()
        {
            this.CellTemplate = new DataGridViewDisableCheckBoxCell();
        }
    }

    public class DataGridViewDisableCheckBoxCell : DataGridViewCheckBoxCell
    {
        private bool enabledValue = true;

        /// <summary>
        /// This property decides whether the checkbox should be shown 
        /// checked or unchecked.
        /// </summary>

        public bool Enabled
        {
            get
            {
                return enabledValue;
            }
            set
            {
                enabledValue = value;
            }
        }

        /// Override the Clone method so that the Enabled property is copied.

        public override object Clone()
        {
            DataGridViewDisableCheckBoxCell cell =
                (DataGridViewDisableCheckBoxCell)base.Clone();
            cell.Enabled = this.Enabled;
            return cell;
        }

        protected override void OnContentClick(DataGridViewCellEventArgs e)
        {
            if (this.enabledValue)
                base.OnContentClick(e);
        }

        protected override void OnContentDoubleClick(DataGridViewCellEventArgs e)
        {
            if (this.enabledValue)
                base.OnContentDoubleClick(e);
        }

        protected override void Paint
        (Graphics graphics, Rectangle clipBounds, Rectangle cellBounds,
                int rowIndex, DataGridViewElementStates elementState, object value,
                object formattedValue, string errorText, DataGridViewCellStyle cellStyle,
                DataGridViewAdvancedBorderStyle advancedBorderStyle,
                    DataGridViewPaintParts paintParts)
        {

            SolidBrush cellBackground = new SolidBrush(cellStyle.BackColor);
            graphics.FillRectangle(cellBackground, cellBounds);
            cellBackground.Dispose();
            PaintBorder(graphics, clipBounds, cellBounds,
            cellStyle, advancedBorderStyle);
            Rectangle checkBoxArea = cellBounds;
            Rectangle buttonAdjustment = this.BorderWidths(advancedBorderStyle);
            checkBoxArea.X += buttonAdjustment.X;
            checkBoxArea.Y += buttonAdjustment.Y;

            checkBoxArea.Height -= buttonAdjustment.Height;
            checkBoxArea.Width -= buttonAdjustment.Width;
            Point drawInPoint = new Point(cellBounds.X + cellBounds.Width / 2 - 7,
                cellBounds.Y + cellBounds.Height / 2 - 7);

            if (Convert.ToBoolean(value) == true)
            {
                if (this.enabledValue)
                    base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
                else
                    CheckBoxRenderer.DrawCheckBox(graphics, drawInPoint, System.Windows.Forms.VisualStyles.CheckBoxState.CheckedDisabled);
            }
            else
            {
                if (this.enabledValue)
                    base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
                else
                    CheckBoxRenderer.DrawCheckBox(graphics, drawInPoint, System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedDisabled);
            }
        }
    } 
}
