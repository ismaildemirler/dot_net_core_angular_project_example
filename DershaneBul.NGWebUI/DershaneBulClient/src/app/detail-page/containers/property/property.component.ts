import { Component, OnInit } from '@angular/core';
import { DetailPageService } from "../../services/detail-page.service";
import { FirmProperty } from "../../models/firm-property";


import { FirmPropertyMenu } from "../../models/firm-property-menu";

@Component({
  selector: 'app-property',
  templateUrl: './property.component.html',
  styleUrls: ['./property.component.css']
})
export class PropertyComponent implements OnInit {
  firmProperty$: FirmProperty = {
    description: "test"
  };
  firmPropertyMenu$: FirmPropertyMenu[] = [
    {
      propertyId: "dd",
      propertyTitle: "ddd"
    }
  ];
 

 
  constructor(private firmDetailService: DetailPageService) { 
  }
 
  ngOnInit() { 
    //this._firmDetailService.getProperty().subscribe(data => this.firmProperty$ = data);
    this.firmDetailService.getFirmPropertyMenu().subscribe(data => this.firmPropertyMenu$.push(data));
  
    
  }

}
