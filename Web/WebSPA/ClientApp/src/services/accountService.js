import axios from 'axios'
//TODO handle local token expiration, logout..

const accountApi = 'api/account';

export default class AccountService {
  regenerateKey(user) {
    return new Promise((resolve, reject) => {
      axios.post(`${accountApi}/regenerateKey/${user.id}`, null, {
        headers: {
          'Content-Type': 'application/json'
        }
      }).then(u => {
        resolve(u.data);
      }, err => reject(err));
    });
  }

  createUser(user) {
    return new Promise((resolve, reject) => {
      axios.post(`${accountApi}`, user, {
        headers: {
          'Content-Type': 'application/json'
        }
      }).then(u => {
        resolve(u.data);
      }, err => reject(err));
    });
  }

  updateUser(user) {
    return new Promise((resolve, reject) => {
      axios.put(`${accountApi}`, user, {
        headers: {
          'Content-Type': 'application/json'
        }
      }).then(u => {
        resolve(u.data);
      }, err => reject(err));
    });
  }

  getuser(user) {
    return new Promise((resolve, reject) => {
      axios.get(`${accountApi}?email=${user.email}`).then(u => {
        resolve(u.data);
      }, err => reject(err));
    });
  }
}