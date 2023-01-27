using IdentityModel.Client;
using Newtonsoft.Json;
using System.Text;

var client = new HttpClient();
var apiUrl = "https://localhost:5072";
var identityUrl = "https://localhost:5000";

var disco = await client.GetDiscoveryDocumentAsync(identityUrl);
if (disco == null)
{
    Console.WriteLine("Failed to get Discovery Document!");
    return;
}

var tokenReponse = await client.RequestTokenAsync(new TokenRequest
{
    Address = disco.TokenEndpoint,
    ClientId = "client",
    ClientSecret = "secret",
    Parameters =
    {
        { "scope", "bankOfDotNet" },
        { "grant_type", "client_credentials" }
    }
});

if (tokenReponse == null)
{
    Console.WriteLine("Failed to get access token!");
    return;
}
Console.WriteLine("-------------Printing Token-------------");
Console.WriteLine(tokenReponse.AccessToken);
Console.WriteLine("----------------------------------------");

//set token to client
client.SetBearerToken(tokenReponse.AccessToken);

//create customer
var customer = new StringContent(JsonConvert.SerializeObject(new
{
    FirstName = "Ahmed",
    LastName = "Mohammed"
}), Encoding.UTF8, "application/json");

var createCustomerResponse = await client.PostAsync($"{apiUrl}/api/customers", customer);
if (!createCustomerResponse.IsSuccessStatusCode)
{
    Console.WriteLine($"Failed to create customer {createCustomerResponse.StatusCode}");
    return;
}

var getAllCustomersResponse = await client.GetAsync($"{apiUrl}/api/customers");
if (!getAllCustomersResponse.IsSuccessStatusCode)
{
    Console.WriteLine($"Failed to get customers {getAllCustomersResponse.StatusCode}");
    return;
}

Console.WriteLine("-------------Printing All Customers-------------");
Console.WriteLine(await getAllCustomersResponse.Content.ReadAsStringAsync());
Console.WriteLine("------------------------------------------------");
