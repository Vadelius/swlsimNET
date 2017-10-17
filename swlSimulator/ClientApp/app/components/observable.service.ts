
import { Injectable } from '@angular/core';
import { Http } from "@angular/http";
import { ReplaySubject } from 'rxjs/ReplaySubject';

@Injectable()
export class ObservableService
 { 
    http: any;
    constructor (http:Http){}

private _data$ = new ReplaySubject(1);

get data$() {
  return this._data$.asObservable();
}

loadData() {
   return this.http.post()
    .subscribe(((data: any) => this._data$.next(data)));
}
}
