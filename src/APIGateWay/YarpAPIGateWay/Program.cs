namespace YarpAPIGateWay
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
            //Add services to the container

            var app = builder.Build();

            // We are mapping the reverse proxy here
            app.MapReverseProxy();

            app.Run();
        }
    }
}
