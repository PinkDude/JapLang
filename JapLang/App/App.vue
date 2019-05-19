<template>
    <v-app>
        <v-toolbar app style="background-color: #A52A2A;">
            <div class="text-xs-center">
                <v-btn class="btn" color="white" flat @click="goTo('news')">
                    <v-icon>home</v-icon>
                    Новости
                </v-btn>
            </div>
            <div class="text-xs-center">
                <v-btn class=" btn" color="white" flat @click="goTo('words')">
                    <v-icon>art_track</v-icon>
                    Иероглифы
                </v-btn>
            </div>
            <div class="text-xs-center">
                <v-btn class=" btn" color="white" flat @click="goTo('tests')">
                    <v-icon>spellcheck</v-icon>
                    Тесты
                </v-btn>
            </div>
            <v-spacer></v-spacer>
            <div class="text-xs-center">
                <div v-if="!isJoin">
                    <!-- <v-icon color="white" >face</v-icon> -->
                    <v-btn class=" btn" color="white" flat @click="join=!join">
                        Войти
                    </v-btn>
                    <v-btn class=" btn" color="white" flat @click="reg=!reg">
                        Зарегистрироваться
                    </v-btn>
                </div>
                
                <div v-if="isJoin">
                        <v-btn class=" btn" v-if="isAdmin" color="white" flat @click="goTo('addnews')">
                            <v-icon></v-icon>
                            Добавить новость
                        </v-btn>
                        <v-btn class=" btn" v-if="isAdmin" color="white" flat @click="goTo('addword')">
                            <v-icon></v-icon>
                            Добавить слово
                        </v-btn>
                    <v-btn class=" btn" color="white" flat @click="logout()">Выйти</v-btn>
                </div>
            </div>
        </v-toolbar>
        <v-content>
            <v-container fluid>
                <router-view>
                </router-view>
                <v-dialog v-model="join"
                          :fullscreen="$vuetify.breakpoint.mdAndDown"
                          max-width="600">
                    <v-card>
                        <v-toolbar dark color="primary">
                            <h4>
                                Авторизация
                            </h4>
                            <v-spacer></v-spacer>
                            <v-btn icon dark @click.native="join = false">
                                <v-icon>close</v-icon>
                            </v-btn>
                        </v-toolbar>
                        <div class="card">
                            <v-text-field prepend-icon="person" label="Логин" v-model="login">
                            </v-text-field>
                            <v-text-field prepend-icon="lock" label="Пароль" type="password" v-model="pass">
                            </v-text-field>
                        </div>
                        <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn class="btn primary" @click="submit()">Войти</v-btn>
                        </v-card-actions>
                    </v-card>
                </v-dialog>

                <v-dialog v-model="reg"
                          :fullscreen="$vuetify.breakpoint.mdAndDown"
                          max-width="600">
                    <v-card>
                        <v-toolbar dark color="primary">
                            <h4>
                                Регистрация
                            </h4>
                            <v-spacer></v-spacer>
                            <v-btn icon dark @click.native="reg = false">
                                <v-icon>close</v-icon>
                            </v-btn>
                        </v-toolbar>
                        <div class="card">
                            <v-text-field prepend-icon="person" label="Логин" v-model="login">
                            </v-text-field>
                            <v-text-field prepend-icon="lock" label="Пароль" type="password" v-model="pass">
                            </v-text-field>
                            <v-text-field prepend-icon="lock" label="Повторите пароль" type="password" v-model="pass2">
                            </v-text-field>
                            <p style="color: red;">{{errorReg}}</p>
                        </div>
                        <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn class="btn primary" @click="register()">Зарегистрироваться</v-btn>
                        </v-card-actions>
                    </v-card>
                </v-dialog>
            </v-container>
        </v-content>
    </v-app>
</template>
<script>
    import { mapActions, mapGetters } from "vuex";
    export default {
        data() {
            return {
                isAdmin: this.$cookies.get('isAdmin'),
                isJoin: this.$cookies.get('isJoin'),
                join: false,
                reg: false,
                login: '',
                pass: '',
                pass2: '',
                errorReg: ''
            }
        },
        methods: {
            logout() {
                this.$http.post(`api/account/logout`)
                    .then((res) => {
                        var cookies = document.cookie.split(";");

                        for (var i = 0; i < cookies.length; i++) {
                            var cookie = cookies[i];
                            var eqPos = cookie.indexOf("=");
                            var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
                            document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
                        }
                        this.isAdmin = null;
                        this.isJoin = null;
                        this.$router.push({ name: 'news' });
                    })
                    .catch((err) => console.log(err))
            },
            submit() {
                var data = {
                    Login: this.login,
                    Password: this.pass
                }
                this.$http.post(`api/account/join`, data)
                    .then((res) => {
                        this.$toastr.success('Вход выполнен успешно!');
                        this.$cookies.set('isJoin', true);
                        if (res.data === 'Admin') {
                            this.$cookies.set('isAdmin', true);
                            this.isJoin = true;
                            this.isAdmin = true;
                        }
                        this.join = false;
                        this.$router.push({name: 'news'});
                    })
                    .catch((err) => {
                        console.log(err);
                        this.$toastr.error('Что-то пошло не так!');
                    })
            },
            register() {
                if (this.pass != this.pass2) {
                    this.errorReg = 'Пароли не совпадают';
                    return;
                }
                this.errorReg = '';
                var data = {
                    Login: this.login,
                    Password: this.pass
                }

                this.$http.post(`/api/account/reg`, data)
                    .then((res) => {
                        this.reg = false;
                        this.$toastr.success('Регистрация прошла успешно!');
                        this.login = '';
                        this.pass = '';
                        this.pass2 = '';
                        this.submit();
                    })
                    .catch((err) => {
                        console.log(err);
                        this.$toastr.error('Что-то пошло не так!');
                        this.errorReg = 'Пароль дожен содержать хотя бы одну букву, цифру и знак. Не менее 8 символов. Или же такой логин уже существует.'
                    })
            },
            goTo(str) {
                this.$router.push({ name: str })
            }
        },
        created: function () {
            
        },
        mounted: function () {
            
        },
        computed: {
        }
    }

</script>
<style>
    .action-btn {
        float: right;
    }

    .action-icon {
        color: white;
    }

    .main {
        background-color: brown;
        color: white;
        text-align: center;
        word-wrap: break-word;
    }

    .news_text {
        word-wrap: break-word;
        max-width: 400px;
    }

    .center {
        text-align: center
    }

    .word_text {
        padding-left: 10px;
        padding-right: 10px;
        word-wrap: break-word;
        font-size: 16px;
    }

    .font20 {
        font-size: 20px;
    }

    .css-adaptive {
        max-width: 300px;
        height: auto;
    }

    .card {
        padding: 15px;
    }
</style>