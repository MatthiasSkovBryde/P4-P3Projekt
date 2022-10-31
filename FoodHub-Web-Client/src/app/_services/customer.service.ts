import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { environment } from "src/environments/environment";
import { DirectCustomerResponse, StaticCustomerResponse, NewCustomerRequest } from "../_models/customer";

@Injectable({
    providedIn: 'root'
})
export class CustomerService {
    constructor(private http: HttpClient) {}

    private get customerUrl(): string {
        return environment.ApiUrl + "/Customer";
    }

    public getAll(): Observable<StaticCustomerResponse[]> {
        return this.http.get<StaticCustomerResponse[]>(this.customerUrl)
    }

    public create(request: NewCustomerRequest): Observable<DirectCustomerResponse> {
        return this.http.post<DirectCustomerResponse>(this.customerUrl, request);
    }

    public getById(customerId: number): Observable<DirectCustomerResponse> {
        return this.http.get<DirectCustomerResponse>(`${this.customerUrl}/${customerId}`);
    }

    public update(customerId: number, request: NewCustomerRequest): Observable<DirectCustomerResponse> {
        return this.http.put<DirectCustomerResponse>(`${this.customerUrl}/${customerId}`, request);
    }

    public delete(customerId: number): Observable<DirectCustomerResponse> {
        return this.http.delete<DirectCustomerResponse>(`${this.customerUrl}/${customerId}`);
    }
}