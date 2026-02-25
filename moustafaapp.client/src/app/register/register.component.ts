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
      this.autoGenerateUsername();
    }


  autoGenerateUsername() {
    this.RegesterUserForm.get('fullName')?.valueChanges.subscribe(name => {
      if (name) {
        const username = name.replace(/\s+/g, '').toLowerCase();

        this.RegesterUserForm.get('userName')
          ?.setValue(username, { emitEvent: false });
      }
    });
  }

  CreatRegisterForm() {
    this.RegesterUserForm = this.fb.group({
      fullName: ["", [Validators.required,
        Validators.pattern('^[a-zA-Z ]{3,20}$') ]],
      userName: [""],
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



  SaveRegister() {
    console.log(this.ModelRegester);

    this._RegisterService.RegisterUser(this.ModelRegester).subscribe(
      result => {
        console.log('Register Successful');
        console.log(this.ModelRegester);
        alert("Regester is Successful")
        this.router.navigate(['/Login'], {
          state: {
            email: this.ModelRegester.email,
            password: this.ModelRegester.password
          }
        }); 
      },

      error => {
        console.log(error.error);
      }
    );
  }

  RegisterUser() {

    if (this.RegesterUserForm.invalid) {
      alert("Register is invalid");
      return;
    }

    this.ModelRegester = this.RegesterUserForm.value;

    this._RegisterService.IsExistEmail(this.ModelRegester.email).subscribe(emailExists => {

      if (emailExists) {
        alert("Email already exists");
        return;
      }

      this._RegisterService.IsExistUserName(this.ModelRegester.userName).subscribe(userExists => {

        if (userExists) {
          alert("Username already exists");
          return;
        }

        this._RegisterService.IsExistPhoneNo(this.ModelRegester.phoneNumber).subscribe(phoneExists => {

          if (phoneExists) {
            alert("Phone already exists");
            return;
          }

         
          this.SaveRegister();

        });

      });

    });
  }
}

