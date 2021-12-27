import { ResponseGeneral } from '../../../../models/response/response-general';

export class ResponseFirm extends ResponseGeneral {
  public firmId: string;
  public firmName: string;
  public firmDescription: string;
  public fullName: string;
  public userName: string;
  public street: string;
  public addressDescription: string;
  public doorNumber: string;
  public cityName: string;
  public townName: string;
}
