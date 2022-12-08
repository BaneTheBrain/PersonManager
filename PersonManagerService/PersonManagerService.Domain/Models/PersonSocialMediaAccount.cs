namespace PersonManagerService.Domain.Models
{
    public class PersonSocialMediaAccount
    {
        public Guid PersonSocialMediaAccountId { get; set; }
        public string Address { get; set; }
        public Person Person { get; set; }
        public Guid PersonId { get; set; }
        public SocialMediaAccount SocialMediaAccount { get; set; }
        public Guid SocialMediaAccountId { get; set; }
    }
}