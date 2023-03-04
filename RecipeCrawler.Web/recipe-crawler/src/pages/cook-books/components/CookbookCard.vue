<script setup lang="ts">
import { NCard, NIcon, NSpace } from 'naive-ui';
import { DocumentHorizontal } from '@vicons/carbon';
import { Cookbook } from '../../../models/shared/cookbook.model';
import { type PropType } from 'vue';
import { useRecipeStore } from "../../../recipe-store";
defineProps({
    cookbook: {
        type: Object as PropType<Cookbook>,
        required: true
    }
});

const store = useRecipeStore();

async function cardClicked(): Promise<void> {
 //do nothing
    return;
}

async function deleteClicked(id: number): Promise<void> {
    await store.deleteCookbook(id);
} 

</script>
<template>
    <n-card closable hoverable size="medium" :title="cookbook?.name" @close="deleteClicked(cookbook.id)" @click="cardClicked()">
        <template #cover>
            <n-space justify="center">
                <img v-if="cookbook!.coverImageBase64!.length > 0" />
                <n-icon v-if="cookbook!.coverImageBase64!.length < 1" size="128">
                    <DocumentHorizontal />
                </n-icon>
            </n-space>
        </template>
    </n-card>
</template>
<style scoped>
.n-icon {
    padding: 4%;
}
.n-card {
    max-height: 200px;
    height: 200px;
}
</style>