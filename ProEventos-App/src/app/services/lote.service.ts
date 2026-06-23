import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Lote } from '../models/Lote';

@Injectable(
  //{ providedIn: 'root' }
)
export class LoteService {

  constructor(private http: HttpClient) { }

  baseUrl: string = 'https://localhost:44368/api/lotes';

  public getLotes(eventoId:number): Observable<Lote[]> {

    return this.http.get<Lote[]>(`${this.baseUrl}/${eventoId}`);

  }

  public salvarLotes(eventoId:number, lotes: Lote[]): Observable<Lote[]> {

    return this.http.put<Lote[]>(`${this.baseUrl}/salvar/${eventoId}`, lotes);

  }

  public deleteLote(eventoId: number, id:number): Observable<any> {

    return this.http.delete(`${this.baseUrl}/deletar/${eventoId}/lote/${id}`);

  }


}
