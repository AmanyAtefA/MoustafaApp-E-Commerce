import { EmailValidator } from "@angular/forms"

export interface IUser {

  userId : string;
  userName : string;
  email : EmailValidator;
  phoneNumber : number;
  roles : string[];
  isActive : boolean;
}
