import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ConversionComponent } from './forex/conversion/conversion.component';
import { RateHistoryComponent } from './forex/rate-history/rate-history.component';

const routes: Routes = [
  {path: 'currency-exchange', component: ConversionComponent},
  {path: 'rate-change-history', component: RateHistoryComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
