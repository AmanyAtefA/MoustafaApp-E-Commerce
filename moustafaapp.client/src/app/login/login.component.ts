import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ILogin } from '../../IModels/ilogin';
import { RegisterService } from '../../Service/register.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {

  ModelILogin: ILogin = {} as ILogin;
  UserLoginForm!: FormGroup;

  constructor(private _RegisterService: RegisterService,
    private router: Router, private fb: FormBuilder) {

  }
    ngOnInit(): void {
      this.CreatLoginForm();
     
    }

  CreatLoginForm() {
    this.UserLoginForm = this.fb.group({
      UserName: ['', [Validators.required,
      Validators.pattern('^[a-zA-Z]{3,20}$')]],
      Password: ['', [Validators.required,
        Validators.minLength(6), Validators.maxLength(20),
      ]]

    })
  }

  Login() {

      if (this.UserLoginForm.valid) {
        this.ModelILogin = this.UserLoginForm.value;

        this._RegisterService.Login(this.ModelILogin).subscribe(
          (res) => {
              localStorage.setItem("token", res.token);
            console.log('Login successful', res);
            alert("Wellcom to Shop.Co")
            this.router.navigate(['/Home']);
          },
          (error) => {
            console.error('Login failed', error);
            alert("UserName Or Password is invalid")
            if (error.error) {
              console.log(' the error:', error.error);
            }
          }
        );
      } else {
        console.log('Form is invalid');
      }
    
  }

 
}
