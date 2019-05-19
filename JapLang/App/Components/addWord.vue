<template>
    <div>
        <v-card>
            <div class="card">
                <v-text-field label="Слово на японском" v-model="word.word"></v-text-field>
                <img :src="word.gif" class="css-adaptive" />
                <br>
                <input ref="file" type="file" accept="image/gif" @change="previewFiles" />
                <v-text-field label="Транскрипция " v-model="word.transcription">
                </v-text-field>
                <br />
                <p>Уровень JLPT:</p>
                <select class="select" v-model="category">
                    <option v-for="(cat, i) in wordCategories" v-bind:value="cat.wordJapanCategoryId">
                        {{cat.name}}
                    </option>
                </select>
                <br />
                <table>
                    <tbody>
                        <tr v-for="(wordr, i) in word.wordRus">
                            <v-text-field label="Перевод слова на русский" v-model="word.wordRus[i].word"></v-text-field>
                        </tr>
                        <tr>
                            <v-btn @click="addRusWord()" style="width: 100%">Добавить еще один перевод</v-btn>
                        </tr>
                    </tbody>
                </table>
            </div>
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn v-if="!IsEdit" class="action-btn btn primary" @click="addword()">Добавить</v-btn>
                <v-btn v-if="IsEdit" class="action-btn btn primary" @click="editword()">Сохранить</v-btn>
            </v-card-actions>
        </v-card>
    </div>
</template>
<script>
    export default {
        data() {
            return {
                word: {
                    word: '',
                    gifIsNew: false,
                    gif: '',
                    wordRus: [{word:''}],
                    transcription: '',
                },
                ImageName: '',
                wordCategories: [],
                IsEdit: false,
                category: null
            }
        },
        methods: {
            previewFiles() {
                var f = this.$refs.file.files[0];
                this.ImageName = f.name;
                var reader = new FileReader();
                // Closure to capture the file information.
                reader.onload = (e) => {
                    // Note: arrow function used here, so that "this.imageData" refers to the imageData of Vue component
                    // Read image as base64 and set to imageData
                    this.word.gif = e.target.result;
                    this.word.gifIsNew = true;
                }
                // Read in the image file as a data URL.
                reader.readAsDataURL(f);
            },
            editword() {
                var data = {
                    word: this.word.word,
                    gifIsNew: this.word.gifIsNew,
                    gif: {
                        content: this.word.gifIsNew ? this.word.gif.replace(/^data:image\/[a-z]+;base64,/, "") : null,
                        Filename: this.ImageName
                    },
                    transcription: this.word.transcription,
                    wordRus: this.word.wordRus,
                    wordJapanCategoryId: this.category
                }

                this.$http.put(`/api/words/${this.word.id}`, data)
                    .then((resp) => {
                        this.$toastr.success('Сохранено');
                        this.$router.push('/words');
                    })
                    .catch((err) => {

                    })
            },
            addword() {
                var data = {
                    word: this.word.word,
                    gifIsNew: this.word.gifIsNew,
                    gif: {
                        content: this.word.gifIsNew ? this.word.gif.replace(/^data:image\/[a-z]+;base64,/, "") : null,
                        Filename: this.ImageName
                    },
                    transcription: this.word.transcription,
                    wordRus: this.word.wordRus,
                    wordJapanCategoryId: this.category
                }
                this.$http.post(`api/words/`, data)
                    .then((res) => {
                        this.$toastr.success('Добавлено');
                        this.$router.push('/words');
                    })
                .catch((err) => console.log(err))
            },
            addRusWord() {
                this.word.wordRus.push({word:''});
            },
            getCategories() {
                this.$http.get(`/api/words/categories`)
                    .then((res) => {
                        this.wordCategories = res.data;
                        this.category = this.wordCategories[this.wordCategories.length-1].wordJapanCategoryId;
                    })
                    .catch((err) => console.log(err));
            },
            fetchWord(id) {
                this.$http.get(`api/words/${id}`)
                    .then((res) => {
                        this.word = res.data;
                        this.category = res.data.category.wordJapanCategoryId
                    })
                    .catch((err) => console.log(err))
            }
        },
        created: function () {
            this.getCategories();
            if (this.$route.query.edit) {
                this.fetchWord(this.$route.query.id);
                this.IsEdit = true;
            }
        }
    }
</script>
<style>
    .card {
        padding: 15px;
    }

    .action-btn {
        right: 0;
    }
    .select {
        border: 3px solid #A52A2A;
    }
</style>