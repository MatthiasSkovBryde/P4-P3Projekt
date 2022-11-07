import { Component, OnInit } from '@angular/core';
import { AuthenticationService, CustomerService } from 'src/app/_services';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(private authenticationService: AuthenticationService, private customerService: CustomerService) { }

  public loggedIn: boolean = false;

  ngOnInit(): void {
    this.authenticationService.OnTokenChanged.subscribe((token) => {
      if (token !== '') {
        this.loggedIn = true;
      }
      else {
        this.loggedIn = false;
      }
    });
  }

  public logud(): void {
    this.authenticationService.revoketoken().subscribe();
  }
  

}
