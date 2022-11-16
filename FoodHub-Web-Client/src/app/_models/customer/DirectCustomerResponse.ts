import { StaticAccountResponse } from "../account";

export interface DirectCustomerResponse {
    customerID: number;
    account: StaticAccountResponse;
    firstName: string;
    lastName: string;
    phoneNumber: string;
    zipCode: string;
    created_At: Date;
    address: string;
}