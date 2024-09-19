export class User {
    id: string;
    email: string;
    phoneNumber: string;
    firstName: string;
    lastName: string;
    roles?: string[];

    constructor(id: string, email: string, phoneNumber: string, firstName: string, lastName: string, roles?: string[]) {
        this.id = id;
        this.email = email;
        this.phoneNumber = phoneNumber;
        this.firstName = firstName;
        this.lastName = lastName;
        this.roles = roles;
    }
}
