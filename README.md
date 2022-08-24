# CoinspotAPI.  A simple .NET SDK for accessing the Coinspot REST API

A .Net 4.5.2 example SDK library for interacting with Coinspot REST API, written in Visual Basic

A simple SDK/API client developed to assist people understanding the requirements of the Coinspot API.

Includes handler library and test console app to show usage.  Test app implements all of the read-only endpoints provided by Coinspot, and provides a manual processor to allow the user to manually specify an endpoint and parameter string for more detailed testing.

A good starter to show what the Coinspot API requires for a successful connection since their documentation is very light.

## Getting Started

1. Simply download or pull the CoinspotAPI.dll file from the release folder, or pull the whole project and build yourself.
2. Include the library in your project
3. (if using the test console app you need to update the key and secret in the app.config before build or if not building and simply running from my build add key and secret in CoinspotAPITest.exe.config in release folder)

### Example Usage with Parameter
```
Dim CoinType= "BTC"
Dim EndpointURL = "/api/ro/my/balances/" & CoinType
Dim JSONParameters = "{""cointype"":""" & CoinType & """}"
Dim CS = New CoinspotAPI.CoinspotAPIHandler(key, secret)
Dim CSResponse = CS.CallAPI(EndPointURL, JSONParameters)
```

