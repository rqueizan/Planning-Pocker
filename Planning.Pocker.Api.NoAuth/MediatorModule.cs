using Autofac;
using FluentValidation;
using MediatR;
using Planning.Pocker.Api.NoAuth.Behavior;
using Planning.Pocker.Api.NoAuth.Handlers;
using System.Reflection;
using Module = Autofac.Module;

namespace Planning.Pocker.Api.NoAuth
{
    public class MediatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(BaseHandler).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(CreateCartaCommand).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IValidator<>));
            builder.Register<ServiceFactory>(defaultResolveRequestContext =>
            {
                var lifetimeScope = defaultResolveRequestContext.Resolve<IComponentContext>();
                return runtimeType => lifetimeScope.Resolve(runtimeType);
            });
            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}
