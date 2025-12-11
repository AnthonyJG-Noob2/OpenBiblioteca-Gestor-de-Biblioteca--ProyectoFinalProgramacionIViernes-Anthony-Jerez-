namespace Biblioteca_Domain.Entities
{
    public abstract class EntityBase
    {
        public int Id { get;  set; }
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; protected set; }

        protected void Touch() => UpdatedAt = DateTime.UtcNow;
    }
}
