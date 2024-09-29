using SharedKernal;

namespace ecms.Domain.ValueObjects;

public sealed record ContactInfo
{
    public string RepresentativeName { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string CompanyName { get; set; }

    public string Description { get; set; }

    public ContactInfo(string representativeName, string phoneNumber, string email, string companyName, string description)
    {
        Ensure.NotNullOrEmpty(representativeName);
        Ensure.NotNullOrEmpty(phoneNumber);
        Ensure.NotNullOrEmpty(email);
        Ensure.NotNullOrEmpty(companyName);
        Ensure.NotNullOrEmpty(description);
        RepresentativeName = representativeName;
        PhoneNumber = phoneNumber;
        Email = email;
        CompanyName = companyName;
        Description = description;
    }

    public override string ToString()
    {
        return $"{RepresentativeName}, {PhoneNumber}, {Email}, {CompanyName}, {Description}";
    }
}
