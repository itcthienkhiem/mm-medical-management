This library is built based on the following documents, thanks to their authors.

Compound Document file format
http://sc.openoffice.org/compdocfileformat.pdf

Excel file format 
http://sc.openoffice.org/excelfileformat.pdf

Microsoft Office 97 Drawing File Format
http://chicago.sourceforge.net/devel/docs/escher/

Record structures in BIFF8/BIFF8X format are implemented.

It can read read worksheets in a workbook and read cells in a worksheet.
It can read cell content(text,number,datetime or error) and 
cell format(font,alignment,linestyle,background,etc).
It can read pictures in the file, get informations of image size, position,
data and format.

Liu Junfeng 2006-11-02

Update notes:
2007-5-17
display each Sheet in separate tabpage		
fixed some bugs	in FORMULA.cs and Form1.cs

2007-5-26
decode FORMULAR result