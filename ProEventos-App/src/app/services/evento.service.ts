import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { map, Observable, take } from 'rxjs';

import { Evento } from './../models/Evento';
import { PaginatedResult } from '../models/Pagination';

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

  public getEventos(page:number, itemsPerPage:number, termo?:string): Observable<PaginatedResult<Evento[]>>
  {
    const paginatedResult = {} as PaginatedResult<Evento[]>;
    let params = new HttpParams();
    let termoParam = termo ?? '';

    if (page !== null && itemsPerPage !== null)
    {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    if (termoParam !== null && termoParam !== '')
       params = params.append('termos', termoParam);

    return this.http.get<Evento[]>(this.baseUrl, { observe: 'response', params }).pipe(
      take(1),
      map((response) =>
      {
        paginatedResult.result = response.body ?? {} as Evento[];

        if (response.headers.has('X-Pagination'))
        {
          paginatedResult.pagination = JSON.parse(response.headers.get('X-Pagination') ?? '');
        }

        return paginatedResult;

      })
    );

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
