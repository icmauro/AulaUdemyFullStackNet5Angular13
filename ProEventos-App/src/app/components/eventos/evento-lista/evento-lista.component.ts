import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventoService } from './../../../services/evento.service';
import { Evento } from './../../../models/Evento';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from "ngx-spinner";
import { Router } from '@angular/router';

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
  public eventosFiltrados: Evento[] = [];

  public eventoId: number = 0;

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
        console.log(error);
        this.spinnerService.hide();
        this.toastrService.error('Erro ao carregar os Evento(s)!', 'Erro');
      },
      complete: () => { this.spinnerService.hide(); }
    });

  }

  public get FiltroLista(): string {
    return this._filtroLista;
  }

  public set FiltroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this.FiltroLista ? this.FiltrarEventos(this.FiltroLista) : this.eventos;
  }

  public FiltrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();

    return this.eventos.filter(
      (evento: { tema: string; local: string }) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
        evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )

  }

  public AlternarImagem(): void {

    this.mostrarImagem = !this.mostrarImagem;

  }

  public detalheEvento(id: number): void {
    this.router.navigate([`/eventos/detalhe/${id}`]);
  }

  //\/\/\/Eventos do Modal Confirm
  public openModal(template: TemplateRef<void>, eventoId: number): void
  {
    this.eventoId = eventoId;
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  public confirm(): void {

    this.modalRef?.hide();
    this.spinnerService.show();

    this.eventoService.deleteEvento(this.eventoId).subscribe({
      next: (result: any) => {
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

  ngOnInit(): void {

    this.spinnerService.show();
    this.getEventos();

  }

}
