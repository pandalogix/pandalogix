import * as types from '../actions/actionType';

export function UserManager(state = {}, action) {
    switch (action.type) {
        case types.USER_LOGIN: 
            return Object.assign(...state, action.user);
        case types.USER_LOGOUT:
            return {};
        default:
            return state;
    }
    
}