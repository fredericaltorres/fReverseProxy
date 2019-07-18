# fReverseProxy
Sample creating a .NET Core / C# Reverse Proxy with library ProxyKit

## Example

### The project AWebApi implements the endpoint
Url: `https://localhost:44343/api/values2`

```JSON
["FRED","value2","7/17/2019","7/17/2019 10:36:42 PM"]
```


### The project fReverseProxy implement the reverse proxy
Calling the following url `https://localhost:44338/api/values2` will 
1. Add the header "tutu": 'tutu-string'
2. Call AWebApi endpoint Url: `https://localhost:44343/api/values2`
    - The tutu header value is detected by the endpoint and added to the list of value returned
3. Update to JSON and add the string "added-on-the-fly"
4. Add the header "headerfinal": '123'

```JSON
["FRED","value2","7/17/2019","7/17/2019 10:39:24 PM","tutu-string", "added-on-the-fly" ] 
```
