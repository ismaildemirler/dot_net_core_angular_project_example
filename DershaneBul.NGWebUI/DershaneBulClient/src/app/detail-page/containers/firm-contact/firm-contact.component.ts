import { Component, OnInit } from '@angular/core';
import { DetailPageService } from '../../services/detail-page.service';
import { FirmAddress } from '../../models/firm-address';

@Component({
    selector: 'app-firm-contact',
    templateUrl: './firm-contact.component.html',
    styleUrls: ['./firm-contact.component.css']
})
export class FirmContactComponent implements OnInit {

    constructor(private detailPageService: DetailPageService) { }

    firmAddress: FirmAddress;

    ngOnInit() {
        this.getFirmAddress();
    }

    getFirmAddress(): void {
        this.detailPageService.getFirmAddress()
            .subscribe((firmAddress: FirmAddress) => {
                this.firmAddress = firmAddress;
            });
    }
}
