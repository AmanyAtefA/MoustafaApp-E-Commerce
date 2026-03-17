import { Component, OnInit } from '@angular/core';
import { IDepartment } from '../../IModels/IDepartment';
import { Observable } from 'rxjs';
import { DepartmentsService } from '../../Service/departments.service';
import { BrandService } from '../../Service/brand.service';
import { RegisterService } from '../../Service/register.service';
import { IBrand } from '../../IModels/IBrand';
import { ActivatedRoute, Router } from '@angular/router';

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

  brandOpenDesktop = false;
  brandOpenMobile = false;

  timeout: any;

  Departments$!: Observable<IDepartment[]>;
  Brands$ = this._BrandService.Brands$;
  currentUser$!: Observable<any>;

  constructor(
    private _DepartmentsService: DepartmentsService,
    private _RegisterService: RegisterService,
    private _BrandService: BrandService,
    private router: Router,
    private route: ActivatedRoute) { }


  ngOnInit(): void {
    this.Departments$ = this._DepartmentsService.Departments$;
    this.currentUser$ = this._RegisterService.currentUserObservable$;
    this._DepartmentsService.loadDepartments();
    this._BrandService.loadBrands();
    console.log(this.currentUser$)
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

  showBrandMenu() {
    clearTimeout(this.timeout);
    this.brandOpenDesktop = true;
  }

  hideBrandMenu() {
    this.timeout = setTimeout(() => {
      this.brandOpenDesktop = false;
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

  toggleBrandMobile() {
    this.brandOpenMobile = !this.brandOpenMobile;
  }
  logout() {
    this._RegisterService.Logout();
  }


  //searchProducts(text: string) {

  //  this.router.navigate(['/Products'], {
  //    queryParams: { search: text }
  //  });

  //}

  searchProducts(text: string) {

    this.updateFilter({ search: text });
  }

  updateFilter(filter: any) {

    const currentParams = this.route.snapshot.queryParams;

    this.router.navigate(['/Products'], {
      queryParams: {
        ...currentParams,
        ...filter
      }
    });

  }

}
