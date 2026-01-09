import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, tap } from 'rxjs';
import { environment } from '../environments/environment.development';
import { ILogin } from '../IModels/ilogin';
import { IUser } from '../IModels/iuser';
import { Roles } from '../IModels/roles';
import { JwtHelperService } from '@auth0/angular-jwt';
import { IRegisterUser } from '../IModels/Iregister-user';


@Injectable({
  providedIn: 'root'  
})
export class RegisterService {

  private isLogedSubject: new BehaviorSubject<boolean>(this.IsLoged);

  private currentUserSubject = new BehaviorSubject<any>(null);
  public currentUserObservable$ = this.currentUserSubject.asObservable();
  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) {
   
    this.getUserFromLocalStorage();
  }

  RegisterUser(User: IRegisterUser): Observable<IRegisterUser> {

    return this.http.post<IRegisterUser>(environment.baseUrl + "User/RegisterUser",User)
  }


  GetAllUsersWithRolls(): Observable<IUser[]> {
    return this.http.get<IUser[]>(environment.baseUrl + "User/GetAllUsersWithRolls")
  }

  GetUserByUserName(UserName:string): Observable<IUser> {
    return this.http.get<IUser>(environment.baseUrl + "User/GetUserByUserName/" + UserName)
  }

  DeleteUser(userName: string): Observable<string> {
    return this.http.delete<string>(environment.baseUrl + "User/DeleteUser" , { params: { userName } } );
  }


  GetAllRoles(): Observable<Roles[]> {
    return this.http.get<Roles[]>(environment.baseUrl + "User/GetAllRoles")
  }

  AddRole(userName: string, role: string): Observable<Roles> {
    return this.http.post<Roles>(environment.baseUrl +  "User/AddRoll",{ role, userName})
  }

  DeleteRole(userName: string, role: string): Observable<Roles> {
    return this.http.delete<Roles>(environment.baseUrl + "User/DeleteRole/", { params: { userName ,role } } )
  }

  Login(Login: ILogin): Observable<ILogin> {

    return this.http.post<ILogin>(environment.baseUrl + "User/Login", Login)
      .pipe(
        tap(result => {
          localStorage.setItem("token", result.token);
          this.isLogedSubject.next(true);

          const decoded = this.jwtHelper.decodeToken(result.token);
          const user = {
            userName: decoded["userName"],
            userId: decoded["userId"],
            role: decoded["role"],
            phone: decoded["phone"],
            email: decoded["email"],
            exp: decoded["exp"]
          };
          localStorage.setItem('user', JSON.stringify(user));

          this.currentUserSubject.next(user);

      })
    );
  }

  getUserFromLocalStorage() {
    const userJson = localStorage.getItem('user');
    if (userJson) {
      const user = JSON.parse(userJson);
      this.currentUserSubject.next(user);
    }
  }

  get currentUser() {
    return this.currentUserSubject.value;
  }

  setUser(user: any) {
    this.currentUserSubject.next(user);
  }


  Logout() {

    localStorage.removeItem("token");
    localStorage.removeItem("user");
    this.currentUserSubject.next(null);
    this.isLogedSubject.next(false);
  }

  get IsLoged(): boolean {
    return (localStorage.getItem("token")) ? true : false
  }

  IsLogedSubjectBoolean(): Observable<boolean> {
    return this.isLogedSubject.asObservable();
  }

  IsExistEmail(Email: string): Observable<boolean> {
    return this.http.get<boolean>(environment.baseUrl + "user/IsExistEmail/"+{Email});
  }
  
  IsExistUserName(UserName: string): Observable<boolean> {
    return this.http.get<boolean>(environment.baseUrl + "user/IsExistUserName/"+{UserName});
  }

  
  IsExistPhoneNo(PhoneNo: string): Observable<boolean> {
    return this.http.get<boolean>(environment.baseUrl + "user/IsExistPhoneNo/"+{PhoneNo});
  }


}

