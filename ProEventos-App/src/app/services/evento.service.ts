import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Evento } from './../models/Evento';

@Injectable(
  //{ providedIn: 'root' }
)
export class EventoService {

  constructor(private http: HttpClient) { }

  baseUrl: string = 'https://localhost:44368/api/eventos'; 

  public getEventos(): Observable<Evento[]> {

    return this.http.get<Evento[]>(this.baseUrl);

  }

  public getEventosByTema(tema:string): Observable<Evento[]> {

    return this.http.get<Evento[]>(`${this.baseUrl}/tema/${tema}`);

  }

  public getEventoById(id:number): Observable<Evento> {

    return this.http.get<Evento>(`${this.baseUrl}/${id}`);

  }

}
