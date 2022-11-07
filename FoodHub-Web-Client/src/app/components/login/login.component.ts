import { Component, ErrorHandler, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ColdObservable } from 'rxjs/internal/testing/ColdObservable';
import { AuthenticationRequest } from 'src/app/_models/authentication';
import { AuthenticationService } from 'src/app/_services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  visible: boolean = true;
  changetype: boolean = true;

  viewpass() {
    this.visible = !this.visible;
    this.changetype = !this.changetype;
  }

  constructor(private router: Router, private authenticationService: AuthenticationService, private route: ActivatedRoute) { }

  public request: AuthenticationRequest = { email: '', password: '' };
  private returnUrl: string = "";
  private guardType: number = 0;

  ngOnInit(): void {
    this.returnUrl = this.route.snapshot.queryParams["returnUrl"] || "/";
    this.guardType = this.route.snapshot.queryParams["guard"] || 0;
    this.authenticationService.OnTokenChanged.subscribe((token) => {
      if (token !== "") {
        this.router.navigate([this.returnUrl]);
      }
    });
  }

  public login(): void {
    this.authenticationService.authenticate(this.request).subscribe({
      next: () => {
        this.router.navigate([this.returnUrl]);
      },
      error: (err) => {
        console.error(Object.values(err.error.errors).join(', '));
      }
    });
  }
}

