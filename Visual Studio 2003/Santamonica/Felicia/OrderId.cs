using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for OrderId.
	/// </summary>
	public class OrderId : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox postMortemBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox bseBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox descriptionBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox itemNoBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox unitIdBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label itemNoLabel;

		private SmartDatabase smartDatabase;
		private DataOrderLine dataOrderLine;
		private DataOrderLineId dataOrderLineId;
		private System.Windows.Forms.Button button3;
		private int result;
	
		public OrderId(SmartDatabase smartDatabase, DataOrderLine dataOrderLine, DataOrderLineId dataOrderLineId)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			this.smartDatabase = smartDatabase;
			this.dataOrderLine = dataOrderLine;
			this.dataOrderLineId = dataOrderLineId;

			this.updateForm();
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
			this.button5 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.postMortemBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.bseBox = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.descriptionBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.itemNoBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.unitIdBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.itemNoLabel = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.button5.Location = new System.Drawing.Point(160, 168);
			this.button5.Size = new System.Drawing.Size(72, 40);
			this.button5.Text = "Avbryt";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.button4.Location = new System.Drawing.Point(240, 72);
			this.button4.Size = new System.Drawing.Size(72, 40);
			this.button4.Text = "Provt.";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(240, 168);
			this.button2.Size = new System.Drawing.Size(72, 40);
			this.button2.Text = "Klar";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(240, 120);
			this.button1.Size = new System.Drawing.Size(72, 40);
			this.button1.Text = "Obd.";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// postMortemBox
			// 
			this.postMortemBox.Location = new System.Drawing.Point(104, 120);
			this.postMortemBox.ReadOnly = true;
			this.postMortemBox.Size = new System.Drawing.Size(128, 20);
			this.postMortemBox.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(104, 104);
			this.label5.Text = "Obduktion";
			// 
			// bseBox
			// 
			this.bseBox.Location = new System.Drawing.Point(8, 120);
			this.bseBox.ReadOnly = true;
			this.bseBox.Size = new System.Drawing.Size(88, 20);
			this.bseBox.Text = "";
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular);
			this.label6.Location = new System.Drawing.Point(8, 104);
			this.label6.Text = "Provtagning";
			// 
			// descriptionBox
			// 
			this.descriptionBox.Location = new System.Drawing.Point(104, 40);
			this.descriptionBox.ReadOnly = true;
			this.descriptionBox.Size = new System.Drawing.Size(128, 20);
			this.descriptionBox.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(104, 24);
			this.label3.Text = "Beskrivning";
			// 
			// itemNoBox
			// 
			this.itemNoBox.Location = new System.Drawing.Point(8, 40);
			this.itemNoBox.ReadOnly = true;
			this.itemNoBox.Size = new System.Drawing.Size(88, 20);
			this.itemNoBox.Text = "";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular);
			this.label4.Location = new System.Drawing.Point(8, 24);
			this.label4.Text = "Artikelnr";
			// 
			// unitIdBox
			// 
			this.unitIdBox.Location = new System.Drawing.Point(8, 80);
			this.unitIdBox.ReadOnly = true;
			this.unitIdBox.Size = new System.Drawing.Size(88, 20);
			this.unitIdBox.Text = "";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular);
			this.label1.Location = new System.Drawing.Point(8, 64);
			this.label1.Text = "ID-nr";
			// 
			// itemNoLabel
			// 
			this.itemNoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.itemNoLabel.Location = new System.Drawing.Point(5, 3);
			this.itemNoLabel.Size = new System.Drawing.Size(307, 20);
			this.itemNoLabel.Text = "Identitet";
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.button3.Location = new System.Drawing.Point(8, 168);
			this.button3.Size = new System.Drawing.Size(72, 40);
			this.button3.Text = "Ändra ID";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// OrderId
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.postMortemBox);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.bseBox);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.descriptionBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.itemNoBox);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.unitIdBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.itemNoLabel);
			this.Text = "Identitet";

		}
		#endregion


		private void button4_Click(object sender, System.EventArgs e)
		{
			if (dataOrderLineId.bseTesting)
			{
				dataOrderLineId.bseTesting = false;
			}
			else
			{
				dataOrderLineId.bseTesting = true;
			}

			this.updateForm();

		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (dataOrderLineId.postMortem)
			{
				dataOrderLineId.postMortem = false;
			}
			else
			{
				dataOrderLineId.postMortem = true;
			}

			this.updateForm();

		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.result = 1;

			dataOrderLineId.commit();

			this.Close();

		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			this.result = 0;
			this.Close();
		}

		private void updateForm()
		{
			itemNoBox.Text = dataOrderLine.itemNo;
			descriptionBox.Text = dataOrderLine.description;
			unitIdBox.Text = dataOrderLineId.unitId;
			
			bseBox.Text = "";
			if (dataOrderLineId.bseTesting) bseBox.Text = "Ja";

			postMortemBox.Text = "";
			if (dataOrderLineId.postMortem) postMortemBox.Text = "Ja";

		}

		public int getResult()
		{
			return result;
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			string unitId = "";

			DataItem dataItem = new DataItem(smartDatabase, dataOrderLine.itemNo);
			if (dataItem.idGroupCode == "1")
			{
				IdType1Pad idPad = new IdType1Pad();
				idPad.ShowDialog();
				unitId = idPad.getInputString();
				idPad.Dispose();
			}
			if (dataItem.idGroupCode == "")
			{
				Keyboard keyboard = new Keyboard(20);
				keyboard.setStartTab(1);
				keyboard.ShowDialog();
				unitId = keyboard.getInputString();
				keyboard.Dispose();
			}

			dataOrderLineId.unitId = unitId;

			this.updateForm();

		}

	}
}
