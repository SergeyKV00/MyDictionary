import api from "@/engine/api";
import { EndPointBuilder } from "@/engine/api";
import type { ListType } from "@/engine/types/ListType";
import type { StudyDeckType } from "../types/models/StudyDeckType";
import type { CreateStudyDeckRequest } from "../types/requests/CreateStudyDeckRequest";
import type { GetStudyDeckListRequest } from "../types/requests/GetStudyDeckListRequest";

import type { AddDictionaryToStudyDeckRequest, AddWordsToStudyDeckRequest } from "../types/requests/StudyDeckSettingsRequests";
import type { GetStudyDeckRequest, GetStudyDeckWordsRequest } from "../types/requests/GetStudyDeckRequests";
import type { StudyDeckDetailResponse } from "../types/responses/StudyDeckDetailResponse";
import type { StudyDeckWordResponse } from "../types/responses/StudyDeckWordResponse";

const endpoint = new EndPointBuilder("StudyDecks");

class StudyDeckApi {
    async list(payload: GetStudyDeckListRequest) {
        return api.post<ListType<StudyDeckType[]>>(endpoint.build("List"), payload);
    }
    async create(payload: CreateStudyDeckRequest) {
        return api.post<string>(endpoint.build("Create"), payload);
    }
    async delete(id: string) {
        const query = { id: id };
        return api.delete(endpoint.build("Delete", query));
    }
    async addDictionary(payload: AddDictionaryToStudyDeckRequest) {
        return api.post(endpoint.build(`AddDictionary`), payload);
    }
    async updateDictionary(payload: AddDictionaryToStudyDeckRequest) {
        return api.post(endpoint.build(`UpdateDictionary`), payload);
    }
    async removeDictionary(payload: Omit<AddDictionaryToStudyDeckRequest, 'isSynchronized'>) {
        return api.post(endpoint.build(`RemoveDictionary`), payload);
    }
    async addWords(payload: AddWordsToStudyDeckRequest) {
        return api.post(endpoint.build(`AddWords`), payload);
    }
    async removeWords(payload: AddWordsToStudyDeckRequest) {
        return api.post(endpoint.build(`RemoveWords`), payload);
    }
    async get(payload: GetStudyDeckRequest) {
        return api.post<StudyDeckDetailResponse>(endpoint.build(`Get`), payload);
    }
    async getWords(payload: GetStudyDeckWordsRequest) {
        return api.post<ListType<StudyDeckWordResponse[]>>(endpoint.build(`GetWords`), payload);
    }
}

export default new StudyDeckApi();
