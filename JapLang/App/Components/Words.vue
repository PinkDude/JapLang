<template>
<div>
<div>
    <v-card>
        <div class="center font20">
            Все иероглифы выделены цветом соответствующим уровню JLPT
            <table>
                <tbody>
                    <tr>
                        <td style="background-color: #e4f4e7; width: 30px">
                            
                        </td>
                        <td>
                             5-й кю
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #eae0d6; width: 30px">
                            
                        </td>
                        <td>
                             4-й кю
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #e4e3f7; width: 30px">
                            
                        </td>
                        <td>
                             3-й кю
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #f9dddd; width: 30px">
                            
                        </td>
                        <td>
                             2-й кю
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #f7f7c4; width: 30px">
                            
                        </td>
                        <td>
                             1-й кю
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #fff; width: 30px">
                            
                        </td>
                        <td>
                             Вне категорий
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <v-container fluid grid-list-md>
            <v-layout row wrap>
                <v-flex v-for="(item, i) in words"
                        :key="i"
                        v-bind="{['xs$4'] : true}">
                    <v-card min-width="400px">
                        <div class="center" v-bind:style="{'background-color': item.category.color}">
                            <div class="action-btn" v-show="$cookies.get('isAdmin')">
                                <v-icon class="mr-1" @click="editWord( item.id)">create</v-icon>
                                <v-icon class="mr-1" @click="deleteWord(item.id)">delete</v-icon>
                            </div>
                            <div class="center">
                                <h1>
                                    {{item.word}}
                                </h1>
                            </div>
                        </div>
                        <div class="center" v-if="item.gif">
                            <img :src="item.gif" style="max-width:600px">
                        </div>
                        <div class="center font20">
                            {{transcription}}: {{item.transcription}}
                        </div>
                        <p v-for="(wordRus, i) in item.wordRus" :key="i" class="word_text center">
                            {{wordRus.word}}
                        </p>
                    </v-card>
                </v-flex>
            </v-layout>
        </v-container>

    </v-card>
</div>
</div>
</template>

<script>
export default{
    data(){
        return {
            transcription: 'Транскрипция',
            words:{},
            loading: false
        }
    },
        methods: {
            editWord(id) {
                this.$router.push({ path: `adm/word`, query: { id: id, edit: true } });
            },
            deleteWord(id) {
                this.$http.delete(`api/words/${id}`)
                    .then((resp) => {
                        this.$toastr.success('�������');
                        this.getWords();
                    })
                    .catch((err) => {

                    })
            },
        getWords(){
            this.loading = true;
            this.$http.get(`/api/words`)
            .then((response) => {
                this.words = response.data;
                console.log(this.words);
            })
            .catch((ex) => console.log(ex))
            .finally(() => this.loading = false)
        },
        openWord(id){
            this.$router.push();
        }
    },
    created:function(){
        this.getWords();
    }
}
</script>
<style scoped>
table{
    margin: auto;
    border: 1px solid;
    padding: 2px;
}
TD, TH {
    padding: 3px; /* Поля вокруг содержимого таблицы */
    border: 1px solid black; /* Параметры рамки */
   }
</style>