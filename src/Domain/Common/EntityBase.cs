namespace Domain.Common
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
    }
}
