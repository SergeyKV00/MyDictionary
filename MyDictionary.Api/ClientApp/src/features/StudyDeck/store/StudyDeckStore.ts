import { defineStore } from "pinia";
import StudyDeckApi from "../api/StudyDeckApi";
import type { StudyDeckType } from "../types/models/StudyDeckType";
import type { CreateStudyDeckRequest } from "../types/requests/CreateStudyDeckRequest";
import type { GetStudyDeckListRequest } from "../types/requests/GetStudyDeckListRequest";
import type { AddDictionaryToStudyDeckRequest, AddWordsToStudyDeckRequest } from "../types/requests/StudyDeckSettingsRequests";
import type { GetStudyDeckWordsRequest } from "../types/requests/GetStudyDeckRequests";

export const useStudyDeckStore = defineStore("studyDeck", {
    state: () => ({
        studyDecks: [] as StudyDeckType[],
        loading: false,
    }),
    getters: {
        getStudyDecks: (state) => state.studyDecks,
    },
    actions: {
        async fetchStudyDecks(request: GetStudyDeckListRequest = {}) {
            try {
                this.loading = true;
                const response = await StudyDeckApi.list(request);
                if (response?.error != null) {
                    return Promise.reject(response.error);
                }
                this.studyDecks = response?.data?.data ?? [];
            } finally {
                this.loading = false;
            }
        },
        async create(request: CreateStudyDeckRequest) {
            this.loading = true;
            const response = await StudyDeckApi.create(request).finally(() => this.loading = false);
            if (response?.error != null) return Promise.reject(response.error);

            await this.fetchStudyDecks();
            return response?.data;
        },
        async delete(id: string) {
            this.loading = true;
            const response = await StudyDeckApi.delete(id).finally(() => this.loading = false);
            if (response?.error != null) return Promise.reject(response.error);

            await this.fetchStudyDecks();
        },
        async addDictionary(id: string, payload: Omit<AddDictionaryToStudyDeckRequest, 'studyDeckId'>) {
            this.loading = true;
            const request: AddDictionaryToStudyDeckRequest = { studyDeckId: id, ...payload };
            const response = await StudyDeckApi.addDictionary(request).finally(() => this.loading = false);
            if (response?.error != null) return Promise.reject(response.error);
        },
        async updateDictionary(id: string, payload: Omit<AddDictionaryToStudyDeckRequest, 'studyDeckId'>) {
            this.loading = true;
            const request: AddDictionaryToStudyDeckRequest = { studyDeckId: id, ...payload };
            const response = await StudyDeckApi.updateDictionary(request).finally(() => this.loading = false);
            if (response?.error != null) return Promise.reject(response.error);
        },
        async removeDictionary(id: string, dictionaryId: string) {
            this.loading = true;
            const request = { studyDeckId: id, dictionaryId };
            const response = await StudyDeckApi.removeDictionary(request).finally(() => this.loading = false);
            if (response?.error != null) return Promise.reject(response.error);
        },
        async addWords(id: string, payload: Omit<AddWordsToStudyDeckRequest, 'studyDeckId'>) {
            this.loading = true;
            const request: AddWordsToStudyDeckRequest = { studyDeckId: id, ...payload };
            const response = await StudyDeckApi.addWords(request).finally(() => this.loading = false);
            if (response?.error != null) return Promise.reject(response.error);
        },
        async removeWords(id: string, wordIds: string[]) {
            this.loading = true;
            const request = { studyDeckId: id, wordIds };
            const response = await StudyDeckApi.removeWords(request).finally(() => this.loading = false);
            if (response?.error != null) return Promise.reject(response.error);
        },
        async get(id: string) {
            this.loading = true;
            const response = await StudyDeckApi.get({ id }).finally(() => this.loading = false);
            if (response?.error != null) return Promise.reject(response.error);
            return response?.data;
        },
        async getWords(payload: GetStudyDeckWordsRequest) {
            const response = await StudyDeckApi.getWords(payload);
            if (response?.error != null) return Promise.reject(response.error);
            return response?.data;
        }
    },
});
