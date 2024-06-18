import {Inject, Injectable} from '@angular/core';
import {JWT_DECODE} from "../../constants/jwt-decode.constant";
import {JwtPayload} from "jwt-decode";

@Injectable({
    providedIn: 'root'
})
export class JwtService {

    public constructor(
        @Inject(JWT_DECODE) private readonly jwtDecode: typeof JWT_DECODE
    ) {
    }

    public decodeToken<T = JwtPayload>(token: string): T {
        return this.jwtDecode<T>(token);
    }

    public getExpiration(token: string): Date | null {
        const decodedToken = this.decodeToken<JwtPayload>(token);

        if (!decodedToken.exp) {
            return null;
        }

        return new Date(decodedToken.exp * 1000);
    }

    public hasExpiration(token: string): boolean {
        const decodedToken = this.decodeToken<JwtPayload>(token);

        return !!decodedToken.exp;
    }

    /**
     * @param token - jwt token
     * @param offset - offset in seconds
     */
    public isTokenExpired(token: string, offset: number = 0): boolean {
        const expiration = this.getExpiration(token);

        if (!expiration) {
            throw new Error("Token has no exp (expiry) claim.")
        }

        const dateToCompare = new Date(new Date().getTime() + (offset * 1000));

        return dateToCompare > expiration;
    }
}
