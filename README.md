# Shop TDD and Mocking Dojo
Hi and welcome to the Shop Dojo. This Dojo shows how to start Test Driven Development by using NUnit, Moq and FluentAssertions. To get off running, after checkout, do the following:
* Open Solution in Visual Studio
* Call `Restore Nuget packages`
* Call `Run Unit Tests`
* Everything should be green - now follow the tasks 

## Scenario
Our shop is growing and so it is time to sell our products abroad. Therefore we allow customers to pay in different currencies. Depending on the currency the customer has to pay an extra fee on top of the actual price, which is defined by the sum of the products' total prices. In this scenario we don't care about summing up the prize, but on implementing the calculation of the extra fee and converting the price to the chosen currency.

## Task 1 - Currency Extra Fee PriceCalculation
Since we sell our products from Germany, no customer of the Euro currency area has to pay a fee. 
We charge a fee for every other country of 7%, but at least 10.00 EUR. There are some exceptional cases, that we have to consider:
* GBP: 5%, no minimum fee (British Pounds)
* CHF: 3%, no minimum fee (Swiss Francs)
* DKK: 4%, no minimum fee (Danish Crowns)
* USD: 6%, minimum fee of 8.00 EUR (US Dollars)
* CAD: 6%, minimum fee of 9.00 EUR (Canadian Dollars)

**Your task is to implement the business logic for calculating the fee.** At this point, do not care about converting the price from EUR to the customer's currency.

Use the *Test Driven Development* cycle:
1. Write a failing test
2. Make that test pass
3. Refactor the code

Therefore start by using the given `PriceCalculatorTest` class. Tip: Make usage of the `Currency` enum for determining, which currency is used to pay the bill. It is located in the `lise.dojo.shop.currency` dll, that is referenced by the solution.

Helpful links for this task:
* [NUnit Documentation](https://github.com/nunit/docs/wiki/NUnit-Documentation)
* [FluentAssertions Documentation](https://github.com/dennisdoomen/fluentassertions/wiki)

## Task 2 - Currency Conversion PriceCalculation
It is now time to calculate the price, the customer has to pay. As long as he wants to pay in his currency, we should convert the calculated price with fee to the correct currency. There are two alternative services, that provide conversion rates. Make our `PriceCalculator` depend on these services (you can choose one or maybe find a better solution). As long as we have no control of the results and availability of the services, your tests should not directly depend on them, but use mocks to fake their behavior.

**Your task is to implement the extended business logic for currency conversion.** Keep on TDD'ing and additionally use the `Moq` library to fake the behavior. Remember, the provided converters are blackboxes for you. You can't predict their behavior.

Helpful links for this task:
* [NUnit Documentation](https://github.com/nunit/docs/wiki/NUnit-Documentation)
* [FluentAssertions Documentation](https://github.com/dennisdoomen/fluentassertions/wiki)
* [Moq Documentation](https://github.com/Moq/moq4/wiki/Quickstart)

Used currency converters in `lise.dojo.shop.currency`:
* [Currency Layer Currency Converter](https://currencylayer.com/) (REST API - needs access key, has limited free plan)
* [KowaBunga Currency Converter](http://currencyconverter.kowabunga.net/converter.asmx) (SOAP API - free)

## Credits
written by 
* Martin Dieblich 
* [Steve Korzinetzki](https://twitter.com/skorzinetzki)
