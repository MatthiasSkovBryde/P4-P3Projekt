import { Component, OnInit } from '@angular/core';
import { CustomerService } from 'src/app/_services/customer.service';
import { CustomerRequest, DirectCustomerResponse, NewCustomerRequest } from 'src/app/_models/customer';
import { AuthenticationService } from 'src/app/_services';
import { JwtDecodePlus } from 'src/app/helpers/JWTDecodePlus';
import { AccountRequest } from 'src/app/_models/account';
import { NotificationService } from 'src/app/_services/notification.service';

@Component({
  selector: 'app-userinfo',
  templateUrl: './userinfo.component.html',
  styleUrls: ['./userinfo.component.css']
})

export class UserinfoComponent implements OnInit {
  public allowEdit: boolean = false;

  customer: DirectCustomerResponse = {customerID: 0, account:{ accountID: 0, email: ''}, firstName: '', lastName: '', phoneNumber: '', zipCode: '', address: '', created_At: new Date()};
  customerRequest: CustomerRequest = {customerID: 0, accountID: 0, firstName: '', lastName: '', phoneNumber: '', zipCode: '', address: ''};

  constructor(private customerService:CustomerService, private authService: AuthenticationService, private notification: NotificationService) { }

  public save(): void {
    if (confirm('Er du sikker på at du vil gemme oplysningerne?')) {

      this.customerRequest = {
        customerID: this.customer.customerID,
        accountID: this.customer.account.accountID,
        firstName: this.customer.firstName,
        lastName: this.customer.lastName,
        phoneNumber: this.customer.phoneNumber,
        zipCode: this.customer.zipCode,
        address: this.customer.address
      };

      let accountRequest: AccountRequest = {
        email: this.customer.account.email,
        password: 'NONE'
      };

      let request: NewCustomerRequest = {
        account: accountRequest,
        customer: this.customerRequest
      }

      this.customerService.update(this.customer.customerID, request).subscribe( x => {
        this.customer = x;

        this.allowEdit = false;
      });
      this.notification.showSuccess('Success','Account was updated');
    }
    else {
      this.notification.showError('Error', 'Blev ikke gemt!');
    }
  }

  public edit(): void {
    if (!this.allowEdit) {
      this.allowEdit = true;
    }
  }

  ngOnInit(): void {
    let customerId = JwtDecodePlus.jwtDecode(this.authService.AccessToken).nameid;
    this.customerService.getById(customerId).subscribe(response => { this.customer = response; })
   }
}
