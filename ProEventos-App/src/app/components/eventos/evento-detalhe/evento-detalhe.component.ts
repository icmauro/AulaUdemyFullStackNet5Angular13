import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.css']
})
export class EventoDetalheComponent implements OnInit {

  form!: FormGroup;

  get tema(): any {
    return this.form.get('tema');
  }
  get local(): any {
    return this.form.get('local');
  }
  get dataEvento(): any {
    return this.form.get('dataEvento');
  }
  get qtdPessoas(): any {
    return this.form.get('qtdPessoas');
  }
  get imagemURL(): any {
    return this.form.get('imagemURL');
  }
  get telefone(): any {
    return this.form.get('telefone');
  }
  get email(): any {
    return this.form.get('email');
  }


  constructor(private formBuilder: FormBuilder ) { }

 public criarInstancia(): void {

    this.form = this.formBuilder.group({

      local:      ['', Validators.required],
      dataEvento: ['', Validators.required],
      tema:       ['', [Validators.required, Validators.minLength(4), Validators.maxLength(150)]],
      qtdPessoas: ['', [Validators.required, Validators.max(100000)]],
      lote:       ['', Validators.required],
      imagemURL:  ['', Validators.required],
      telefone:   ['', Validators.required],
      email:      ['', [Validators.required, Validators.email]],

    })

 }

  public resetarForm(): void {
    this.form.reset();
  }

  ngOnInit(): void {
    this.criarInstancia();
  }

}
