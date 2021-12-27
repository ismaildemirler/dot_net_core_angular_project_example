import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListOfCoursesComponent } from './containers/list-of-courses/list-of-courses.component';

const routes: Routes = [
    {
        path: 'courselist',
        component: ListOfCoursesComponent
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
    declarations: []
})
export class ListPageRoutingModule { }
