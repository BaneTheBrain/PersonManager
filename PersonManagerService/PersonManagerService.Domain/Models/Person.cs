namespace PersonManagerService.Domain.Models;

public class Person
{
    public Guid PersonId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<PersonSkill> PersonSkills { get; set; }
    public ICollection<PersonSocialMediaAccount> PersonSocialMediaAccounts { get; set; }
}