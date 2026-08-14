import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { debounceTime, map, tap } from 'rxjs';
import { Palestrante } from '../../../models/Palestrante';

import { PalestranteService } from '../../../services/palestrante.service';

@Component({
  selector: 'app-palestrante-detalhe',
  templateUrl: './palestrante-detalhe.component.html',
  styleUrls: ['./palestrante-detalhe.component.css']
})
export class PalestranteDetalheComponent implements OnInit {

  public form!: FormGroup; 

  public situacaoDoForm: string = '';
  public corDaDescricao: string = '';

  constructor(private fb: FormBuilder,
              public palestranteService: PalestranteService,
              private toastrService: ToastrService,
              private spinnerService: NgxSpinnerService) { }


  private validation(): void
  {
    this.form = this.fb.group({
      miniCurriculo: [''],
    });
  }

  public get miniCurriculo(): any
  {
    return this.form.get('miniCurriculo');
  }

  private verificaForm(): void
  {
    this.form.valueChanges.pipe(
      map(() =>
      {
        this.situacaoDoForm = 'Mini Curriculo está sendo atualizado';
        this.corDaDescricao = 'text-warning';
      }),
      debounceTime(2000),
      tap(() => {  })
    ).subscribe(
      {
        next: () =>
        {
          this.palestranteService.putPalestrante(this.form.value).subscribe(
            {
              next: () =>
              {
                this.situacaoDoForm = 'Mini Curriculo foi atualizado';
                this.corDaDescricao = 'text-success';

                setTimeout(() =>
                {
                  this.situacaoDoForm = 'Mini Curriculo foi carregado';
                  this.corDaDescricao = 'text-muted';
                }, 2000)

              },
              error: (error: any) =>
              {
                console.log(error);
                this.toastrService.error('Mini Curriculo não foi atualizado!', 'Erro');
              }
            });
        }
      }).add(() => this.spinnerService.hide());
  }

  private carregarPalestrante(): void
  {
    this.palestranteService.getPalestrante().subscribe(
      {
        next: (palestrante: Palestrante) =>
        {
          this.form.patchValue(palestrante);
        },
        error: (error: any) =>
        {
          console.log(error);
          this.toastrService.error('Erro ao carregar o Palestrante!', 'Erro');
        }
      }).add(() => { });
  }

  ngOnInit(): void
  {
    this.validation();
    this.verificaForm();
    this.carregarPalestrante();
  }

}
