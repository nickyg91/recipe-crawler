// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;

Console.WriteLine("Hello, World!");

var configuration = new ConfigurationBuilder()
    .AddJsonFile($"");

configuration.Build();
    