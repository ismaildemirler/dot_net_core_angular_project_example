import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CourseDetailComponent } from './containers/course-detail/course-detail.component';
import { DetailPageService } from './services/detail-page.service';
import { DetailPageRoutingModule } from './detail-page-routing.module';
import { MediaComponent } from './media/media.component';
import { PropertyComponent } from './containers/property/property.component';
import { VideoComponent } from './containers/video/video.component';
import { CommentComponent } from './containers/comment/comment.component';
import { FirmContactComponent } from './containers/firm-contact/firm-contact.component';
import { NgxGalleryModule } from 'ngx-gallery';
import { CarouselComponent } from './containers/carousel/carousel.component';

@NgModule({
  declarations: [
    CourseDetailComponent,
    MediaComponent,
    PropertyComponent,
    VideoComponent,
    CommentComponent,
    FirmContactComponent,
    CarouselComponent
  ],
  providers: [
    DetailPageService
  ],
  imports: [
    CommonModule,
    RouterModule,
    HttpClientModule,
    ReactiveFormsModule,
    DetailPageRoutingModule,
    NgxGalleryModule 
  ],
  exports: [CourseDetailComponent]
})
export class DetailPageModule {
}
