import {Injectable} from "@angular/core";
import {Http, Headers} from "@angular/http";
import {Observable} from "rxjs/Observable";
import "rxjs/add/observable/of";
import "rxjs/add/operator/share";
import "rxjs/add/operator/map";
import {Data} from "./data";

@Injectable()
export class ResultService {

      private url:string = "api/values";
      public data: Data;
      private observable: Observable<any>;

    constructor(private http: Http) { }

    public getData(): any {
    let headers: Headers;
    if (this.data) {
        // if `data` is available just return it as `Observable`
        return Observable.of(this.data);
    } else if (this.observable) {
        // if `this.observable` is set then the request is in progress
        // return the `Observable` for the ongoing request
        return this.observable;
    } else {
        // example header (not necessary)
        headers = new Headers();
        headers.append("Content-Type", "application/json");
        // create the request, store the `Observable` for subsequent subscribers
        this.observable = this.http.get(this.url, {
            headers: headers
        })
            .map(response => {
                // when the cached data is available we don't need the `Observable` reference anymore

                if (response.status === 400) {
                    return "FAILURE";
                } else if (response.status === 200) {
                    this.data = new Data(response.json());
                    return this.data;
                }
                // make it shared so more than one subscriber can get the result
            })
            .share();
        return this.observable;
    }
}
    }
