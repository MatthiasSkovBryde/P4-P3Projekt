import { Component, OnInit } from '@angular/core';
import { CustomerService } from 'src/app/_services/customer.service';
import { CustomerRequest, DirectCustomerResponse } from 'src/app/_models/customer';
import { AccountService, AuthenticationService } from 'src/app/_services';
import { JwtDecodePlus } from 'src/app/helpers/JWTDecodePlus';
import { NotificationService } from 'src/app/_services/notification.service';
import { AccountRequest } from 'src/app/_models/account';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  private customerID: number = 0;
  public allowEdit: boolean = false;
  public isAdmin: boolean = false;

  public allowSaveF: boolean = true;
  public allowSaveL: boolean = true;
  public allowSaveP: boolean = true;
  public allowSaveE: boolean = true;
  public allowSaveA: boolean = true;
  public allowSaveZ: boolean = true;
  public allowSave: boolean = false;

  customer: DirectCustomerResponse = {customerID: 0, account:{ accountID: 0, email: '', role: ''}, firstName: '', lastName: '', phoneNumber: '', zipCode: '', created_At: new Date(), address: ''};
  public customerRequest: CustomerRequest = { accountID: 0, firstName: '', lastName: '', phoneNumber: '', zipCode: '', customerID: 0, address: ''};

  constructor(private customerService: CustomerService, private authService: AuthenticationService, private notification: NotificationService, private accountService: AccountService) { }

  ngOnInit(): void {
    /*
    let customerId = JwtDecodePlus.jwtDecode(this.authService.AccessToken).nameid;
    this.customerService.getById(customerId).subscribe(response => { this.customer = response; })
    */
   this.authService.OnTokenChanged.subscribe((token) => {
      this.customerID = JwtDecodePlus.jwtDecode(token).nameid;
      this.isAdmin = JwtDecodePlus.jwtDecode(token).role === 'Admin' ? true : false;
      this.customerService.getById(this.customerID).subscribe(data => this.customer = data);
   });
  }

  public save(): void {
    this.validateInfo();
    if (this.allowSave === true) {

      if (confirm('Er du sikker på at du vil gemme oplysningerne?')) {
  
        let request: CustomerRequest = {
          customerID: this.customer.customerID,
          accountID: this.customer.account.accountID,
          firstName: this.customer.firstName,
          lastName: this.customer.lastName,
          phoneNumber: this.customer.phoneNumber,
          zipCode: this.customer.zipCode,
          address: this.customer.address
        }
  
        let accountRequest: AccountRequest = {
          password: 'NONE',
          email: this.customer.account.email,
          role: this.customer.account.role
        }
  
        this.customerService.update(this.customer.customerID, request).subscribe( x => {
          this.customer = x;
        });
  
        this.accountService.update(this.customer.account.accountID, accountRequest).subscribe( z => {
          this.customer.account = z;
        });
        
        this.allowEdit = false;
        this.notification.showSuccess('Success','Account was updated');
      }
      else {
        this.notification.showError('Error', 'Blev ikke gemt!');
      }
    }
  }
  
  public edit(): void {
    if (!this.allowEdit) {
      this.allowEdit = true;
    }
  }

  public pressed: number = 0;
  public egg(): void {
    this.pressed ++;  
    if (this.pressed >= 10) {
      window.open('https://www.tec.dk');
      window.location.reload();
    }
  }

  public validateInfo(): void {
    var firstName = (<HTMLInputElement>document.getElementById("firstName")).value;
    var lastName = (<HTMLInputElement>document.getElementById("lastName")).value;
    var phoneNumber = (<HTMLInputElement>document.getElementById("phoneNumber")).value;
    var email = (<HTMLInputElement>document.getElementById("email")).value;
    var address = (<HTMLInputElement>document.getElementById("address")).value;
    var zipCode = (<HTMLInputElement>document.getElementById("zipCode")).value;

    if (firstName == "" || firstName == " ") {
      this.allowSaveF = false;
      return;
    }

    if (lastName == "" || lastName == " ") {
      this.allowSaveL = false;
      return;
    }

    if (phoneNumber == "" || phoneNumber == " ") {
      this.allowSaveP = false;
      return;
    }

    if (email == "" || email == " ") {
      this.allowSaveE = false;
      return;
    }

    if (address == "" || address == " ") {
      this.allowSaveA = false;
      return;
    }

    if (zipCode == "" || zipCode == " ") {
      this.allowSaveZ = false;
      return;
    }

    if (this.allowSaveF && this.allowSaveL && this.allowSaveP && this.allowSaveE && this.allowSaveA && this.allowSaveZ){
      this.allowSave = true;
    }
  }
}
