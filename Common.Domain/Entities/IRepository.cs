namespace Common.Domain.Entities
{
    interface IRepository<T> where T : AggregateRoot
    {
    }
}
