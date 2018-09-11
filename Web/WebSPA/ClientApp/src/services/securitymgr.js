import config from '../constants/authorizationConstants'
import jwt_decode from 'jwt-decode'

//TODO handle local token expiration, logout..

function authCallback(errorDesc, token, error, tokenType) {
    //This function is called after loginRedirect and acquireTokenRedirect. Not called with loginPopup
    // msal object is bound to the window object after the constructor is called.
    if (token) {
        console.log(token);
    }
    else {
        console.log(error + ":" + errorDesc);
    }
}

class ApplicationContext {
    constructor() {
        // eslint-disable-next-line
        this.msal = new Msal.UserAgentApplication(config.client_id, config.authority, authCallback);
        this.msal.redirectUri = config.redirect_uri;
    }

    login() {
        return new Promise((resolve, reject) => {
            this.msal._redirectUri = `${window.location.protocol}//${window.location.hostname}${window.location.port ? `:${window.location.port}` : ''}`;
            this.msal.loginPopup().then((tokens) => {
                console.log(tokens);
                var decoded = jwt_decode(tokens);
                console.log(decoded);
                 var user ={
                     name :decoded.name,
                     email:decoded.emails[0],
                     expiration:new Date(decoded.exp *1000),
                     objectId:decoded.oid
                 }

                if (sessionStorage) {
                    sessionStorage.setItem('user', JSON.stringify(user));
                }
                resolve(user);
            }, err => {
                reject(err);
            });

        });
    }
    logout() {
        this.msal.logout();
    }

    loginCallback() {
        this.userManager.signinPopupCallback().then(user => {
            debugger
            console.log(user);
            console.log(window.location);
            var hash = window.location.hash.substr(1);
            var result = hash.split('&').reduce(function (result, item) {
                var parts = item.split('=');
                result[parts[0]] = parts[1];
                return result;
            }, {});
            console.log(result);
        }, err => {
            console.log(err);
        });


    }

    signinSilentCallback() {
        this.userManager.signinRedirectCallback().then(resp => {
            debugger
            console.log('signinSilentCallback' + resp);
            console.log(window.location);
        }, err => {
            console.log(err);
        })
    }
    getAcessToken() {
        this.msal.acquireTokenSilent(config.scope).then(token => {
            console.log(token)
            // var header = new Headers();
            // header.append('Authorization', `Bearer ${token}`)
            // var myInit = {
            //     method: 'post',
            //     headers: {
            //        'Authorization': `Bearer ${token}`,
            //        'Content-Type':'application/json'
            //     },
            //     // mode: 'no-cors',
            //     // cache: 'default',
            //     body:'{}'
            // };
            // fetch('https://localhost:8140/api/PadExecutor?padIdnetifier=d', myInit).then(rep => {
            //     console.log(rep);
            // }).catch(err => {
            //     console.log(err);
            // });
            // jQuery.ajax({
            //     url: "https://localhost:8140/api/PadExecutor?padIdnetifier=d",
            //     type: "POST",
            //     headers: {
            //         "Accept": "application/json; charset=utf-8",
            //         "Content-Type": "application/json; charset=utf-8",
            //         'Authorization': `Bearer ${token}`,
            //         'Access-Control-Allow-Headers':'*'
            //     },
            //     data: {},
            //     dataType: "json"
            // }).done(rep => {
            //     console.log(rep);
            // }).fail(err => {
            //     console.log(err);
            // });


        }, err => {
            console.log(err);
        });
    }


}

export let applicationContext = new ApplicationContext();