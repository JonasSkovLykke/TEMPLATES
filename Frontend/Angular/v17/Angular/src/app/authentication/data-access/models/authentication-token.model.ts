export class AuthenticationToken {
    tokenType: string;
    accessToken: string;
    expiresIn: number;
    refreshToken: string;

    constructor(tokenType: string, accessToken: string, expiresIn: number, refreshToken: string) {
        this.tokenType = tokenType;
        this.accessToken = accessToken;
        this.expiresIn = expiresIn;
        this.refreshToken = refreshToken;
    }
}
