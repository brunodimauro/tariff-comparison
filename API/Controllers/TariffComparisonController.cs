using Microsoft.AspNetCore.Mvc;
using Model;
using System.Collections.Generic;

namespace TariffComparison.Controllers
{
    public class TariffComparisonController : Controller
    {
        [HttpGet]
        [Route("/api/v1/tariff/{consumption}")]
        public IActionResult Get(decimal consumption)
        {
            if (consumption < 0)
                return BadRequest("Consumption must be a positive number");

            var comparison = new ProductComparison(new List<Product>() {
                    new Product() {
                        Name = "basic electricity tariff",
                        CalculateAnnualCost = c => 12 * 5 + c * 0.22m
                    },
                    new Product() {
                        Name = "Packaged tariff",
                        CalculateAnnualCost = c => 800 + ((c > 4000) ? (c - 4000) * 0.3m : 0)
                    },
                });

            var productCosts = comparison.Evaluate(consumption);
            return Ok(productCosts);
        }
    }
}
