import { RedeSocial } from './RedeSocial';
import { Evento } from './Evento';
import { UserUpdate } from './identity/UserUpdate';

export interface Palestrante {

  id: number;
  userId: number;
  miniCurriculo: string;
  user: UserUpdate;
  redeSociais: RedeSocial[];
  palestranteEvento: Evento[];

}
