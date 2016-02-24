namespace Neighbours.Data.Models
{
    public class District
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual City City { get; set; }
    }
}