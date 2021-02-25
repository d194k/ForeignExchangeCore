import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { AppconfigService } from './core/services/appconfig.service';
import { ForexModule } from './forex/forex.module';
import { HttpClientModule } from '@angular/common/http';

const appInitializerFn = (AppconfigService: AppconfigService) => {
  return () => AppconfigService.load();
}

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    CoreModule,
    ForexModule
  ],
  providers: [AppconfigService,
  {provide: APP_INITIALIZER, useFactory: appInitializerFn, deps: [AppconfigService], multi: true}],
  bootstrap: [AppComponent]
})
export class AppModule { }
