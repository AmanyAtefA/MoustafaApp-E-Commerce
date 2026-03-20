import { Component, OnInit, HostListener } from '@angular/core';
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
  isSearchOpen = false;

  // ✅ state واحدة بدل كذا variable
  activeMenu: 'shop' | 'brand' | null = null;

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
  }

  // ✅ Desktop
  openMenu(menu: 'shop' | 'brand') {
    if (window.innerWidth <= 768) return;
    this.activeMenu = menu;
  }

  closeMenu() {
    if (window.innerWidth <= 768) return;
    this.activeMenu = null;
  }

  // ✅ Mobile
  toggleMenuMobile(menu: 'shop' | 'brand') {
    this.activeMenu = this.activeMenu === menu ? null : menu;
  }

  toggleMenu() {
    this.menuOpen = !this.menuOpen;
  }

  closeMobileMenu() {
    if (window.innerWidth <= 768) {
      this.menuOpen = false;
      this.activeMenu = null; // يقفل shop / brand كمان
    }
  }

  logout() {
    this._RegisterService.Logout();
  }


  //searchProducts(text: string) {

  //  this.router.navigate(['/Products'], {
  //    queryParams: { search: text }
  //  });

  //}



  toggleSearch() {
    this.isSearchOpen = !this.isSearchOpen;
  }

  searchProducts(text: string) {
    this.updateFilter({ search: text });
  }

  updateFilter(filter: any) {
    const currentParams = this.route.snapshot.queryParams;
    
    this.router.navigate(['/Products'], {
      queryParams: {
     
        ...filter
      }
    });
    console.log(filter);
  }

  @HostListener('document:click', ['$event'])
  onClickOutside(event: Event) {

    
    if (window.innerWidth > 768) return;

    const target = event.target as HTMLElement;

    if (target.closest('.navStyle')) return;

    this.activeMenu = null;
  }

  @HostListener('window:resize')
  onResize() {
    if (window.innerWidth >= 768) {
      this.menuOpen = false;  
      this.activeMenu = null;  
    }
  }


}
