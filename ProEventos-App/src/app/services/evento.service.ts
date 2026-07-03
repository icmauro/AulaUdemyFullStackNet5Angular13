import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, take } from 'rxjs';

import { Evento } from './../models/Evento';

@Injectable(
  //{ providedIn: 'root' }
)
export class EventoService {

  constructor(private http: HttpClient) { }

  baseUrl: string = 'https://localhost:44368/api/eventos';

  // currentHeader = new HttpHeaders(
  //   {
  //     Authorization: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJpbWF1cm8iLCJlbWFpbCI6Iml0YWxvQHRlcnJhLmNvbS5iciIsIm5iZiI6MTc4Mjk2NDgzNCwiZXhwIjoxNzgzMDUxMjM0LCJpYXQiOjE3ODI5NjQ4MzR9.b98ChFRi4nV3LWfst9wq5WMqfuZ3eKu_hJYfz-Q4AXY'
  //   });

  public getEventos(): Observable<Evento[]> {

    return this.http.get<Evento[]>(this.baseUrl).pipe(take(1));

  }

  public getEventosByTema(tema:string): Observable<Evento[]> {

    return this.http.get<Evento[]>(`${this.baseUrl}/tema/${tema}`).pipe(take(1));

  }

  public getEventoById(id:number): Observable<Evento> {

    return this.http.get<Evento>(`${this.baseUrl}/${id}`).pipe(take(1));

  }

  public postEvento(evento: Evento): Observable<Evento> {

    return this.http.post<Evento>(`${this.baseUrl}`, evento).pipe(take(1));

  }

  public putEvento(evento: Evento, id:number): Observable<Evento> {

    return this.http.put<Evento>(`${this.baseUrl}/atualizar/${id}`, evento).pipe(take(1));

  }

  public deleteEvento(id: number): Observable<any> {

    return this.http.delete(`${this.baseUrl}/deletar/${id}`).pipe(take(1));

  }

}
