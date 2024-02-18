using Marvel.API.InputModels;

namespace Marvel.API.Utils
{
    public static class MarvelApiAuthenticator
    {
        private static readonly IConfiguration Configuration;

        static MarvelApiAuthenticator()
        {
            // Configurar o ambiente (produção, desenvolvimento, etc.)
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            // Inicializar a configuração com base no ambiente
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public static RequestApiParameter GenerateApiParameters(int limit = 20, int offset = 0, string? name = null)
        {
            var ts = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var publicKey = Configuration["MarvelApiSettings:PublicKey"];
            var privateKey = Configuration["MarvelApiSettings:PrivateKey"];
            var parameters = ts + privateKey + publicKey;
            var hash = parameters.ToMD5Hash();

            return new RequestApiParameter
            {
                ApiKey = publicKey!,
                Hash = hash,
                Ts = ts,
                Limit = limit,
                Offset = offset,
                Name = name
            };
        }
    }
}
