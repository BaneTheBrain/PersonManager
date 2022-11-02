namespace PersonManagerService.Domain.Models;

public class Person
{
    public Guid PersonId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public IEnumerable<PersonSkill> PersonSkills { get; set; }
    public IEnumerable<PersonSocialMediaAccount> PersonSocialMediaAccounts { get; set; }
}