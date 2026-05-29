import { Component, OnInit,TemplateRef } from '@angular/core';
import { EventoService } from './../../services/evento.service';
import { Evento } from './../../models/Evento';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from "ngx-spinner";

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {
 modalRef?: BsModalRef;
 message?: string;

  constructor(private eventoService: EventoService,
              private modalService: BsModalService,
              private toastrService: ToastrService,
              private spinnerService: NgxSpinnerService) { }

  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];

  widthImg: number = 50;
  heightImg: number = 60;

  mostrarImagem: boolean = true;
  private _filtroLista: string = '';

  public getEventos(): void {

    this.eventoService.getEventos().subscribe({
      next: (_evento: Evento[]) => {
        this.eventos = _evento;
        this.eventosFiltrados = _evento;
      },
      error: (error: any) => {
        this.spinnerService.hide();
        this.toastrService.error('Erro ao carregar os Evento(s)!', 'Erro');
      },
      complete: () => { this.spinnerService.hide(); }
    });

  }

  public get FiltroLista(): string
  {
    return this._filtroLista;
  }

  public set FiltroLista(value: string)
  {
    this._filtroLista = value;
    this.eventosFiltrados = this.FiltroLista ? this.FiltrarEventos(this.FiltroLista) : this.eventos;
  }

  public FiltrarEventos(filtrarPor: string): Evento[]
  {
    filtrarPor = filtrarPor.toLocaleLowerCase();

    return this.eventos.filter(
      (evento: { tema: string; local: string }) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
                                                   evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )

  }

  public AlternarImagem(): void {

    this.mostrarImagem = !this.mostrarImagem;

  }

  //\/\/\/Eventos do Modal Confirm
  public openModal(template: TemplateRef<void>):void {
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }
 
  public confirm(): void {

    this.modalRef?.hide();
    this.toastrService.success('O Evento foi deletado com sucesso!', 'Deletado');
  }

  public decline(): void {

    this.modalRef?.hide();
  }
  //\/\/\/Eventos do Modal Confirm

  ngOnInit(): void {

    this.spinnerService.show();
    this.getEventos(); 

  //setTimeout(() => {
  //  /** spinner ends after 5 seconds */
  //  this.spinnerService.hide();
  //}, 5000);

  }

}
