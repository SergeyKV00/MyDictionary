import api from "@/engine/api";
import { EndPointBuilder }  from "@/engine/api"
import type { ListType } from "@/engine/types/ListType";
import type { DictionaryItemResponse } from "../types/responses/DictionaryItemResponse";
import type { DictionaryItemListRequest } from "../types/requests/DictionaryItemListRequest";
import type { DictionaryItemCreateRequests } from "../types/requests/DictionaryItemCreateRequests";
import { DictionaryItemType } from "../types/models/DictionaryItemType";
import type { DictionaryItemAggregateWeightRequests } from "../types/requests/DictionaryItemAggregateWeightRequests";
import type { DictionaryItemRequest } from "../types/requests/DictionaryItemRequest";

const endpoint = new EndPointBuilder("DictionaryItems");

class DictionaryItemApi {
  async list(payload: DictionaryItemListRequest) {
    return api.post<ListType<DictionaryItemType[]>>(endpoint.build("List"), payload);
  }
  async create(payload: DictionaryItemCreateRequests) {
    return api.post<string>(endpoint.build("Create"), payload);
  }
  async update(payload: DictionaryItemType) {
    return api.put<string>(endpoint.build("Update"), payload);
  }
  async delete(id: string) {
    var query = { id: id }
    return api.delete(endpoint.build("Delete", query));
  }
  async aggregateWeight(payload: DictionaryItemAggregateWeightRequests) {
    return api.post<number>(endpoint.build("AggregateWeight"), payload);
  }
  async get(payload: DictionaryItemRequest) {
    return api.post<DictionaryItemType>(endpoint.build("Get"), payload);
  }
}

export default new DictionaryItemApi();