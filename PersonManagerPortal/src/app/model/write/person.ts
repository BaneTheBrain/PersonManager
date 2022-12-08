
export class PersonRequest {
    public personId!: string;
    public firstName: string;
    public lastName: string;
    public personSkills: Array<string>;
    public personSocialMediaAccounts: Array<PersonSocialMediaAccountRequest>;

    constructor(firstName: string, lastName: string, personSkills: Array<string>, personSocialMediaAccounts: Array<PersonSocialMediaAccountRequest>) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.personSkills = personSkills;
        this.personSocialMediaAccounts = personSocialMediaAccounts;
    }
}

export class PersonSocialMediaAccountRequest
{
    public accountId : string;
    public address : string;
    public type : string | undefined;

    constructor(accountId: string, address: string, type: string | undefined) {
        this.accountId = accountId;
        this.address = address;
        this.type = type;
    }
}