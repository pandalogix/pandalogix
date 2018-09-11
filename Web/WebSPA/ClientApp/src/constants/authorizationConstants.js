export default {
  client_id : '66c57ad6-4d8b-4f81-a127-8d20e68d0d04',
  authority: 'https://login.microsoftonline.com/tfp/pandalogix.onmicrosoft.com/B2C_1_email',
  scope: ['openid'],
  redirect_uri:`${window.location.protocol}//${window.location.hostname}${window.location.port ? `:${window.location.port}` : ''}/callback`,
  automaticSilentRenew: true,
  post_logout_redirect_uri:`${window.location.protocol}//${window.location.hostname}${window.location.port ? `:${window.location.port}` : ''}/`,
  silent_redirect_uri:`${window.location.protocol}//${window.location.hostname}${window.location.port ? `:${window.location.port}` : ''}/silent`,
  response_type :'id_token',
  policy_id:'b2c_1_email'
}