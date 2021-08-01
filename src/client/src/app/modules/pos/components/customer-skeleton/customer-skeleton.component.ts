import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-customer-skeleton',
  templateUrl: './customer-skeleton.component.html',
  styles: [
  ]
})
export class CustomerSkeletonComponent implements OnInit {
  customers = new Array(6);
  constructor() { }

  ngOnInit(): void {
  }

}
