import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { DirectAccountResponse, StaticAccountResponse, AccountRequest } from '../_models/account';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private url: string = environment.ApiUrl + "/Account";

  constructor(private http: HttpClient) { }

  public getAll(): Observable<StaticAccountResponse[]> {
    return this.http.get<StaticAccountResponse[]>(this.url);
  }

  public getById(accountId: number): Observable<DirectAccountResponse> {
    return this.http.get<DirectAccountResponse>(`${this.url}/${accountId}`);
  }

  public update(accountId: number, request: AccountRequest): Observable<DirectAccountResponse> {
    console.log(this.url, accountId, request);
    return this.http.put<DirectAccountResponse>(`${this.url}/${accountId}`, request);
  }

  public delete(accountId: number): Observable<DirectAccountResponse> {
    return this.http.delete<DirectAccountResponse>(`${this.url}/${accountId}`);
  }
}
