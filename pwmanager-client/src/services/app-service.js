import axios from 'axios';
import authHeader from './auth-header';

const API_URL = 'https://localhost:7155/api/v1/Application';

class AppService {
    findAllAutoComplete() {
        return axios
            .get(API_URL + '/FindAllAutoComplete', {
                headers: authHeader()
            })
            .then(response => {
                return response.data;
            });
    }
    findByName(name) {
        return axios
            .get(API_URL + '/FindByName?name=' + name, {
                headers: authHeader()
            })
            .then(response => {
                return response.data;
            });
    }
}

export default new AppService();