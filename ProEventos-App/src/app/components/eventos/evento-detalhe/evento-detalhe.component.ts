import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder, FormControlName, FormArray } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { Evento } from '../../../models/Evento';
import { Lote } from '../../../models/Lote';

import { EventoService } from '../../../services/evento.service';
import { LoteService } from '../../../services/lote.service';

import { Constants } from '../../../util/constants';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.css']
})
export class EventoDetalheComponent implements OnInit {

  constructor(private formBuilder: FormBuilder,
    private localeService: BsLocaleService,
    private activateRouter: ActivatedRoute,
    private eventoService: EventoService,
    private loteService: LoteService,
    private toastrService: ToastrService,
    private spinnerService: NgxSpinnerService,
    private modalService: BsModalService,
    private router: Router) { }

  public form!: FormGroup;

  public funcaoGlobal = Constants;

  public evento = {} as Evento;

  private estadoSalvarOuAlterar: string = 'salvar';

  private eventoId!: number;

  public loteAtual = {} as Lote & { indice: number };

  modalRef?: BsModalRef;

  get tema(): any
  {
    return this.form.get('tema');
  }
  get local(): any
  {
    return this.form.get('local');
  }
  get dataEvento(): any
  {
    return this.form.get('dataEvento');
  }
  get qtdPessoas(): any
  {
    return this.form.get('qtdPessoas');
  }
  get lote(): any
  {
    return this.form.get('lote');
  }
  get imagemURL(): any
  {
    return this.form.get('imagemURL');
  }
  get telefone(): any
  {
    return this.form.get('telefone');
  }
  get email(): any
  {
    return this.form.get('email');
  }

  get lotes(): FormArray
  {
    return this.form.get('lotes') as FormArray;
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
      lotes:      this.formBuilder.array([]),

    })

  }

  public carregarEvento(): void
  {
    this.eventoId = Number(this.activateRouter.snapshot.paramMap.get('id') ?? 0);

    if (this.eventoId > 0)
    {
      this.estadoSalvarOuAlterar = 'atualizar';

      this.eventoService.getEventoById(this.eventoId).subscribe({
        next: (_evento: Evento) => {
          this.evento = { ..._evento };
          this.form.patchValue(this.evento);
          this.evento.lotes?.forEach(lote => { this.lotes.push(this.criarLote(lote)) });
        },
        error: (error: any) => {
          console.log(error);
          this.spinnerService.hide();
          this.toastrService.error('Erro ao carregar os Evento(s)!', 'Erro');
        },
        complete: () => { }

      }).add(() => { this.spinnerService.hide(); });
    }

  }

  public salvarEvento(): void
  {
    this.spinnerService.show();

    // console.log(this.form.controls);

    if (this.form.valid)
    {
      if (this.estadoSalvarOuAlterar == 'atualizar')
      {
        this.evento = { ...this.form.value, id: this.evento.id };
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

      }
    }).add(() => { this.spinnerService.hide(); })
  }

  private salvar(evento: Evento): void
  {
    this.eventoService.postEvento(evento).subscribe({
      next: (_evento: Evento) => {
        this.toastrService.success('O Evento foi inserido com sucesso!', 'Inserido');
        this.router.navigate([`/eventos/detalhe/${_evento.id}`]);
      },
      error: (error: any) => {
        console.log(error);
        this.spinnerService.hide();
        this.toastrService.error('Erro ao inserir o Evento(s)!', 'Erro');
      },
      complete: () => {

      }
    }).add(() => { this.spinnerService.hide(); })
  }

  public resetarForm(): void
  {
    this.form.reset();
  }

  //    \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ Lotes \/ \/ \/ \/ \/ \/ \/ \/ \/ \/

  public retornaTituloLote(campo: string): string
  {
    return campo == null || campo == '' ? 'Nome do lote' : campo;
  }

  public adicionarLote(): void
  {
    this.lotes.push(this.criarLote({ id: 0 } as Lote));
  }

  public criarLote(lote: Lote): FormGroup
  {
      return this.formBuilder.group(
      {
        id:         [lote.id],
        nome:       [lote.nome, Validators.required],
        preco:      [lote.preco, Validators.required],
        qtd:        [lote.qtd, Validators.required],
        dataInicio: [lote.dataInicio, Validators.required],
        dataFim:    [lote.dataFim, Validators.required]

      })
  }

  public modoEditar(): boolean {
    return this.estadoSalvarOuAlterar == 'atualizar';
  }

  public salvarLotes(): void
  {

    if (this.lotes.length == 0)
    {
      return;
    }

    if (this.form.controls['lotes'].valid)
    {
      this.spinnerService.show();
      this.loteService.salvarLotes(this.eventoId, this.form.value.lotes).subscribe({
        next: () =>
        {
          this.toastrService.success('Os Lotes foram salvos sucesso!', 'Sucesso');
          // this.lotes.reset();
          this.carregarEvento();
        },
        error: (error: any) =>
        {
          console.log(error);
          this.toastrService.error('Erro ao tentar salvar o(s) Lote(s)!', 'Erro');
        },
        complete: () => {  }
      }).add(() =>
      {
        this.spinnerService.hide();
      })
    }
  }

  public carregarLotes(): void
  {
    this.loteService.getLotes(this.eventoId).subscribe({
      next: (lotesRetorno: Lote[]) =>
      {
        lotesRetorno.forEach(lote => {
          this.lotes.push(this.criarLote(lote));
        });
      },
      error: (error: any) =>
      {
        console.log(error);
        this.toastrService.error('Erro ao tentar carregar o(s) Lote(s)!', 'Erro');
      },
      complete: () => { }

    }).add(() => { this.spinnerService.hide(); })
  }

  public removerLote(template: TemplateRef<void>, index: number): void
  {
    this.loteAtual.indice = index;

    if (this.lotes.at(index).value?.id > 0)
    {
      this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
      this.loteAtual = this.lotes.at(index).value;
    }
    else
    {
      this.lotes.removeAt(index);
    }
  }

  public confirmDeleteLote(): void
  {
    this.modalRef?.hide();
    this.spinnerService.show();

    this.loteService.deleteLote(this.eventoId, this.loteAtual.id).subscribe({
      next: () =>
      {
        this.lotes.removeAt(this.loteAtual.indice);
        this.toastrService.success('Lote excluído com sucesso!', 'Sucesso');
      },
      error: (error: any) =>
      {
        console.log(error);
        this.toastrService.error('Erro ao tentar excluir o Lote!', 'Erro');
      },
      complete: () => { }

    }).add(() => { this.spinnerService.hide(); })

  }

  public declineDeleteLote(): void
  {
    this.modalRef?.hide();
  }

  //    /\ /\ /\ /\ /\ /\ /\ /\ /\ /\ Lotes /\ /\ /\ /\ /\ /\ /\ /\ /\ /\

  ngOnInit(): void {

    this.spinnerService.show();

    this.criarInstancia();
    this.carregarEvento();
    this.localeService.use("pt-br")

    this.spinnerService.hide();


  }


}
