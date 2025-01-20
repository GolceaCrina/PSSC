using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PSSC_Proiect.Api.Messaging;

public static class RabbitMqHelper
{
    public static async Task<IConnection> CreateConnectionAsync()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost", // Adresa serverului RabbitMQ
            UserName = "guest",    // Utilizator implicit
            Password = "guest",    // Parolă implicită
            AutomaticRecoveryEnabled = true, // Activează recuperarea automată
            NetworkRecoveryInterval = TimeSpan.FromSeconds(10) // Intervalul pentru reconectare
        };

        try
        {
            // Creează conexiunea asincron
            var connection = await factory.CreateConnectionAsync(new List<string> { "localhost" }, "PSSC_Proiect_Connection");
            Console.WriteLine("RabbitMQ connection created successfully.");
            return connection;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating RabbitMQ connection: {ex.Message}");
            throw;
        }
    }
}