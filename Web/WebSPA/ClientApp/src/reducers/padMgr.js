import * as types from '../actions/actionType';

export function PadManager(state = {}, action) {
  switch (action.type) {
    case types.PAD_CREATED:
      return action.pad;//Object.assign(...state, action.user);
    case types.PAD_UPDATED:
      return Object.assign(...state, action.pad)
    case types.PAD_DELETED:
      return null;
    case types.PAD_GETID:
      return action.pad;
    default:
      return state;
  }
}
export function PadsManager(state = {}, action) {
  switch (action.type) {
    case types.PAD_GETPAGED:
      return action.pads;
    default:
      return state;
  }
}
