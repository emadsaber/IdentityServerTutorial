using IdentityServer4.Models;
using IdentityServer4.Test;

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
                },
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { "bankOfDotNet" },
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    }
                },

            };
        }

        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId= "1",
                    Username= "test1",
                    Password= "P@ssw0rd",
                },
                new TestUser
                {
                    SubjectId= "2",
                    Username= "test2",
                    Password= "P@ssw0rd",
                }
            };
        }
    }
}