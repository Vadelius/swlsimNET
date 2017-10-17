import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';

@Injectable()
export class ResultResolver implements Resolve<any> {
	constructor() {}

	resolve(route: ActivatedRouteSnapshot): any {}
}
