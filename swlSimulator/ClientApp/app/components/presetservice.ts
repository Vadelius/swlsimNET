import { Injectable } from "@angular/core";
import { Http } from "@angular/http";

@Injectable()
export class PresetsService {

    constructor(private http: Http) {
    }
}

// Promise<Preset[]> {
//     return this.http.get("/api/presets").toPromise().then(resp => resp.json() as Preset[]).catch(this.errorhandler);
