import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CourseDetailComponent } from './containers/course-detail/course-detail.component';

const routes: Routes = [
    {
        path: 'coursedetail/:id',
        component: CourseDetailComponent
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
    declarations: []
})
export class DetailPageRoutingModule { }
