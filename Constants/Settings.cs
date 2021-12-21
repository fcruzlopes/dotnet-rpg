namespace dotnet_rpg.Constants
{
    public class Settings
    {
        private Settings()
        {
            //This class must not be instantiated
        }

        public static string AppsettingsFile = "appsettings.json";
        public static string DbConnectionName = "DotnetRPGConnection";
        public static string ProgramVersion = "v2.0.1";        
    }
}