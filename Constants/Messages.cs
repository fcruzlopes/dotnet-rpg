namespace dotnet_rpg.Constants
{
    public class Messages
    {
        private Messages()
        {
            //This class must not be instantiated
            //Constant messages class   
        }

        public static string InvalidCredentials = "The username or password entered are wrong|";
        public static string UserAlreadyExists = "The user entered is already registered|";
        public static string IdNotFound = "The given id of the object was not found|"; 
        public static string DotNetRpg = "Something went wrong|";
    }
}