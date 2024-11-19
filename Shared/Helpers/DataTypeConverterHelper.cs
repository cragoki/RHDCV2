namespace Shared.Helpers
{
    public static class DataTypeConverterHelper
    {
        public static int StringToInt(string value) 
        {
            var result = 0;

            if (Int32.TryParse(value, out int valueInt))
            { 
                result = valueInt;
            }

            return result;
        }

        public static decimal StringToDecimal(string value)
        {
            decimal result = 0;

            if (Decimal.TryParse(value, out decimal valueDecimal))
            {
                result = valueDecimal;
            }

            return result;
        }

        public static decimal ConvertFractionalToDecimalOdds(string fractionalOdds)
        {
            if (string.IsNullOrWhiteSpace(fractionalOdds))
                return 0;

            if (fractionalOdds == "evens")
                return 2;
            var parts = fractionalOdds.Split('/');
            if (parts.Length != 2)
                throw new ArgumentException("Invalid fractional odds format. Expected format: 'a/b'.");

            if (decimal.TryParse(parts[0], out decimal numerator) &&
                decimal.TryParse(parts[1], out decimal denominator) &&
                denominator != 0)
            {
                return (numerator / denominator) + 1;
            }

            return 0;
        }

    }
}
