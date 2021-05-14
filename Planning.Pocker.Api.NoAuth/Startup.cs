using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Planning.Pocker.Api.NoAuth.Behavior;
using Planning.Pocker.Api.NoAuth.Data;
using Planning.Pocker.Api.NoAuth.Handlers;

namespace Planning.Pocker.Api.NoAuth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));

            services.AddMediatR(typeof(Startup));

            //services.AddScoped(typeof(LoggingBehavior<,>), typeof(IPipelineBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateCartaValidator>());
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Planning.Pocker.Api.NoAuth", Version = "v1" });
            });

            services.AddDbContext<ApiDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //var container = new ContainerBuilder();
            //container.Populate(services);
            //container.RegisterModule(new MediatorModule());
            //return new AutofacServiceProvider(container.Build());

            //var builder = new ContainerBuilder();
            //builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            //var mediatrOpenTypes = new[]
            //{
            //    typeof(IRequestHandler<,>),
            //    typeof(IRequestExceptionHandler<,,>),
            //    typeof(IRequestExceptionAction<,>),
            //    typeof(INotificationHandler<>),
            //};
            //foreach (var mediatrOpenType in mediatrOpenTypes)
            //{
            //    builder
            //        .RegisterAssemblyTypes(typeof(BaseHandler).GetTypeInfo().Assembly)
            //        .AsClosedTypesOf(mediatrOpenType)
            //        .AsImplementedInterfaces();
            //}
            //builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            //builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            //builder.RegisterGeneric(typeof(RequestExceptionActionProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            //builder.RegisterGeneric(typeof(RequestExceptionProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            ////builder.RegisterGeneric(typeof(GenericRequestPreProcessor<>)).As(typeof(IRequestPreProcessor<>));
            ////builder.RegisterGeneric(typeof(GenericRequestPostProcessor<,>)).As(typeof(IRequestPostProcessor<,>));
            ////builder.RegisterGeneric(typeof(GenericPipelineBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            ////builder.RegisterGeneric(typeof(ConstrainedRequestPostProcessor<,>)).As(typeof(IRequestPostProcessor<,>));
            ////builder.RegisterGeneric(typeof(ConstrainedPingedHandler<>)).As(typeof(INotificationHandler<>));
            //builder.Register<ServiceFactory>(ctx =>
            //{
            //    var c = ctx.Resolve<IComponentContext>();
            //    return t => c.Resolve(t);
            //});
            //var container = builder.Build();
            //builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
            //    .AsImplementedInterfaces();
            ////var mediator = container.Resolve<IMediator>();
            //return new AutofacServiceProvider(builder.Build()); //return mediator;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Planning.Pocker.Api.NoAuth v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
