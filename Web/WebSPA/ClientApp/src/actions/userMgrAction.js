import * as types from './actionType';
import {
  applicationContext
} from '../services/securitymgr';
import AccountService from '../services/accountService';

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
  return {
    type: types.USER_LOGIN,
    user: user
  };
}

export function userLogout() {
  return {
    type: types.USER_LOGOUT,
    user: {}
  };
}

export function userKeyRegenerate(user) {
  return {
    type: types.USER_APIKEY,
    user: user
  };
}

export function regenerateKey(user) {
  return function (dispatch) {
    const account = new AccountService();

    return account.regenerateKey(user).then(user => {
      dispatch(userKeyRegenerate(user));

    }).catch(error => {
      throw (error);
    });
  };
}