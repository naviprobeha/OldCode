using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for Keyboard.
	/// </summary>
	public class Keyboard : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Button button22;
		private System.Windows.Forms.Button button23;
		private System.Windows.Forms.Button button24;
		private System.Windows.Forms.Button button25;
		private System.Windows.Forms.Button button26;
		private System.Windows.Forms.Button button27;
		private System.Windows.Forms.Button button28;
		private System.Windows.Forms.Button button15;
		private System.Windows.Forms.Button button16;
		private System.Windows.Forms.Button button17;
		private System.Windows.Forms.Button button18;
		private System.Windows.Forms.Button button19;
		private System.Windows.Forms.Button button20;
		private System.Windows.Forms.Button button21;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.Button button9;
		private System.Windows.Forms.Button button10;
		private System.Windows.Forms.Button button11;
		private System.Windows.Forms.Button button12;
		private System.Windows.Forms.Button button13;
		private System.Windows.Forms.Button button14;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button29;
		private System.Windows.Forms.Button button30;
		private System.Windows.Forms.TextBox inputBox;
		private System.Windows.Forms.TextBox inputBox2;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Button button31;
		private System.Windows.Forms.Button button32;
		private System.Windows.Forms.Button button33;
		private System.Windows.Forms.Button button34;
		private System.Windows.Forms.Button button35;
		private System.Windows.Forms.Button button36;
		private System.Windows.Forms.Button button37;
		private System.Windows.Forms.Button button38;
		private System.Windows.Forms.Button button39;
		private System.Windows.Forms.Button button40;
		private System.Windows.Forms.Button button41;
		private System.Windows.Forms.Button button42;
		private System.Windows.Forms.Button button43;
		private System.Windows.Forms.Button button44;
	
		private string inputString;
		private System.Windows.Forms.Button button45;
		private int maxLength;

		public Keyboard(int maxLength)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			inputString = "";
			this.maxLength = maxLength;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.button31 = new System.Windows.Forms.Button();
			this.button30 = new System.Windows.Forms.Button();
			this.button29 = new System.Windows.Forms.Button();
			this.inputBox = new System.Windows.Forms.TextBox();
			this.button22 = new System.Windows.Forms.Button();
			this.button23 = new System.Windows.Forms.Button();
			this.button24 = new System.Windows.Forms.Button();
			this.button25 = new System.Windows.Forms.Button();
			this.button26 = new System.Windows.Forms.Button();
			this.button27 = new System.Windows.Forms.Button();
			this.button28 = new System.Windows.Forms.Button();
			this.button15 = new System.Windows.Forms.Button();
			this.button16 = new System.Windows.Forms.Button();
			this.button17 = new System.Windows.Forms.Button();
			this.button18 = new System.Windows.Forms.Button();
			this.button19 = new System.Windows.Forms.Button();
			this.button20 = new System.Windows.Forms.Button();
			this.button21 = new System.Windows.Forms.Button();
			this.button8 = new System.Windows.Forms.Button();
			this.button9 = new System.Windows.Forms.Button();
			this.button10 = new System.Windows.Forms.Button();
			this.button11 = new System.Windows.Forms.Button();
			this.button12 = new System.Windows.Forms.Button();
			this.button13 = new System.Windows.Forms.Button();
			this.button14 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.button44 = new System.Windows.Forms.Button();
			this.button43 = new System.Windows.Forms.Button();
			this.button42 = new System.Windows.Forms.Button();
			this.button41 = new System.Windows.Forms.Button();
			this.button40 = new System.Windows.Forms.Button();
			this.button39 = new System.Windows.Forms.Button();
			this.button38 = new System.Windows.Forms.Button();
			this.button37 = new System.Windows.Forms.Button();
			this.button36 = new System.Windows.Forms.Button();
			this.button35 = new System.Windows.Forms.Button();
			this.button34 = new System.Windows.Forms.Button();
			this.button33 = new System.Windows.Forms.Button();
			this.button32 = new System.Windows.Forms.Button();
			this.inputBox2 = new System.Windows.Forms.TextBox();
			this.button45 = new System.Windows.Forms.Button();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(322, 214);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.button45);
			this.tabPage1.Controls.Add(this.button31);
			this.tabPage1.Controls.Add(this.button30);
			this.tabPage1.Controls.Add(this.button29);
			this.tabPage1.Controls.Add(this.inputBox);
			this.tabPage1.Controls.Add(this.button22);
			this.tabPage1.Controls.Add(this.button23);
			this.tabPage1.Controls.Add(this.button24);
			this.tabPage1.Controls.Add(this.button25);
			this.tabPage1.Controls.Add(this.button26);
			this.tabPage1.Controls.Add(this.button27);
			this.tabPage1.Controls.Add(this.button28);
			this.tabPage1.Controls.Add(this.button15);
			this.tabPage1.Controls.Add(this.button16);
			this.tabPage1.Controls.Add(this.button17);
			this.tabPage1.Controls.Add(this.button18);
			this.tabPage1.Controls.Add(this.button19);
			this.tabPage1.Controls.Add(this.button20);
			this.tabPage1.Controls.Add(this.button21);
			this.tabPage1.Controls.Add(this.button8);
			this.tabPage1.Controls.Add(this.button9);
			this.tabPage1.Controls.Add(this.button10);
			this.tabPage1.Controls.Add(this.button11);
			this.tabPage1.Controls.Add(this.button12);
			this.tabPage1.Controls.Add(this.button13);
			this.tabPage1.Controls.Add(this.button14);
			this.tabPage1.Controls.Add(this.button7);
			this.tabPage1.Controls.Add(this.button6);
			this.tabPage1.Controls.Add(this.button5);
			this.tabPage1.Controls.Add(this.button4);
			this.tabPage1.Controls.Add(this.button3);
			this.tabPage1.Controls.Add(this.button2);
			this.tabPage1.Controls.Add(this.button1);
			this.tabPage1.Location = new System.Drawing.Point(4, 4);
			this.tabPage1.Size = new System.Drawing.Size(314, 188);
			this.tabPage1.Text = "Bokstäver";
			// 
			// button31
			// 
			this.button31.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button31.Location = new System.Drawing.Point(252, 4);
			this.button31.Size = new System.Drawing.Size(64, 24);
			this.button31.Text = "BS";
			this.button31.Click += new System.EventHandler(this.button31_Click);
			// 
			// button30
			// 
			this.button30.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button30.Location = new System.Drawing.Point(216, 152);
			this.button30.Size = new System.Drawing.Size(40, 32);
			this.button30.Text = "SP";
			this.button30.Click += new System.EventHandler(this.button30_Click);
			// 
			// button29
			// 
			this.button29.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button29.Location = new System.Drawing.Point(264, 152);
			this.button29.Size = new System.Drawing.Size(52, 32);
			this.button29.Text = "OK";
			this.button29.Click += new System.EventHandler(this.button29_Click);
			// 
			// inputBox
			// 
			this.inputBox.Location = new System.Drawing.Point(4, 8);
			this.inputBox.Size = new System.Drawing.Size(240, 20);
			this.inputBox.Text = "";
			// 
			// button22
			// 
			this.button22.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button22.Location = new System.Drawing.Point(164, 152);
			this.button22.Size = new System.Drawing.Size(32, 32);
			this.button22.Text = "Ö";
			this.button22.Click += new System.EventHandler(this.button22_Click);
			// 
			// button23
			// 
			this.button23.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button23.Location = new System.Drawing.Point(124, 152);
			this.button23.Size = new System.Drawing.Size(32, 32);
			this.button23.Text = "Ä";
			this.button23.Click += new System.EventHandler(this.button23_Click);
			// 
			// button24
			// 
			this.button24.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button24.Location = new System.Drawing.Point(84, 152);
			this.button24.Size = new System.Drawing.Size(32, 32);
			this.button24.Text = "Å";
			this.button24.Click += new System.EventHandler(this.button24_Click);
			// 
			// button25
			// 
			this.button25.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button25.Location = new System.Drawing.Point(44, 152);
			this.button25.Size = new System.Drawing.Size(32, 32);
			this.button25.Text = "Z";
			this.button25.Click += new System.EventHandler(this.button25_Click);
			// 
			// button26
			// 
			this.button26.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button26.Location = new System.Drawing.Point(4, 152);
			this.button26.Size = new System.Drawing.Size(32, 32);
			this.button26.Text = "Y";
			this.button26.Click += new System.EventHandler(this.button26_Click);
			// 
			// button27
			// 
			this.button27.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button27.Location = new System.Drawing.Point(284, 112);
			this.button27.Size = new System.Drawing.Size(32, 32);
			this.button27.Text = "X";
			this.button27.Click += new System.EventHandler(this.button27_Click);
			// 
			// button28
			// 
			this.button28.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button28.Location = new System.Drawing.Point(204, 112);
			this.button28.Size = new System.Drawing.Size(32, 32);
			this.button28.Text = "V";
			this.button28.Click += new System.EventHandler(this.button28_Click);
			// 
			// button15
			// 
			this.button15.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button15.Location = new System.Drawing.Point(164, 112);
			this.button15.Size = new System.Drawing.Size(32, 32);
			this.button15.Text = "U";
			this.button15.Click += new System.EventHandler(this.button15_Click);
			// 
			// button16
			// 
			this.button16.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button16.Location = new System.Drawing.Point(124, 112);
			this.button16.Size = new System.Drawing.Size(32, 32);
			this.button16.Text = "T";
			this.button16.Click += new System.EventHandler(this.button16_Click);
			// 
			// button17
			// 
			this.button17.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button17.Location = new System.Drawing.Point(84, 112);
			this.button17.Size = new System.Drawing.Size(32, 32);
			this.button17.Text = "S";
			this.button17.Click += new System.EventHandler(this.button17_Click);
			// 
			// button18
			// 
			this.button18.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button18.Location = new System.Drawing.Point(44, 112);
			this.button18.Size = new System.Drawing.Size(32, 32);
			this.button18.Text = "R";
			this.button18.Click += new System.EventHandler(this.button18_Click);
			// 
			// button19
			// 
			this.button19.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button19.Location = new System.Drawing.Point(4, 112);
			this.button19.Size = new System.Drawing.Size(32, 32);
			this.button19.Text = "Q";
			this.button19.Click += new System.EventHandler(this.button19_Click);
			// 
			// button20
			// 
			this.button20.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button20.Location = new System.Drawing.Point(284, 72);
			this.button20.Size = new System.Drawing.Size(32, 32);
			this.button20.Text = "P";
			this.button20.Click += new System.EventHandler(this.button20_Click);
			// 
			// button21
			// 
			this.button21.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button21.Location = new System.Drawing.Point(244, 72);
			this.button21.Size = new System.Drawing.Size(32, 32);
			this.button21.Text = "O";
			this.button21.Click += new System.EventHandler(this.button21_Click);
			// 
			// button8
			// 
			this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button8.Location = new System.Drawing.Point(204, 72);
			this.button8.Size = new System.Drawing.Size(32, 32);
			this.button8.Text = "N";
			this.button8.Click += new System.EventHandler(this.button8_Click);
			// 
			// button9
			// 
			this.button9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button9.Location = new System.Drawing.Point(164, 72);
			this.button9.Size = new System.Drawing.Size(32, 32);
			this.button9.Text = "M";
			this.button9.Click += new System.EventHandler(this.button9_Click);
			// 
			// button10
			// 
			this.button10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button10.Location = new System.Drawing.Point(124, 72);
			this.button10.Size = new System.Drawing.Size(32, 32);
			this.button10.Text = "L";
			this.button10.Click += new System.EventHandler(this.button10_Click);
			// 
			// button11
			// 
			this.button11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button11.Location = new System.Drawing.Point(84, 72);
			this.button11.Size = new System.Drawing.Size(32, 32);
			this.button11.Text = "K";
			this.button11.Click += new System.EventHandler(this.button11_Click);
			// 
			// button12
			// 
			this.button12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button12.Location = new System.Drawing.Point(44, 72);
			this.button12.Size = new System.Drawing.Size(32, 32);
			this.button12.Text = "J";
			this.button12.Click += new System.EventHandler(this.button12_Click);
			// 
			// button13
			// 
			this.button13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button13.Location = new System.Drawing.Point(4, 72);
			this.button13.Size = new System.Drawing.Size(32, 32);
			this.button13.Text = "I";
			this.button13.Click += new System.EventHandler(this.button13_Click);
			// 
			// button14
			// 
			this.button14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button14.Location = new System.Drawing.Point(284, 32);
			this.button14.Size = new System.Drawing.Size(32, 32);
			this.button14.Text = "H";
			this.button14.Click += new System.EventHandler(this.button14_Click);
			// 
			// button7
			// 
			this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button7.Location = new System.Drawing.Point(244, 32);
			this.button7.Size = new System.Drawing.Size(32, 32);
			this.button7.Text = "G";
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// button6
			// 
			this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button6.Location = new System.Drawing.Point(204, 32);
			this.button6.Size = new System.Drawing.Size(32, 32);
			this.button6.Text = "F";
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button5.Location = new System.Drawing.Point(164, 32);
			this.button5.Size = new System.Drawing.Size(32, 32);
			this.button5.Text = "E";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button4.Location = new System.Drawing.Point(124, 32);
			this.button4.Size = new System.Drawing.Size(32, 32);
			this.button4.Text = "D";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button3.Location = new System.Drawing.Point(84, 32);
			this.button3.Size = new System.Drawing.Size(32, 32);
			this.button3.Text = "C";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(44, 32);
			this.button2.Size = new System.Drawing.Size(32, 32);
			this.button2.Text = "B";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(4, 32);
			this.button1.Size = new System.Drawing.Size(32, 32);
			this.button1.Text = "A";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.button44);
			this.tabPage2.Controls.Add(this.button43);
			this.tabPage2.Controls.Add(this.button42);
			this.tabPage2.Controls.Add(this.button41);
			this.tabPage2.Controls.Add(this.button40);
			this.tabPage2.Controls.Add(this.button39);
			this.tabPage2.Controls.Add(this.button38);
			this.tabPage2.Controls.Add(this.button37);
			this.tabPage2.Controls.Add(this.button36);
			this.tabPage2.Controls.Add(this.button35);
			this.tabPage2.Controls.Add(this.button34);
			this.tabPage2.Controls.Add(this.button33);
			this.tabPage2.Controls.Add(this.button32);
			this.tabPage2.Controls.Add(this.inputBox2);
			this.tabPage2.Location = new System.Drawing.Point(4, 4);
			this.tabPage2.Size = new System.Drawing.Size(314, 188);
			this.tabPage2.Text = "Siffror";
			// 
			// button44
			// 
			this.button44.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button44.Location = new System.Drawing.Point(176, 88);
			this.button44.Size = new System.Drawing.Size(48, 40);
			this.button44.Text = "-";
			this.button44.Click += new System.EventHandler(this.button44_Click);
			// 
			// button43
			// 
			this.button43.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button43.Location = new System.Drawing.Point(232, 136);
			this.button43.Size = new System.Drawing.Size(72, 40);
			this.button43.Text = "OK";
			this.button43.Click += new System.EventHandler(this.button43_Click);
			// 
			// button42
			// 
			this.button42.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button42.Location = new System.Drawing.Point(176, 40);
			this.button42.Size = new System.Drawing.Size(48, 40);
			this.button42.Text = "BS";
			this.button42.Click += new System.EventHandler(this.button42_Click);
			// 
			// button41
			// 
			this.button41.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button41.Location = new System.Drawing.Point(176, 136);
			this.button41.Size = new System.Drawing.Size(48, 40);
			this.button41.Text = "0";
			this.button41.Click += new System.EventHandler(this.button41_Click);
			// 
			// button40
			// 
			this.button40.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button40.Location = new System.Drawing.Point(120, 136);
			this.button40.Size = new System.Drawing.Size(48, 40);
			this.button40.Text = "3";
			this.button40.Click += new System.EventHandler(this.button40_Click);
			// 
			// button39
			// 
			this.button39.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button39.Location = new System.Drawing.Point(64, 136);
			this.button39.Size = new System.Drawing.Size(48, 40);
			this.button39.Text = "2";
			this.button39.Click += new System.EventHandler(this.button39_Click);
			// 
			// button38
			// 
			this.button38.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button38.Location = new System.Drawing.Point(120, 88);
			this.button38.Size = new System.Drawing.Size(48, 40);
			this.button38.Text = "6";
			this.button38.Click += new System.EventHandler(this.button38_Click);
			// 
			// button37
			// 
			this.button37.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button37.Location = new System.Drawing.Point(64, 88);
			this.button37.Size = new System.Drawing.Size(48, 40);
			this.button37.Text = "5";
			this.button37.Click += new System.EventHandler(this.button37_Click);
			// 
			// button36
			// 
			this.button36.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button36.Location = new System.Drawing.Point(120, 40);
			this.button36.Size = new System.Drawing.Size(48, 40);
			this.button36.Text = "9";
			this.button36.Click += new System.EventHandler(this.button36_Click);
			// 
			// button35
			// 
			this.button35.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button35.Location = new System.Drawing.Point(64, 40);
			this.button35.Size = new System.Drawing.Size(48, 40);
			this.button35.Text = "8";
			this.button35.Click += new System.EventHandler(this.button35_Click);
			// 
			// button34
			// 
			this.button34.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button34.Location = new System.Drawing.Point(8, 136);
			this.button34.Size = new System.Drawing.Size(48, 40);
			this.button34.Text = "1";
			this.button34.Click += new System.EventHandler(this.button34_Click);
			// 
			// button33
			// 
			this.button33.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button33.Location = new System.Drawing.Point(8, 88);
			this.button33.Size = new System.Drawing.Size(48, 40);
			this.button33.Text = "4";
			this.button33.Click += new System.EventHandler(this.button33_Click);
			// 
			// button32
			// 
			this.button32.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button32.Location = new System.Drawing.Point(8, 40);
			this.button32.Size = new System.Drawing.Size(48, 40);
			this.button32.Text = "7";
			this.button32.Click += new System.EventHandler(this.button32_Click);
			// 
			// inputBox2
			// 
			this.inputBox2.Location = new System.Drawing.Point(4, 8);
			this.inputBox2.Size = new System.Drawing.Size(308, 20);
			this.inputBox2.Text = "";
			// 
			// button45
			// 
			this.button45.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button45.Location = new System.Drawing.Point(244, 112);
			this.button45.Size = new System.Drawing.Size(32, 32);
			this.button45.Text = "W";
			this.button45.Click += new System.EventHandler(this.button45_Click);
			// 
			// Keyboard
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.tabControl1);
			this.Text = "Tangentbord";

		}
		#endregion

		private void onButtonPress(string ch)
		{
			if (ch == "BS") 
			{
				if (inputString.Length > 0)
				{
					inputString = inputString.Substring(0, inputString.Length-1);
				}
			}
			else
			{
				if (inputString == null) 
				{
					inputString = ch;
				}
				else
				{
					if (inputString.Length < maxLength)
					{
						inputString = inputString + ch;
					}
				}
			}

			inputBox.Text = inputString;
			inputBox2.Text = inputString;
		}

		public string getInputString()
		{
			return inputString;
		}

		public void setInputString(string inputString)
		{
			this.inputString = inputString;
			this.inputBox.Text = inputString;
			this.inputBox2.Text = inputString;
		}


		private void button1_Click(object sender, System.EventArgs e)
		{
			onButtonPress("A");
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			onButtonPress("B");		
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			onButtonPress("C");		
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			onButtonPress("D");		
		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			onButtonPress("E");		
		}

		private void button6_Click(object sender, System.EventArgs e)
		{
			onButtonPress("F");		
		}

		private void button7_Click(object sender, System.EventArgs e)
		{
			onButtonPress("G");		
		}

		private void button14_Click(object sender, System.EventArgs e)
		{
			onButtonPress("H");		
		}

		private void button13_Click(object sender, System.EventArgs e)
		{
			onButtonPress("I");		
		}

		private void button12_Click(object sender, System.EventArgs e)
		{
			onButtonPress("J");		
		}

		private void button11_Click(object sender, System.EventArgs e)
		{
			onButtonPress("K");		
		}

		private void button10_Click(object sender, System.EventArgs e)
		{
			onButtonPress("L");		
		}

		private void button9_Click(object sender, System.EventArgs e)
		{
			onButtonPress("M");		
		}

		private void button8_Click(object sender, System.EventArgs e)
		{
			onButtonPress("N");		
		}

		private void button21_Click(object sender, System.EventArgs e)
		{
			onButtonPress("O");		
		}

		private void button20_Click(object sender, System.EventArgs e)
		{
			onButtonPress("P");		
		}

		private void button19_Click(object sender, System.EventArgs e)
		{
			onButtonPress("Q");
		}

		private void button18_Click(object sender, System.EventArgs e)
		{
			onButtonPress("R");		
		}

		private void button17_Click(object sender, System.EventArgs e)
		{
			onButtonPress("S");		
		}

		private void button16_Click(object sender, System.EventArgs e)
		{
			onButtonPress("T");		
		}

		private void button15_Click(object sender, System.EventArgs e)
		{
			onButtonPress("U");		
		}

		private void button28_Click(object sender, System.EventArgs e)
		{
			onButtonPress("V");		
		}

		private void button27_Click(object sender, System.EventArgs e)
		{
			onButtonPress("X");		
		}

		private void button26_Click(object sender, System.EventArgs e)
		{
			onButtonPress("Y");		
		}

		private void button25_Click(object sender, System.EventArgs e)
		{
			onButtonPress("Z");		
		}

		private void button24_Click(object sender, System.EventArgs e)
		{
			onButtonPress("Å");		
		}

		private void button23_Click(object sender, System.EventArgs e)
		{
			onButtonPress("Ä");		
		}

		private void button22_Click(object sender, System.EventArgs e)
		{
			onButtonPress("Ö");		
		}

		private void button30_Click(object sender, System.EventArgs e)
		{
			onButtonPress(" ");				
		}

		private void button31_Click(object sender, System.EventArgs e)
		{
			onButtonPress("BS");				
		}

		private void button29_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button43_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button34_Click(object sender, System.EventArgs e)
		{
			onButtonPress("1");			
		}

		private void button39_Click(object sender, System.EventArgs e)
		{
			onButtonPress("2");		
		}

		private void button40_Click(object sender, System.EventArgs e)
		{
			onButtonPress("3");		
		}

		private void button33_Click(object sender, System.EventArgs e)
		{
			onButtonPress("4");		
		}

		private void button37_Click(object sender, System.EventArgs e)
		{
			onButtonPress("5");		
		}

		private void button38_Click(object sender, System.EventArgs e)
		{
			onButtonPress("6");		
		}

		private void button32_Click(object sender, System.EventArgs e)
		{
			onButtonPress("7");		
		}

		private void button35_Click(object sender, System.EventArgs e)
		{
			onButtonPress("8");		
		}

		private void button36_Click(object sender, System.EventArgs e)
		{
			onButtonPress("9");		
		}

		private void button42_Click(object sender, System.EventArgs e)
		{
			onButtonPress("BS");		
		}

		private void button41_Click(object sender, System.EventArgs e)
		{
			onButtonPress("0");		
		}

		public void setStartTab(int i)
		{
			tabControl1.SelectedIndex = i;
		}

		private void button44_Click(object sender, System.EventArgs e)
		{
			onButtonPress("-");	
		}

		public void setHeaderText(string text)
		{
			this.Text = text;
		}

		private void button45_Click(object sender, System.EventArgs e)
		{
			onButtonPress("W");
		}

	}
}
