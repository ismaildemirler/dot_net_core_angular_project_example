import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { FirmProperty } from "../models/firm-property"
import { FirmContact } from "../models/firm-contact"
import { FirmAddress } from "../models/firm-address"
import { FirmPropertyMenu } from "../models/firm-property-menu"
import { AppConstants } from 'src/app/utils/app-constants';
import { Observable } from 'rxjs/Rx'

@Injectable({
    providedIn: 'root'
})
export class DetailPageService {

    constructor(private constants: AppConstants, private _http: HttpClient) {

    }
    firmAddress: FirmAddress;
    path = this.constants.BASE_URL_DEV;

    getProperty(): Observable<FirmProperty> {
        let apiPropertyUrl = this.path + 'firms/getfirma';
        return this._http.get<FirmProperty>(apiPropertyUrl);
    }

    getFirmPropertyMenu(): Observable<FirmPropertyMenu> {
        let apiFirmPropertyMenuUrl = this.path + 'firms/getFirmPropertyMenu';
        return this._http.get<FirmPropertyMenu>(apiFirmPropertyMenuUrl);
    }
    getFirmContact(): Observable<FirmContact> {
        let apiFirmContactUrl = this.path + 'firms/getFirmContact';
        return this._http.get<FirmContact>(apiFirmContactUrl);
    }

    getFirmAddress(): Observable<FirmAddress> {
        let apiFirmAddressUrl = this.path + 'firms/getFirmAddress';
        return Observable.create((subscriber) => {
            this._http.get<FirmAddress>(apiFirmAddressUrl, { params: { AddressId: 'B2BB6BBB-B528-40E6-90A7-8B2640BB7E9B' } })
                .subscribe(response => {
                    this.firmAddress = JSON.parse(JSON.stringify(response));
                    subscriber.next((this.firmAddress));
                });
        });
    }
}
