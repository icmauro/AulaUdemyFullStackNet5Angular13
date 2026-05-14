import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  constructor(private http: HttpClient) { }

  public eventos: any;

  public getEventos(): void {

    this.http.get('https://localhost:44368/api/eventos').subscribe(
      response => this.eventos = response,
      error => console.log(error)
    );

  }

  ngOnInit(): void {

    this.getEventos();

  }

}
