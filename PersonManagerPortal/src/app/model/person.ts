export interface IPerson
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
    address : string
    type : string
}