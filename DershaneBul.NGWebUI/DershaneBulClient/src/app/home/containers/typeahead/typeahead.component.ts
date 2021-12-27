import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { FilterService } from '../../../componentmodules/filter/services/filter.service';
import { RequestFirm } from '../../../componentmodules/filter/models/request/request-firm';
import { catchError, debounceTime, distinctUntilChanged, map, tap, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-typeahead',
  templateUrl: './typeahead.component.html',
  styleUrls: ['./typeahead.component.css']
})

export class TypeaheadComponent {
  requestFirm?: RequestFirm
  model: any;
  searching = false;
  searchFailed = false;

  constructor(private filterService: FilterService) { }

  initializeFirmRequest(term: string): RequestFirm {
    let requestFirm = new RequestFirm();
    requestFirm.searchText = term;
    requestFirm.pageIndex = "1";
    requestFirm.pageSize = "10";
    return requestFirm;
  }

  search = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => this.searching = true),
      switchMap(term =>
        this.filterService.getFirms(this.initializeFirmRequest(term)).pipe(
          tap(() => this.searchFailed = false),
          catchError(() => {
            this.searchFailed = true;
            return of([]);
          }))
      ),
      tap(() => this.searching = false)
    )

  formatter = (x: { firmName: string }) => x.firmName;
}
