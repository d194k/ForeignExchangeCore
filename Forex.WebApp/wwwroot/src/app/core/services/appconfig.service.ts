import { Injectable } from '@angular/core';
import { Appsettings } from '../models/appsettings';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import {catchError, map} from 'rxjs/operators';

@Injectable()
export class AppconfigService {

  public static configuration: Appsettings;  

  constructor(private http: HttpClient) { }

  load() {
    const settingFile = `assets/configuration/appsettings.${environment.name}.json`;

    return new Promise<void>((resolve, reject) => {
      this.http.get(settingFile).toPromise()
      .then((response: Appsettings) => {
        AppconfigService.configuration = <Appsettings>(response);
        resolve();
      })
      .catch((response: any) => {
        reject(`Could not load file '${settingFile}': ${JSON.stringify(response)}`)
      })
    })
  }
}
