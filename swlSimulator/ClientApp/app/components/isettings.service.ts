import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import "rxjs/add/operator/map";
import { Observable } from "rxjs/Observable";
import { ISettings } from "./isettings";
import { Configuration } from "./app/app.constants";

@Injectable()
export class ISettingsService {
    constructor(private _http: Http,
        private _settings: Configuration) {
    }

    public getAll = (): Observable<ISettings[]> => {
        return this._http.get(this._settings.ServerWithApiUrl)
            .map(data => data.json());
    };
}