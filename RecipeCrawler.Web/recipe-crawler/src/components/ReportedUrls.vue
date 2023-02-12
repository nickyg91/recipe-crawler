<script lang="ts" setup>
import { NSpace, NDataTable, DataTableColumn, useMessage } from "naive-ui";
import { inject, reactive, ref } from "vue";
import { ParsedResponse } from "../pages/recipe/models/parsed-response.model";
import { CrawlerApi, injectionKey } from "../services/crawler-api.service";
const crawlerApi: CrawlerApi | undefined = inject(injectionKey);
const messageService = useMessage();
const loading = ref(false);
const data = ref(new Array<ParsedResponse>());
const columns: Array<DataTableColumn> = [
  {
    key: "title",
    title: "Title",
  },
  {
    key: "url",
    title: "URL",
  },
];

const pagination = reactive({
  page: 1,
  pageCount: 1,
  pageSize: 5,
  itemCount: 0,
  pageSizes: [5, 10, 15, 20],
  showSizePicker: true,
  onUpdatePageSize: (pageSize: number) => {
    pagination.pageSize = pageSize;
    pagination.page = 1;
    loadTableData(pagination.page);
  },
});

const loadTableData = (page: number) => {
  loading.value = true;
  crawlerApi
    ?.getReportedUrls(page, pagination.pageSize)
    .then((result) => {
      data.value = result.data.recipes;
      pagination.page = page;
      pagination.itemCount = result.data.totalItems;
      pagination.pageCount = Math.ceil(
        result.data.totalItems / pagination.pageSize
      );
    })
    .catch(() => {
      messageService.error("Error loading data!", {
        closable: true,
      });
    })
    .finally(() => {
      loading.value = false;
    });
};
loadTableData(1);
</script>
<template>
  <n-space style="margin-top: 15px" vertical justify="center">
    <n-data-table
      remote
      ref="table"
      :loading="loading"
      @update:page="loadTableData"
      :columns="columns"
      :data="data"
      :pagination="pagination"
    >
    </n-data-table>
  </n-space>
</template>
