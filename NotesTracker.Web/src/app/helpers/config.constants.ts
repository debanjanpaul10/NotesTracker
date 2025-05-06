export const ApiBaseUrl = 'https://app-webapi-notes-tracker-h4h5f8fxdhh7d6cy.centralindia-01.azurewebsites.net';
export const MsalAuth = {
  ClientId: 'e77dd298-0900-4a7b-b91a-4f62e7c3b1c6',
  Authorities: {
    SignupSignIn:
      'https://debanjanlab.b2clogin.com/debanjanlab.onmicrosoft.com/b2c_1_susi',
    EditProfile:
      'https://debanjanlab.b2clogin.com/debanjanlab.onmicrosoft.com/b2c_1_edit_profile',
    PasswordReset:
      'https://debanjanlab.b2clogin.com/debanjanlab.onmicrosoft.com/b2c_1_pass_reset',
  },
  RedirectBaseUris: [
    'http://localhost:4200',
    'https://jolly-hill-03d2ef300.6.azurestaticapps.net',
  ],
  AuthorityDomain: 'debanjanlab.b2clogin.com',
};
export const LocalHostBaseUrl = 'localhost';
