using lise.dojo.shop.currency;

namespace lise.dojo.shop
{
    public class PriceCalculator
    {
        private const decimal NoMinimumFee = 0m;

        private const decimal EURFeePercentage = 0m;

        private const decimal NotEURMinimumFee = 10m;
        private const decimal NotEURFeePercentage = 0.07m;

        private const decimal GBPFeePercentage = 0.05m;
        private const decimal CHFFeePercentage = 0.03m;
        private const decimal DKKFeePercentage = 0.04m;

        private const decimal USDMinimumFee = 8m;
        private const decimal USDFeePercentage = 0.06m;

        private const decimal CADMinimumFee = 9m;
        private const decimal CADFeePercentage = 0.06m;

        private decimal _feePercentage;
        private decimal _minimumFee;
        private readonly ICurrencyConverter _currencyConverter;

        public PriceCalculator(ICurrencyConverter currencyConverter)
        {
            _currencyConverter = currencyConverter;
        }

        public decimal CalculateExtraFee(decimal price, Currency toCurrency)
        {
            if (price < 0m)
            {
                throw new PriceCalculationException("The price must not be negative!");
            }

            SetMinimumFeeAndFeePercentage(toCurrency);
            return CalculateExtraFeeFor(price);
        }

        private decimal CalculateExtraFeeFor(decimal price)
        {
            var fee = price*_feePercentage;
            return fee > _minimumFee ? fee : _minimumFee;
        }

        private void SetMinimumFeeAndFeePercentage(Currency toCurrency)
        {
            switch (toCurrency)
            {
                case Currency.EUR:
                    _feePercentage = EURFeePercentage;
                    _minimumFee = NoMinimumFee;
                    break;
                case Currency.GBP:
                    _feePercentage = GBPFeePercentage;
                    _minimumFee = NoMinimumFee;
                    break;
                case Currency.CHF:
                    _feePercentage = CHFFeePercentage;
                    _minimumFee = NoMinimumFee;
                    break;
                case Currency.DKK:
                    _feePercentage = DKKFeePercentage;
                    _minimumFee = NoMinimumFee;
                    break;
                case Currency.USD:
                    _feePercentage = USDFeePercentage;
                    _minimumFee = USDMinimumFee;
                    break;
                case Currency.CAD:
                    _feePercentage = CADFeePercentage;
                    _minimumFee = CADMinimumFee;
                    break;
                default:
                    _feePercentage = NotEURFeePercentage;
                    _minimumFee = NotEURMinimumFee;
                    break;
            }
        }

        public decimal RetrieveConversionRate(Currency toCurrency)
        {
            return _currencyConverter.GetCurrentConversionRate(toCurrency);
        }

        public decimal CalculateFinalPrice(decimal price, Currency toCurrency)
        {
            throw new System.NotImplementedException();
        }
    }
}