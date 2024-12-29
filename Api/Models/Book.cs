namespace LibraryManagementApi.Models
{
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string Description{ get; set; }
        public required int PublishedYear { get; set; }
    }
}
