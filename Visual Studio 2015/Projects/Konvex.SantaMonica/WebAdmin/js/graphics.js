/**
 * Copyright (C) 2002-2005
 * W3L GmbH
 * Technologiezentrum Ruhr
 * Universit‰tsstraﬂe 142
 * D-44799 Bochum
 * 
 * Author: Dipl.Ing. Doga Arinir
 * E-Mail: doga.arinir@w3l.de
 *
 * This software is provided 'as-is', without any express or implied
 * warranty.  In no event will the author or the company be held liable 
 * for any damages arising from the use of this software. EXPECT BUGS!
 * 
 * You may use this software in compiled form in any way you desire PROVIDING it is
 * not sold for profit without the authors written permission, and providing that this
 * notice and the authors name is included. If the source code in this file is used in 
 * any commercial application then acknowledgement must be made to the author of this file.
 */
function checkbrowser()
{
	this.b = document.body;
	this.dom = document.getElementById ? 1:0;
	this.ie = this.b && typeof this.b.insertAdjacentHTML != 'undefined';
	this.mozilla = typeof(document.createRange) != 'undefined' && typeof((document.createRange()).setStartBefore) != 'undefined';
}

function Graphics(container)
{
	this.bw = new checkbrowser();
	this.color = 'green';
	this.backbuffer = '';
	//Drawing to div-element or to the document itself?
	if (typeof(container) == 'string' && container != '' && typeof(document.getElementById(container)) != 'undefined')
	{
		this.container = document.getElementById(container);
		
		this.clear = function() {this.container.innerHTML = "";this.backbuffer = '';}
		var paint_ie = function()
		{
			this.container.insertAdjacentHTML("BeforeEnd", this.backbuffer);
			this.backbuffer = '';
		}
		var paint_moz = function()
		{
			var r = document.createRange();
			r.setStartBefore(this.container);
			this.container.appendChild(r.createContextualFragment(this.backbuffer));
			this.backbuffer = '';
		}		
		this.paint = this.bw.ie ? paint_ie : paint_moz;
	}
	else
	{
		this.clear = function() {this.backbuffer = '';}
		this.paint = function() 
		{
			document.write(this.backbuffer);
			this.backbuffer = '';
		}
	}	
}

Graphics.prototype.setPixel = function(x, y, w, h)
{
	this.backbuffer += '<div style="position:absolute;left:'+x+'px;top:'+y+'px;width:'+w+'px;height:'+h+'px;background-color:'+this.color+';overflow:hidden;"></div>';
}

Graphics.prototype.drawLine = function(x1, y1, x2, y2)
{
	//Always draw from left to right. This makes the algorithm much easier...
	if (x1 > x2)	{
		var tmpx = x1; var tmpy = y1;
		x1 = x2; y1 = y2;
		x2 = tmpx; y2 = tmpy;
	}
	
	var dx = x2 - x1;	
	var dy = y2 - y1; var sy = 1;	
	if (dy < 0)	{
		sy = -1;
		dy = -dy;
	}
	
	dx = dx << 1; dy = dy << 1;
	if (dy <= dx)
	{	
		var fraction = dy - (dx>>1);
		var mx = x1;
		while (x1 != x2)
		{
			x1++;
			if (fraction >= 0)
			{
				this.setPixel(mx, y1,x1-mx,2);
				y1 += sy;
				mx = x1;
				fraction -= dx;
			}
			fraction += dy;
		}
		this.setPixel(mx,y1,x1-mx,2);
	} 
	else 
	{
		var fraction = dx - (dy>>1);
		var my = y1;
		if (sy > 0)
		{		
			while (y1 != y2)
			{
				y1++;
				if (fraction >= 0)
				{
					this.setPixel(x1++, my,2,y1-my);
					my = y1;
					fraction -= dy;
				}
				fraction += dx;
			}	
			this.setPixel(x1,my,1,y1-my);
		}
		else
		{
			while (y1 != y2)
			{
				y1--;
				if (fraction >= 0)
				{
					this.setPixel(x1++, y1,2,my-y1);
					my = y1;
					fraction -= dy;
				}
				fraction += dx;
			}	
			this.setPixel(x1,y1,2,my-y1);		
		}
	}
}
