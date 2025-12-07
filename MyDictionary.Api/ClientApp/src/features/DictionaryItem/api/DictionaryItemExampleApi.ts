import api, { EndPointBuilder } from "@/engine/api";
import type { DictionaryItemExampleListRequest } from "../types/requests/DictionaryItemExampleListRequest";
import type { ListType } from "@/engine/types/ListType";
import type { DictionaryItemExampleType } from "../types/models/DictionaryItemExampleType";

const endpoint = new EndPointBuilder("DictionaryItemExamples");

class DictionaryItemExampleApi {
  async list(payload: DictionaryItemExampleListRequest) {
    return await api.post<ListType<DictionaryItemExampleType[]>>(endpoint.build("List"), payload);
  }
}

export default new DictionaryItemExampleApi();