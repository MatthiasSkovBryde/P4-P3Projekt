import { Component, OnInit } from '@angular/core';
import { CustomerService } from 'src/app/_services/customer.service';
import { CustomerRequest, DirectCustomerResponse } from 'src/app/_models/customer';
import { AccountRequest } from 'src/app/_models/account';


@Component({
  selector: 'app-userinfo',
  templateUrl: './userinfo.component.html',
  styleUrls: ['./userinfo.component.css']
})
export class UserinfoComponent implements OnInit {
  public request: AccountRequest = { email: '', password: '' };
  customers: DirectCustomerResponse[] = []
  
  customer:DirectCustomerResponse = {
    customerID: 0,
    account: {
      accountID: 0,
      email: ""
    },
    firstName: '',
    lastName: '',
    phoneNumber: '',
    zipCode: 0,
    created_At: new Date()
  }

  
  constructor(private customerService:CustomerService) { }




  ngOnInit(): void {
    this.customers.push(this.customer);
  }

}
