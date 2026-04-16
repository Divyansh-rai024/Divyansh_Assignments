
using Microsoft.Azure.Cosmos;

namespace QrSecureApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<CosmosDbService>(sp =>
            {
                var config = builder.Configuration;

                var endpoint = config["CosmosDb:Endpoint"]!;
                var key = config["CosmosDb:PrimaryKey"]!;
                var db = config["CosmosDb:DatabaseName"]!;
                var container = config["CosmosDb:ContainerName"]!;

                var client = new CosmosClient(endpoint, key);

                return new CosmosDbService(client, db, container);
            });

            builder.Services.AddSingleton<KeyVaultService>(sp =>
            {
                var config = builder.Configuration;

                var vaultUrl = config["KeyVault:VaultUrl"]!;
                var keyName = config["KeyVault:KeyName"]!;

                return new KeyVaultService(vaultUrl, keyName);
            });
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.WebHost.UseUrls("http://0.0.0.0:7080");
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
