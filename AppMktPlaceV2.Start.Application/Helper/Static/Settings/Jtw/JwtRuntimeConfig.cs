namespace AppMktPlaceV2.Start.Application.Helper.Static.Settings.Jtw
{
    public class JwtRuntimeConfig
    {
        public static string Secret { get; set; }
        public static string Audience { get; set; }
        public static string Issuer { get; set; }
        public static int RefreshTokenExpiryTimeInDay { get; set; }
        public static int ExpiresInHour { get; set; }
    }
}
