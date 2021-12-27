import { Guid } from 'guid-typescript';
import { RequestGeneral } from '../../../../models/request/request-general';

export class RequestFirm extends RequestGeneral {
    firmId: string;
    cityId: string;
    programId: Guid;
    searchText: string;
}
