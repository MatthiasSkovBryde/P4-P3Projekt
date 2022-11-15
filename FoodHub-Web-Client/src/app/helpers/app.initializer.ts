import { AuthenticationService } from "../_services/authentication.service";

export function appInitializer(authenticattionService: AuthenticationService) {
    return () => new Promise(resolve => {
        // attempt to add refresh token on app start up to auto authenticate
        authenticattionService.refreshToken().subscribe().add(resolve(true));
    });
}