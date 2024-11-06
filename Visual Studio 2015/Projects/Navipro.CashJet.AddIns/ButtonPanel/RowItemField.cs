using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.CashJet.AddIns
{
    public class RowItemField
    {
        private string _name;
        private string _text;
        private int _posX;
        private int _posY;
        private string _relative;
        private string _fontName;
        private int _fontSize;
        private string _fontStyle;
        private int _width;
        private int _height;
        private string _textAlign;
        private string _anchor;
        private string _foreColor;
        private string _backColor;

        public RowItemField() { }

        public string name { get { return _name; } set { _name = value; } }
        public string text { get { return _text; } set { _text = value; } }
        public int posX { get { return _posX; } set { _posX = value; } }
        public int posY { get { return _posY; } set { _posY = value; } }
        public string relative { get { return _relative; } set { _relative = value; } }
        public string fontName { get { return _fontName; } set { _fontName = value; } }
        public int fontSize { get { return _fontSize; } set { _fontSize = value; } }
        public string fontStyle { get { return _fontStyle; } set { _fontStyle = value; } }
        public int width { get { return _width; } set { _width = value; } }
        public int height { get { return _height; } set { _height = value; } }
        public string textAlign { get { return _textAlign; } set { _textAlign = value; } }
        public string anchor { get { return _anchor; } set { _anchor = value; } }
        public string foreColor { get { return _foreColor; } set { _foreColor = value; } }
        public string backColor { get { return _backColor; } set { _backColor = value; } }
    }




}
