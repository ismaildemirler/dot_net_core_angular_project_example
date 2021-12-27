import { Component, OnInit } from '@angular/core';
import { FilterService } from '../../../componentmodules/filter/services/filter.service';
import { ResponseFirm } from '../../../componentmodules/filter/models/response/response-firm';
import { ResponseProgram } from '../../../componentmodules/filter/models/response/response-program';
import { RequestFirm } from '../../../componentmodules/filter/models/request/request-firm';

@Component({
    selector: 'app-course-list',
    templateUrl: './course-list.component.html',
    styleUrls: ['./course-list.component.css']
})
export class CourseListComponent implements OnInit {

    constructor(
        private filterService: FilterService
    ) { }

    firms: ResponseFirm[] = [];
    programs: ResponseProgram[] = [];

    ngOnInit() {
        this.getFirms();
        this.getPrograms();
    }

    initializeFirmRequest(): RequestFirm {
        let requestFirm = new RequestFirm();
        requestFirm.pageSize = "1";
        requestFirm.pageIndex = "1";
        return requestFirm;
    }

    getFirms(requestFirm?: RequestFirm): void {
        if (!requestFirm) {
            requestFirm = this.initializeFirmRequest();
        }
        this.filterService.getFirms(requestFirm)
            .subscribe((firms: ResponseFirm[]) => {
                this.firms = firms;
            });
    }

    getPrograms(): void {
        this.filterService.getPrograms()
            .subscribe((programs: ResponseProgram[]) => {
                this.programs = programs;
            });
    }

    filterFirmByProgram(program: ResponseProgram) {
        let requestFirm = this.initializeFirmRequest();
        requestFirm.programId = program.programId;
        this.getFirms(requestFirm);
    }
}
