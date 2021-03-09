Changes in codes , because of Errors

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
