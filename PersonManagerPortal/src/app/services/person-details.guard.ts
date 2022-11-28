import { isIdentifier } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PersonDetailsGuard implements CanActivate {
  
  constructor(
    private router: Router) {
}
  
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    
      const id = String(route.paramMap.get('id'));
      if(!this.isValidGUID(id)){
        this.router.navigate(['/persons']);
        return false;
      }
      return true;
  }

  isValidGUID(value: string): boolean {
    if (value.length > 0) {
        if (!(/^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$/).test(value)) {
            return false;
        }
    }

    return true;
}
  
}
