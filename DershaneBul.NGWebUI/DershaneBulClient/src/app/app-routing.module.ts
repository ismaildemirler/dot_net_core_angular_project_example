import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MembershipComponent } from 'src/app/componentmodules/auth/containers/membership/membership.component';
import { AuthGuard } from 'src/app/componentmodules/auth/guards/auth.guard';
import { CourseDetailComponent } from './detail-page/containers/course-detail/course-detail.component';
import { VideoComponent } from './detail-page/containers/video/video.component';
import { PropertyComponent } from './detail-page/containers/property/property.component';
import { CourseListComponent } from './home/containers/course-list/course-list.component';
import { ListOfCoursesComponent } from './list-page/containers/list-of-courses/list-of-courses.component';
import { HomepageComponent } from './home/containers/homepage/homepage.component';

const routes: Routes = [
  {
    path: 'home',
    component: HomepageComponent
    //canActivate: [ProtectedPageGuard],
    //canLoad: [ProtectedPageGuard]
  },
  {
    path: 'courselist',
    component: ListOfCoursesComponent
    //canActivate: [ProtectedPageGuard],
    //canLoad: [ProtectedPageGuard]
  },
  {
    path: "membership",
    component: MembershipComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "coursedetail/:id",
    component: CourseDetailComponent
  },
  {
    path: "property",
    component: PropertyComponent
  },
  {
    path: "video",
    component: VideoComponent
  },
  { path: "**", redirectTo: "/home", pathMatch: "full" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  declarations: []
})
export class AppRoutingModule { }
