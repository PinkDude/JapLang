<template>
    <div>
        <v-card>
            <div class="card" >
                <v-text-field  label="Заголовок" v-model="news.tittle"></v-text-field>
                <img :src="news.image" class="css-adaptive"/>
                <br>
                <input ref="file" type="file" accept="image/jpeg,image/png,image/jpg" @change="previewFiles" />
                <v-textarea label="Текст новости" v-model="news.text">
                </v-textarea>
            </div>
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn v-if="!IsEdit" class="action-btn btn primary" @click="addnews()">Добавить</v-btn>
                <v-btn v-if="IsEdit" class="action-btn btn primary" @click="editnews()">Сохранить</v-btn>
            </v-card-actions>
        </v-card>
    </div>
</template>
<script>
    export default {
        data() {
            return {
                news: {
                    tittle: '',
                    image: '',
                    text: '',
                    ImageIsNew: true
                },
                imgIsNew: false,
                ImageName: '',
                IsEdit: false
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
                    this.news.image = e.target.result;
                    this.imgIsNew = true;
                }
                // Read in the image file as a data URL.
                reader.readAsDataURL(f);
            },
            editnews() {
                var data2 = {
                    tittle: this.news.tittle,
                    ImageFile: {
                        content: this.imgIsNew ? this.news.image.replace(/^data:image\/[a-z]+;base64,/, "") : null,
                        Filename: this.ImageName
                    },
                    text: this.news.text,
                    ImageIsNew: this.imgIsNew
                }

                this.$http.put(`/api/news/${this.news.id}`, data2)
                    .then((resp) => {
                        this.$toastr.success('Сохранено');
                        this.$router.push('/');
                    })
                    .catch((err) => {

                    })
            },
            addnews() {
                var data2 = {
                    tittle: this.news.tittle,
                    ImageFile: {
                        content: this.news.image.replace(/^data:image\/[a-z]+;base64,/, ""),
                        Filename: this.ImageName
                    },
                    text: this.news.text,
                    ImageIsNew: true
                }
                
                this.$http.post(`/api/news`, data2)
                    .then((response) => {
                        this.$toastr.success('Добавлено');
                        this.$router.push('/');
                    })
                .catch((err) => console.log(err))
            },
            fetchNews(id) {
                this.$http.get(`api/news/${id}`)
                    .then((res) => {
                        this.news = res.data;
                    })
                    .catch((err) => console.log(err))
            }
        },
        created: function () {
            if (this.$route.query.edit) {
                this.fetchNews(this.$route.query.id);
                this.IsEdit = true;
            }
        }
    }
</script>
<style>
.card{
    padding: 15px;
}
.action-btn{
    right: 0;
}
</style>
