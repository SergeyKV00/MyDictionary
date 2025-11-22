import { t } from "@/locales/i18";
import { errorCodes as authErrors } from "@/features/Auth/api/ErrorCodes";

const errorCodes =  {
  ...authErrors
}

class ErrorMessage {
  has(code: string | null) {
    var error = errorCodes[code ?? ""];
    return error != undefined && error != null;
  }
  show(code: string | null) {
    var error = errorCodes[code ?? ""];
    return error ?? "";
  }
}

export default new ErrorMessage();

export interface ErrorNotificationConfig {
  description?: string;
  message?: string;
}

export const NotifiableErrors: Record<string, ErrorNotificationConfig> = {
  "Validation.Error": {
    description: t("Errors.ValidationError"),
    message: ""
  },
};