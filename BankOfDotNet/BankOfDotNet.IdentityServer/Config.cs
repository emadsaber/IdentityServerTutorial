using IdentityServer4.Models;

namespace BankOfDotNet.IdentityServer
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("bankOfDotNet", "A Sample for Identity Server with .NET")
            };
        }

        internal static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope> {
               new ApiScope("bankOfDotNet")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "bankOfDotNet" },
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    }
                }
            };
        }
    }
}