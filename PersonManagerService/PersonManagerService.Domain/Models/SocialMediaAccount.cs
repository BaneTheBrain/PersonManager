namespace PersonManagerService.Domain.Models
{
    public class SocialMediaAccount
    {
        public Guid SocialMediaAccountId { get; set; }
        public string Type { get; set; }
        public IEnumerable<PersonSocialMediaAccount> PersonSocialMediaAccounts { get; set; }
    }
}