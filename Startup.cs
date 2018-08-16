using Markdig;
using Markdig.Extensions.AutoIdentifiers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NormiesRe.Models;
using NormiesRe.Post;
using Westwind.AspNetCore.Markdown;

namespace NormiesRe
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
            services.AddMvc();
            
            services.AddDbContext<NormiesDbContext>(options =>
                options.UseSqlite("Data Source=NormiesRe.db"));

            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IPostListService, PostListService>();
            services.AddTransient<INewPostService, NewPostService>();
            services.AddTransient<IPostFindService, PostFindService>();
            services.AddTransient<IPostDeleteService, PostDeleteService>();
            services.AddTransient<INewCommentService, NewCommentService>();
            
            services.AddMarkdown(config =>
            {
                // Create custom MarkdigPipeline 
                // using MarkDig; for extension methods
                config.ConfigureMarkdigPipeline = builder =>
                {
                    builder.UseEmphasisExtras(Markdig.Extensions.EmphasisExtras.EmphasisExtraOptions.Default)
                        .UsePipeTables()
                        .UseGridTables()                        
                        .UseAutoIdentifiers(AutoIdentifierOptions.GitHub) 
                        .UseAutoLinks() 
                        .UseAbbreviations()
                        .UseYamlFrontMatter()
                        .UseEmojiAndSmiley(true)                        
                        .UseListExtras()
                        .UseFigures()
                        .UseTaskLists()
                        .UseCustomContainers()
                        .UseGenericAttributes()
                        .DisableHtml();
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
            
        }
    }
}
