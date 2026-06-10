import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidatorField } from '../../../helper/ValidatorField';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.css']
})
export class PerfilComponent implements OnInit {

  form!: FormGroup
  constructor(private formBuilder: FormBuilder) { }

  public criarIntancia(): void {

    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('senha', 'confirmarSenha')
    };

    this.form = this.formBuilder.group({

      titulo:         ['', Validators.required],
      primeiroNome:   ['', Validators.required],
      ultimoNome:     ['', Validators.required],
      funcao:         ['', Validators.required],
      email:          ['', [Validators.required, Validators.email]],
      telefone:       ['', Validators.required],
      descricao:      ['', Validators.required],
      senha:          ['', [Validators.required, Validators.minLength(6)]],
      confirmarSenha: ['', Validators.required],

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

  public get telefone(): any {
    return this.form.get('telefone');
  }

  public get descricao(): any {
    return this.form.get('descricao');
  }

  public get senha(): any {
    return this.form.get('senha');
  }

  public get confirmarSenha(): any {
    return this.form.get('confirmarSenha');
  }

  ngOnInit(): void {
    this.criarIntancia();
  }

}
