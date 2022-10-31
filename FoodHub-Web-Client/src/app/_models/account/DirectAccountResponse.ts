import { StaticCustomerResponse } from "../customer";

export interface DirectAccountResponse {
    accountID: number;
    email: string;
    customer: StaticCustomerResponse;
}