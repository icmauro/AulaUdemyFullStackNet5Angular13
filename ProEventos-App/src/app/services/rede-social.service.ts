import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take } from 'rxjs';
import { RedeSocial } from '../models/RedeSocial';

@Injectable(
  // { providedIn: 'root' }
)
export class RedeSocialService {

  constructor(private http: HttpClient) { }

  baseUrl: string = 'https://localhost:44368/api/redesocial';

  public getRedesSociais(origem: string, id: number): Observable<RedeSocial[]>
  {
    let URL = id === 0 ? `${this.baseUrl}/${origem}` : `${this.baseUrl}/${origem}/${id}`;

    return this.http.get<RedeSocial[]>(URL).pipe(take(1));
  }

  public salvarRedesSociais(origem: string, id: number, redeSociais: RedeSocial[]): Observable<RedeSocial[]>
  {
    let URL = id === 0 ? `${this.baseUrl}/${origem}/salvar` : `${this.baseUrl}/${origem}/salvar/${id}`;

    return this.http.put<RedeSocial[]>(URL, redeSociais).pipe(take(1));
  }

  public deletarRedesSociais(origem: string, eventoId: number, redeSocialId: number): Observable<any>
  {
    let URL = eventoId === 0 ? `${this.baseUrl}/deletar/${origem}/${redeSocialId}` : `${this.baseUrl}/deletar/${origem}/${eventoId}/${redeSocialId}`;

    return this.http.delete(URL).pipe(take(1));
  }

}
