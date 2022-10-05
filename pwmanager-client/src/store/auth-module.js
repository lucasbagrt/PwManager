import UserService from '../services/user-service';

const user = JSON.parse(localStorage.getItem('user'));
const initialState = user ? {
    status: {
        loggedIn: true
    },
    user
} : {
    status: {
        loggedIn: false
    },
    user: null
};

export const auth = {
    namespaced: true,
    state: initialState,
    actions: {
        login({
            commit
        }, user) {
            return UserService.login(user).then(
                user => {
                    commit('loginSuccess', user);
                    return Promise.resolve(user);
                },
                error => {
                    commit('loginFailure');
                    return Promise.reject(error);
                }
            );
        },
        logout({
            commit
        }) {
            UserService.logout();
            commit('logout');
        }
    },
    mutations: {
        loginSuccess(state, user) {
            state.status.loggedIn = true;
            state.user = user;
        },
        loginFailure(state) {
            state.status.loggedIn = false;
            state.user = null;
        },
        logout(state) {
            state.status.loggedIn = false;
            state.user = null;
        }
    }
};