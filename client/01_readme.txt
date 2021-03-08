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
