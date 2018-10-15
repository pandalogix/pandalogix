import * as types from '../actions/actionType';

export function UserManager(state = {}, action) {
    switch (action.type) {
        case types.USER_LOGIN: 
            return action.user;//Object.assign(...state, action.user);
        case types.USER_LOGOUT:
            return {};
        case types.USER_APIKEY:
            return action.user;
        default:
            return state;
    }
    
}