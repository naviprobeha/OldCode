using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.CashJet.AddIns
{
    public class RowItem
    {
        private string _id;
        private RowItemFieldCollection _fields;
        private int _height;

        public RowItem() 
        {
            _fields = new RowItemFieldCollection();
        }

        public RowItemFieldCollection fields { get { return _fields; } set { _fields = value; } }
        public string id { get { return _id; } set { _id = value; } }
        public int height { get { return _height; } set { _height = value; } }

        public void createField(string name, string text, int posX, int posY, string relative, string fontName, int fontSize, string fontStyle, int width, int height, string textAlign, string anchor, string foreColor, string backColor) 
        {
            RowItemField rowItemField = new RowItemField();
            rowItemField.name = name;
            rowItemField.text = text;
            rowItemField.posX = posX;
            rowItemField.posY = posY;
            rowItemField.relative = relative;
            rowItemField.fontName = fontName;
            rowItemField.fontSize = fontSize;
            rowItemField.fontStyle = fontStyle;
            rowItemField.width = width;
            rowItemField.height = height;
            rowItemField.textAlign = textAlign;
            rowItemField.anchor = anchor;
            rowItemField.foreColor = foreColor;
            rowItemField.backColor = backColor;

            _fields.Add(rowItemField);
        }
    }




}
