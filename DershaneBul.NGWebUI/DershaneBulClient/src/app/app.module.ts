import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppConstants } from 'src/app/utils/app-constants';
import { AlertifyService } from 'src/app/services/alertify/alertify.service';
import { GlobalErrorHandler } from 'src/app/utils/handlers/global-error.handler';
import { NavmenuComponent } from 'src/app/layout/navmenu/navmenu.component';
import { ReactiveFormsModule } from '../../node_modules/@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AuthModule } from 'src/app/componentmodules/auth/auth.module';
import { APP_BASE_HREF } from '@angular/common';
import { HomeModule } from 'src/app/home/home.module';
import { ListPageModule } from './list-page/list-page.module';
import { DetailPageModule } from './detail-page/detail-page.module';
import { NgxGalleryModule } from 'ngx-gallery';

@NgModule({
    declarations: [
        AppComponent,
        NavmenuComponent
    ],
    imports: [
        AuthModule,
        HomeModule,
        ListPageModule,
        DetailPageModule,
        BrowserModule,
        AppRoutingModule,
        ReactiveFormsModule,
        HttpClientModule,
        NgxGalleryModule
    ],
    providers: [
        AlertifyService,
        AppConstants,
        {
            provide: ErrorHandler,
            useClass: GlobalErrorHandler
        },
        {
            provide: APP_BASE_HREF,
            useValue: '/'
        }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
