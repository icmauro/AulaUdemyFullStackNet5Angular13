import { Component } from '@angular/core';

import { User } from './models/identity/User';
import { AccountService } from './services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ProEventos-App';

  constructor(public accountService: AccountService) { }

  ngOnInit(): void
  {
    this.setCurrentUser();
  }

  public setCurrentUser(): void
  {
    let user = {} as User

    if (localStorage.getItem('user')) {
      user = JSON.parse(localStorage.getItem('user') ?? '{}') as User;
    }
    else
    {
      user = {} as User;
    }

    if (user?.userName)
      this.accountService.setCurrentUser(user);

  }

}
