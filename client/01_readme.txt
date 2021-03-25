1. How to start:
   client>ng serve

Changes in codes , because of Errors

In tsconfig.json: NEW!!!
  "angularCompilerOptions": {
    "enableI18nLegacyMessageIdFormat": false,
    "strictInjectionParameters": true,
    "strictInputAccessModifiers": true,
    "strictTemplates": false
  }

1. account.service.ts:
   was:
    login(model: any) {
-->     return this.http.post(this.baseUrl + 'account/login', model).pipe(
        map((response: User) => {
          const user = response;
          if (user) {
            localStorage.setItem('user', JSON.stringify(user));
            this.setCurrentUser(user);
          }
        })
      )
    }

    Now:
    login(model: any) {
-->     return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
        map((response: User) => {
          const user = response;
          if (user) {
            localStorage.setItem('user', JSON.stringify(user));
            this.setCurrentUser(user);
          }
        })
      )
    }

2. In app.module.ts:
   was:
    setCurrentUser() {
-->   const user: User = JSON.parse(localStorage.getItem('user'));
      this.accountSerice.setCurrentUser(user);
    }
    Now:
    setCurrentUser() {
      const user: User = JSON.parse(localStorage.getItem('user') || '{}');
      this.accountSerice.setCurrentUser(user);
    }

3. Notify User:
   with ngx toastr
   >cd client
   >npm install ngx-toastr
   Than add styles:
            "styles": [
              "./node_modules/bootstrap/dist/css/bootstrap.min.css",
              "./node_modules/ngx-bootstrap/datepicker/bs-datepicker.css",
      -->     "./node_modules/font-awesome/css/font-awesome.css",
      -->     "./node_modules/ngx-toastr/toastr.css",
              "src/styles.scss"
            ],

4. In _services:
   was:
    const HttpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer' + JSON.parse(localStorage.getItem('user')).token
      })
    }
   Now: add || '{}'
    const HttpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer' + JSON.parse(localStorage.getItem('user') || '{}').token
      })
    }

5. interface:
    was: let currentUser: User;
    now: let currentUser: User = {} as User;


6. isMatch:
   Was:
      matchValues(matchTo: string): ValidatorFn {
        return (control: AbstractControl) => {
          return control?.value === control?.parent?.controls[matchTo].value
            ? null : {isMatching: true}
        }
      }

   Now:
      matchValues(matchTo: string): ValidatorFn {
        return (control: AbstractControl) => {
          if (control?.parent?.controls === undefined) return {isMatching: false};
          var passValue = (control?.parent?.controls as { [key: string]: AbstractControl })[matchTo].value;
          return control?.value === passValue
            ? null : {isMatching: true}
        }
      }


4. Create Guards:
D:\Werk\Leren\_udemy\_NetCoreAngular\DatingApp\client\src\app\_guards>ng g guard auth --skip-tests

5. Use Bootstrap themes:
   bootswatch.com
   client> npm install bootswatch
  "styles": [
                "./node_modules/bootstrap/dist/css/bootstrap.min.css",
                "./node_modules/ngx-bootstrap/datepicker/bs-datepicker.css",
     -->        "./node_modules/bootswatch/dist/united/bootstrap.css",


6. Drop database:
   API> dotnet ef database drop

7. Search Angular documents:
   valor-software.com/ngx-bootstrap/#/tabs

8. ngx-gallery - npm
   https://www.npmjs.com/package/@kolkov/ngx-gallery
   >npm install @kolkov/ngx-gallery

9. ngx-spinner
   >npm install @angular/cdk
   >npm install @schematics/angular/utility/change
   >ng add ngx-spinner

10, Install SignalR
  >npm install @microsoft/signalr@3.1.8   
