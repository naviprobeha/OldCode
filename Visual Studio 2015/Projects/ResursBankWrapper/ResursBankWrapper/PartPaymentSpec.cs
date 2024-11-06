using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ResursWrapper
{
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    [Guid("3B713DC4-F6A8-4719-9059-9BC54AB446BE")]
    public interface IPartPaymentSpec
    {
        [DispId(0x3000)]
        void AddSpecLine(SpecLine line);

        [DispId(0x3002)]
        string TotalAmount { get; set; }

        [DispId(0x3003)]
        string TotalVatAmount { get; set; }

        [DispId(0x3004)]
        string BonusPoints { get; set; }
    }


    [ClassInterface(ClassInterfaceType.None)]
    [Guid("AA29120F-34C8-4871-9977-12174A5D7BCE")]
    public class PartPaymentSpec : IPartPaymentSpec
    {
        public PartPaymentSpec()
        {
            SpecLines = new List<SpecLine>();
        }

        public void AddSpecLine(SpecLine line)
        {
            SpecLines.Add(line);
        }

        public List<SpecLine> SpecLines { get; set; }
        public string TotalAmount { get; set; }
        public string TotalVatAmount { get; set; }
        public string BonusPoints { get; set; }

    }
}
