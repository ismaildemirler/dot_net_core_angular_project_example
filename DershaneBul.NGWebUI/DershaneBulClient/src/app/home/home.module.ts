import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HomepageComponent } from 'src/app/home/containers/homepage/homepage.component';
import { HomeService } from 'src/app/home/services/home.service';
import { CourseListComponent } from './containers/course-list/course-list.component';
import { CourseTypeComponent } from 'src/app/home/containers/course-type/course-type.component';
import { CounterComponent } from 'src/app/home/containers/counter/counter.component';
import { HomeRoutingModule } from './home-routing.module';
import { TypeaheadComponent } from './containers/typeahead/typeahead.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    HomepageComponent,
    CourseListComponent,
    TypeaheadComponent,
    CourseTypeComponent,
    CounterComponent
  ],
  providers: [
    HomeService
  ],
  imports: [
    CommonModule,
    RouterModule,
    HttpClientModule,
    ReactiveFormsModule,
    HomeRoutingModule,
    NgbModule,
    FormsModule
  ],
  exports: [HomepageComponent]
})
export class HomeModule {
}
