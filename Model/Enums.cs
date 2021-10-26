using System.ComponentModel;

namespace Model
{
    public class Enums
    {
        public enum eTariffType
        {
            [Description("Basic electricity tariff")]
            Basic = 1,
            [Description("Packaged tariff")]
            Packaged = 2
        }
    }
}
