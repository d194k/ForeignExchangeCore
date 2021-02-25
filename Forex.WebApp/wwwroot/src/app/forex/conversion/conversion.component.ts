import { Component, OnInit } from '@angular/core';
import { CrudService } from 'src/app/shared/services/crud.service';

@Component({
  selector: 'app-conversion',
  templateUrl: './conversion.component.html',
  styleUrls: ['./conversion.component.scss']
})
export class ConversionComponent implements OnInit {

  public pageTitle = 'Currency Conversion';
  private conversionEndpoint = 'exchange'; 

  public firstCurrency: string;
  public amount: number;
  public secondCurrency: string;
  public exchangedAmount: number;
  public exchangeDate: string;

  public hasError = false;
  public hasSuccess = false;
  public currencyExchanged: any;

  constructor(private crudService: CrudService) { }

  ngOnInit(): void {
  }

  public convertCurrency() {
    this.hasError=false;
    this.hasSuccess = false;
    if (this.firstCurrency && this.secondCurrency && this.amount) {
      const param = {
        FirstCurrencyCode: this.firstCurrency.trim().toUpperCase(),
        SecondCurrencyCode: this.secondCurrency.trim().toUpperCase(),
        CurrencyAmount: this.amount,
        ExchangeDate: this.exchangeDate
      };
      this.crudService.postByUrl(this.conversionEndpoint, param).subscribe((response: any) => {
        if(response!== null && response.StatusCode === 200) {
          this.currencyExchanged = response.Result; 
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
    } else{
      this.clear();
    }        
  }

  public clear() {
    this.hasError= false;
    this.hasSuccess = false;
    this.firstCurrency = null;
    this.secondCurrency = null;
    this.exchangeDate = null;
    this.exchangedAmount = null;
    this.amount = null;
    this.currencyExchanged = null;
  }
}
