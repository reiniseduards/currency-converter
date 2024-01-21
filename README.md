Currency Converter Application Readme

Overview
This repository contains a simple Currency Converter web application implemented using ASP.NET MVC. The application allows users to convert an amount from one currency to another using real-time exchange rates from a configured API.

The repository includes the following components:

1. Controller: HomeController.cs - Manages the application's logic, including handling user input, calling the external exchange rate API, and updating the view.

2. View: Index.cshtml - The main view for the Currency Converter, providing a form for users to input the amount and select the source and target currencies. The result is displayed below the form.

3. JavaScript: submitExchangeForm.js - Contains client-side logic for handling form submission, making AJAX requests to the server, and updating the view based on the API response.

4. Models:
Currency.cs - Represents a currency with properties for its symbol and name. The GetCurrencies method provides a dictionary of common currencies.
Response.cs - Represents the JSON response from the external exchange rate API. It includes properties corresponding to different fields in the API response.

5. Tests: HomeControllerTests.cs - Unit tests for the HomeController class, ensuring the proper functioning of the application logic.

How to Run
To run the Currency Converter application, follow these steps:

1.Ensure you have the necessary dependencies installed, including ASP.NET MVC and jQuery.

2.Clone the repository to your local machine.
git clone https://github.com/your-username/CurrencyConverter.git

3.Open the solution in Visual Studio or your preferred IDE.

4.Build and run the application.

5.Access the application through a web browser at http://localhost:your-port-number/ (the specific port number may vary).

Usage
1. Open the Currency Converter application in your web browser.

2. Enter the amount you want to convert, select the source currency, and choose the target currency.

3. Click the "Convert" button.

4. View the result, which includes the converted amount and the exchange rate.

Models
1. Currency Model (Currency.cs)
Represents a currency with properties for its symbol and name. The GetCurrencies method provides a dictionary of common currencies.

2. Response Model (Response.cs)
Represents the JSON response from the external exchange rate API. It includes properties corresponding to different fields in the API response.

Testing
The application includes unit tests to ensure the correctness of the HomeController class. These tests use the MSTest framework and Moq library for mocking dependencies.

To run the tests:

1. Open the HomeControllerTests.cs file in your IDE.

2. Build and run the tests using the testing tools provided by your IDE.

3. Verify that all tests pass, ensuring the proper functionality of the TryConvert method for both valid and invalid inputs.

Dependencies
The application uses the following dependencies:

ASP.NET MVC
jQuery

Ensure that these dependencies are included and up-to-date.

Additional Notes
The application provides error handling for cases where the API request fails or an exception occurs. Users will see an error message in such cases.

The client-side validation ensures that users enter a valid amount in the correct format before submitting the form.

The application is extensible, and additional features or improvements can be made by extending the existing codebase.
