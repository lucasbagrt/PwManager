import axios from 'axios';
import authHeader from './auth-header';

const API_URL = 'https://localhost:7155/api/v1/User';

class UserService {
  login(user) {
    return axios
      .post(API_URL + '/Authenticate', {
        username: user.username,
        password: user.password
      })
      .then(response => {
        if (response.data.token) {
          localStorage.setItem('user', JSON.stringify(response.data));
        }

        return response.data;
      });
  }

  getUserByToken() {
    return axios
      .get(API_URL + '/GetUserByToken', { headers: authHeader() })
      .then(response => {        
        return response.data;
      });
  }

  logout() {
    localStorage.removeItem('user');
  } 
}

export default new UserService();