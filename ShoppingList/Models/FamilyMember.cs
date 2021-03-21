namespace ShoppingList.Models
{
    public class FamilyMember
    {
        public string Id { get; set; }
        public int FamilyId { get; set; }
        public Family Family { get; set; }
    }
}