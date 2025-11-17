import { t } from "@/locales/i18";

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