import  api from "@/engine/api";
import { EndPointBuilder }  from "@/engine/api"
import type { LoginPayload } from "../types/LoginPayload";
import type { RegistractionPayload } from "../types/RegistrationPayload";

const endpoint = new EndPointBuilder("Users");
const errorMap: Record<string, string> = {
  
}

class AuthApi {
  async login(payload: LoginPayload) {
    return api.post<string>(endpoint.build("Login"), payload);
  }
  async register(payload: RegistractionPayload) {
    return api.post<string>(endpoint.build("Create"), payload);
  }
}

export default new AuthApi();