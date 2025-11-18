import  api from "@/engine/api";
import { EndPointBuilder }  from "@/engine/api"
import type { LoginPayload } from "../types/LoginPayload";

const endpoint = new EndPointBuilder("Users");

class AuthApi {
  async login(payload: LoginPayload) {
    return api.post<string>(endpoint.build("Login"), payload);
  }
}

export default new AuthApi();