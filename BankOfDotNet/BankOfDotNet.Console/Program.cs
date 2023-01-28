using IdentityModel.Client;
using Newtonsoft.Json;
using System.Text;

internal class Program
{
    private static string apiUrl = "https://localhost:5072";
    private static string identityUrl = "https://localhost:5000";

    private static async Task Main(string[] args)
    {
        var client = await GetResourceOwnerHttpClient();
        //var client = await GetClientCredentialsHttpClient();

        //create customer
        var customer = new StringContent(JsonConvert.SerializeObject(new
        {
            FirstName = $"Ahmed",
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
    }

    private static async Task<HttpClient?> GetClientCredentialsHttpClient()
    {
        var client = new HttpClient();

        var disco = await client.GetDiscoveryDocumentAsync(identityUrl);
        if (disco == null)
        {
            Console.WriteLine("Failed to get Discovery Document!");
            return null;
        }

        var tokenReponse = await client.RequestTokenAsync(new IdentityModel.Client.TokenRequest
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
            return null;
        }
        Console.WriteLine("-------------Printing Token-------------");
        Console.WriteLine(tokenReponse.AccessToken);
        Console.WriteLine("----------------------------------------");

        //set token to client
        client.SetBearerToken(tokenReponse.AccessToken);

        return client;
    }

    private static async Task<HttpClient?> GetResourceOwnerHttpClient()
    {
        var client = new HttpClient();

        var disco = await client.GetDiscoveryDocumentAsync(identityUrl);
        if (disco == null)
        {
            Console.WriteLine("Failed to get Discovery Document!");
            return null;
        }

        var tokenReponse = await client.RequestPasswordTokenAsync(
                new PasswordTokenRequest
                {
                    ClientId = "ro.client",
                    ClientSecret = "secret",
                    UserName = "test1",
                    Password = "P@ssw0rd",
                    Address = disco.TokenEndpoint,
                }
            );

        if (tokenReponse == null)
        {
            Console.WriteLine("Failed to get access token!");
            return null;
        }
        Console.WriteLine("-------------Printing Token-------------");
        Console.WriteLine(tokenReponse.AccessToken);
        Console.WriteLine("----------------------------------------");

        //set token to client
        client.SetBearerToken(tokenReponse.AccessToken);

        return client;
    }
}