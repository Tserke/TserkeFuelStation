// See https://aka.ms/new-console-template for more information
using FuelStation.Model;
using System.Net.Http;
using System.Net.Http.Json;

Console.WriteLine("Hello, World!");

TransactionLine line = new TransactionLine();
line.TransactionId = 1;
line.ItemId = 1;
line.Quantity = 1m;
line.ItemPrice = 50m;
line.NetValue = 50m;
line.DiscountPercent = 10;
line.DiscountValue = 5m;
line.TotalValue = 45m;
var httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("https://localhost:7069/");
var response = await httpClient.PostAsJsonAsync("transactionLine", line);