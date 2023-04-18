using Swashbuckle.AspNetCore.SwaggerUI;

namespace RealState.WebAPI.Extentions
{
    public static class AppExtension
    {
        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "RealState API");
                options.DefaultModelRendering(ModelRendering.Model);

            });
        }
    }
}
