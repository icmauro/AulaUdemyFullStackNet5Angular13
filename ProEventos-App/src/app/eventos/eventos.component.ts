import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  constructor(private http: HttpClient) { }

  public eventos: any = [];
  public eventosFiltrados: any = [];

  widthImg: number = 50;
  heightImg: number = 60;

  mostrarImagem: boolean = true;
  private _filtroLista: string = '';

  public getEventos(): void {

    this.http.get('https://localhost:44368/api/eventos').subscribe(
      response => {
           this.eventos = response;
           this.eventosFiltrados = this.eventos;
      },
      error => console.log(error)
    );

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

  public FiltrarEventos(filtrarPor: string): any
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

  ngOnInit(): void {

    this.getEventos();

  }

}
