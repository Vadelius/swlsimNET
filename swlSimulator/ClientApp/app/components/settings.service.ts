import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import "rxjs/add/operator/map";
import { Observable } from "rxjs/Observable";
import { Settings } from "./settings.interface";
import { Configuration } from "./app/app.constants";

@Injectable()
export class SettingsService {
    constructor(private _http: Http,
        private _settings: Configuration) {
    }

    public getAll = (): Observable<Settings[]> => {
        return this._http.get(this._settings.ServerWithApiUrl)
            .map(data => data.json());
    };
}