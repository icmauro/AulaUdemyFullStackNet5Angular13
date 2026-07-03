import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, ReplaySubject, take } from 'rxjs';

import { User } from '../models/identity/User';
import { UserUpdate } from '../models/identity/UserUpdate';

@Injectable(
  // {  providedIn: 'root'  }
)
export class AccountService {

  baseUrl: string = 'https://localhost:44368/api/account';

  private currentUserSource = new ReplaySubject<User | UserUpdate>(1);
  public currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }
   

  public login(model: any): Observable<void>
  {
    return this.http.post<User>(this.baseUrl + '/Login', model).pipe(
      take(1),
      map((user: User) =>
      {
        if (user)
        {
          this.setCurrentUser(user);
        }

      })
    )
  }

  public registrar(model: any): Observable<void>
  {
    return this.http.post<User>(this.baseUrl + '/Registrar', model).pipe(
      take(1),
      map((user: User) =>
      {
        if (user)
        {
          this.setCurrentUser(user);
        }

      })
    )
  }

  public getUsuario(): Observable<UserUpdate>
  {
    return this.http.get<UserUpdate>(this.baseUrl + '/GetUsuario').pipe(take(1));
  }

  public atualizarUsuario(userUpdate: UserUpdate): Observable<void>
  {
    return this.http.put<UserUpdate>(this.baseUrl + '/Atualizar', userUpdate).pipe(
      take(1),
      map((user: UserUpdate) =>
      {
        if (user)
        {
          this.setCurrentUser(user);
        }

      })
    );
  }

  public logout(): void
  {
    localStorage.removeItem('user');
    this.currentUserSource.next(null as any);
    this.currentUserSource.complete();
  }

  public setCurrentUser(user: User | UserUpdate):void
  {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

}
