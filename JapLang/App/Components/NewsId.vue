<template>
<div>
KEK
</div>
</template>

<script>
export default{
    data(){
        return{
            news:{},
            loading: false
        }
    },
    methods:{
        getNews(){
            this.loading = true;
            this.$http.get(`/api/news/${this.$route.params.newsId}`)
            .then((response) => {
                this.news = response.data;
            })
            .catch((ex) => console.log(ex))
            .finally(() => this.loading = false)
        }
    }
}
</script>