import { defineStore } from "pinia";
import type { DictionaryItemResponse } from "../types/responses/DictionaryItemResponse";
import type { DictionaryItemListRequest } from "../types/requests/DictionaryItemListRequest";
import DictionaryItemApi from "../api/DictionaryItemApi";
import type { DictionaryDto } from "@/models/dto/dictionary/DictionaryDto";
import type { DictionaryItemCreateRequests } from "../types/requests/DictionaryItemCreateRequests";
import type { DictionaryItemType } from "../types/models/DictionaryItemType";

export const useDictionaryItemStore = defineStore("dictionaryItem", {
  state: () => ({
    // dictionaryId: null as string | null,  // TODO_U Delete change on dictionaryStore
    // dictionaryName: null as string | null, // TODO_U Delete change on dictionaryStore
    dictionary: JSON.parse(localStorage.getItem("dictionary") ?? "") as DictionaryDto | null,
    dictionaryItems: [] as DictionaryItemResponse[],
    dictionaryItemTotal: 0,
    loading: false
  }),
  getters: {
    getDictionary: (state) => state.dictionary
  },
  actions: {
    async fetchDictionaryItems(request: DictionaryItemListRequest) {
      this.loading = true;

      const response = await DictionaryItemApi.list(request).finally(() => this.loading = false);
      if (response?.error != null) {
        return Promise.reject();
      }

      this.dictionaryItems = response?.data?.data ?? [];
      this.dictionaryItemTotal = response?.data?.total ?? 0;
    },
    async create(request: DictionaryItemCreateRequests) {
      this.loading = true;

      const response = await DictionaryItemApi.create(request).finally(() => this.loading = false);
      if (response?.error != null) {
        return Promise.reject();
      }
      await this.fetchDictionaryItems({ dictionaryId: this.dictionary?.id! });
    },
    async update(request: DictionaryItemType) {
      this.loading = true;

      const response = await DictionaryItemApi.update(request).finally(() => this.loading = false);
      if (response?.error != null) {
        return Promise.reject();
      }
      await this.fetchDictionaryItems({ dictionaryId: this.dictionary?.id! });
    },
    async delete(id: string) {
      this.loading = true;

      const response = await DictionaryItemApi.delete(id).finally(() => this.loading = false);
      if (response?.error != null) {
        return Promise.reject();
      }
      await this.fetchDictionaryItems({ dictionaryId: this.dictionary?.id! });
    },
    setDictionary(dictionary: DictionaryDto | null) {
      this.dictionary = dictionary;

      if (dictionary) {
        localStorage.setItem("dictionary", JSON.stringify(dictionary));
      } else {
        localStorage.removeItem("dictionary");
      }
    }
  }
})