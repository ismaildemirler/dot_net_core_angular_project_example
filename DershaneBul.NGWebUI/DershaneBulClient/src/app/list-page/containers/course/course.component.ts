import { Component, OnInit, Input } from '@angular/core';
declare var jQuery: any;

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent implements OnInit {

  @Input() id: number;

  constructor() { }

  ngOnInit() {
    (function ($) {

    })(jQuery);
  }

}
