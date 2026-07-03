import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { ValidatorField } from '../../../helper/ValidatorField';
import { AccountService } from '../../../services/account.service';
import { Constants } from '../../../util/constants';

import { User } from '../../../models/identity/User';

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.component.html',
  styleUrls: ['./cadastro.component.css']
})
export class CadastroComponent implements OnInit {

  public form!: FormGroup

  public funcaoGlobal = Constants;

  public user = {} as User;
  constructor(private formBuilder: FormBuilder,
              private accountService: AccountService,
              private router: Router,
              private toastrService: ToastrService) { }

  public criarIntancia(): void {

    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('password', 'confirmarPassword')
    };

    this.form = this.formBuilder.group({

      primeiroNome:      ['', Validators.required],
      ultimoNome:        ['', Validators.required],
      email:             ['', [Validators.required, Validators.email]],
      usuario:           ['', Validators.required],
      password:          ['', [Validators.required,Validators.minLength(6)]],
      confirmarPassword: ['', Validators.required],

    }, formOptions)
  }

  public get primeiroNome(): any {
    return this.form.get('primeiroNome');
  }

  public get ultimoNome(): any {
    return this.form.get('ultimoNome');
  }

  public get email(): any {
    return this.form.get('email');
  }

  public get usuario(): any {
    return this.form.get('usuario');
  }

  public get password(): any {
    return this.form.get('password');
  }

  public get confirmarPassword(): any {
    return this.form.get('confirmarPassword');
  }

  public registrar(): void
  {
    this.user = { ...this.form.value };
    this.accountService.registrar(this.user).subscribe(
    {
        next: () =>
        {
           this.router.navigateByUrl('/dashboard');
        },
        error: (error: any) =>
        {
          this.toastrService.error(error.error, 'Erro');
        },
        complete: () => { }

    })
  }

  ngOnInit(): void {
    this.criarIntancia();
  }

}
