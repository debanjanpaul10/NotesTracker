import { Injectable, signal, WritableSignal } from '@angular/core';

/**
 * Common Service
 * 
 * A shared service that manages application-wide state and common functionality.
 * This service provides centralized state management for UI components that need
 * to share state across different parts of the application. It uses Angular signals
 * for reactive state management and provides getter/setter methods for easy access.
 * 
 * The service currently manages the visibility state of the bug report flyout,
 * allowing components to show or hide the bug report drawer from anywhere in
 * the application.
 */
@Injectable({
  providedIn: 'root',
})
export class CommonService {
  /**
   * Signal that tracks the visibility state of the bug report flyout
   * 
   * This signal maintains the current visibility state of the bug report drawer.
   * Components can subscribe to changes in this signal to reactively update
   * their UI when the flyout visibility changes.
   */
  private bugFlyoutVisible: WritableSignal<boolean> = signal(false);

  /**
   * Creates an instance of CommonService
   * 
   * Initializes the service with default state values. The bug report flyout
   * is initially set to hidden (false).
   */
  constructor() { }

  /**
   * Gets the current visibility state of the bug report flyout
   * 
   * Returns the current boolean value indicating whether the bug report
   * drawer is visible or hidden. This getter provides read access to the
   * internal signal state.
   * 
   * @returns {boolean} True if the bug report flyout is visible, false if hidden
   */
  public get isBugFlyoutVisible(): boolean {
    return this.bugFlyoutVisible();
  }

  /**
   * Sets the visibility state of the bug report flyout
   * 
   * Updates the internal signal state to control whether the bug report
   * drawer should be displayed or hidden. This setter provides write access
   * to the internal signal state and will trigger reactive updates in any
   * components that are subscribed to this signal.
   * 
   * @param {boolean} isVisible - The visibility state to set (true for visible, false for hidden)
   */
  public set isBugFlyoutVisible(isVisible: boolean) {
    this.bugFlyoutVisible.set(isVisible);
  }
}
