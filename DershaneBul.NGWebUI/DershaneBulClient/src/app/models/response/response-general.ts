export class ResponseGeneral {
  Success: boolean;
  StatusCode: Int32Array;
  Message: string
  Details: Array<string> = [];
}
