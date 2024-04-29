import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DiscordCallbackPageComponent } from './discord-callback-page.component';

describe('DiscordCallbackPageComponent', () => {
  let component: DiscordCallbackPageComponent;
  let fixture: ComponentFixture<DiscordCallbackPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DiscordCallbackPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DiscordCallbackPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
