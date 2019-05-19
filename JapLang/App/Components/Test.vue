<template>
    <div>
        <v-card>
            <v-container fluid grid-list-md>
                <v-layout row wrap>
                    <v-flex v-for="(item, i) in tests"
                            :key="i"
                            v-bind="{['xs$4'] : true}">
                        <v-card min-width="400px" @click="openD(item.lang)">
                            <div class="main">
                                <h3 @click="openNews(item.id)">
                                    {{item.tittle}}
                                </h3>
                            </div>
                            <div style="text-align: center">
                                <img :src="item.img" class="css-adaptive">
                            </div>
                        </v-card>
                    </v-flex>
                </v-layout>
            </v-container>
        </v-card>
        <v-dialog v-model="jap"
                  :fullscreen="$vuetify.breakpoint.mdAndDown"
                  :persistent="true"
                  max-width="600">
            <v-card>
                <v-toolbar dark color="primary">
                    <h4>
                        Тест
                    </h4>
                    <v-spacer></v-spacer>
                    <v-btn icon dark @click.native="jap = false; endJap = false;">
                        <v-icon>close</v-icon>
                    </v-btn>
                </v-toolbar>
                <div class="card">
                    <div v-for="(item, i) in japQ" v-if="isViewJap(i)">
                        <h4>{{item.wordJap}}</h4>
                        <v-text-field label="Ответ на русском" v-model="answerRus">
                        </v-text-field>
                        <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn class="btn primary" @click="answerJap(i, answerRus)">Дальше</v-btn>
                        </v-card-actions>
                    </div>
                    <div v-if="endJap">
                        <h4>
                            Вы закончили тест!
                        </h4>
                        Ваш результат: {{currAnsJap / japQ.length * 100}}%
                    </div>
                </div>
                
            </v-card>
        </v-dialog>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                endJap: false,
                isFirst: true,
                answerRus: '',
                jap: false,
                rus: false,
                currAnsJap: 0,
                japQ: [],
                tests: [{
                    tittle: 'С японского на русский',
                    img: '/Files/Сяпторусс.jpg',
                    lang: 'jap'
                },
                    {
                        tittle: 'С русского на японский',
                        img: '/Files/Сяпторусс.jpg',
                        lang: 'rus'
                    }
                ]
        }
        },
        methods: {
            isViewJap(id) {
                if (id === 0) return this.isFirst;
                return this.japQ[id-1].next
            },
            openD(lang) {
                this[lang] = !this[lang];
                if (lang === 'jap') {
                    this.japQ = [];
                    this.getTestJap();
                }
            },
            answerJap(id, answ) {
                if (this.jap) {
                    if (this.japQ[id].answer.find((ans) => {
                        return ans.word === answ
                    }) ) {
                        this.currAnsJap++
                    }
                    this.japQ[id].next = true;
                    if (id === this.japQ.length - 1) {
                        this.endJap = true;
                    }
                    
                    if (this.isFirst) {
                        this.isFirst = false;
                    }
                    else {
                        this.japQ[id-1].next = false;
                    }
                }
                this.answerRus = '';
            },
            getTestJap() {
                this.$http.get(`/api/tests/jap`)
                    .then((res) => {
                        this.japQ = res.data.result;
                        this.isFirst = true;
                        this.currAnsJap = 0;
                    })
                    .catch((err) => console.log(err));
            }
        }
    }
</script>