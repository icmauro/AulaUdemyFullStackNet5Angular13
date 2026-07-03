import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(private router: Router,
              public accountService: AccountService) { }

  isCollapsed = true;

  ngOnInit(): void {
  }

  public logout(): void
  {
    this.accountService.logout();
    this.router.navigateByUrl('/user/login');
  }

  public mostrarMenu(): boolean {
    return this.router.url != '/user/login';
  }
}
