namespace dotnet_rpg.Models
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        //With the name Character and ID, Entity Framework knows that this should be a Foreign Key
        public int CharacterId { get; set; }
        public Character Character { get; set; }
    }
}