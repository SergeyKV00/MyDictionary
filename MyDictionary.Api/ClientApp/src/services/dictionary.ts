import Api from "@/engine/api";
import type { ListResult } from "@/models/base/ListResult";
import type { DictionaryDto } from "@/models/dto/dictionary/DictionaryDto";
import type { DictionaryListRequest } from '@/models/dto/dictionary/DictionaryListRequest';
import type { CreateDictionaryRequest } from "@/models/dto/dictionary/CreateDictionaryRequest";


const endpoint = "UserDictionaries";

class DictionaryService {
    async list(request: DictionaryListRequest | null) {
        const response = await Api.post<ListResult<DictionaryDto>>(`/${endpoint}/List`, request);
        return response;
    }
    async create(request: CreateDictionaryRequest) {
        await Api.post(`/${endpoint}/Create`, request);
    }
    async get(id: string) {
        const response = await Api.get<DictionaryDto>(`/${endpoint}/Get?id=${id}`);
        return response;
    }
    async delete(dictionaryId: string) {
        await Api.delete(`/${endpoint}/Delete?id=${dictionaryId}`);
    }
    async searchAcross(term: string | null) {
        const response = await Api.post<unknown[]>(`/${endpoint}/SearchAcross`, { term });
        return response;
    }
}

export default new DictionaryService();