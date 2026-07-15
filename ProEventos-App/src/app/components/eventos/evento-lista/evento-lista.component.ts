import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from "ngx-spinner";


import { PaginatedResult, Pagination } from '../../../models/Pagination';
import { Evento } from './../../../models/Evento';

import { EventoService } from './../../../services/evento.service';

import { debounceTime, Subject } from 'rxjs';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.css']
})
export class EventoListaComponent implements OnInit {

  modalRef?: BsModalRef;
  message?: string;

  constructor(private eventoService: EventoService,
              private modalService: BsModalService,
              private toastrService: ToastrService,
              private spinnerService: NgxSpinnerService,
              private router: Router) { }

  public eventos: Evento[] = [];

  public pagination = {} as Pagination;

  public eventoId: number = 0;

  widthImg: number = 50;
  heightImg: number = 60;

  mostrarImagem: boolean = true;
  private _filtroLista: string = '';

  public termoBuscaChanged: Subject<string> = new Subject<string>();

  public getEventos(): void
  {
    this.spinnerService.show();

    this.eventoService.getEventos(this.pagination.currentPage,
                                  this.pagination.itemsPerPage).subscribe({
      next: (_paginatedResulted: PaginatedResult<Evento[]>) =>
      {
        this.eventos = _paginatedResulted.result;
        this.pagination = _paginatedResulted.pagination;
      },
      error: (error: any) => {
        console.log(error);
        this.spinnerService.hide();
        this.toastrService.error('Erro ao carregar os Evento(s)!', 'Erro');
      },
      complete: () => {  }
    }).add(() => { this.spinnerService.hide(); });

  }


  public FiltrarEventos(filtrarPor: string):void
  {
    if (!this.termoBuscaChanged.observed)
    {
      this.spinnerService.show();

      this.termoBuscaChanged.pipe(debounceTime(1000)).subscribe(
      {
        next:(filtroTermo) =>
        {
           this.eventoService.getEventos(this.pagination.currentPage, this.pagination.itemsPerPage, filtroTermo).subscribe({
            next: (_paginatedResulted: PaginatedResult<Evento[]>) =>
            {
              this.eventos = _paginatedResulted.result;
              this.pagination = _paginatedResulted.pagination;
            },
            error: (error: any) =>
            {
              console.log(error);
              this.spinnerService.hide();
              this.toastrService.error('Erro ao carregar os Evento(s)!', 'Erro');
            },
            complete: () =>
            {
            }
          }).add(() => { this.spinnerService.hide(); });
        }
      });
    }

    this.termoBuscaChanged.next(filtrarPor);

  }

  public AlternarImagem(): void {

    this.mostrarImagem = !this.mostrarImagem;

  }

  public detalheEvento(id: number): void {
    this.router.navigate([`/eventos/detalhe/${id}`]);
  }

  //\/\/\/\/Eventos do Modal Confirm
  public openModal(template: TemplateRef<void>, eventoId: number): void
  {
    this.eventoId = eventoId;
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  public confirm(): void {

    this.modalRef?.hide();
    this.spinnerService.show();

    this.eventoService.deleteEvento(this.eventoId).subscribe({
      next: (result: any) =>
      {
        if (result.message == "Evento deleteado com sucesso.")
        {
          this.toastrService.success('O Evento foi deletado com sucesso!', 'Deletado');
          this.getEventos();
        }
        else {
          this.toastrService.error(result.message, 'Erro');
        }

      },
      error: (error: any) => {
        console.log(error);
        this.spinnerService.hide();
        this.toastrService.error(`Erro ao tentar deletar o Evento ${this.eventoId}`, 'Erro');
      },
      complete: () => {
        this.spinnerService.hide();
      }
    })

    
  }

  public decline(): void {

    this.modalRef?.hide();
  }
  //\/\/\/Eventos do Modal Confirm

  public pageChanged(event: any): void
  {
    this.pagination.currentPage = event.page;
    this.getEventos();
  }

  ngOnInit(): void {

    this.pagination = { currentPage: 1, itemsPerPage: 5, totalItens : 3} as Pagination;
    this.getEventos();

  }

}
