import { Component, HostListener, OnInit } from '@angular/core';
import { IDepartment } from '../../IModels/IDepartment';
import { Observable } from 'rxjs';
import { DepartmentsService } from '../../Service/departments.service';
import { RegisterService } from '../../Service/register.service';

@Component({
  selector: 'app-navebar',
  templateUrl: './navebar.component.html',
  styleUrl: './navebar.component.css'
})
export class NavebarComponent implements OnInit {

  menuOpen = false;
  isDropdownOpen = false;
  shopOpenDesktop = false;
  shopOpenMobile = false;

  timeout: any;

  Departments$!: Observable<IDepartment[]>;
  currentUser$!: Observable<any>;

  constructor(
    private _DepartmentsService: DepartmentsService,
    private _RegisterService: RegisterService
  ) { }

  ngOnInit(): void {
    this.Departments$ = this._DepartmentsService.Departments$;
    this.currentUser$ = this._RegisterService.currentUserObservable$;
    this._DepartmentsService.loadDepartments();
  }

  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  showMenu() {
    clearTimeout(this.timeout);
    this.shopOpenDesktop = true;
  }

  hideMenu() {
    this.timeout = setTimeout(() => {
      this.shopOpenDesktop = false;
    }, 150);
  }

  toggleMenu() {
    this.menuOpen = !this.menuOpen;
  }

  toggleShopMobile() {
    this.shopOpenMobile = !this.shopOpenMobile;
  }

  toggleShopDesktop() {
    this.shopOpenDesktop = !this.shopOpenDesktop;
  }

  logout() {
    this._RegisterService.Logout();
  }
}
