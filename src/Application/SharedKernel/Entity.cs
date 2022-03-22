namespace SharedKernel;

public abstract class Entity<T>
{
    protected T Id { get; private set; }
    
    protected Entity(T id)
    {
        Id = id;
    }
}