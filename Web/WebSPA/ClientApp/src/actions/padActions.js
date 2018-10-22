import * as types from './actionType';
import PadService from '../services/padService';

export function createPad(pad) {
  return function (dispatch) {
    const padService = new PadService();

    return padService.save(pad).then(pad => {
      dispatch(createdPad(pad));
    }).catch(error => {
      throw (error);
    });
  };
}


export function updatePad(pad) {
  return function (dispatch) {
    const padService = new PadService();
    return padService.save(pad).then(pad => {
      dispatch(updatePad(pad));
    }).catch(error => {
      throw (error);
    });
  };
}

export function deletePad(pad) {
  return function (dispatch) {
    const padService = new PadService();
    return padService.delete(pad.id).then(() => {
      dispatch(deletePad());
    }).catch(error => {
      throw (error);
    });
  };
}


export function retreivePad(id) {
  return function (dispatch) {
    const padService = new PadService();
    if(id==='0'){
      dispatch(getPad({
        id:0,
        identifier:'00000000-0000-0000-0000-000000000001',
        name:'default name',
        description:'new pad description'
      }));
      return;
    }
    return padService.get(id).then(pad => {
      dispatch(getPad(pad));
    }).catch(error => {
      throw (error);
    });
  };
}

export function retreivePads(page) {
  return function (dispatch) {
    const padService = new PadService();
    return padService.getpads(page).then(pads => {
      dispatch(getPads(pads));
    }).catch(error => {
      throw (error);
    });
  };
}

export function getPads(pads) {
  return { type: types.PAD_GETPAGED, pads: pads };
}

export function getPad(pad) {
  return { type: types.PAD_GETID, pad: pad };
}

export function creatingPad(pad) {
  return { type: types.PAD_CREATING, pad: pad };
}


export function createdPad(pad) {
  return { type: types.PAD_CREATED, pad: pad };
}

export function updatedPad(pad) {
  return { type: types.PAD_UPDATED, pad: pad };
}

export function deletedPad() {
  return { type: types.PAD_DELETED };
}