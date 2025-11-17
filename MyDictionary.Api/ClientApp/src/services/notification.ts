import { ElNotification } from "element-plus";
import type { FrontendError } from "@/models/base/FrontendError";
import { NotifiableErrors } from "@/engine/errorConfig";

let notificationOffset = 20;
const notificationGap = 16; 

export class NotificationService {
  static notifyError(err: FrontendError) {
    const config = NotifiableErrors[err.code];

    const title = config?.description ?? err.code;
    const message = config?.message ?? err.message;

    if (err.validation) {
      for (const key in err.validation) {
        err.validation[key]!.forEach(msg => {
            ElNotification({ 
                type: "error", 
                title, 
                message: msg, 
                duration: 5000,
                offset: notificationOffset,
                customClass: 'center-message'
            });
        });
        notificationOffset += notificationGap + 60;
      }
      notificationOffset = 20;
    } else {
        ElNotification({ 
            type: "error", 
            title, 
            message, 
            duration: 5000,
            customClass: 'center-message'
        });
    }
  }
}