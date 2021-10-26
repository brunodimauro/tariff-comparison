using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTest
{
    public class TestProduct
    {
        [Fact]
        public void CheckSingleProductComparison()
        {
            Mock<IProduct> product = new Mock<IProduct>();
            product.SetupProperty(p => p.CalculateAnnualCost, c => 12 * 5 + c * 0.22m);
            product.SetupProperty(p => p.CalculateAnnualCost, c => 800 + ((c > 4000) ? (c - 4000) * 0.3m : 0));

            var comparison = new ProductComparison(new List<IProduct>() { product.Object });
            var compResult = comparison.Evaluate(3500);

            Assert.AreEqual(compResult.First().AnnualCost, 770);
            Assert.AreEqual(compResult.Last().AnnualCost, 800);
        }

        [Fact]
        public void CheckSingleProductComparisonWrong()
        {
            Mock<IProduct> product = new Mock<IProduct>();
            product.SetupProperty(p => p.CalculateAnnualCost, c => 12 * 5 + c * 0.22m);
            product.SetupProperty(p => p.CalculateAnnualCost, c => 800 + ((c > 4000) ? (c - 4000) * 0.3m : 0));

            var comparison = new ProductComparison(new List<IProduct>() { product.Object });
            var compResult = comparison.Evaluate(4500);

            Assert.AreNotEqual(compResult.First().AnnualCost, 770);
            Assert.AreNotEqual(compResult.Last().AnnualCost, 800);
        }
    }
}
