<template>
    <div > 
        <div >
            <v-card >
                <v-container fluid grid-list-md>
                    <v-layout row wrap>
                        <v-flex v-for="(item, i) in news"
                        :key="i"
                        v-bind="{['xs$6'] : true}">
                        <v-card min-width="400px" >
                            <div class="main">
                                <div class="action-btn" v-show="$cookies.get('isAdmin')">
                                    <v-icon class="mr-1" style="color:white;" @click="editNews( item.id)">create</v-icon>
                                    <v-icon class="mr-1" style="color:white;" @click="deleteNews(item.id)">delete</v-icon>
                                </div>
                                <h3 ><!--@click="openNews(item.id)"-->
                                    {{item.tittle}}
                                </h3>
                            </div>
                            <div style="text-align: center">
                            <img :src="item.image" class="css-adaptive">
                            </div>
                            <div align="center">
                            <p v-if="item.text" class="news_text center">
                            {{item.text}}
                            </p>
                            </div>
                            <v-card-actions>
                                <v-spacer></v-spacer>
                                <span class=subheading mr-2>{{item.date | formatDate}}</span>
                                <v-icon class="mr-1">visibility</v-icon>
                                <span class=subheading mr-2>{{item.viewCount}}</span>
                            </v-card-actions>
                        </v-card>
                        </v-flex>
                    </v-layout>
                </v-container>
            </v-card>
        </div>
    </div>
</template>
<script>
    export default {
        data(){
            return {
                isAdmin: this.$cookies.get('isAdmin'),
                news: null,
                take: 10,
                skip: 0,
                loading: false
            }
        },
        methods: {
            editNews( id) {
                this.$router.push({ path: `adm/news`, query: { id: id, edit:true } });
            },
            deleteNews(id) {
                this.$http.delete(`api/news/${id}`)
                    .then((resp) => {
                        this.$toastr.success('Удалено');
                        this.fetchNews();
                    })
                    .catch((err) => {

                    })
            },
            fetchNews() {
                
                this.loading = true;
                this.$http.get(`/api/news?Take=${this.take}&Skip=${this.skip}`)
                .then((response) => {
                    this.news = response.data;
                })
                .catch((ex) => console.log(ex))
                .finally((res) => this.loading = false)
            },
            openNews(id){
                this.$router.push(`news/${id}`)
            }
        },
        created:function(){
            this.fetchNews();
            this.isAdmin= this.$cookies.get('isAdmin')
        },
        computed: {
            
        }
    }
</script>
