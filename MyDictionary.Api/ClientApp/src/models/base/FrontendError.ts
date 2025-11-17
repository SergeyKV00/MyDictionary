export interface FrontendError {
  code: string;
  message: string;
  status?: number;
  validation?: Record<string, string[]>;
  raw?: any;
}