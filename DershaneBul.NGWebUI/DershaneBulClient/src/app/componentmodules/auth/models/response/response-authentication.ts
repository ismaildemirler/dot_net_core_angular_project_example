import { ResponseGeneral } from 'src/app/models/response/response-general';

export class ResponseAuthentication extends ResponseGeneral {
  RefreshToken: string;
  Token: string
}
