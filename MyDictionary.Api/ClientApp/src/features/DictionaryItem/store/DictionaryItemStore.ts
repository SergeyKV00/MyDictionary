import { defineStore } from "pinia";
import type { DictionaryItemListRequest } from "../types/requests/DictionaryItemListRequest";
import DictionaryItemApi from "../api/DictionaryItemApi";
import type { DictionaryDto } from "@/models/dto/dictionary/DictionaryDto";
import type { DictionaryItemCreateRequests } from "../types/requests/DictionaryItemCreateRequests";
import type { DictionaryItemType } from "../types/models/DictionaryItemType";
import { AggregateWeightType } from "../types/requests/DictionaryItemAggregateWeightRequests";

export const useDictionaryItemStore = defineStore("dictionaryItem", {
  state: () => ({
    dictionary: JSON.parse(localStorage.getItem("dictionary") ?? "") as DictionaryDto | null,
    dictionaryItems: [] as DictionaryItemType[],
    dictionaryItemTotal: 0,
    loading: false,
    minWeight: 0,
    maxWeight: 0,
    query: {
      offset: 0,
      limit: 1000
    }
  }),
  getters: {
    getDictionary: (state) => state.dictionary
  },
  actions: {
    async fetchDictionaryItems(request: DictionaryItemListRequest) {
      try {
        this.loading = true;

        const response = await DictionaryItemApi.list({...request, ...this.query})
        if (response?.error != null) {
          return Promise.reject();
        }

        this.dictionaryItems = response?.data?.data ?? [];
        this.dictionaryItemTotal = response?.data?.total ?? 0;
        
        this.minWeight = await this.fetchWeight(AggregateWeightType.min) ?? 0;
        this.maxWeight = await this.fetchWeight(AggregateWeightType.max) ?? 0;
      } finally {
        this.loading = false
      }
    },
    async create(request: DictionaryItemCreateRequests) {
      this.loading = true;

      const response = await DictionaryItemApi.create(request).finally(() => this.loading = false);
      if (response?.error != null) {
        return Promise.reject();
      }
      await this.fetchDictionaryItems({ dictionaryId: this.dictionary?.id!, ...this.query });
    },
    async update(request: DictionaryItemType) {
      this.loading = true;

      const response = await DictionaryItemApi.update(request).finally(() => this.loading = false);
      if (response?.error != null) {
        return Promise.reject();
      }
      await this.fetchDictionaryItems({ dictionaryId: this.dictionary?.id!, ...this.query });
    },
    async delete(id: string) {
      this.loading = true;

      const response = await DictionaryItemApi.delete(id).finally(() => this.loading = false);
      if (response?.error != null) {
        return Promise.reject();
      }
      await this.fetchDictionaryItems({ dictionaryId: this.dictionary?.id!, ...this.query });
    },
    setDictionary(dictionary: DictionaryDto | null) {
      this.dictionary = dictionary;

      if (dictionary) {
        localStorage.setItem("dictionary", JSON.stringify(dictionary));
      } else {
        localStorage.removeItem("dictionary");
      }
    },
    async fetchWeight(agrWeightType: AggregateWeightType) {
      var response = await DictionaryItemApi.aggregateWeight({ 
        dictionaryId: this.dictionary?.id!, 
        weightAggregate: agrWeightType
      });

      if (response?.error != null) {
        return Promise.reject();
      }
      return response?.data;
    }
  }
})