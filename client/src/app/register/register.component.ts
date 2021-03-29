import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  // model: any = {};
  registerForm: FormGroup = {} as FormGroup;
  maxDate: Date = new Date();
  validationErrors: string[] = [];

  constructor(private accountService: AccountService,
              private toastr: ToastrService,
              private fb: FormBuilder,
              private router: Router) { }

  ngOnInit(): void {
    this.initializeForm();
    // this.maxDate = new Date();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);
  }

  initializeForm() {
    this.registerForm = this.fb.group({
      gender: ['male'],
      userName: ['', Validators.required],
      knownAs: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      password: ['', [Validators.required,
        Validators.minLength(4), Validators.maxLength(8)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]]
    })

  }


  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      if (control?.parent?.controls === undefined) return {isMatching: false};
      var passValue = (control?.parent?.controls as { [key: string]: AbstractControl })[matchTo].value;
      return control?.value === passValue
        ? null : {isMatching: true}
    }
  }


  register() {
    // console.log(this.registerForm.value);
    this.accountService.register(this.registerForm.value).subscribe(response => {
      // this.cancel();
      this.router.navigateByUrl('/members');
    }, error => {
      this.validationErrors = error;
      // this.toastr.error(error.error);
    });
  }

  cancel() {
    console.log('User cancel...');
    this.cancelRegister.emit(false);
  }

}
