import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder, FormControlName } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { Evento } from '../../../models/Evento';
import { EventoService } from '../../../services/evento.service';

import { Constants } from '../../../util/constants';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.css']
})
export class EventoDetalheComponent implements OnInit {

  constructor(private formBuilder: FormBuilder,
              private localeService: BsLocaleService,
              private router: ActivatedRoute,
              private eventoService: EventoService,
              private toastrService: ToastrService,
              private spinnerService: NgxSpinnerService)
  { }

  public form!: FormGroup;

  public funcaoGlobal = Constants;

  public evento = {} as Evento;

  private estadoSalvarOuAlterar: string = 'salvar';

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
  get lote(): any {
    return this.form.get('lote');
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

  public carregarEvento(): void
  {
    const eventoIdParam = this.router.snapshot.paramMap.get('id');

    if (eventoIdParam != null)
    {
      this.estadoSalvarOuAlterar = 'atualizar';

      this.eventoService.getEventoById(+eventoIdParam).subscribe({
        next:(_evento: Evento) => {
          this.evento = { ..._evento };
          this.form.patchValue(this.evento);
       },
        error: (error: any) => {
          this.spinnerService.hide();
          this.toastrService.error('Erro ao carregar os Evento(s)!', 'Erro');
        },
        complete: () => { this.spinnerService.hide(); }

      })
    }

  }

  public salvarAlteracao(): void {
    this.spinnerService.show();

    // console.log(this.form.controls);

   if (this.form.valid)
   {
      if (this.estadoSalvarOuAlterar == 'atualizar')
      {
        this.evento = { ...this.form.value, id:this.evento.id };
        this.atualizar(this.evento);
      }
      else if (this.estadoSalvarOuAlterar == 'salvar')
      {
        this.evento = { ...this.form.value };
        this.salvar(this.evento);
      }

    }

    this.spinnerService.hide();

  }

  private atualizar(evento: Evento): void
  {
    console.log(evento);

    this.eventoService.putEvento(evento, evento.id).subscribe({
      next: () => {
        this.toastrService.success('O Evento foi Atualizado com sucesso!', 'Atualizado');
      },
      error: (error: any) => {
        console.log(error);
        this.spinnerService.hide();
        this.toastrService.error('Erro ao atualizar os Evento(s)!', 'Erro');
      },
      complete: () => {
        this.spinnerService.hide();
      }
    })
  }

  private salvar(evento: Evento): void
  {
    this.eventoService.postEvento(evento).subscribe({
      next: () => {
        this.toastrService.success('O Evento foi inserido com sucesso!', 'Inserido');
      },
      error: (error: any) => {
        console.log(error);
        this.spinnerService.hide();
        this.toastrService.error('Erro ao inserir o Evento(s)!', 'Erro');
      },
      complete: () => {
        this.spinnerService.hide();
      }
    })
  }

  public resetarForm(): void
  {
    this.form.reset();
  }

  ngOnInit(): void
  {

    this.spinnerService.show();

    this.criarInstancia();
    this.carregarEvento();
    this.localeService.use("pt-br")

    this.spinnerService.hide();


  }


}
