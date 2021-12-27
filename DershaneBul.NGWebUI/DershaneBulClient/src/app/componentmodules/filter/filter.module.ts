import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CourseFilterComponent } from './containers/course-filter/course-filter.component';
import { FilterService } from './services/filter.service';

@NgModule({
    declarations: [
        CourseFilterComponent
    ],
    providers: [
        FilterService
    ],
    imports: [
        CommonModule,
        RouterModule,
        HttpClientModule,
        ReactiveFormsModule,
    ],
    exports: [CourseFilterComponent]
})
export class FilterModule {
}
