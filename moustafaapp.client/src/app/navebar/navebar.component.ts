import { Component, HostListener, OnInit } from '@angular/core';

@Component({
  selector: 'app-navebar',
  templateUrl: './navebar.component.html',
  styleUrl: './navebar.component.css'
})
export class NavebarComponent   {


  menuOpen = false;
  shopOpenMobile = false;
  shopOpenDesktop = false;

  timeout: any;

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

  @HostListener('window:resize', ['$event'])

  onResize(event: any) {
    if (window.innerWidth > 768) {
      this.menuOpen = false; 
      this.shopOpenMobile = false;
    }
  }



}
