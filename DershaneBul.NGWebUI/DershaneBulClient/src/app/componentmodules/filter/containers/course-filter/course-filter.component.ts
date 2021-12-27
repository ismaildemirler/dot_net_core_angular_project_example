import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FilterService } from '../../services/filter.service';
import { ResponseCity } from '../../models/response/response-city';
import { FormGroup, FormControl } from '@angular/forms';
import { Subject } from "rxjs/Subject";
import "rxjs/add/operator/debounceTime";
import "rxjs/add/operator/distinctUntilChanged";
import { Subscription, Observable } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { RequestFirm } from '../../models/request/request-firm';
import { ResponseProgram } from '../../models/response/response-program';
import { Guid } from 'guid-typescript';

@Component({
    selector: 'app-course-filter',
    templateUrl: './course-filter.component.html',
    styleUrls: ['./course-filter.component.css']
})
export class CourseFilterComponent implements OnInit {

    @Input() requestFirm: RequestFirm;
    @Output() firmRequest: EventEmitter<RequestFirm> = new EventEmitter();

    constructor(
        private filterService: FilterService
    ) { }

    ngOnInit() {
        this.getCities();
        this.getPrograms();
        this.createLoginForm();
        this.observeTextChange();
    }

    filterForm: FormGroup;
    cities: ResponseCity[] = [];
    programs: ResponseProgram[] = [];
    searchTextChanged: Subject<string> = new Subject<string>();

    createLoginForm() {
        this.filterForm = new FormGroup({
            city: new FormControl(''),
            program: new FormControl('')
        });
    }

    textSearch(searchText: string) {
        this.searchTextChanged.next(searchText);
    }

    observeTextChange() {
        this.searchTextChanged
            .debounceTime(1000) // wait 300ms after the last event before emitting last event
            .distinctUntilChanged() // only emit if value is different from previous value
            .subscribe(text => {
                if (!this.requestFirm) {
                    this.requestFirm = new RequestFirm();
                }
                this.requestFirm.searchText = text;
                this.firmRequest.emit(this.requestFirm);
            });
    }

    onChangeType(programId: Guid) {
        if (programId) {
            if (!this.requestFirm) {
                this.requestFirm = new RequestFirm();
            }
            this.requestFirm.programId = programId;
            this.firmRequest.emit(this.requestFirm);
        }
    }

    onChangeCity(cityId: string) {
        if (cityId) {
            if (!this.requestFirm) {
                this.requestFirm = new RequestFirm();
            }
            this.requestFirm.cityId = cityId;
            this.firmRequest.emit(this.requestFirm);
        }
    }

    getCities(): void {
        this.filterService.getCities()
            .subscribe((cities: ResponseCity[]) => {
                this.cities = cities;
            });
    }

    getPrograms(): void {
        this.filterService.getPrograms()
            .subscribe((programs: ResponseProgram[]) => {
                this.programs = programs;
            });
    }
}
