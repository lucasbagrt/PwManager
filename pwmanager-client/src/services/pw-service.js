import axios from 'axios';
import authHeader from './auth-header';

const API_URL = 'https://localhost:7155/api/v1/Password';

class PwService {
  findAll() {
    return axios
      .get(API_URL, { headers: authHeader() })
      .then(response => {       
        return response.data;
      });
  }
  findAllGrouped() {
    return axios
      .get(API_URL + '/FindAllGrouped', { headers: authHeader() })
      .then(response => {       
        return response.data;
      });
  }
  post(password) {
    return axios
      .post(API_URL, {
        username: password.username,
        password: password.password,
        app_id: password.app_id
      }, { headers: authHeader() })
      .then(response => {        
        return response.data;
      });
  }
  put(password) {
    return axios
      .put(API_URL, {
        id: password.id,
        username: password.username,
        password: password.password,
        app_id: password.app_id,
        user_id: password.user_id
      }, { headers: authHeader() })
      .then(response => {        
        return response.data;
      });
  }
  delete(id) {
    return axios
      .delete(API_URL, + '/' + id, { headers: authHeader() })
      .then(response => {        
        return response.data;
      });
  }
  decrypt(pw) {
    return axios
      .post(API_URL, {
        password: pw
      }, { headers: authHeader() })
      .then(response => {        
        return response.data;
      });
  }
  logout() {
    localStorage.removeItem('user');
  } 
}

export default new PwService();