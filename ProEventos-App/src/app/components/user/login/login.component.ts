import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { UserLogin } from '../../../models/identity/UserLogin';
import { AccountService } from '../../../services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  model = {} as UserLogin;
  constructor(private accountService: AccountService,
              private router: Router,
              private toastrService: ToastrService,
              private spinnerService: NgxSpinnerService) { }
                        

  public login(): void
  {
    this.spinnerService.show();

    this.accountService.login(this.model).subscribe(
    {
        next: () =>
        {
          this.router.navigateByUrl('/dashboard');
        },
        error: (error: any) =>
        {
          if (error.status === 401)
            this.toastrService.error('Usuário ou senha inválidos!', 'Erro');
          else
          {
            console.log(error);
            this.toastrService.error('Erro ao realizar login!', 'Erro');
          }
        },
        complete: () => { }

      }).add(() => this.spinnerService.hide());
  }

  ngOnInit(): void {
  }

}
