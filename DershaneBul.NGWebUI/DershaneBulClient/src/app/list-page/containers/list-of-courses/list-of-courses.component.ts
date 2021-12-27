import { Component, OnInit } from '@angular/core';
import { FilterService } from '../../../componentmodules/filter/services/filter.service';
import { RequestFirm } from '../../../componentmodules/filter/models/request/request-firm';
import { ResponseFirm } from '../../../componentmodules/filter/models/response/response-firm';

@Component({
    selector: 'app-list-of-courses',
    templateUrl: './list-of-courses.component.html',
    styleUrls: ['./list-of-courses.component.css']
})
export class ListOfCoursesComponent implements OnInit {

    constructor(
        private filterService: FilterService
    ) { }

    ngOnInit() {
        this.getFirms();
    }

    initializeFirmRequest(): RequestFirm {
        let requestFirm = new RequestFirm();
        requestFirm.pageSize = "1";
        requestFirm.pageIndex = "1";
        return requestFirm;
    }

    firms: ResponseFirm[] = [];

    getFirms(requestFirm?: RequestFirm): void {
        if (!requestFirm) {
            requestFirm = this.initializeFirmRequest();
        }
        this.filterService.getFirms(requestFirm)
            .subscribe((firms: ResponseFirm[]) => {
                this.firms = firms;
            });
    }

    getFirmRequest(requestFirm: RequestFirm) {
        this.getFirms(requestFirm);
    }
}
