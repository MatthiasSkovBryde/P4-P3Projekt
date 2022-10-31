import jwtDecode from 'jwt-decode'; //npm install jwt-decode

export interface JwtPayloadPlus {
    nameid: number;
    given_name: string;
    email: string;
    exp: number;
    nbf: number;
    iat: number;
}

export class JwtDecodePlus {
    public static jwtDecode(token: string): JwtPayloadPlus {
        return jwtDecode(token);
    }
}