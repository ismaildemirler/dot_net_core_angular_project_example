import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { AppConstants } from '../../../utils/app-constants';
import { ResponseCity } from '../models/response/response-city';
import { RequestCity } from '../models/request/request-city';
import { RequestFirm } from '../models/request/request-firm';
import { ResponseFirm } from '../models/response/response-firm';
import { ResponseProgram } from '../models/response/response-program';
import { RequestProgram } from '../models/request/request-program';

@Injectable({
    providedIn: 'root'
})
export class FilterService {

    constructor(
        private httpClient: HttpClient,
        private constants: AppConstants
    ) { }

    path = this.constants.BASE_URL_DEV;

    getFirms(requestFirm: RequestFirm): Observable<ResponseFirm[]> {
        let headers = new HttpHeaders();
        headers = headers.append("Content-Type", "application/json");
        return Observable.create((subscriber) => {
            this.httpClient
                .post(this.path + "firms/getfirms", requestFirm, { headers: headers })
                .subscribe(response => {
                    const array = JSON.parse(JSON.stringify(response["firms"])) as ResponseFirm[];
                    const listModel = array.map((data, index) => ResponseFirm[index] = data);
                    subscriber.next(listModel);
                });
        });
    }

    getPrograms(): Observable<ResponseProgram[]> {
        let headers = new HttpHeaders();
        headers = headers.append("Content-Type", "application/json");
        let requestProgram = new RequestProgram();
        return Observable.create((subscriber) => {
            this.httpClient
                .post(this.path + "parameter/getprograms", requestProgram, { headers: headers })
                .subscribe(response => {
                    const array = JSON.parse(JSON.stringify(response["programs"])) as ResponseProgram[];
                    const listModel = array.map((data, index) => ResponseProgram[index] = data);
                    subscriber.next(listModel);
                });
        });
    }

    getCities(): Observable<ResponseCity[]> {
        let headers = new HttpHeaders();
        headers = headers.append("Content-Type", "application/json");
        let requestCity = new RequestCity();
        return Observable.create((subscriber) => {
            this.httpClient
                .post(this.path + "parameter/getcities", requestCity, { headers: headers })
                .subscribe(response => {
                    const array = JSON.parse(JSON.stringify(response["cities"])) as ResponseCity[];
                    const listModel = array.map((data, index) => ResponseCity[index] = data);
                    subscriber.next(listModel);
                });
        });
    }
}
