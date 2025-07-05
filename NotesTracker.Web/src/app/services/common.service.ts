import { Injectable, signal, WritableSignal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CommonService {
  private bugFlyoutVisible: WritableSignal<boolean> = signal(false);
  constructor() {}

  public get isBugFlyoutVisible(): boolean {
    return this.bugFlyoutVisible();
  }

  public set isBugFlyoutVisible(isVisible: boolean) {
    this.bugFlyoutVisible.set(isVisible);
  }
}
