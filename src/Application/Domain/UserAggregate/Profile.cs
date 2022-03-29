using SharedKernel;

namespace Domain.UserAggregate;

public sealed class Profile : Entity<long>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? MiddleName { get; private set; }
    public int Age { get; private set; }
    public Sex Sex { get; private set; }
    public string? Interests { get; private set; }
    public string City { get; private set; }
    
    public Profile(
        long userId,
        string firstName, 
        string lastName, 
        string? middleName,
        int age, 
        Sex sex,
        string? interests, 
        string city) : base(userId)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        Age = age;
        Sex = sex;
        Interests = interests;
        City = city;
    }

    public static Profile Create(
        long userId,
        string firstName,
        string lastName,
        string? middleName,
        int age, Sex sex,
        string? interests,
        string city)
    {
        return new Profile(
            userId,
            firstName,
            lastName,
            middleName,
            age,
            sex,
            interests,
            city);
    }
}
