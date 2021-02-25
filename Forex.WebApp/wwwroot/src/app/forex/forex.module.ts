import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConversionComponent } from './conversion/conversion.component';
import { RateHistoryComponent } from './rate-history/rate-history.component';
import { FormsModule } from '@angular/forms';
import { ChartModule } from 'primeng/chart';



@NgModule({
  declarations: [ConversionComponent, RateHistoryComponent],
  imports: [
    CommonModule,
    FormsModule,
    ChartModule
  ]
})
export class ForexModule { }
