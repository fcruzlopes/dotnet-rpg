namespace dotnet_rpg.Constants
{
    public class Messages
    {
        private Messages()
        {
            //This class must not be instantiated
            //Constant messages class   
        }

        public static string InvalidCredentials = "The username or password entered are wrong";
        public static string UserAlreadyExists = "The user entered is already registered";
        public static string IdNotFound = "The given id of the object was not found";
        public static string CharacterNotFound = "The given character was not found"; 
        public static string DotNetRpg = "Something went wrong|";
        public static string WeaponAlreadyExists = "The given character already has a weapon";
        public static string SkillNotFound = "Skill not found";
        public static string CharacterDefeated = "Character Defeated";
    }
}