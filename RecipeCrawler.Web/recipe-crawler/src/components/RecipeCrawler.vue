<script setup lang="ts">
    import { reactive, ref } from 'vue';
    import axios, { AxiosResponse } from 'axios';
    import { ParsedResponse } from '../models/parsed-response.model';
    import { NButton, NGrid, NGi, NForm, NFormItem, NInput, NRow, NCol } from 'naive-ui';
    let url = ref('');
    let response = reactive(new ParsedResponse());
    const submit = () => {
        axios.post("/api/crawler", {
            url: url.value
        }).then((result: AxiosResponse<ParsedResponse>) => {
            response.ingredients = result.data.ingredients;
            response.steps = result.data.steps;
        }).catch(error => {

        });
    }
</script>
<template>
<div>
    <n-grid cols="1">
        <n-gi>
            <n-form>
                <n-form-item label="Url">
                    <n-input v-model:value="url" />
                </n-form-item>
                <n-row>
                    <n-col>
                        <n-button @click="submit()" type="primary">
                            Submit
                        </n-button>
                    </n-col>
                </n-row>
            </n-form>
        </n-gi>
    </n-grid>
</div>
    <div class="box" v-if="response.ingredients?.length > 0">
        <div class="columns">
            <div class="column is-6 is-offset-one-quarter">
                <div v-for="item in response.ingredients" class="py-3">
                    <div v-html="item">

                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="box" v-if="response.steps?.length > 0">
        <div class="columns">
            <div class="column is-6 is-offset-one-quarter">
                <div v-for="item in response.steps" class="py-3">
                    <div v-html="item">

                    </div>
                </div>
            </div>
        </div>
    </div>
</template>