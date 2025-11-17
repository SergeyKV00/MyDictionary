export interface ApiResponse<T> {
  value: T | null;
  isSuccess: boolean;
  error?: {
    code: string;
    description: string;
    metadata?: { property: string; message: string }[];
  };
  title?: string;
  status?: number;
}