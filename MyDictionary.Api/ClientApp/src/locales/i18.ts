import { createI18n } from 'vue-i18n';
import en from './en.json';
import ru from './ru.json';

const messages = {
  ru,
  en,
};

export enum LangEnum {
  ru = 'ru',
  en = 'en',
}

let lang: LangEnum = LangEnum.ru;

export const i18n = createI18n({
  locale: lang,
  fallbackLocale: LangEnum.en,
  legacy: false,
  messages,
});

export const t = (key: string, values?: Record<string, unknown>): string => {
  return i18n.global.t(key, values ?? {});
};