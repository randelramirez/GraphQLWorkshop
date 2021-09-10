using GraphQL.Data;
using GraphQL.Extensions;
using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GraphQL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<DataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(nameof(DataContext))));

            services.AddGraphQLServer()
                .AddFiltering()
                .AddSorting()
                .RegisterApplicationTypes()
                .RegisterApplicationDataLoaders()
                .RegisterApplicationQueries()
                .RegisterApplicationMutations()
                .RegisterApplicationSubscriptions();
            
            // services.AddDbContext<DataContext>(options => options.UseSqlite("Data Source=conferences.db"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });

            app.UseGraphQLVoyager(new VoyagerOptions()
            {
                GraphQLEndPoint = "/graphql",
            },path: "/graphql-voyager");
        }
    }
}