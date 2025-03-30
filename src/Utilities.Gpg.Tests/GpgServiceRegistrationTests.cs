using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Utilities.Gpg.MediatR;

namespace Utilities.Gpg.Tests;

public class GpgServiceRegistrationTests
{
	private readonly IConfiguration _configuration = new ConfigurationBuilder()
		.AddInMemoryCollection(new Dictionary<string, string?>
		{
			{ "Gpg:KeyFolderPath", "Value1" }
		})
		.Build();

	[Fact]
	public void AddGpgServices_RegistersAllServices_CorrectlyResolvesTypes()
	{
		// Arrange
		ServiceCollection services = new();
		IConfiguration configuration = _configuration;
		services.AddSingleton(configuration);

		// Act
		_ = services.AddGpgServices();
		ServiceProvider serviceProvider = services.BuildServiceProvider();

		IMediator? mediator = serviceProvider.GetService<IMediator>();
		IGpg? gpg = serviceProvider.GetService<IGpg>();

		// Assert
		Assert.NotNull(mediator);
		_ = Assert.IsType<Mediator>(mediator);

		Assert.NotNull(gpg);
		_ = Assert.IsType<Gpg>(gpg);
	}

	[Fact]
	public void AddGpgServices_ReturnsServiceCollection()
	{
		// Arrange
		ServiceCollection services = new();
		IConfiguration configuration = _configuration;
		services.AddSingleton(configuration);

		// Act
		IServiceCollection result = services.AddGpgServices();

		// Assert
		Assert.Same(services, result); // Ensures the method returns the same IServiceCollection
	}

	[Fact]
	public void AddGpgServices_ScopedLifetime_VerifyInstanceWithinScope()
	{
		// Arrange
		ServiceCollection services = new();
		IConfiguration configuration = _configuration;
		services.AddSingleton(configuration);

		// Act
		_ = services.AddGpgServices();
		ServiceProvider serviceProvider = services.BuildServiceProvider();

		// Assert
		using IServiceScope scope = serviceProvider.CreateScope();
		IMediator? service1 = scope.ServiceProvider.GetService<IMediator>();
		IMediator? service2 = scope.ServiceProvider.GetService<IMediator>();
		IGpg? service3 = scope.ServiceProvider.GetService<IGpg>();
		IGpg? service4 = scope.ServiceProvider.GetService<IGpg>();

		Assert.NotSame(service1, service2);
		Assert.Same(service3, service4);
	}

	[Fact]
	public void AddGpgServices_ScopedLifetime_VerifyInstancesAcrossScopes()
	{
		// Arrange
		ServiceCollection services = new();
		IConfiguration configuration = _configuration;
		services.AddSingleton(configuration);

		// Act
		_ = services.AddGpgServices();
		ServiceProvider serviceProvider = services.BuildServiceProvider();

		// Assert
		IMediator? service1, service2;
		IGpg? service3, service4;
		using (IServiceScope scope1 = serviceProvider.CreateScope())
		{
			service1 = scope1.ServiceProvider.GetService<IMediator>();
			service3 = scope1.ServiceProvider.GetService<IGpg>();
		}

		using (IServiceScope scope2 = serviceProvider.CreateScope())
		{
			service2 = scope2.ServiceProvider.GetService<IMediator>();
			service4 = scope2.ServiceProvider.GetService<IGpg>();
		}

		Assert.NotSame(service1, service2);
		Assert.Same(service3, service4);
	}

	[Fact]
	public void AddGpgServices_DecryptFileCommandHandler_VerifyMediatorHandlerExists()
	{
		// Arrange
		ServiceCollection services = new();

		// Act
		_ = services.AddGpgServices();
		List<ServiceDescriptor> serviceDescriptors = services.ToList();

		// Assert
		ServiceDescriptor? handlerDescriptor = serviceDescriptors.FirstOrDefault(sd =>
			sd.ServiceType == typeof(IRequestHandler<DecryptFileCommand>));

		Assert.NotNull(handlerDescriptor);
		Assert.Equal(ServiceLifetime.Transient, handlerDescriptor.Lifetime);
	}

	[Fact]
	public void AddGpgServices_EncryptFileCommandHandler_VerifyMediatorHandlerExists()
	{
		// Arrange
		ServiceCollection services = new();

		// Act
		_ = services.AddGpgServices();
		List<ServiceDescriptor> serviceDescriptors = services.ToList();

		// Assert
		ServiceDescriptor? handlerDescriptor = serviceDescriptors.FirstOrDefault(sd =>
			sd.ServiceType == typeof(IRequestHandler<EncryptFileCommand>));

		Assert.NotNull(handlerDescriptor);
		Assert.Equal(ServiceLifetime.Transient, handlerDescriptor.Lifetime);
	}
}
