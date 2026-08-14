import { Component, OnInit } from '@angular/core';

import { Router } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { debounceTime, Subject } from 'rxjs';

import { PaginatedResult, Pagination } from '../../../models/Pagination';
import { Palestrante } from '../../../models/Palestrante';
import { PalestranteService } from '../../../services/palestrante.service';

@Component({
  selector: 'app-palestrante-lista',
  templateUrl: './palestrante-lista.component.html',
  styleUrls: ['./palestrante-lista.component.css']
})
export class PalestranteListaComponent implements OnInit {

  public termoBuscaChanged: Subject<string> = new Subject<string>();

  public pagination = {} as Pagination;

  public palestrantes: Palestrante[] = [];

  public palestranteId: number = 0;

  constructor(private palestranteService: PalestranteService,
              private toastrService: ToastrService,
              private spinnerService: NgxSpinnerService,
              private router: Router) { }



  public getPalestrantes(): void
  {
    this.spinnerService.show();

    this.palestranteService.getPalestrantes(this.pagination.currentPage, this.pagination.itemsPerPage).subscribe({
      next: (_paginatedResulted: PaginatedResult<Palestrante[]>) =>
       {
         this.palestrantes = _paginatedResulted.result;
         this.pagination = _paginatedResulted.pagination;
       },
      error: (error: any) =>
       {
         console.log(error);
         this.spinnerService.hide();
         this.toastrService.error('Erro ao carregar os Evento(s)!', 'Erro');
       },
      complete: () => { }
      }).add(() => { this.spinnerService.hide(); });

  }

  public FiltrarPalestrantes(filtrarPor: string): void
  {
    if (!this.termoBuscaChanged.observed)
    {
      this.spinnerService.show();

      this.termoBuscaChanged.pipe(debounceTime(1000)).subscribe(
        {
          next: (filtroTermo) => {
            this.palestranteService.getPalestrantes(this.pagination.currentPage, this.pagination.itemsPerPage, filtroTermo).subscribe({
              next: (_paginatedResulted: PaginatedResult<Palestrante[]>) =>
              {
                this.palestrantes = _paginatedResulted.result;
                this.pagination = _paginatedResulted.pagination;
              },
              error: (error: any) =>
              {
                console.log(error);
                this.spinnerService.hide();
                this.toastrService.error('Erro ao carregar os Palestrante(s)!', 'Erro');
              },
              complete: () =>
              {
              }
            }).add(() => { this.spinnerService.hide(); });
          }
        });
    }

    this.termoBuscaChanged.next(filtrarPor);

  }

  ngOnInit(): void
  {
    this.pagination = { currentPage: 1, itemsPerPage: 5 } as Pagination;
    this.getPalestrantes();
  }

}
