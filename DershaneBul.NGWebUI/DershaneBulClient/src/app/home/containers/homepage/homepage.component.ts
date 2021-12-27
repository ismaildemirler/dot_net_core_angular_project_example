import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../componentmodules/auth/services/auth.service';
import { FilterService } from '../../../componentmodules/filter/services/filter.service';
import { ResponseProgram } from '../../../componentmodules/filter/models/response/response-program';

@Component({
    selector: 'app-homepage',
    templateUrl: './homepage.component.html',
    styleUrls: ['./homepage.component.css']
})
export class HomepageComponent implements OnInit {

    constructor(
        private filterService: FilterService,
        private authService: AuthService
    ) { }

    ngOnInit() {
        this.getPrograms();
    }

    programs: ResponseProgram[] = [];

    getPrograms(): void {
        this.filterService.getPrograms()
            .subscribe((programs: ResponseProgram[]) => {
                this.programs = programs;
            });
    }

    get isAuthenticated() {
        return this.authService.loggedIn();
    }

    logOut() {
        this.authService.logOut();
    }
}
