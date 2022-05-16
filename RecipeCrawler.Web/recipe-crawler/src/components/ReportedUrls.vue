<script lang="ts" setup>
import {
  NSpace,
  NDataTable,
  DataTableColumn,
  PaginationInfo,
  NSpin,
  useNotification,
} from "naive-ui";
import { inject, reactive, ref } from "vue";
import { ParsedResponse } from "../models/parsed-response.model";
import { CrawlerApi, injectionKey } from "../services/crawler-api.service";
const crawlerApi: CrawlerApi | undefined = inject(injectionKey);

let loading = ref(false);
const notificationService = useNotification();
let data: ParsedResponse[] = reactive([]);
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
// crawlerApi?.getReportedUrls(1, )

const loadTableData = (page: number, pageSize: number) => {
  loading.value = true;
  crawlerApi
    ?.getReportedUrls(page, pageSize)
    .then((result) => {
      pagination.itemCount = result.data.totalItems;
      data.push(...result.data.recipes);
    })
    .catch((err) => {
      notificationService.error({
        title: "Error loading data!",
        content: "Unable to load reported URLs.",
      });
    })
    .finally(() => {
      loading.value = false;
    });
};

const pagination: PaginationInfo = reactive({
  page: 1,
  pageCount: 1,
  itemCount: 1,
  pageSize: 5,
  pageSizes: [5, 10, 15, 20],
  showSizePicker: true,
  startIndex: 0,
  endIndex: 0,
  onChange: (page: number) => {
    pagination.page = page;
    loadTableData(page, pagination.pageSize);
  },
  onUpdatePageSize: (pageSize: number) => {
    pagination.pageSize = pageSize;
  },
});
loadTableData(1, 5);
</script>
<template>
  <n-spin :show="loading">
    <n-space style="margin-top: 15px" vertical justify="center">
      <n-data-table :columns="columns" :data="data" :pagination="pagination">
      </n-data-table>
    </n-space>
  </n-spin>
</template>
