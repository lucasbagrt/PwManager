import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import "bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import { FontAwesomeIcon } from './plugins/font-awesome'
import VueGoodTablePlugin from 'vue-good-table-next';
import SimpleTypeahead from 'vue3-simple-typeahead';
import 'vue-good-table-next/dist/vue-good-table-next.css'
import 'vue3-simple-typeahead/dist/vue3-simple-typeahead.css';
createApp(App)
  .use(router)
  .use(store)
  .use(SimpleTypeahead)
  .use(VueGoodTablePlugin)  
  .component("font-awesome-icon", FontAwesomeIcon)
  .mount("#app");