using System.Runtime.InteropServices;

namespace ResursWrapper
{
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    [Guid("44AAA04A-334F-4262-A8E3-8B320F5C26DB")]
    public interface ISpecLine
    {
        [DispId(0x2000)]
        string Id { get; set; }
        [DispId(0x2001)]
        string ArtNo { get; set; }
        [DispId(0x2002)]
        string Description { get; set; }
        [DispId(0x2003)]
        int Quantity { get; set; }
        [DispId(0x2004)]
        string UnitMeasure { get; set; }
        [DispId(0x2005)]
        string UnitAmountWithoutVat { get; set; }
        [DispId(0x2006)]
        string VatPct { get; set; }
        [DispId(0x2007)]
        string TotalVatAmount { get; set; }
        [DispId(0x2008)]
        string TotalAmount { get; set; }
    }

    [ClassInterface(ClassInterfaceType.None)]
    [Guid("174AF668-BE9B-402B-BD8B-DE6065D7A3B8")]
    public class SpecLine : ISpecLine
    {
        public string Id { get; set; }
        public string ArtNo { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string UnitMeasure { get; set; }
        public string UnitAmountWithoutVat { get; set; }
        public string VatPct { get; set; }
        public string TotalVatAmount { get; set; }
        public string TotalAmount { get; set; }

    }
}
