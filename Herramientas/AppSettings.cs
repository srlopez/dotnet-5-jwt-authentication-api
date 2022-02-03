namespace WebApi.Settings
{
    // Utilizamos esta clase para mapear la configuración
    // en la seccion Appsettings de appsetings.json
    // Se carga en startup.cs
    // services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
    // Y se utiliza como cualquier servicio

    public class AppSettings
    {
        public string Secret { get; set; }
    }
}