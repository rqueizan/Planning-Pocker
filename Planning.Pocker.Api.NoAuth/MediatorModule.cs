using Autofac;
using FluentValidation;
using MediatR;
using Planning.Pocker.Api.NoAuth.Behavior;
using Planning.Pocker.Api.NoAuth.Handlers;
using System.Linq;
using System.Reflection;
using Module = Autofac.Module;

namespace Planning.Pocker.Api.NoAuth
{
    public class MediatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(BaseHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.RegisterAssemblyTypes(typeof(BaseHandler).GetTypeInfo().Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();

            //builder.Register<SingleInstanceFactory>(context =>
            //{
            //    var componentContext = context.Resolve<IComponentContext>();
            //    return t =>
            //    {
            //        object o;
            //        return componentContext.TryResolve(t, out o) ? o : null;
            //    };
            //});

            //builder.Register<MultiInstanceFactory>(context =>
            //{
            //    var componentContext = context.Resolve<IComponentContext>();
            //    return t =>
            //    {
            //        var resolved =
            //            (IEnumerable<object>)componentContext.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            //        return resolved;
            //    };
            //});

            //builder.Register<ServiceFactory>(ctx =>
            //{
            //    var c = ctx.Resolve<IComponentContext>();
            //    return t => c.Resolve(t);
            //});

            //builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}
