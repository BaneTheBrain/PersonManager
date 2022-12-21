namespace PersonManagerService.Domain.Models;

public class PersonSkillAccount
{
    public Guid PersonId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? AccountAddress { get; set; }
    public string? AccountType { get; set; }
    public Guid? AccountId { get; set; }
    public string? SkillName { get; set; }
}
