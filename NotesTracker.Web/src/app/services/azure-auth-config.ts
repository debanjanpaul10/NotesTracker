import { Configuration, BrowserCacheLocation } from '@azure/msal-browser';

import { LocalHostBaseUrl, MsalAuth } from '../helpers/config.constants';

/**
 * Checks if IE
 */
const isIE =
  window.navigator.userAgent.indexOf('MSIE ') > -1 ||
  window.navigator.userAgent.indexOf('Trident/') > -1;

/**
 * Determines the redirect URI based on the current environment.
 * @returns The appropriate redirect URI.
 */
const determineRedirectUri = (): string => {
  const hostname = window.location.hostname;

  if (hostname === LocalHostBaseUrl) {
    // Development environment
    return MsalAuth.RedirectBaseUris[0];
  } else {
    // Production environment
    return MsalAuth.RedirectBaseUris[1];
  }
};

/**
 * Configures the B2C Policies
 */
export const b2cPolicies = {
  names: {
    signUpSignIn: 'b2c_1_susi',
    editProfile: 'b2c_1_edit_profile',
    passwordReset: 'b2c_1_pass_reset',
  },
  authorities: {
    signUpSignIn: {
      authority: MsalAuth.Authorities.SignupSignIn,
    },
    editProfile: {
      authority: MsalAuth.Authorities.EditProfile,
    },
    passReset: {
      authority: MsalAuth.Authorities.PasswordReset,
    },
  },
  authorityDomain: MsalAuth.AuthorityDomain,
};

/**
 * Configures the MSAL.
 */
export const msalConfig: Configuration = {
  auth: {
    clientId: MsalAuth.ClientId,
    authority: b2cPolicies.authorities.signUpSignIn.authority,
    knownAuthorities: [b2cPolicies.authorityDomain],
    redirectUri: determineRedirectUri(),
  },
  cache: {
    cacheLocation: BrowserCacheLocation.LocalStorage,
    storeAuthStateInCookie: isIE,
  },
};

/**
 * Shows the protected resources.
 */
export const protectedResources = {
  notesApi: {
    endpoint: MsalAuth.RedirectBaseUris,
    scopes: ['openid'],
  },
};

/**
 * Configures the password reset request.
 */
export const passwordResetRequest = {
  authority: b2cPolicies.authorities.passReset.authority,
  scopes: ['openid'],
};
