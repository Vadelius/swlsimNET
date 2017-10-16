import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import {Observable} from "rxjs/Observable";

@Injectable()
export class ResultService {
    constructor(private http: Http) {}

    getData(): Observable<any> {
    return this.http.get("/api/values/");
}}
