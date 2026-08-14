import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ValidatorField } from '../../../helper/ValidatorField';
import { UserUpdate } from '../../../models/identity/UserUpdate';
import { AccountService } from '../../../services/account.service';
import { Constants } from '../../../util/constants';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.css']
})
export class PerfilComponent implements OnInit {

  public usuario = {} as UserUpdate;

  public get ehPalestrante(): boolean
  {
    return this.usuario.funcao === 'Palestrante';
  }

  constructor(private formBuilder: FormBuilder,
              private accountService: AccountService,
              private router: Router,
              private toastrService: ToastrService,
              private spinnerService: NgxSpinnerService) { }



  public setFormValue(usuario:UserUpdate): void
  {
    this.usuario = usuario;
  }

  ngOnInit(): void
  {

  }

}
