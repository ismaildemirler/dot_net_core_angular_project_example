import { Guid } from "guid-typescript";
import { ResponseGeneral } from '../../../../models/response/response-general';

export class ResponseProgram extends ResponseGeneral{
  public programId: Guid;
  public programDescription: string;
}
