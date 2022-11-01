namespace PersonManagerService.Domain.Models
{
    public class PersonSocialMediaAccount
    {
        public string Address { get; set; }
        public Person Person { get; set; }
        public Guid PersonId { get; set; }
        public SocialMediaAccount SocialMediaAccount { get; set; }
        public Guid SocialMediaAccountId { get; set; }
    }
}