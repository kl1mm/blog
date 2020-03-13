using FluentValidation;
using kli.Blog.Core.Behaviours;
using kli.Blog.Core.Settings;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace kli.Blog.Core
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
		{
			if (services == null)
				throw new ArgumentNullException(nameof(services));

			services.Configure<AppSettings>(configuration, cb => cb.BindNonPublicProperties = true); 
			services.AddValidators();
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
			services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);

			return services;
		}

		private static IServiceCollection AddValidators(this IServiceCollection services)
		{
			foreach(var serviceDescriptor in CollectServiceDescriptorsForInterface(typeof(IValidator<>)))
				services.Add(serviceDescriptor);
			
			return services;
		}

		private static IEnumerable<ServiceDescriptor> CollectServiceDescriptorsForInterface(Type openGenericType)
		{
			return from type in typeof(ServiceCollectionExtensions).Assembly.GetTypes()
				   where !type.IsAbstract && !type.IsGenericTypeDefinition
				   let interfaces = type.GetInterfaces()
				   let genericInterfaces = interfaces.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == openGenericType)
				   let matchingInterface = genericInterfaces.FirstOrDefault()
				   where matchingInterface != null
				   select ServiceDescriptor.Transient(matchingInterface, type);
		}
	}
}
