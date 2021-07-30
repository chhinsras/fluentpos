import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-catalog-skeleton',
  templateUrl: './catalog-skeleton.component.html',
  styleUrls: ['./catalog-skeleton.component.scss']
})
export class CatalogSkeletonComponent implements OnInit {
  @Input() showImage : Boolean;
  products = new Array(20);
  
  constructor() { }

  ngOnInit(): void {
  }

}
