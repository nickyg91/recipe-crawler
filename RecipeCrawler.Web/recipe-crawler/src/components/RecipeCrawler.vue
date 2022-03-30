
function toRefs() {
throw new Error('Function not implemented.');
}
<script setup lang="ts">
    import { ref } from 'vue';
    import axios from 'axios';
    let url = ref('');
    let recipeHtml = ref('');
    const submit = () => {
        axios.post("/api/crawler", {
            url: url.value
        }).then(result => {
            recipeHtml.value = result.data;
        }).catch(error => {

        });
    }
function toRefs() {
throw new Error('Function not implemented.');
}
</script>
<template>
    <div class="section">
        <div class="columns">
            <div class="column is-6 is-offset-one-quarter">
                <form class="box">
                    <div class="field">
                        <div class="label">
                            Url
                        </div>
                        <div class="control">
                            <input class="input" type="text" v-model="url" />
                        </div>
                    </div>
                    <div class="field">
                        <button type="button" @click="submit()" class="button is-fullwidth is-primary">
                            Submit
                        </button>
                    </div>
                </form>
            </div>
        </div>
    </div>
    <div class="box" v-if="recipeHtml.length > 0">
        <div class="columns">
            <div class="column is-6 is-offset-one-quarter" v-html="recipeHtml"></div>
        </div>
    </div>
</template>