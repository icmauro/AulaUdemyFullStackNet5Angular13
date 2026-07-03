import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChildActivationEnd, RouterModule, Routes } from '@angular/router';

import { EventosComponent } from './components/eventos/eventos.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ContatosComponent } from './components/contatos/contatos.component';
import { PalestrantesComponent } from './components/palestrantes/palestrantes.component';
import { PerfilComponent } from './components/user/perfil/perfil.component';
import { EventoDetalheComponent } from './components/eventos/evento-detalhe/evento-detalhe.component';
import { EventoListaComponent } from './components/eventos/evento-lista/evento-lista.component';
import { UserComponent } from './components/user/user.component';
import { LoginComponent } from './components/user/login/login.component';
import { CadastroComponent } from './components/user/cadastro/cadastro.component';
import { HomeComponent } from './components/home/home.component';

import { AuthGuard } from './guard/auth.guard';

const routes: Routes = [
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path:'user', redirectTo : 'user/perfil'},
      { path: 'user/perfil', component: PerfilComponent },
      {
        path: 'eventos', component: EventosComponent,
        children: [
          { path: 'detalhe/:id', component: EventoDetalheComponent },
          { path: 'detalhe', component: EventoDetalheComponent },
          { path: 'lista', component: EventoListaComponent }
        ]
      },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'contatos', component: ContatosComponent },
      { path: 'palestrantes', component: PalestrantesComponent }
    ]
  },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'cadastro', component: CadastroComponent }
    ]
  },
  { path: 'home', component: HomeComponent },
  { path: '**', redirectTo: 'home', pathMatch: 'full'  }
];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
