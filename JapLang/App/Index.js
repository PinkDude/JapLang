import 'material-design-icons-iconfont/dist/material-design-icons.css'
import BootstrapVue from 'bootstrap-vue';
import Vue from 'vue';
import VueRouter from 'vue-router';
import Vuetify from 'vuetify';
import App from './App.vue';
import News from './News.vue';
import Words from './Components/Words.vue';
import NewsId from './Components/NewsId.vue';
import AddNews from './Components/addNews.vue';
import AddWord from './Components/addWord.vue';
import Adm from './Components/adm.vue';
import Tests from './Components/Test.vue';
import 'vuetify/dist/vuetify.min.css';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';
import moment from 'moment';
import VueToastr2 from 'vue-toastr-2';
import 'vue-toastr-2/dist/vue-toastr-2.min.css';
import VueCookies from 'vue-cookies';
Vue.use(VueCookies);

window.toastr = require('toastr');

Vue.use(VueToastr2);

Vue.use(Vuetify, {
    theme: {
        primary: '#A52A2A',
        secondary: '#FFFFFF',
        accent: '#ed5565',
        error: '#ff3b30',
        info: '#2196F3',
        success: '#4cd964',
        warning: '#ffcc22',
        secondgrey: '#E0E0E0',
        bargrey: '#EEEEEE',
    }
});
Vue.config.productionTip = false;
Vue.use(VueRouter);
Vue.use(BootstrapVue);

import VueResource from 'vue-resource'

Vue.use(VueResource)

var filter = function(text, length, clamp){
    clamp = clamp || '...';
    var node = document.createElement('div');
    node.innerHTML = text;
    var content = node.textContent;
    return content.length > length ? content.slice(0, length) + clamp : content;
};

Vue.filter('truncate', filter);

Vue.filter('formatDate', function (value) {
    if (value) {
        return moment(String(value)).format('DD.MM.YYYY');
    }
})


const routes = [
    {
        path: '/',
        component: App,
        children:[
            {
                path: '',
                name: 'news',
                component: News
            },
            {
                path:'news/:newsId',
                name: 'newsById',
                component: NewsId
            },
            {
                path:'words',
                name: 'words',
                component: Words
            },
            {
                path: 'tests',
                name: 'tests',
                component: Tests
            },
            {
                path:'adm',
                component: Adm,
                children:[
                    {
                        path: 'news',
                        name: 'addnews',
                        component:AddNews
                    },
                    {
                        path: 'word',
                        name: 'addword',
                        component:AddWord
                    }
                ]
            }
        ]
    }
    
]

const router = new VueRouter({
    routes,
    mode: 'history'
})

new Vue({
    el: '#app',
    template: "<div><router-view></router-view></div>",
    router
})