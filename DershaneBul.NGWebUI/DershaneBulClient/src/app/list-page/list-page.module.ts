import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ListOfCoursesComponent } from './containers/list-of-courses/list-of-courses.component';
import { ListPageRoutingModule } from './list-page-routing.module';
import { ListPageService } from './services/list-page.service';
import { FilterModule } from '../componentmodules/filter/filter.module';
import { CourseComponent } from './containers/course/course.component';

@NgModule({
    declarations: [
        ListOfCoursesComponent,
        CourseComponent
    ],
    providers: [
        ListPageService
    ],
    imports: [
        CommonModule,
        RouterModule,
        FilterModule,
        HttpClientModule,
        ReactiveFormsModule,
        ListPageRoutingModule
    ],
    exports: [ListOfCoursesComponent]
})
export class ListPageModule {
}
