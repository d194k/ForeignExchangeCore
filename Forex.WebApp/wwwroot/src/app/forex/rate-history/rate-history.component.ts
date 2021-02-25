import { Component, OnInit } from '@angular/core';
import { CrudService } from 'src/app/shared/services/crud.service';

@Component({
  selector: 'app-rate-history',
  templateUrl: './rate-history.component.html',
  styleUrls: ['./rate-history.component.scss']
})
export class RateHistoryComponent implements OnInit {


  public pageTitle = 'Rate History';
  public currency: string;
  public fromDate: string;
  public toDate: string;

  public hasError = false;
  public hasSuccess = false;
  private rateHistory: any;
  public chartData: any;


  private rateHistoryEndpoint = 'ratehistory';

  constructor(private crudService: CrudService) {    
  }

  ngOnInit(): void {
  }

  public getRateHistory() {
    this.hasError = false;
    this.hasSuccess = false;
    if (this.currency && this.fromDate && this.toDate) {
      var f = new Date(this.fromDate);
      var t = new Date(this.toDate);
      if (t > f) {
        const url = `${this.rateHistoryEndpoint}?currency=${this.currency.toUpperCase()}&from=${this.fromDate}&to=${this.toDate}`;
        this.crudService.getByUrl(url).subscribe((response: any) => {
          if(response!== null && response.StatusCode === 200) {
            this.rateHistory = response.Result;
            if (response.Result?.Records.length > 0){
              var records = response.Result.Records;
              var labels = records.map(x => x.RecordDate);
              var data = records.map(x => x.ExchangeRate);
              this.chartData = {
                labels: labels,
                datasets: [{
                  label: this.rateHistory.CurrencyCode,
                  backgroundColor: 'red',
                  data: data
                }]
              };
            } 
            this.hasSuccess = true;
          }else{
            this.hasError = true;
          }
        },
        error => {          
          this.hasError = true;
          console.log(error);
        },
        () => {});
      }else {
        this.clear();
      }      
    }else {
      this.clear();
    }    
  }

  public clear() {
    this.hasError= false;
    this.hasSuccess = false;
    this.currency = null;;
    this.fromDate= null;
    this.toDate = null;
  }
  
}
