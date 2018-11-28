import axios from 'axios'

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

  getPads(page, pageSize, userId) {
    return new Promise((resolve, reject) => {
      axios.get(`${padApi}?page=${page}&pageSize=${pageSize}&userid=${userId}`, {
        headers: {
          'Content-Type': 'application/json'
        }
      }).then(u => {
        resolve(u.data);
      }, err => reject(err));
    });
  }

  getExecutionHistory(page, pageSize, userId) {
    return new Promise((resolve, reject) => {
      axios.get(`${padApi}/history?page=${page}&pageSize=${pageSize}&userid=${userId}`, {
        headers: {
          'Content-Type': 'application/json'
        }
      }).then(u => {
        resolve(u.data);
      }, err => reject(err));
    });
  }
}