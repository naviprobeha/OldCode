using System;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for CordConv.
	/// </summary>
	public class CordConv
	{
		public class Matrix
		{
			public int rows;
			public int cols;
			public double[] data1;
			public double[] data2;
			public double[] data3;

			public Matrix(int rows, int cols, double data1, double data2, double data3)
			{
				this.rows = rows;
				this.cols = cols;
				this.data1 = new double[3];
				this.data2 = new double[3];
				this.data3 = new double[3];
				this.data1[0] = data1;
				this.data2[0] = data2;
				this.data3[0] = data3;
			}

			public Matrix(int rows, int cols, double data11, double data12, double data13, double data21, double data22, double data23, double data31, double data32, double data33)
			{
				this.rows = rows;
				this.cols = cols;
				this.data1 = new double[3];
				this.data2 = new double[3];
				this.data3 = new double[3];

				this.data1[0] = data11;
				this.data1[1] = data12;
				this.data1[2] = data13;

				this.data2[0] = data21;
				this.data2[1] = data22;
				this.data2[2] = data23;

				this.data3[0] = data31;
				this.data3[1] = data32;
				this.data3[2] = data33;

			}
		}

		private double a = 6378137;
		//private double b = 6356752.314140;
		private double e2 = 0.0066943800229;
 
		private double a_rt = 6377397.155;
		private double b_rt = 6356078.962818;
		private double e2_rt = 0.0066743722318;

		private double Pi = 3.1415926535;

		private Matrix sweref;
		private Matrix temp;
		private Matrix result;
		private Matrix trans;
		private Matrix rotb;

		public CordConv()
		{
			//
			// TODO: Add constructor logic here
			//

			sweref = new Matrix(3, 1, 0,0,0);
			temp = new Matrix(3, 1, 0,0,0);
			result = new Matrix(3, 1, 0,0,0);
			trans = new Matrix(3, 1, -419.375, -99.352, -591.349);
			rotb = new Matrix(3,3, 0,0,0, 0,0,0, 0,0,0);


		}

		private void fill_rotb()
		{
			double wx = 0.000004123137;
			double wy = 0.000008810252;
			double wz = -0.000038117239;

			rotb.data1[0] = (Math.Cos(wy)*Math.Cos(wz));
			rotb.data1[1] = (Math.Cos(wx)*Math.Sin(wz) + Math.Sin(wx)*Math.Sin(wy)*Math.Cos(wz));
			rotb.data1[2] = (-Math.Cos(wx)*Math.Sin(wy)*Math.Cos(wz) + Math.Sin(wx)*Math.Sin(wz));

			rotb.data2[0] = (-Math.Cos(wy)*Math.Sin(wz));
			rotb.data2[1] = (Math.Cos(wx)*Math.Cos(wz) - Math.Sin(wx)*Math.Sin(wy)*Math.Sin(wz));
			rotb.data2[2] = (Math.Sin(wx)*Math.Cos(wz) + Math.Cos(wx)*Math.Sin(wy)*Math.Sin(wz));

			rotb.data3[0] = (Math.Sin(wy));
			rotb.data3[1] = (-Math.Sin(wx)*Math.Cos(wy));
			rotb.data3[2] = (Math.Cos(wx)*Math.Cos(wy));

		}

		private void madd(Matrix m1, Matrix m2, ref Matrix dest)
		{
			int i, j;

			if ((m1.rows != m2.rows) || (m1.cols != m2.cols))
				throw new Exception("Add error: different sizes.");

			if ((m1.rows != dest.rows) || (m1.cols != dest.cols))
				throw new Exception("Add error: different sizes.");

			for (i=0; i < m1.rows; i++)
			{
				for (j=0; j < m1.cols; j++)
				{
					if (i == 0) dest.data1[j] = m1.data1[j] + m2.data1[j];
					if (i == 1) dest.data2[j] = m1.data2[j] + m2.data2[j];
					if (i == 2) dest.data3[j] = m1.data3[j] + m2.data3[j];
				}
			}

		}


		private void smmult(ref Matrix m, double s)
		{
			int i, j;

			for (i=0; i< m.rows; i++)
			{
				for (j=0; j<m.cols; j++)
				{
					if (i == 0) m.data1[j] = m.data1[j] * s;
					if (i == 1) m.data2[j] = m.data2[j] * s;
					if (i == 2) m.data3[j] = m.data3[j] * s;
				}
			}
		}

		
		private void mmult(Matrix m1, Matrix m2, ref Matrix dest)
		{

			int i, j, k;
			double cellval;

			if (m1.cols != m2.rows)
				throw new Exception("Mult error: m1 cols not equal to m2 rows.");

			if (m2.cols != dest.cols)
				throw new Exception("Mult error: dest cols not equal to m2 cols.");

			if (m1.rows != dest.rows)
				throw new Exception("Mult error: dest rows not equal to m1 rows.");

			for (i=0; i<m1.rows; i++)
			{
				for (j=0; j<m2.cols; j++)
				{
					cellval = 0.0;
					for (k=0; k<m1.cols; k++)
					{
						if ((i == 0) && (k == 0)) cellval += m1.data1[k] * m2.data1[j];
						if ((i == 0) && (k == 1)) cellval += m1.data1[k] * m2.data2[j];
						if ((i == 0) && (k == 2)) cellval += m1.data1[k] * m2.data3[j];

						if ((i == 1) && (k == 0)) cellval += m1.data2[k] * m2.data1[j];
						if ((i == 1) && (k == 1)) cellval += m1.data2[k] * m2.data2[j];
						if ((i == 1) && (k == 2)) cellval += m1.data2[k] * m2.data3[j];

						if ((i == 2) && (k == 0)) cellval += m1.data3[k] * m2.data1[j];
						if ((i == 2) && (k == 1)) cellval += m1.data3[k] * m2.data2[j];
						if ((i == 2) && (k == 2)) cellval += m1.data3[k] * m2.data3[j];
					}
					if (i == 0) dest.data1[j] = cellval;
					if (i == 1) dest.data2[j] = cellval;
					if (i == 2) dest.data3[j] = cellval;

				}
			}
		}

		private void mzero(ref Matrix m)
		{
			int j;

			for (j=0; j<m.cols; j++)
			{
				m.data1[j] = 0.0;
				m.data2[j] = 0.0;
				m.data3[j] = 0.0;
			}
		}

		private double atanh(double x)
		{
			return (0.5 * Math.Log((1+x) / (1-x)));
		}

		private double cosh(double x)
		{
			return (Math.Exp(x) + Math.Exp(-x))/2;
		}

		private double sinh(double x)
		{
			return (Math.Exp(x) - Math.Exp(-x))/2;
		}

		private void SR93_ll_to_xy(double lat, double lon, double h, ref double X, ref double Y, ref double Z)
		{
			double N, LAT_R, LON_R;
			LAT_R = lat * Pi / 180;
			LON_R = lon * Pi / 180;
			N = a / Math.Sqrt(1 - e2 * Math.Pow(Math.Sin(LAT_R),2));
			X = (N + h) * Math.Cos(LAT_R) * Math.Cos(LON_R);
			Y = (N + h) * Math.Cos(LAT_R) * Math.Sin(LON_R);
			Z = (N * (1-e2) + h) * Math.Sin(LAT_R);
		}

		private void SR93_to_RR92(double sx, double sy, double sz, ref double rx, ref double ry, ref double rz)
		{
			sweref.data1[0] = sx;
			sweref.data2[0] = sy;
			sweref.data3[0] = sz;

			fill_rotb();
			mzero(ref temp);
			mzero(ref result);
			mmult(rotb, sweref, ref temp);
			smmult(ref temp, 1.00000099496);
			madd(trans, temp, ref result);
			rx = result.data1[0];
			ry = result.data2[0];
			rz = result.data3[0];
		}


		private void RR92_xy_to_ll(double X, double Y, double Z, ref double lat, ref double lon, ref double h)
		{
			double LAT_R, LON_R, p, OMEGA, N;
			p = Math.Sqrt(Math.Pow(X,2) + Math.Pow(Y,2));
			OMEGA = Math.Atan(Z / (p * Math.Sqrt(1 - e2_rt)));
			LAT_R = Math.Atan( (Z + ((a_rt * e2_rt) / Math.Sqrt(1 - e2_rt)) * Math.Pow(Math.Sin(OMEGA), 3)) / (p - (a_rt * e2_rt * Math.Pow(Math.Cos(OMEGA),3))));
			LON_R = Math.Atan(Y / X);
			N = a_rt / Math.Sqrt(1-e2_rt * Math.Pow(Math.Sin(LAT_R),2));
			h = p / Math.Cos(LAT_R) - N;
			lat = LAT_R * 180.0 / Pi;
			lon = LON_R * 180.0 / Pi;

		}

		private void RR92_to_RT90(double lat92, double lon92, ref int x90, ref int y90)
		{
			double e;
			double f;
			double A, B, C, D, E, N;
			double a_t, n, B1, B2, B3, B4;
			double iso_lat;
			double lat, lon_diff;
			double k0 = 1;

			lat = lat92 * Pi / 180.0;
			lon_diff = (lon92 - 
				((360 * 17.564753086)/400.0)) * (Pi / 180.0);


			f = (a_rt - b_rt) / a_rt;
			n = f / (2-f);
			e = Math.Sqrt(e2_rt);
			a_t = (a_rt / (1+n)) * (1 + Math.Pow(n,2) / 4 + Math.Pow(n, 4) / 64);


			A = e2_rt;
			B = (5 * Math.Pow(e, 4) - Math.Pow(e, 6)) / 6;
			C = (104 * Math.Pow(e, 6) - (45 * Math.Pow(e, 8))) / 120;
			D = (1237 * Math.Pow(e, 8)) / 1260;

			iso_lat = lat - 
					(Math.Sin(lat) * Math.Cos(lat) * 
				(A + B * Math.Pow(Math.Sin(lat), 2) + 
				(C * Math.Pow(Math.Sin(lat), 4)) + 
				(D * Math.Pow(Math.Sin(lat), 6)))
				);

			E = Math.Atan(Math.Tan(iso_lat) / Math.Cos(lon_diff));
			N = atanh(Math.Cos(iso_lat) * Math.Sin(lon_diff));
			
			B1 = (n/2) - (2 * Math.Pow(n, 2) / 3) + (5 * Math.Pow(n, 3) / 16) + (41 * Math.Pow(n, 4) / 180);
			B2 = (13 * Math.Pow(n, 2) / 48) - (3 * Math.Pow(n, 3) / 5) + (557 * Math.Pow(n, 4) / 1440);
			B3 = (61 * Math.Pow(n, 3) / 240) - (103 * Math.Pow(n, 4) / 140);
			B4 = (49561103 * Math.Pow(n, 4) / 161280);

			x90 = (int)(k0 * a_t * (E + B1 * Math.Sin(2*E) * cosh(2*N) + B2 * Math.Sin(4*E) * cosh(4*N) + B3 * Math.Sin(6*E) * cosh(6*N) + B4 * Math.Sin(8*E) * cosh(8*N)));
			y90 = (int)(k0 * a_t * (N + B1 * Math.Cos(2*E) * sinh(2*N) + B2 * Math.Cos(4*E) * sinh(4*N) + B3 * Math.Cos(6*E) * sinh(6*N) + B4 * Math.Cos(8*E) * sinh(8*N)) + 1500000.0);


		}

		public void WGS84_to_RT90(double wlat, double wlon, double wh, ref int Xrt, ref int Yrt)
		{

			double lat, lon, h, x, y, z;
			double x2, y2, z2;

			lat = 0.0;
			lon = 0.0;
			h = 0.0;
			x = 0.0;
			y = 0.0;
			z = 0.0;
			x2 = 0.0;
			y2 = 0.0;
			z2 = 0.0;

			SR93_ll_to_xy(wlat, wlon, wh, ref x, ref y, ref z);
			System.Windows.Forms.MessageBox.Show("Conv1: "+x.ToString()+", "+y.ToString());
			SR93_to_RR92(z, y, z, ref x2, ref y2, ref z2);
			System.Windows.Forms.MessageBox.Show("Conv2: "+x2.ToString()+", "+y2.ToString());
			RR92_xy_to_ll(x2, y2, z2, ref lat, ref lon, ref h);
			System.Windows.Forms.MessageBox.Show("Entering XY conv ("+lat.ToString()+", "+lon.ToString()+")");
			RR92_to_RT90(lat, lon, ref Xrt, ref Yrt);
			System.Windows.Forms.MessageBox.Show("Done");

		}
	}
}
