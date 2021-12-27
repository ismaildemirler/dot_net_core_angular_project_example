import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class AppConstants {
    BASE_URL_DEV: string = "*************";
    BASE_URL_PRO: string = "";
    TOKEN = "TOKEN";
    REFRESH_TOKEN = "REFRESH_TOKEN";
}
