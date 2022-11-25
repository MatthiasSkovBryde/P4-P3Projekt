import { AuthenticationService } from "../_services/authentication.service";

export function appInitializer(authenticationService: AuthenticationService) {
    return () => new Promise(resolve => {
        // attempt to add refresh token on app start up to auto authenticate
        authenticationService.refreshToken().subscribe().add(resolve(true));
    });
}