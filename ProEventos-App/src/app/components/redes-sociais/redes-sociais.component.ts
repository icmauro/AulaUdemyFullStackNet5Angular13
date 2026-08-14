import { Component, Input, OnInit, Output, TemplateRef } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { RedeSocial } from '../../models/RedeSocial';

import { RedeSocialService } from '../../services/rede-social.service';
import { Constants } from '../../util/constants';

@Component({
  selector: 'app-redes-sociais',
  templateUrl: './redes-sociais.component.html',
  styleUrls: ['./redes-sociais.component.css']
})
export class RedesSociaisComponent implements OnInit {

  public modalRef?: BsModalRef;
  public message?: string;

  public formRS!: FormGroup;
  public redeSocialAtual = {} as RedeSocial & { indice : number};

  @Input() eventoId: number = 0;

  public funcaoGlobal = Constants;

  get redesSociais(): FormArray
  {
    return this.formRS.get('redesSociais') as FormArray;
  }

  constructor(private redeSocialService: RedeSocialService,
              private modalService: BsModalService,
              private toastrService: ToastrService,
              private spinnerService: NgxSpinnerService,
              private formBuilder: FormBuilder) { }
              

  public criarInstancia(): void
  {

    this.formRS = this.formBuilder.group(
    {
        redesSociais: this.formBuilder.array([])
    })

  }

  public retornaTitulo(campo: string): string
  {
    return campo == null || campo == '' ? 'Nome da Rede Social ' : campo;
  }

  public carregarRedeSocial(id: number = 0): void
  {
    let origem = 'palestrante';

    if (this.eventoId !== 0)
      origem = 'evento';

    this.spinnerService.show();

    this.redeSocialService.getRedesSociais(origem, id).subscribe(
      {
        next: (_redeSociais: RedeSocial[]) =>
        {
          _redeSociais.forEach(redesocial =>
          {
            this.redesSociais.push(this.criarRedeSocial(redesocial));
          });
        },
        error: (error: any) =>
        {
          console.log(error);
          this.toastrService.error('Erro ao tentar carregar a(s) Rede Social(ais)!', 'Erro');
        }
      }).add(() => { this.spinnerService.hide() });
  }


  public adicionarRedeSocial(): void
  {
    this.redesSociais.push(this.criarRedeSocial({ id: 0 } as RedeSocial));
  }

  public criarRedeSocial(redeSocial: RedeSocial): FormGroup
  {
    return this.formBuilder.group(
      {
        id:    [redeSocial.id],
        nome:  [redeSocial.nome, Validators.required],
        url:   [redeSocial.url, Validators.required]

      })
  }

  public salvarRedeSociais(): void
  {
    let origem = 'palestrante';

    if (this.eventoId !== 0)
      origem = 'evento';

    if (this.redesSociais.length == 0)
      return;

    if (this.formRS.controls['redesSociais'].valid)
    {
      this.spinnerService.show();

      this.redeSocialService.salvarRedesSociais(origem, this.eventoId, this.formRS.value.redesSociais).subscribe(
       {
          next: () =>
          {
             this.toastrService.success('Rede Sociais foram salvos com sucesso!', 'Sucesso');
             this.redesSociais.clear();
             this.carregarRedeSocial(this.eventoId);
          },
          error: (error: any) =>
          {
             console.log(error);
             this.toastrService.error('Erro ao tentar salvar a(s) Rede Social(ais)!', 'Erro');
          },
           complete: () => { }
       }).add(() => { this.spinnerService.hide(); })
    }
  }

  public removerRedeSocial(template: TemplateRef<void>, index: number): void
  {

    if (this.redesSociais.at(index).value?.id > 0)
    {
       this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
       this.redeSocialAtual = { ...this.redesSociais.at(index).value, indice: index };

      console.log(this.redeSocialAtual);
    }
    else
    {
      this.redesSociais.removeAt(index);
    }
  }

  //\/\/\/\/Eventos do Modal Confirm
  public decline(): void {

    this.modalRef?.hide();
  }

  public confirmDeleteRedeSocial(): void
  {
    this.modalRef?.hide();
    this.spinnerService.show();
    let origem = 'palestrante';

    if (this.eventoId !== 0)
      origem = 'evento';

    this.redeSocialService.deletarRedesSociais(origem, this.eventoId, this.redeSocialAtual.id).subscribe(
    {
        next: () =>
        {
          this.redesSociais.removeAt(this.redeSocialAtual.indice);
          this.toastrService.success('Rede Social excluído com sucesso!', 'Sucesso');
        },
        error: (error: any) =>
        {
          console.log(error);
          this.toastrService.error('Erro ao tentar excluir a Rede Social!', 'Erro');
        },
        complete: () => { }

    }).add(() => { this.spinnerService.hide(); })

  }
  //\/\/\/Eventos do Modal Confirm

  ngOnInit(): void
  {
    this.criarInstancia();


    this.carregarRedeSocial(this.eventoId);

  }

}
