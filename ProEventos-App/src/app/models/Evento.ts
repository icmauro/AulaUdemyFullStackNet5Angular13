import { RedeSocial } from './RedeSocial';
import { Lote } from './Lote';
import { Palestrante } from './Palestrante';


export interface Evento {

  id: number;   
  local: string; 
  dataEvento?: Date;
  tema: string;              
  qtdPessoas: number;
  lote: string;
  imagemURL: string;
  telefone: string; 
  email: string;
  lotes: Lote[]; 
  redeSocial: RedeSocial[]; 
  palestranteEventos: Palestrante[]; 

}
