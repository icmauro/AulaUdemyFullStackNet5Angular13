import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { map, Observable, take } from 'rxjs';

import { PaginatedResult } from '../models/Pagination';
import { Palestrante } from '../models/Palestrante';

@Injectable(
  // { providedIn: 'root' }
)
export class PalestranteService {

  constructor(private http: HttpClient) { }

  baseUrl: string = 'https://localhost:44368/api/palestrantes';

  // currentHeader = new HttpHeaders(
  //   {
  //     Authorization: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJpbWF1cm8iLCJlbWFpbCI6Iml0YWxvQHRlcnJhLmNvbS5iciIsIm5iZiI6MTc4Mjk2NDgzNCwiZXhwIjoxNzgzMDUxMjM0LCJpYXQiOjE3ODI5NjQ4MzR9.b98ChFRi4nV3LWfst9wq5WMqfuZ3eKu_hJYfz-Q4AXY'
  //   });

  public getPalestrantes(page: number, itemsPerPage: number, termo?: string): Observable<PaginatedResult<Palestrante[]>>
  {
    const paginatedResult = {} as PaginatedResult<Palestrante[]>;
    let params = new HttpParams();
    let termoParam = termo ?? '';

    if (page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    if (termoParam !== null && termoParam !== '')
      params = params.append('termos', termoParam);

    return this.http.get<Palestrante[]>(this.baseUrl + '/todos', { observe: 'response', params }).pipe(
      take(1),
      map((response) => {
        paginatedResult.result = response.body ?? {} as Palestrante[];

        if (response.headers.has('X-Pagination')) {
          paginatedResult.pagination = JSON.parse(response.headers.get('X-Pagination') ?? '');
        }

        return paginatedResult;

      })
    );

  }

  public getPalestrante(): Observable<Palestrante> {

    return this.http.get<Palestrante>(`${this.baseUrl}/buscar`).pipe(take(1));

  }

  public postPalestrante(): Observable<Palestrante> {  

    return this.http.post<Palestrante>(this.baseUrl, {} ).pipe(take(1));

  }

  public putPalestrante(palestrante: Palestrante): Observable<Palestrante> {

    return this.http.put<Palestrante>(`${this.baseUrl}/atualizar`, palestrante).pipe(take(1));

  }

}
