import { AccountRequest } from "../account";
import { CustomerRequest } from "./CustomerRequest";

export interface NewCustomerRequest {
    customer: CustomerRequest;
    account: AccountRequest; 
}