namespace CarPool
{
    public class Configuration
    {
        public static string key = "superSecretKey@345";
        public static string Issuer = "https://localhost:7107";
        public static string Audience = "https://localhost:7107";

        public static string swaggerDocTitle = "Carpool";
        public static string swaggerDocVersion = "v1";
        public static string swaggerAuthSchema = "Bearer";
        public static string swaggerAuthName = "Authorization";
        public static string swaggerBearerFormate = "JWT";
        public static string swaggerAuthDescription = "JWT Authorization header using the Bearer scheme.";
    }
}
