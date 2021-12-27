import { Injectable } from '@angular/core';
import { FilterService } from '../../componentmodules/filter/services/filter.service';

@Injectable({
    providedIn: 'root'
})
export class ListPageService {

    constructor(
        private filterService: FilterService
    ) { }
}
