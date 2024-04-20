import { Component } from '@angular/core';
import {NzSpinComponent} from "ng-zorro-antd/spin";

@Component({
  selector: 'app-loading-spinner',
  standalone: true,
    imports: [
        NzSpinComponent
    ],
  templateUrl: './loading-spinner.component.html',
  styleUrl: './loading-spinner.component.css'
})

export class LoadingSpinnerComponent
{}
