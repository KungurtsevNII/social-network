namespace SharedKernel;

public abstract class Entity<T>
{
    public T Id { get; private set; }
    
    protected Entity(T id)
    {
        Id = id;
    }
}