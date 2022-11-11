import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { environment } from "src/environments/environment";
import { DirectCustomerResponse, StaticCustomerResponse, NewCustomerRequest } from "../_models/customer";

@Injectable({
    providedIn: 'root'
})
export class CustomerService {
    private url: string = environment.ApiUrl + "/customer"

    constructor(private http: HttpClient) {}

    public getAll(): Observable<StaticCustomerResponse[]> {
        return this.http.get<StaticCustomerResponse[]>(this.url)
    }

    public create(request: NewCustomerRequest): Observable<DirectCustomerResponse> {
        return this.http.post<DirectCustomerResponse>(this.url, request);
    }

    public getById(customerId: number): Observable<DirectCustomerResponse> {
        return this.http.get<DirectCustomerResponse>(`${this.url}/${customerId}`);
    }

    public update(customerId: number, request: NewCustomerRequest): Observable<DirectCustomerResponse> {
        return this.http.put<DirectCustomerResponse>(`${this.url}/${customerId}`, request);
    }

    public delete(customerId: number): Observable<DirectCustomerResponse> {
        return this.http.delete<DirectCustomerResponse>(`${this.url}/${customerId}`);
    }
}