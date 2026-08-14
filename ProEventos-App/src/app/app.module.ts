import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'
import { FormsModule, ReactiveFormsModule} from '@angular/forms'

import { AppComponent } from './app.component';
import { EventosComponent } from './components/eventos/eventos.component';
import { PalestrantesComponent } from './components/palestrantes/palestrantes.component';
import { PerfilComponent } from './components/user/perfil/perfil.component';
import { ContatosComponent } from './components/contatos/contatos.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { NavComponent } from './nav/nav.component';
import { EventoDetalheComponent } from './components/eventos/evento-detalhe/evento-detalhe.component';
import { EventoListaComponent } from './components/eventos/evento-lista/evento-lista.component';
import { UserComponent } from './components/user/user.component';
import { LoginComponent } from './components/user/login/login.component';
import { CadastroComponent } from './components/user/cadastro/cadastro.component';
import { TituloComponent } from './shared/titulo/titulo.component';
import { HomeComponent } from './components/home/home.component';
import { PerfilDetalheComponent } from './components/user/perfil/perfil-detalhe/perfil-detalhe.component';
import { PalestranteListaComponent } from './components/palestrantes/palestrante-lista/palestrante-lista.component';
import { PalestranteDetalheComponent } from './components/palestrantes/palestrante-detalhe/palestrante-detalhe.component';
import { RedesSociaisComponent } from './components/redes-sociais/redes-sociais.component';

import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from "ngx-spinner";
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { TabsModule } from 'ngx-bootstrap/tabs';

import { NgxCurrencyModule, CURRENCY_MASK_CONFIG, CurrencyMaskConfig } from "ngx-currency";

import { EventoService } from './services/evento.service';
import { LoteService } from './services/lote.service';
import { AccountService } from './services/account.service';
import { PalestranteService } from './services/palestrante.service';
import { RedeSocialService } from './services/rede-social.service';

import { DateTimeFormatPipe } from './helper/date-time-format.pipe';

import { AppRoutingModule } from './app-routing.module';
import { JwtInterceptor } from './interceptor/jwt.interceptor';




defineLocale('pt-br', ptBrLocale);

export const CustomCurrencyMaskConfig: CurrencyMaskConfig = {
  align: "left",
  allowNegative: false,
  allowZero: true,
  decimal: ",",
  precision: 2,
  prefix: "R$ ",
  suffix: "",
  thousands: ".",
  nullable: false,
  inputMode: 0 // Abre espaço para digitação natural (muda o comportamento do cursor)
};


@NgModule({
  declarations: [
    AppComponent,
    EventosComponent,
    PalestrantesComponent,
    NavComponent,
    DateTimeFormatPipe,
    TituloComponent,
    ContatosComponent,
    DashboardComponent,
    PerfilComponent,
    EventoDetalheComponent,
    EventoListaComponent,
    UserComponent,
    LoginComponent,
    CadastroComponent,
    HomeComponent,
    PerfilDetalheComponent,
    PalestranteListaComponent,
    PalestranteDetalheComponent,
    RedesSociaisComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    CollapseModule,
    TooltipModule,
    BsDropdownModule,
    ToastrModule.forRoot({
      timeOut: 5000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      progressBar: true,
    }),
    NgxSpinnerModule,
    ModalModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    BsDatepickerModule,
    NgxCurrencyModule,
    PaginationModule,
    TabsModule
  ],
  // schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [EventoService,
              LoteService,
              BsModalService,
              AccountService,
              PalestranteService,
              RedeSocialService,
              [{ provide: CURRENCY_MASK_CONFIG, useValue: CustomCurrencyMaskConfig },
              { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }]
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
