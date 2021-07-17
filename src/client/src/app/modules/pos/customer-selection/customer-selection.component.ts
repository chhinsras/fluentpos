import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-customer-selection',
  templateUrl: './customer-selection.component.html',
  styleUrls: ['./customer-selection.component.scss']
})
export class CustomerSelectionComponent implements OnInit {
  formTitle: string;
  constructor() { }

  ngOnInit(): void {
  }

}
