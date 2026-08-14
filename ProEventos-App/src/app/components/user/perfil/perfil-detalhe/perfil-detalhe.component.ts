import { Component, EventEmitter, OnInit, Output } from '@angular/core';

import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { ValidatorField } from '../../../../helper/ValidatorField';
import { UserUpdate } from '../../../../models/identity/UserUpdate';
import { AccountService } from '../../../../services/account.service';
import { PalestranteService } from '../../../../services/palestrante.service';
import { Constants } from '../../../../util/constants';

@Component({
  selector: 'app-perfil-detalhe',
  templateUrl: './perfil-detalhe.component.html',
  styleUrls: ['./perfil-detalhe.component.css']
})
export class PerfilDetalheComponent implements OnInit {

  public form!: FormGroup

  public funcaoGlobal = Constants;

  public userUpdate = {} as UserUpdate;

  @Output() changeFormValue = new EventEmitter();

  constructor(private formBuilder: FormBuilder,
              private accountService: AccountService,
              private palestranteService: PalestranteService,
              private router: Router,
              private toastrService: ToastrService,
              private spinnerService: NgxSpinnerService) { }

  public criarIntancia(): void
  {

    const formOptions: AbstractControlOptions =
    {
      validators: ValidatorField.MustMatch('password', 'confirmarPassword')
    };

    this.form = this.formBuilder.group({

      userName: [''],
      titulo: ['', Validators.required],
      primeiroNome: ['', Validators.required],
      ultimoNome: ['', Validators.required],
      funcao: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', Validators.required],
      descricao: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmarPassword: ['', Validators.required],

    }, formOptions)
  }

  public get titulo(): any {
    return this.form.get('titulo');
  }

  public get primeiroNome(): any {
    return this.form.get('primeiroNome');
  }

  public get ultimoNome(): any {
    return this.form.get('ultimoNome');
  }

  public get funcao(): any {
    return this.form.get('funcao');
  }

  public get email(): any {
    return this.form.get('email');
  }

  public get phoneNumber(): any {
    return this.form.get('phoneNumber');
  }

  public get descricao(): any {
    return this.form.get('descricao');
  }

  public get password(): any {
    return this.form.get('password');
  }

  public get confirmarPassword(): any {
    return this.form.get('confirmarPassword');
  }

  public get userName(): any {
    return this.form.get('userName');
  }

  public carregarUsuario(): void
  {
    // this.spinnerService.show();

    this.accountService.getUsuario().subscribe(
      {
        next: (_userUpdate: UserUpdate) => {
          this.userUpdate = { ..._userUpdate };
          this.form.patchValue(this.userUpdate);

        },
        error: (error: any) => {
          console.log(error);
          this.toastrService.error('Usuario não carregado!', 'Erro');
          this.router.navigate(['/dashboard']);
        },
        complete: () => {
        }
      }).add(() => this.spinnerService.hide());
  }

  public onSubmit(): void
  {
    this.atualizarUsuario();
  }

  public atualizarUsuario()
  {
    this.spinnerService.show();
    this.userUpdate = { ...this.form.value };

    if (this.funcao.value === 'Palestrante')
    {
      this.palestranteService.postPalestrante().subscribe(
        {
          next: () =>
          {
            this.toastrService.success('Função Palestrante Ativada!', 'Sucesso');
          },
          error: (error: any) =>
          {
            console.log(error);
            this.toastrService.error('Erro ao criar Função Palestrante!', 'Erro');
          },
          complete: () =>
          {
          }
        }).add();
    }

    this.accountService.atualizarUsuario(this.userUpdate).subscribe(
      {
        next: () => {
          this.toastrService.success('Usuario atualizado com sucesso!', 'Sucesso');
        },
        error: (error: any) => {
          console.log(error);
          this.toastrService.success('Erro ao tentar atualizar o Usuário!', 'Erro');
        },
        complete: () => {

        }
      }).add(() => this.spinnerService.hide());

  }

  private verificaForm(): void
  {
    this.form.valueChanges.subscribe(
      {
        next: () =>
        {
          this.changeFormValue.emit({ ...this.form.value });
        },
        error: (error: any) =>
        {
        },
        complete: () =>
        {
        }
      })
  }


  ngOnInit(): void
  {
    this.criarIntancia();
    this.carregarUsuario();
    this.verificaForm();
  }

}
