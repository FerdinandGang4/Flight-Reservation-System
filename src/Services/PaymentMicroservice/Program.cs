namespace PaymentMicroservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

       

            var app = builder.Build();

            // Configure the HTTP request pipeline.

         
            app.Run();
        }
    }
}
