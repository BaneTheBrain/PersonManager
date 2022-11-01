namespace PersonManagerService.Domain.Models
{
    public class SocialMediaAccount
    {
        public Guid SocialMediaAccountId { get; set; }
        public string Type { get; set; }
        public ICollection<PersonSocialMediaAccount> PersonSocialMediaAccounts { get; set; }
    }
}