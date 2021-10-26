using System;
using System.Linq;
using System.Collections.Generic;

namespace Model
{
    public class Product : IProduct
    {
        public string Name { get; set; }
        public Func<decimal, decimal> CalculateAnnualCost { get; set; }
    }

    public class ProductCost
    {
        /// <summary>
        /// Product name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Annual cost in euro
        /// </summary>
        public decimal AnnualCost { get; set; }

        public ProductCost(string name, decimal annualCost)
        {
            Name = name;
            AnnualCost = annualCost;
        }
    }

    public class ProductComparison
    {
        private List<IProduct> _products;

        public ProductComparison(IEnumerable<IProduct> products)
        {
            _products = products.ToList();
        }

        public IEnumerable<ProductCost> Evaluate(decimal consumption)
        {
            return _products.Select(p =>
                new ProductCost(p.Name, p.CalculateAnnualCost(consumption))
            ).OrderBy(pc => pc.AnnualCost);
        }
    }
}
