using System;

namespace Model
{
    public interface IProduct
    {
        string Name { get; set; }
        Func<decimal, decimal> CalculateAnnualCost { get; set; }
    }
}
