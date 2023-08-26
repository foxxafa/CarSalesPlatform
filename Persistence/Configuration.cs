using Microsoft.Extensions.Configuration;

namespace Persistence
{
    static class Configuration
    {
        public static string ConnectionString
        {
            get
            {
                ConfigurationManager configureationManager = new();
                configureationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../API"));
                configureationManager.AddJsonFile("appsettings.json");
                return configureationManager.GetConnectionString("VSSQLSERVER");
            }
        }
    }
}
