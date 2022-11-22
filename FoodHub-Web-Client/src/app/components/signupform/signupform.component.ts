import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountRequest } from 'src/app/_models/account';
import { AuthenticationRequest } from 'src/app/_models/authentication';
import { CustomerRequest, NewCustomerRequest} from 'src/app/_models/customer';
import { AuthenticationService, CustomerService } from 'src/app/_services';
import { NotificationService } from 'src/app/_services/notification.service';

@Component({
  selector: 'app-signupform',
  templateUrl: './signupform.component.html',
  styleUrls: ['./signupform.component.css']
})
export class SignupformComponent implements OnInit {
  visible: boolean = true;
  changetype: boolean = true;
  
  private returnUrl: string = "";
  public accountRequest: AccountRequest = {email: '', password: '' };
  public customerRequest: CustomerRequest = { accountID: 0, firstName: '', lastName: '', phoneNumber: '', zipCode: '', customerID: 0, address: ''};
  private request: NewCustomerRequest = { account: this.accountRequest, customer: this.customerRequest };
  public passwordValidator: string = '';
  public isValid: boolean = false;

  constructor(
    private router: Router, 
    private customerService: CustomerService,
    private route: ActivatedRoute,
    private authenticationService: AuthenticationService,
    private notification: NotificationService ) { }

  viewpass() {
    this.visible = !this.visible;
    this.changetype = !this.changetype;
  }

  ngOnInit(): void {
    this.returnUrl = this.route.snapshot.queryParams [ "returnUrl" ] || "/";
    this.authenticationService.OnTokenChanged.subscribe((token) => {
      if (token !== "") {
        this.router.navigate([ this.returnUrl ]);
      }
    });
  }

  public validate(): Promise<boolean> {
    return new Promise<boolean>((resolve) => {
      if (this.accountRequest.password === this.passwordValidator) {
        resolve(true);
      }
      else {
        this.notification.showError("Error","Password aren't the same");
        resolve(false);
      }
    })
  }

  public submit(): void {
    this.validate().then((result) => {
      if (result) {
        this.customerService.create(this.request).subscribe({
          next: () => {
            let loginRequest: AuthenticationRequest = { email: '', password: ''};
            loginRequest = Object.assign(loginRequest, this.request.account);
            this.authenticationService.authenticate(loginRequest).subscribe();
            this.notification.showSuccess('Success','Account was created')
          },
          error: (err) => {
            console.error(Object.values(err.error.errors).join(', '));
          }
        });
      }
    });
  }
}
