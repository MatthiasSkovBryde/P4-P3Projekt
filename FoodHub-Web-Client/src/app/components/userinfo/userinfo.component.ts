import { Component, OnInit } from '@angular/core';
import { CustomerService } from 'src/app/_services/customer.service';
import { DirectCustomerResponse } from 'src/app/_models/customer';
import { AuthenticationService } from 'src/app/_services';
import { JwtDecodePlus } from 'src/app/helpers/JWTDecodePlus';

@Component({
  selector: 'app-userinfo',
  templateUrl: './userinfo.component.html',
  styleUrls: ['./userinfo.component.css']
})
export class UserinfoComponent implements OnInit {
  public customer: DirectCustomerResponse = {
    customerID: 0,
    account: {
      accountID: 0,
      email: ''
    },
    firstName: '',
    lastName: '',
    phoneNumber: '',
    zipCode: '',
    address: '',
    created_At: new Date()
  };

  constructor(private customerService:CustomerService, private authService: AuthenticationService) { }

  ngOnInit(): void {
   let customerId = JwtDecodePlus.jwtDecode(this.authService.AccessToken).nameid;
   this.customerService.getById(customerId).subscribe(response => { this.customer = response; })
  }
}
