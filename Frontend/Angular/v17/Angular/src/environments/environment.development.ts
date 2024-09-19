import { apiEndpoints } from './api_endpoints';

const API_URL: string = '';
const APIKEYNAME: string = 'X-Api-Key';
const APIKEY: string = '';

export const environment = {
  apiUrl: API_URL,
  apiKeyName: APIKEYNAME,
  apiKey: APIKEY,
  apiEndpoints: apiEndpoints(API_URL)
};
