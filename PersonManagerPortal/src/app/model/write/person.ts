import { IPersonSocialMediaAccount } from "../shared/personsocialmediaaccount";

export class CreatePerson {
    public personId!: string;
    public firstName: string;
    public lastName: string;
    public personSkills: Array<string>;
    public personSocialMediaAccounts: Array<IPersonSocialMediaAccount>;

    constructor(firstName: string, lastName: string, socialSkills: Array<string>, socialMediaAccounts: Array<IPersonSocialMediaAccount>) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.personSkills = socialSkills;
        this.personSocialMediaAccounts = socialMediaAccounts;
    }
}