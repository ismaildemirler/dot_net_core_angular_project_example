import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/componentmodules/auth/services/auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-membership',
  templateUrl: './membership.component.html',
  styleUrls: ['./membership.component.css']
})
export class MembershipComponent implements OnInit {

  constructor(
    private authService: AuthService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {
    this.createRegisterForm();
    this.createLoginForm();
  }

  loginUser: any = {};
  registerUser: any = {};

  registerForm: FormGroup;
  loginForm: FormGroup;

  createLoginForm() {
    this.loginForm = this.formBuilder.group({
      email: [""], //Validators.email],
      password: ["", Validators.required]
    })
  }

  createRegisterForm() {
    this.registerForm = this.formBuilder.group({
      fullName: ["", Validators.required],//, Validators.minLength(3)],
      email: ["", Validators.required],
      userName: ["", Validators.required],
      phoneNumber: ["", Validators.required],
      password: ["", Validators.required],
      confirmPassword: ["", Validators.required]
    })
  }

  login() {
    if (this.loginForm.valid) {
      this.loginUser = Object.assign({}, this.loginForm.value);
      this.authService.login(this.loginUser);
    }
  }


  register() {
    if (this.registerForm.valid) {
      this.registerUser = Object.assign({}, this.registerForm.value);
      this.authService.register(this.registerUser);
    }
  }
}
