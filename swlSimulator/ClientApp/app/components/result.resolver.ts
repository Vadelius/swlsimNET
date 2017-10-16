import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { ResultService } from './result.service';

@Injectable()
export class ResultResolver implements Resolve<any> {
  
    constructor(private service: ResultService) {}
  
      resolve(route: ActivatedRouteSnapshot): any {
      return this.service.getData();
  }
  }