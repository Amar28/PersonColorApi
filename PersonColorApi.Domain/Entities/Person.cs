using System;

namespace PersonColorApi.Domain.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string LastName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Color { get; set; } = null!;
    }
}
