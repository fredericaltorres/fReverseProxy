# fReverseProxy
Sample creating a .NET Core / C# Reverse Proxy with library [ProxyKit](https://github.com/damianh/ProxyKit)

## Example

### The project AWebApi implements the endpoint
Url: `https://localhost:44343/api/values2`

Response:
```JSON
[ "FRED","value2","7/17/2019","7/17/2019 10:36:42 PM" ]
```

### The project fReverseProxy implements the reverse proxy
Calling the following url `https://localhost:44338/api/values2` will 
1. Add the header "tutu": 'tutu-string'
1. Call the AWebApi endpoint Url: `https://localhost:44343/api/values2`
    - The tutu header value is detected by the endpoint and added to the list of values returned
1. Update JSON reponse and add the string "added-on-the-fly" to the list of values
1. Add the header "headerfinal": '123'

```JSON
[ "FRED","value2","7/17/2019","7/17/2019 10:39:24 PM","tutu-string","added-on-the-fly" ] 
```

Reverse proxy source code [Startup.cs](fReverseProxy/Startup.cs)