import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginWithDiscordButtonComponent } from './login-with-discord-button.component';

describe('LoginWithDiscordButtonComponent', () => {
  let component: LoginWithDiscordButtonComponent;
  let fixture: ComponentFixture<LoginWithDiscordButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LoginWithDiscordButtonComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LoginWithDiscordButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
