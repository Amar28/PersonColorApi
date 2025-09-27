namespace Application.Persons.DTOs
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string LastName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Color { get; set; } = null!;
    }
}
