export interface CustomerRequest {
    customerID: number;
    accountID?: number;
    firstName: string;
    lastName: string;
    phoneNumber: string;
    zipCode: string;
    address: string;
}