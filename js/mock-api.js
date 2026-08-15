

let _nextId = { eventos: 4, boletas: 7, clientes: 5, ventas: 4 };

const _db = {
  eventos: [
    { id: 1, nombre: 'Noche de Merengue en el Malecón', fecha: '2026-08-22', lugar: 'Malecón Center, Santo Domingo', precio: 1500, cuposDisponibles: 340 },
    { id: 2, nombre: 'Festival de Jazz del Caribe', fecha: '2026-09-05', lugar: 'Teatro Nacional', precio: 2200, cuposDisponibles: 180 },
    { id: 3, nombre: 'Copa ITLA de Baloncesto — Final', fecha: '2026-08-30', lugar: 'Palacio de los Deportes', precio: 800, cuposDisponibles: 620 },
  ],
  clientes: [
    { id: 1, nombre: 'Julier', apellido: 'Pérez', email: 'julier.perez@correo.com', telefono: '809-555-0142' },
    { id: 2, nombre: 'Ana', apellido: 'Reyes', email: 'ana.reyes@correo.com', telefono: '829-555-0198' },
    { id: 3, nombre: 'Carlos', apellido: 'Méndez', email: 'carlos.mendez@correo.com', telefono: '849-555-0271' },
    { id: 4, nombre: 'Yolanda', apellido: 'Cabrera', email: 'yolanda.cabrera@correo.com', telefono: '809-555-0330' },
  ],
  boletas: [
    { id: 1, codigo: 'BLT-0001', estado: 'Vendida', eventoId: 1 },
    { id: 2, codigo: 'BLT-0002', estado: 'Vendida', eventoId: 1 },
    { id: 3, codigo: 'BLT-0003', estado: 'Disponible', eventoId: 1 },
    { id: 4, codigo: 'BLT-0004', estado: 'Vendida', eventoId: 2 },
    { id: 5, codigo: 'BLT-0005', estado: 'Disponible', eventoId: 2 },
    { id: 6, codigo: 'BLT-0006', estado: 'Cancelada', eventoId: 3 },
  ],
  ventas: [
    { id: 1, fechaVenta: '2026-08-08T14:30:00', cantidadBoletas: 2, total: 3000, eventoId: 1, clienteId: 1 },
    { id: 2, fechaVenta: '2026-08-08T16:05:00', cantidadBoletas: 1, total: 2200, eventoId: 2, clienteId: 2 },
    { id: 3, fechaVenta: '2026-08-09T09:12:00', cantidadBoletas: 3, total: 2400, eventoId: 3, clienteId: 3 },
  ],
};

function delay(ms) { return new Promise((res) => setTimeout(res, ms)); }

function buildMockResource(name) {
  return {
    getAll: async () => { await delay(150); return _db[name].map((x) => ({ ...x })); },
    getById: async (id) => { await delay(80); return _db[name].find((x) => x.id === Number(id)); },
    create: async (dto) => {
      await delay(200);
      const saved = { ...dto, id: _nextId[name]++ };
      _db[name].push(saved);
      return saved;
    },
    update: async (id, dto) => {
      await delay(200);
      const idx = _db[name].findIndex((x) => x.id === Number(id));
      const saved = { ...dto, id: Number(id) };
      if (idx >= 0) _db[name][idx] = saved;
      return saved;
    },
    remove: async (id) => {
      await delay(150);
      _db[name] = _db[name].filter((x) => x.id !== Number(id));
      return null;
    },
  };
}

const api = {
  eventos: buildMockResource('eventos'),
  boletas: buildMockResource('boletas'),
  clientes: buildMockResource('clientes'),
  ventas: buildMockResource('ventas'),
  baseUrl: '(vista previa — datos de ejemplo, sin backend real)',
};
