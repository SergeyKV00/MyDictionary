import Api from "@/engine/api";

const endpoint = "DictionaryItems";

class DictionaryItemService {
    async count(dictionaryId: string) {
        return await Api.get<number>(`/${endpoint}/Count?dictionaryId=${dictionaryId}`);
    }
}

export default new DictionaryItemService();