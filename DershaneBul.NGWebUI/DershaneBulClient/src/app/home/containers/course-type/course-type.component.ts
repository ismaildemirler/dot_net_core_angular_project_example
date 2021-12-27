import { Component, Input } from '@angular/core';
import { ResponseProgram } from '../../../componentmodules/filter/models/response/response-program';

@Component({
    selector: 'app-course-type',
    templateUrl: './course-type.component.html',
    styleUrls: ['./course-type.component.css']
})
export class CourseTypeComponent {
  @Input() program: ResponseProgram;
}
