import { Injectable } from '@angular/core';
import { CanActivate, CanDeactivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';
import { ConfirmService } from '../_services/confirm.service';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanActivate, CanDeactivate<unknown> {

  constructor(private confirmServive: ConfirmService) {    
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return true;
  }
  canDeactivate(
    component: MemberEditComponent): Observable<boolean> | boolean {
      if (component.editForm.dirty) {
        // return confirm('Are you sure you want to continue? Any unsaved changes are lost!');
        return this.confirmServive.confirm();
      }
    return true;
  }

}
