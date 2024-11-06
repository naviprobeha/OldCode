using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for WindowsCE.
	/// </summary>
	/// 
	public struct PROCESS_INFORMATION
	{
		public uint hProcess;
		public uint hThread;
		public uint dwProcessId;
		public uint dwThreadId;
	}

	public class WindowsCE
	{
		private const int RasConnSize = 52;//4+4+21*2;

		private class RasConn // managed equivalent of the native RASCONN struct
		{
			private byte[] _data;
			public const int SizeOffset = 0;
			public const int ConnIdOffset = 4;
			public const int ConnNameOffset = 8;

			public RasConn(byte[] data, int offset)
			{
				_data = new byte[RasConnSize];
				Buffer.BlockCopy(data, offset, _data, 0, RasConnSize);
			}

			public string Name
			{
				get
				{
					string retVal = null;
					retVal = Encoding.Unicode.GetString(_data, ConnNameOffset,
						20).TrimEnd('\0');
					return retVal;
				}
			}
			public IntPtr ConnectionId
			{
				get
				{
					IntPtr retVal = IntPtr.Zero;
					retVal = (IntPtr) BitConverter.ToInt32(_data, ConnIdOffset);
					return retVal;
				}
			}
		}


		public WindowsCE()
		{
			//
			// TODO: Add constructor logic here
			//

		}

		[DllImport("coredll.dll", EntryPoint="CreateProcess", SetLastError=true)]
		public extern static int CreateProcess(string lpszImageName, string lpszCmdLine, int lpsaProcess, int lpsaThread, int fInheritHandles, int fdwCreate, int lpvEnvironment, int lpszCurDir, int lpsiStartInfo, int lppiProcInfo);

		[DllImport("coredll.dll", EntryPoint="FindWindow", SetLastError=true)]
		public extern static int FindWindow(string lpszClassName, string lpszWindowName);

		[DllImport("wininet.dll",CharSet=CharSet.Auto)] 
		public extern static int InternetHangup(int nConnection, int dwReserved); 

		[DllImport("coredll.dll", SetLastError=true)]
		public static extern UInt32 RasEnumConnections(byte[] rasconn, ref UInt32 size, ref UInt32 count);

		[DllImport("coredll.dll")]
		public static extern uint RasHangUp(IntPtr pRasConn);


		public static int StartProcess(string imageName, string cmdLine)
		{

			PROCESS_INFORMATION processInfo = new PROCESS_INFORMATION();
			CreateProcess(imageName, cmdLine, 0, 0, 0, 0, 0, 0, 0, 0);

			return Convert.ToInt32(processInfo.dwProcessId);

			
		}

		public static bool isStarted(string windowName)
		{
			if (FindWindow(null, windowName) > 0) return true;

			return false;
		}

		public static void hangUpConnections()
		{
			UInt32 MaxConn = 5;
			UInt32 enumCount = 5;
			UInt32 enumSize = MaxConn * RasConnSize;
			byte[] enumData = new byte[MaxConn * RasConnSize];
			byte[] rasConnSizeBytes = BitConverter.GetBytes(RasConnSize);
			Buffer.BlockCopy(rasConnSizeBytes, 0, enumData, 0, 4);
			UInt32 error = RasEnumConnections(enumData, ref enumSize, ref enumCount);

			int i = 0;
			while(i < enumCount)
			{
				RasConn rasConn = new RasConn(enumData, i*RasConnSize);
				RasHangUp(rasConn.ConnectionId);

				i++;
			}
		}
	}

}

