const API_VERSION: string = 'v1';

export function apiEndpoints(apiUrl: string) {
  return {
    // Authentication
    login: `${apiUrl}/Authentication/login`,
    refresh: `${apiUrl}/Authentication/refresh`,

    // Users
    getMe: `${apiUrl}/${API_VERSION}/Users/me`,
  }
};