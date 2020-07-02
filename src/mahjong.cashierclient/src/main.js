import Vue from 'vue'
import App from './App.vue'
import './plugins/element.js'
import router from './router'
import axios from 'axios'
import VueAxios from 'vue-axios'
import Print from 'vue-print-nb'

Vue.config.productionTip = false
Vue.use(VueAxios, axios)
Vue.use(Print);

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
