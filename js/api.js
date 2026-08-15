

const API_BASE_URL = window.TICKETPRO_API_URL || 'https://localhost:51653/api';

class ApiError extends Error {
  constructor(message, status, details) {
    super(message);
    this.status = status;
    this.details = details;
  }
}

async function request(path, options = {}) {
  let response;
  try {
    response = await fetch(`${API_BASE_URL}${path}`, {
      headers: { 'Content-Type': 'application/json' },
      ...options,
    });
  } catch (networkErr) {
    throw new ApiError(
      'No se pudo contactar la API. Verifica que el backend esté corriendo y que CORS esté habilitado.',
      0,
      networkErr
    );
  }

  if (response.status === 204) return null;

  let body = null;
  const text = await response.text();
  if (text) {
    try { body = JSON.parse(text); } catch { body = text; }
  }

  if (!response.ok) {
    const message = Array.isArray(body?.errors) ? body.errors.join(' · ')
      : Array.isArray(body) ? body.join(' · ')
      : body?.title || body?.message || `Error ${response.status}`;
    throw new ApiError(message, response.status, body);
  }

  return body;
}

function buildResource(name) {
  return {
    getAll: () => request(`/${name}`),
    getById: (id) => request(`/${name}/${id}`),
    create: (dto) => request(`/${name}`, { method: 'POST', body: JSON.stringify(dto) }),
    update: (id, dto) => request(`/${name}/${id}`, { method: 'PUT', body: JSON.stringify(dto) }),
    remove: (id) => request(`/${name}/${id}`, { method: 'DELETE' }),
  };
}

const api = {
  eventos: buildResource('Eventos'),
  boletas: buildResource('Boletas'),
  clientes: buildResource('Clientes'),
  ventas: buildResource('Ventas'),
  baseUrl: API_BASE_URL,
};
