<script setup lang="ts">
import { NGrid, NGi, NCard, NIcon, NSpace } from 'naive-ui';
import { Add } from '@vicons/carbon';
import { Cookbook } from '../../../models/shared/cookbook.model';
import CookbookCard from './CookbookCard.vue';
import { type PropType } from 'vue';
defineProps({
    cookbooks: {
        default: () => [],
        type: Array as PropType<Cookbook[]>,
        required: true,
    },
});
const emits = defineEmits(['onAddClicked']);
function onAddClicked(): void {
    emits('onAddClicked');
}
</script>
<template>
    <n-grid cols="2 s:3 m:4 l:5 xl:6 2xl:7" responsive="screen" :x-gap="15" :y-gap="15" :cols="12">
        <n-gi>
            <n-card hoverable embedded :bordered="true" class="add-cookbook-card" :title="'Create'" size="medium" @click="onAddClicked()">
                <template #cover>
                    <n-space justify="center">
                        <n-icon size="128">
                            <Add />
                        </n-icon>
                    </n-space>
                </template>
            </n-card>
        </n-gi>
        <n-gi v-for="cookbook in cookbooks" :key="cookbook.id">
            <CookbookCard :cookbook="cookbook" />
        </n-gi>
    </n-grid>
</template>
<style scoped>
.add-cookbook-card {
    border-style: dashed;
    height: 200px;
    max-height: 200px;
}

.n-card:hover {
    cursor: pointer;
}
</style>