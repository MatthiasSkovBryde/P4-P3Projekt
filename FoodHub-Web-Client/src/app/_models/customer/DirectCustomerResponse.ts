import { StaticAccountResponse } from "../account";

export interface DirectCustomerResponse {
    customerID: number;
    account: StaticAccountResponse;
    firstName: string;
    lastName: string;
    phoneNumber: string;
    zipCode: number;
    created_At: Date;
}