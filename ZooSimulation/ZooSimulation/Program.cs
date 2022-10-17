// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ZooSimulation;
using ZooSimulation.Clocks;
using ZooSimulation.Interfaces;

var host = CreateHostBuilder(args).Build();
var provider = host.Services.CreateScope().ServiceProvider;

var zooApp = provider.GetRequiredService<ZooApp>();
zooApp.Run();

static IHostBuilder CreateHostBuilder(string[] args)
{
	return Host.CreateDefaultBuilder(args)
		.ConfigureServices((context, services) =>
		{
			services.AddScoped<Zoo>();
			services.AddScoped<IClock, SimulationClock>();
			services.AddScoped<INutritionProvider, NutritionProvider>();
			services.AddScoped<System.Timers.Timer>();
			services.AddScoped<IRandomNumberGenerator, RandomNumberGenerator>();
			services.AddScoped<IAnimalFactory, AnimalFactory>();
			services.AddScoped<IUserInterface, ZooConsoleUserInterface>();
			services.AddScoped<ZooApp>();
		});
}

