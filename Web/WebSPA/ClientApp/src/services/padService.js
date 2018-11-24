import axios from 'axios'
import {
  Pad
} from '../contracts/Pad';
import {
  PAD_CREATED
} from '../actions/actionType';
//TODO handle local token expiration, logout..

const padApi = 'api/pad';

export default class PadService {

  constructor(userId) {
    this.userId = userId;
  }
  save(pad) {
    return new Promise((resolve, reject) => {
      if (pad.id === 0) {
        axios.post(`${padApi}?user=${this.userId}`, JSON.stringify(pad), {
          headers: {
            'Content-Type': 'application/json'
          }
        }).then(u => {
          resolve(u.data);
        }, err => reject(err));
      } else {
        axios.put(`${padApi}`, JSON.stringify(pad), {
          headers: {
            'Content-Type': 'application/json'
          }
        }).then(u => {
          resolve(u.data);
        }, err => reject(err));
      }
    });
  }

  delete(id) {
    return new Promise((resolve, reject) => {
      axios.delete(`${padApi}/${id}`, {
        headers: {
          'Content-Type': 'application/json'
        }
      }).then(u => {
        resolve(u.data);
      }, err => reject(err));
    });
  }

  get(id) {
    return new Promise((resolve, reject) => {
      axios.get(`${padApi}/${id}`, {
        headers: {
          'Content-Type': 'application/json'
        }
      }).then(u => {
        resolve(u.data);
      }, err => reject(err));
    });
  }

  getpads(page) {
    return new Promise((resolve, reject) => {
      axios.get(`${padApi}?page=${page}`, {
        headers: {
          'Content-Type': 'application/json'
        }
      }).then(u => {
        resolve(u.data);
      }, err => reject(err));
    });
  }
}