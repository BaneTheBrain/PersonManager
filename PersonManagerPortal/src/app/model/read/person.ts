
export interface PersonResponse
{
    personId : string
    firstName : string
    lastName : string
    vovels : number
    constenants : number
    fullName : string
    reverseName : string
    personSkills: Array<string>
    personSocialMediaAccounts : Array<PersonSocialMediaAccountResponse>
}

export interface PersonSocialMediaAccountResponse
{
    socialMediaAccountId : string
    address : string
    type : string
}