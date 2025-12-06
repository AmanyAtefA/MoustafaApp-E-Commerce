import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule,ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IRegisterUser } from '../../IModels/Iregister-user';
import { RegisterService } from '../../Service/register.service';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit{

  RegesterUserForm!: FormGroup;

  ModelRegester: IRegisterUser = {} as IRegisterUser;
  ExistEmail = false;
  ExistUserName = false;
  ExistPhoneNo = false;

  constructor(private _RegisterService: RegisterService,
    private router: Router,private fb:FormBuilder)
  {
    
  }
    ngOnInit(): void {
      this.CreatRegisterForm();
    }

  CreatRegisterForm() {
    this.RegesterUserForm = this.fb.group({
      userName: ["", [Validators.required,
      Validators.pattern('^[a-zA-Z]{3,20}$')]],
      phoneNumber: ["", [Validators.required,
      Validators.maxLength(11), Validators.pattern("^[0-9]*$")]],
      email: ["", [Validators.required,
      Validators.email,
      Validators.pattern("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,4}$")]],
      password: ["", [Validators.required,
      Validators.minLength(6), Validators.maxLength(20)]],
      confirmPassword: ["", [Validators.required,
      Validators.minLength(6), Validators.maxLength(20)]]
    })
  }
  

  IsExistEmail() {
    this._RegisterService.IsExistEmail(this.ModelRegester.Email).subscribe(
      result => {
        this.ExistEmail = result
      })
  }
  IsExistUserName() {
    this._RegisterService.IsExistUserName(this.ModelRegester.UserName).subscribe(
      result => {
        this.ExistUserName = result
      })
  }

  IsExistPhoneNo() {
    this._RegisterService.IsExistPhoneNo(this.ModelRegester.PhoneNumber).subscribe(
      result => {
        this.ExistPhoneNo = result
      })
  }

  SaveRegister() {
    this._RegisterService.RegisterUser(this.ModelRegester).subscribe(
      result => {
        console.log('Register Successful');
        console.log(this.ModelRegester);
        alert("Regester is Successful")
          this.router.navigate(['/Login']);  
      },

      Error=> {
        console.error("Error:", Error);
        alert("Check Password and Confirm.");
      }
    );
  }

  RegisterUser() {
    if (!this.RegesterUserForm.valid)
    {
      console.log('Form is invalid');
      alert("Regester is invalid")
    }

    this.ModelRegester = this.RegesterUserForm.value;
    this.IsExistEmail();
    this.IsExistUserName();
    this.IsExistPhoneNo();
    
        if (this.ExistEmail) {
          alert("Email already exists.");
          return; 
        }

         if (this.ExistUserName) {
          alert("User Name already exists.");
          return; 
         }

             if (this.ExistPhoneNo) {
          alert("PhoneNo already exists.");
          return; 
         }

    this.SaveRegister();
  }
}

