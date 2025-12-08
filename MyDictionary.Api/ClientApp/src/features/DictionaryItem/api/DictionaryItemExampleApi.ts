import api, { EndPointBuilder } from "@/engine/api";
import type { DictionaryItemExampleListRequest } from "../types/requests/DictionaryItemExampleListRequest";
import type { ListType } from "@/engine/types/ListType";
import type { DictionaryItemExampleType } from "../types/models/DictionaryItemExampleType";

const endpoint = new EndPointBuilder("DictionaryItemExamples");

class DictionaryItemExampleApi {
  async list(payload: DictionaryItemExampleListRequest) {
    return await api.post<ListType<DictionaryItemExampleType[]>>(endpoint.build("List"), payload);
  }
  async create(payload : DictionaryItemExampleType) {
    await api.post(endpoint.build("Create"), payload);
  }
  async update(payload: DictionaryItemExampleType) {
    await api.put(endpoint.build("Update"), payload);
  }
  async delete(id: string) {
    const query = { id: id }
    await api.delete(endpoint.build("Delete", query));
  }
}

export default new DictionaryItemExampleApi();