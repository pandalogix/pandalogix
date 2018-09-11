import * as types from './actionType';
import { applicationContext }  from '../services/securitymgr';


export function login() {
    return function (dispatch) {
        return applicationContext.login().then(user => {
            dispatch(userLogin(user));
        }).catch(error => {
            throw (error);
        });
    };
}


export function logout() {
    return function (dispatch) {
        applicationContext.logout();
        dispatch(userLogout());
    };
   
}

export function userLogin(user) {
    return { type: types.USER_LOGIN ,user:user};
}

export function userLogout() {
    return { type: types.USER_LOGOUT,user:{}};
}
