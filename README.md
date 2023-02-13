BlazorKiteConnect
===============================

This application is a client for the Zerodha Kite trading API, built using Blazor WebAssembly and ASP.NET Core. It allows users to connect to their Zerodha trading account and access various trading functionalities, such as placing orders and retrieving market data. The application uses the [Mudblazor](https://github.com/MudBlazor/MudBlazor) library for UI components and the [FastEndpoint](https://github.com/FastEndpoints/FastEndpoints) library for connecting to the Zerodha Kite trading API.

Prerequisites
-------------

Before you get started, you will need:

-   A Zerodha trading account.
-   An API key for the Zerodha Kite trading API. You can obtain one by logging into your Zerodha account and navigating to the API section.
-   .NET Core 5.0 or later installed on your development machine.
-   A modern web browser, such as Google Chrome, Mozilla Firefox, or Microsoft Edge.

Getting Started
---------------

1.  Clone this repository to your development machine using Git.

bashCopy code

`git clone https://github.com/Gopichandar/BlazorKiteConnect.git`

1.  Open the solution file in Visual Studio or another .NET development environment.

2.  Replace [YOUR_API_KEY] and [YOUR_API_SECRET] with your actual Zerodha Kite trading API key and secret in the appsettings.json file. Note: The API secret should not be shared with anyone and should be kept secret.

3.  Build and run the application by pressing `F5` or using the "Run" menu.

4.  The application should now open in your web browser. You can log in using your Zerodha trading account credentials.

Features
--------

-   Connect to your Zerodha trading account using your API key.
-   Place orders and view the status of your open orders.
-   Retrieve market data, such as stock prices and trading volumes.

Contributing
------------

If you would like to contribute to this project, please submit a pull request with your changes.

License
-------

This project is licensed under the MIT License. See the [LICENSE](https://github.com/Gopichandar/BlazorKiteConnect/blob/main/LICENSE) file for details.

Acknowledgements
----------------

This project would not have been possible without the Zerodha Kite trading API. Thank you to the Zerodha team for providing this valuable resource to traders and developers.
