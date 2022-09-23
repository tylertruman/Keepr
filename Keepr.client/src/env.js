export const dev = window.location.origin.includes('localhost')
export const baseURL = dev ? 'https://localhost:5001' : ''
export const useSockets = false
export const domain = 'tylertruman.us.auth0.com'
export const clientId = 'aYponvB5txjq1f3GfcMZUCkAzGnHNdDP'
export const audience = 'https://myapi.com'