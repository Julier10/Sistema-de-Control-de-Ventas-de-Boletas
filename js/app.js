

const ICONS = {
  grid: '<svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.8"><rect x="3" y="3" width="7" height="7" rx="1.5"/><rect x="14" y="3" width="7" height="7" rx="1.5"/><rect x="3" y="14" width="7" height="7" rx="1.5"/><rect x="14" y="14" width="7" height="7" rx="1.5"/></svg>',
  calendar: '<svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.8"><rect x="3" y="5" width="18" height="16" rx="2"/><path d="M8 3v4M16 3v4M3 10h18"/></svg>',
  ticket: '<svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.8"><path d="M3 9a2 2 0 0 1 2-2h14a2 2 0 0 1 2 2v1a2 2 0 0 0 0 4v1a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-1a2 2 0 0 0 0-4V9Z"/><path d="M13 6v2M13 16v2M13 11v2"/></svg>',
  users: '<svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.8"><circle cx="9" cy="8" r="3.2"/><path d="M2.5 20c.7-3.4 3.3-5.5 6.5-5.5s5.8 2.1 6.5 5.5"/><circle cx="17.5" cy="8.5" r="2.6"/><path d="M16 14.8c2.6.3 4.6 2.2 5.1 4.7"/></svg>',
  receipt: '<svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.8"><path d="M6 3h12v18l-2.5-1.5L13 21l-2.5-1.5L8 21l-2-1.5V3Z"/><path d="M9 8h6M9 12h6M9 16h3"/></svg>',
  plus: '<svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.2"><path d="M12 5v14M5 12h14"/></svg>',
  search: '<svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.8"><circle cx="11" cy="11" r="7"/><path d="m21 21-4.3-4.3"/></svg>',
  edit: '<svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.8"><path d="M12 20h9"/><path d="M16.5 3.5a2.1 2.1 0 0 1 3 3L7 19l-4 1 1-4Z"/></svg>',
  trash: '<svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.8"><path d="M4 7h16M9 7V4h6v3M6 7l1 13h10l1-13"/></svg>',
  close: '<svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M6 6l12 12M18 6 6 18"/></svg>',
  alert: '<svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.8"><path d="M12 9v4M12 17h.01"/><path d="M10.3 3.9 1.9 18a2 2 0 0 0 1.7 3h16.8a2 2 0 0 0 1.7-3L14.7 3.9a2 2 0 0 0-3.4 0Z"/></svg>',
  spark: '<svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.8"><path d="M12 3v4M12 17v4M3 12h4M17 12h4M5.6 5.6l2.8 2.8M15.6 15.6l2.8 2.8M18.4 5.6l-2.8 2.8M8.4 15.6l-2.8 2.8"/></svg>',
  logo: '<svg viewBox="0 0 24 24" fill="none"><path d="M4 8a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v1.2a1.8 1.8 0 0 0 0 3.6V14a2 2 0 0 1-2 2H6a2 2 0 0 1-2-2v-1.2a1.8 1.8 0 0 0 0-3.6V8Z" fill="#1B1404"/><path d="M14 6.5v11" stroke="#F4B740" stroke-width="1.6" stroke-dasharray="1.6 2"/></svg>',
};

const state = {
  eventos: [],
  boletas: [],
  clientes: [],
  ventas: [],
  ready: false,
  offline: false,
};

const ESTADOS_BOLETA = ['Disponible', 'Vendida', 'Cancelada'];

const VIEWS = [
  { id: 'dashboard', label: 'Panel', icon: 'grid' },
  { id: 'eventos', label: 'Eventos', icon: 'calendar' },
  { id: 'boletas', label: 'Boletas', icon: 'ticket' },
  { id: 'clientes', label: 'Clientes', icon: 'users' },
  { id: 'ventas', label: 'Ventas', icon: 'receipt' },
];

let currentView = 'dashboard';
let searchTerm = '';

// ---------- helpers ----------
const money = (n) => new Intl.NumberFormat('es-DO', { style: 'currency', currency: 'DOP' }).format(Number(n) || 0);
const fmtDate = (d) => { try { return new Date(d).toLocaleDateString('es-DO', { day: '2-digit', month: 'short', year: 'numeric' }); } catch { return d; } };
const fmtDateTime = (d) => { try { return new Date(d).toLocaleString('es-DO', { day: '2-digit', month: 'short', year: 'numeric', hour: '2-digit', minute: '2-digit' }); } catch { return d; } };
const toInputDate = (d) => { try { return new Date(d).toISOString().slice(0, 10); } catch { return ''; } };
const toInputDateTime = (d) => { try { return new Date(d).toISOString().slice(0, 16); } catch { return ''; } };
const escapeHtml = (s) => String(s ?? '').replace(/[&<>"']/g, (c) => ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;' }[c]));

function eventoById(id) { return state.eventos.find((e) => e.id === Number(id)); }
function clienteById(id) { return state.clientes.find((c) => c.id === Number(id)); }

function badgeForEstado(estado) {
  const key = (estado || '').toLowerCase();
  const cls = key === 'disponible' ? 'badge-disponible'
    : key === 'vendida' ? 'badge-vendida'
    : key === 'cancelada' ? 'badge-cancelada'
    : 'badge-default';
  return `<span class="badge ${cls}">${escapeHtml(estado || 'Sin estado')}</span>`;
}

// ---------- toasts ----------
function showToast(message, type = 'ok') {
  const stack = document.getElementById('toast-stack');
  const el = document.createElement('div');
  el.className = `toast ${type === 'error' ? 'error' : ''}`;
  el.innerHTML = `<span class="toast-dot"></span><span>${escapeHtml(message)}</span>`;
  stack.appendChild(el);
  setTimeout(() => { el.style.opacity = '0'; el.style.transition = 'opacity .25s'; setTimeout(() => el.remove(), 250); }, 3200);
}

// ---------- modal ----------
function openModal(innerHtml, { size = 'default' } = {}) {
  closeModal();
  const overlay = document.createElement('div');
  overlay.className = 'modal-overlay';
  overlay.id = 'active-modal-overlay';
  overlay.innerHTML = `<div class="modal ${size === 'confirm' ? 'confirm-box' : ''}">${innerHtml}</div>`;
  overlay.addEventListener('mousedown', (e) => { if (e.target === overlay) closeModal(); });
  document.getElementById('modal-root').appendChild(overlay);
  document.addEventListener('keydown', escToClose);
  return overlay;
}
function escToClose(e) { if (e.key === 'Escape') closeModal(); }
function closeModal() {
  const existing = document.getElementById('active-modal-overlay');
  if (existing) existing.remove();
  document.removeEventListener('keydown', escToClose);
}

function confirmAction(title, message, onConfirm) {
  const overlay = openModal(`
    <div class="modal-head">
      <h2>${escapeHtml(title)}</h2>
      <button class="modal-close" data-close>${ICONS.close}</button>
    </div>
    <div class="modal-body"><p>${escapeHtml(message)}</p></div>
    <div class="modal-foot">
      <button class="btn btn-ghost" data-close>Cancelar</button>
      <button class="btn btn-danger" id="confirm-yes">${ICONS.trash} Eliminar</button>
    </div>
  `, { size: 'confirm' });
  overlay.querySelectorAll('[data-close]').forEach((b) => b.addEventListener('click', closeModal));
  overlay.querySelector('#confirm-yes').addEventListener('click', async () => {
    const btn = overlay.querySelector('#confirm-yes');
    btn.disabled = true;
    btn.textContent = 'Eliminando…';
    try {
      await onConfirm();
      closeModal();
    } catch (err) {
      showToast(err.message || 'No se pudo eliminar', 'error');
      btn.disabled = false;
      btn.innerHTML = `${ICONS.trash} Eliminar`;
    }
  });
}

// ---------- data loading ----------
async function loadAll({ silent = false } = {}) {
  try {
    const [eventos, boletas, clientes, ventas] = await Promise.all([
      api.eventos.getAll(),
      api.boletas.getAll(),
      api.clientes.getAll(),
      api.ventas.getAll(),
    ]);
    state.eventos = eventos || [];
    state.boletas = boletas || [];
    state.clientes = clientes || [];
    state.ventas = ventas || [];
    state.offline = false;
  } catch (err) {
    state.offline = true;
    if (!silent) showToast(err.message, 'error');
  }
  state.ready = true;
  updateConnBanner();
  renderNav();
  renderRoute();
}

function updateConnBanner() {
  const banner = document.getElementById('conn-banner');
  banner.classList.toggle('show', state.offline);
  if (state.offline) {
    banner.innerHTML = `${ICONS.alert}<span>No hay conexión con la API en <code>${escapeHtml(api.baseUrl)}</code>. Verifica que el backend esté corriendo y que tenga CORS habilitado para este origen.</span>`;
  }
}

// ---------- nav / routing ----------
function renderNav() {
  const nav = document.getElementById('sidebar-nav');
  const counts = {
    dashboard: null,
    eventos: state.eventos.length,
    boletas: state.boletas.length,
    clientes: state.clientes.length,
    ventas: state.ventas.length,
  };
  nav.innerHTML = VIEWS.map((v) => `
    <button class="nav-item ${currentView === v.id ? 'active' : ''}" data-view="${v.id}">
      ${ICONS[v.icon]}
      <span>${v.label}</span>
      ${counts[v.id] !== null ? `<span class="count-pill">${counts[v.id]}</span>` : ''}
    </button>
  `).join('');
  nav.querySelectorAll('[data-view]').forEach((btn) => {
    btn.addEventListener('click', () => { window.location.hash = `#/${btn.dataset.view}`; });
  });
}

function renderRoute() {
  const hash = window.location.hash.replace('#/', '') || 'dashboard';
  currentView = VIEWS.some((v) => v.id === hash) ? hash : 'dashboard';
  searchTerm = '';
  renderNav();
  const renderers = {
    dashboard: renderDashboard,
    eventos: renderEventos,
    boletas: renderBoletas,
    clientes: renderClientes,
    ventas: renderVentas,
  };
  renderers[currentView]();
}

window.addEventListener('hashchange', renderRoute);

// ---------- topbar helper ----------
function setTopbar({ eyebrow, title, sub, actionsHtml = '' }) {
  document.getElementById('topbar-eyebrow').textContent = eyebrow;
  document.getElementById('topbar-title').textContent = title;
  document.getElementById('topbar-sub').textContent = sub;
  document.getElementById('topbar-actions').innerHTML = actionsHtml;
}

function contentHtml(html) {
  document.getElementById('content').innerHTML = html;
}

// ============================================================
// DASHBOARD
// ============================================================
function renderDashboard() {
  setTopbar({
    eyebrow: 'Resumen del día',
    title: 'Panel de taquilla',
    sub: 'Estado general de eventos, boletas, clientes y ventas.',
  });

  const totalVendido = state.ventas.reduce((sum, v) => sum + Number(v.total || 0), 0);
  const boletasVendidas = state.boletas.filter((b) => (b.estado || '').toLowerCase() === 'vendida').length;
  const cuposTotales = state.eventos.reduce((sum, e) => sum + Number(e.cuposDisponibles || 0), 0);
  const proximosEventos = [...state.eventos]
    .filter((e) => new Date(e.fecha) >= new Date(new Date().toDateString()))
    .sort((a, b) => new Date(a.fecha) - new Date(b.fecha))
    .slice(0, 5);
  const ultimasVentas = [...state.ventas]
    .sort((a, b) => new Date(b.fechaVenta) - new Date(a.fechaVenta))
    .slice(0, 5);

  contentHtml(`
    <div id="conn-banner-slot"></div>
    <div class="stat-grid">
      <div class="stat-card">
        <div class="stat-label">Eventos activos</div>
        <div class="stat-value">${state.eventos.length}</div>
        <div class="stat-foot">${cuposTotales} cupos disponibles en total</div>
      </div>
      <div class="stat-card">
        <div class="stat-label">Boletas registradas</div>
        <div class="stat-value">${state.boletas.length}</div>
        <div class="stat-foot">${boletasVendidas} marcadas como vendidas</div>
      </div>
      <div class="stat-card">
        <div class="stat-label">Clientes</div>
        <div class="stat-value">${state.clientes.length}</div>
        <div class="stat-foot">en la base de datos</div>
      </div>
      <div class="stat-card">
        <div class="stat-label">Total vendido</div>
        <div class="stat-value accent">${money(totalVendido)}</div>
        <div class="stat-foot">${state.ventas.length} ventas registradas</div>
      </div>
    </div>

    <div class="dash-columns">
      <div class="panel">
        <h3>Próximos eventos</h3>
        <div class="panel-sub">Ordenados por fecha más cercana</div>
        ${proximosEventos.length ? proximosEventos.map((e) => `
          <div class="mini-row">
            <div>
              <div class="mini-name">${escapeHtml(e.nombre)}</div>
              <div class="mini-meta">${escapeHtml(e.lugar)} · ${fmtDate(e.fecha)}</div>
            </div>
            <div class="mini-amount">${money(e.precio)}</div>
          </div>
        `).join('') : `<div class="empty-mini">No hay eventos próximos registrados.</div>`}
      </div>
      <div class="panel">
        <h3>Últimas ventas</h3>
        <div class="panel-sub">Las 5 más recientes</div>
        ${ultimasVentas.length ? ultimasVentas.map((v) => `
          <div class="mini-row">
            <div>
              <div class="mini-name">${escapeHtml(clienteById(v.clienteId)?.nombre || 'Cliente')} ${escapeHtml(clienteById(v.clienteId)?.apellido || '')}</div>
              <div class="mini-meta">${escapeHtml(eventoById(v.eventoId)?.nombre || 'Evento')} · ${v.cantidadBoletas} boleta(s)</div>
            </div>
            <div class="mini-amount">${money(v.total)}</div>
          </div>
        `).join('') : `<div class="empty-mini">Todavía no se han registrado ventas.</div>`}
      </div>
    </div>
  `);

  if (state.offline) document.getElementById('conn-banner-slot').innerHTML = document.getElementById('conn-banner').outerHTML;
}

// ============================================================
// EVENTOS
// ============================================================
function renderEventos() {
  setTopbar({
    eyebrow: `${state.eventos.length} en cartelera`,
    title: 'Eventos',
    sub: 'Conciertos, funciones y presentaciones disponibles para la venta.',
    actionsHtml: `<button class="btn btn-primary" id="btn-new-evento">${ICONS.plus} Nuevo evento</button>`,
  });

  const rows = state.eventos
    .filter((e) => !searchTerm || `${e.nombre} ${e.lugar}`.toLowerCase().includes(searchTerm))
    .sort((a, b) => new Date(a.fecha) - new Date(b.fecha));

  contentHtml(`
    ${toolbarHtml('Buscar por nombre o lugar…')}
    <div class="table-wrap">
      ${rows.length ? `
      <table>
        <thead><tr>
          <th>Evento</th><th>Fecha</th><th>Lugar</th><th>Precio</th><th>Cupos</th><th></th>
        </tr></thead>
        <tbody>
          ${rows.map((e) => `
            <tr>
              <td class="cell-strong">${escapeHtml(e.nombre)}</td>
              <td>${fmtDate(e.fecha)}</td>
              <td>${escapeHtml(e.lugar)}</td>
              <td class="cell-mono">${money(e.precio)}</td>
              <td class="cell-mono">${e.cuposDisponibles}</td>
              <td>
                <div class="row-actions">
                  <button class="btn btn-ghost btn-icon" data-edit="${e.id}" title="Editar">${ICONS.edit}</button>
                  <button class="btn btn-danger btn-icon" data-del="${e.id}" title="Eliminar">${ICONS.trash}</button>
                </div>
              </td>
            </tr>
          `).join('')}
        </tbody>
      </table>` : emptyState('calendar', 'Sin eventos todavía', 'Crea el primer evento para empezar a vender boletas.')}
    </div>
  `);

  bindToolbar(renderEventos);
  document.getElementById('btn-new-evento').addEventListener('click', () => openEventoForm());
  document.querySelectorAll('[data-edit]').forEach((b) => b.addEventListener('click', () => openEventoForm(eventoById(b.dataset.edit))));
  document.querySelectorAll('[data-del]').forEach((b) => b.addEventListener('click', () => {
    const ev = eventoById(b.dataset.del);
    confirmAction('Eliminar evento', `¿Eliminar "${ev.nombre}"? Esta acción no se puede deshacer.`, async () => {
      await api.eventos.remove(ev.id);
      state.eventos = state.eventos.filter((x) => x.id !== ev.id);
      showToast('Evento eliminado');
      renderNav(); renderEventos();
    });
  }));
}

function openEventoForm(evento) {
  const isEdit = Boolean(evento);
  const overlay = openModal(`
    <div class="modal-head">
      <h2>${isEdit ? 'Editar evento' : 'Nuevo evento'}</h2>
      <button class="modal-close" data-close>${ICONS.close}</button>
    </div>
    <div class="modal-body">
      <div id="form-error"></div>
      <div class="field"><label>Nombre del evento</label>
        <input id="f-nombre" value="${isEdit ? escapeHtml(evento.nombre) : ''}" placeholder="Ej. Noche de Salsa en el Malecón" /></div>
      <div class="field-row">
        <div class="field"><label>Fecha</label>
          <input id="f-fecha" type="date" value="${isEdit ? toInputDate(evento.fecha) : ''}" /></div>
        <div class="field"><label>Lugar</label>
          <input id="f-lugar" value="${isEdit ? escapeHtml(evento.lugar) : ''}" placeholder="Ej. Palacio de los Deportes" /></div>
      </div>
      <div class="field-row">
        <div class="field"><label>Precio de la boleta</label>
          <input id="f-precio" type="number" min="0" step="0.01" value="${isEdit ? evento.precio : ''}" placeholder="0.00" /></div>
        <div class="field"><label>Cupos disponibles</label>
          <input id="f-cupos" type="number" min="0" step="1" value="${isEdit ? evento.cuposDisponibles : ''}" placeholder="0" /></div>
      </div>
    </div>
    <div class="modal-foot">
      <button class="btn btn-ghost" data-close>Cancelar</button>
      <button class="btn btn-primary" id="f-save">${isEdit ? 'Guardar cambios' : 'Crear evento'}</button>
    </div>
  `);
  overlay.querySelectorAll('[data-close]').forEach((b) => b.addEventListener('click', closeModal));
  overlay.querySelector('#f-save').addEventListener('click', async () => {
    const dto = {
      id: isEdit ? evento.id : 0,
      nombre: overlay.querySelector('#f-nombre').value.trim(),
      fecha: overlay.querySelector('#f-fecha').value,
      lugar: overlay.querySelector('#f-lugar').value.trim(),
      precio: parseFloat(overlay.querySelector('#f-precio').value) || 0,
      cuposDisponibles: parseInt(overlay.querySelector('#f-cupos').value, 10) || 0,
    };
    if (!dto.nombre || !dto.fecha || !dto.lugar) {
      overlay.querySelector('#form-error').innerHTML = `<div class="form-error">Completa nombre, fecha y lugar.</div>`;
      return;
    }
    await saveEntity(overlay, isEdit, () => isEdit ? api.eventos.update(evento.id, dto) : api.eventos.create(dto), (saved) => {
      if (isEdit) state.eventos = state.eventos.map((x) => (x.id === evento.id ? saved : x));
      else state.eventos.push(saved);
    }, 'Evento guardado', renderEventos);
  });
}

// ============================================================
// BOLETAS  (ticket-stub cards — elemento distintivo)
// ============================================================
function renderBoletas() {
  setTopbar({
    eyebrow: `${state.boletas.length} en el sistema`,
    title: 'Boletas',
    sub: 'Códigos emitidos por evento y su estado de venta.',
    actionsHtml: `<button class="btn btn-primary" id="btn-new-boleta" ${state.eventos.length ? '' : 'disabled title="Crea un evento primero"'}>${ICONS.plus} Nueva boleta</button>`,
  });

  const rows = state.boletas
    .filter((b) => !searchTerm || `${b.codigo} ${eventoById(b.eventoId)?.nombre || ''}`.toLowerCase().includes(searchTerm))
    .sort((a, b) => b.id - a.id);

  contentHtml(`
    ${toolbarHtml('Buscar por código o evento…')}
    ${rows.length ? `<div class="ticket-grid">
      ${rows.map((b) => {
        const ev = eventoById(b.eventoId);
        return `
        <div class="ticket">
          <div class="ticket-main">
            <div class="ticket-actions">
              <button data-edit="${b.id}" title="Editar">${ICONS.edit}</button>
              <button data-del="${b.id}" title="Eliminar">${ICONS.trash}</button>
            </div>
            <div class="ticket-event">${escapeHtml(ev?.nombre || 'Evento no encontrado')}</div>
            <div class="ticket-meta">${ev ? `${escapeHtml(ev.lugar)} · ${fmtDate(ev.fecha)}` : 'Vincula esta boleta a un evento válido'}</div>
            <div class="ticket-code">${escapeHtml(b.codigo)}</div>
            <div class="ticket-badge-row">${badgeForEstado(b.estado)}<span style="font-family:var(--font-mono);font-size:11px;opacity:.5">#${String(b.id).padStart(4, '0')}</span></div>
          </div>
          <div class="ticket-stub">TICKETPRO</div>
        </div>
      `; }).join('')}
    </div>` : emptyState('ticket', 'No hay boletas registradas', state.eventos.length ? 'Emite la primera boleta para un evento.' : 'Primero crea un evento en la sección Eventos.')}
  `);

  bindToolbar(renderBoletas);
  const newBtn = document.getElementById('btn-new-boleta');
  if (newBtn) newBtn.addEventListener('click', () => openBoletaForm());
  document.querySelectorAll('[data-edit]').forEach((b) => b.addEventListener('click', () => openBoletaForm(state.boletas.find((x) => x.id === Number(b.dataset.edit)))));
  document.querySelectorAll('[data-del]').forEach((b) => b.addEventListener('click', () => {
    const bol = state.boletas.find((x) => x.id === Number(b.dataset.del));
    confirmAction('Eliminar boleta', `¿Eliminar la boleta "${bol.codigo}"?`, async () => {
      await api.boletas.remove(bol.id);
      state.boletas = state.boletas.filter((x) => x.id !== bol.id);
      showToast('Boleta eliminada');
      renderNav(); renderBoletas();
    });
  }));
}

function openBoletaForm(boleta) {
  const isEdit = Boolean(boleta);
  const overlay = openModal(`
    <div class="modal-head">
      <h2>${isEdit ? 'Editar boleta' : 'Nueva boleta'}</h2>
      <button class="modal-close" data-close>${ICONS.close}</button>
    </div>
    <div class="modal-body">
      <div id="form-error"></div>
      <div class="field"><label>Código</label>
        <input id="f-codigo" value="${isEdit ? escapeHtml(boleta.codigo) : ''}" placeholder="Ej. BLT-0001" /></div>
      <div class="field-row">
        <div class="field"><label>Evento</label>
          <select id="f-evento">
            <option value="">Selecciona un evento</option>
            ${state.eventos.map((e) => `<option value="${e.id}" ${isEdit && boleta.eventoId === e.id ? 'selected' : ''}>${escapeHtml(e.nombre)}</option>`).join('')}
          </select></div>
        <div class="field"><label>Estado</label>
          <select id="f-estado">
            ${ESTADOS_BOLETA.map((s) => `<option value="${s}" ${isEdit && boleta.estado === s ? 'selected' : ''}>${s}</option>`).join('')}
          </select></div>
      </div>
    </div>
    <div class="modal-foot">
      <button class="btn btn-ghost" data-close>Cancelar</button>
      <button class="btn btn-primary" id="f-save">${isEdit ? 'Guardar cambios' : 'Emitir boleta'}</button>
    </div>
  `);
  overlay.querySelectorAll('[data-close]').forEach((b) => b.addEventListener('click', closeModal));
  overlay.querySelector('#f-save').addEventListener('click', async () => {
    const dto = {
      id: isEdit ? boleta.id : 0,
      codigo: overlay.querySelector('#f-codigo').value.trim(),
      estado: overlay.querySelector('#f-estado').value,
      eventoId: parseInt(overlay.querySelector('#f-evento').value, 10) || 0,
    };
    if (!dto.codigo || !dto.eventoId) {
      overlay.querySelector('#form-error').innerHTML = `<div class="form-error">Ingresa un código y selecciona un evento.</div>`;
      return;
    }
    await saveEntity(overlay, isEdit, () => isEdit ? api.boletas.update(boleta.id, dto) : api.boletas.create(dto), (saved) => {
      if (isEdit) state.boletas = state.boletas.map((x) => (x.id === boleta.id ? saved : x));
      else state.boletas.push(saved);
    }, 'Boleta guardada', renderBoletas);
  });
}

// ============================================================
// CLIENTES
// ============================================================
function renderClientes() {
  setTopbar({
    eyebrow: `${state.clientes.length} registrados`,
    title: 'Clientes',
    sub: 'Compradores registrados en la taquilla.',
    actionsHtml: `<button class="btn btn-primary" id="btn-new-cliente">${ICONS.plus} Nuevo cliente</button>`,
  });

  const rows = state.clientes
    .filter((c) => !searchTerm || `${c.nombre} ${c.apellido} ${c.email}`.toLowerCase().includes(searchTerm))
    .sort((a, b) => a.nombre.localeCompare(b.nombre));

  contentHtml(`
    ${toolbarHtml('Buscar por nombre o correo…')}
    <div class="table-wrap">
      ${rows.length ? `
      <table>
        <thead><tr><th>Cliente</th><th>Correo</th><th>Teléfono</th><th></th></tr></thead>
        <tbody>
          ${rows.map((c) => `
            <tr>
              <td class="cell-strong">${escapeHtml(c.nombre)} ${escapeHtml(c.apellido)}</td>
              <td>${escapeHtml(c.email)}</td>
              <td class="cell-mono">${escapeHtml(c.telefono)}</td>
              <td>
                <div class="row-actions">
                  <button class="btn btn-ghost btn-icon" data-edit="${c.id}" title="Editar">${ICONS.edit}</button>
                  <button class="btn btn-danger btn-icon" data-del="${c.id}" title="Eliminar">${ICONS.trash}</button>
                </div>
              </td>
            </tr>
          `).join('')}
        </tbody>
      </table>` : emptyState('users', 'Sin clientes todavía', 'Registra tu primer cliente para asociarlo a una venta.')}
    </div>
  `);

  bindToolbar(renderClientes);
  document.getElementById('btn-new-cliente').addEventListener('click', () => openClienteForm());
  document.querySelectorAll('[data-edit]').forEach((b) => b.addEventListener('click', () => openClienteForm(clienteById(b.dataset.edit))));
  document.querySelectorAll('[data-del]').forEach((b) => b.addEventListener('click', () => {
    const c = clienteById(b.dataset.del);
    confirmAction('Eliminar cliente', `¿Eliminar a "${c.nombre} ${c.apellido}"?`, async () => {
      await api.clientes.remove(c.id);
      state.clientes = state.clientes.filter((x) => x.id !== c.id);
      showToast('Cliente eliminado');
      renderNav(); renderClientes();
    });
  }));
}

function openClienteForm(cliente) {
  const isEdit = Boolean(cliente);
  const overlay = openModal(`
    <div class="modal-head">
      <h2>${isEdit ? 'Editar cliente' : 'Nuevo cliente'}</h2>
      <button class="modal-close" data-close>${ICONS.close}</button>
    </div>
    <div class="modal-body">
      <div id="form-error"></div>
      <div class="field-row">
        <div class="field"><label>Nombre</label>
          <input id="f-nombre" value="${isEdit ? escapeHtml(cliente.nombre) : ''}" placeholder="Nombre" /></div>
        <div class="field"><label>Apellido</label>
          <input id="f-apellido" value="${isEdit ? escapeHtml(cliente.apellido) : ''}" placeholder="Apellido" /></div>
      </div>
      <div class="field"><label>Correo electrónico</label>
        <input id="f-email" type="email" value="${isEdit ? escapeHtml(cliente.email) : ''}" placeholder="correo@ejemplo.com" /></div>
      <div class="field"><label>Teléfono</label>
        <input id="f-telefono" value="${isEdit ? escapeHtml(cliente.telefono) : ''}" placeholder="809-000-0000" /></div>
    </div>
    <div class="modal-foot">
      <button class="btn btn-ghost" data-close>Cancelar</button>
      <button class="btn btn-primary" id="f-save">${isEdit ? 'Guardar cambios' : 'Registrar cliente'}</button>
    </div>
  `);
  overlay.querySelectorAll('[data-close]').forEach((b) => b.addEventListener('click', closeModal));
  overlay.querySelector('#f-save').addEventListener('click', async () => {
    const dto = {
      id: isEdit ? cliente.id : 0,
      nombre: overlay.querySelector('#f-nombre').value.trim(),
      apellido: overlay.querySelector('#f-apellido').value.trim(),
      email: overlay.querySelector('#f-email').value.trim(),
      telefono: overlay.querySelector('#f-telefono').value.trim(),
    };
    if (!dto.nombre || !dto.apellido || !dto.email) {
      overlay.querySelector('#form-error').innerHTML = `<div class="form-error">Completa nombre, apellido y correo.</div>`;
      return;
    }
    await saveEntity(overlay, isEdit, () => isEdit ? api.clientes.update(cliente.id, dto) : api.clientes.create(dto), (saved) => {
      if (isEdit) state.clientes = state.clientes.map((x) => (x.id === cliente.id ? saved : x));
      else state.clientes.push(saved);
    }, 'Cliente guardado', renderClientes);
  });
}

// ============================================================
// VENTAS
// ============================================================
function renderVentas() {
  setTopbar({
    eyebrow: `${state.ventas.length} registradas`,
    title: 'Ventas',
    sub: 'Historial de ventas de boletas por cliente y evento.',
    actionsHtml: `<button class="btn btn-primary" id="btn-new-venta" ${state.eventos.length && state.clientes.length ? '' : 'disabled title="Necesitas al menos un evento y un cliente"'}>${ICONS.plus} Registrar venta</button>`,
  });

  const rows = state.ventas
    .filter((v) => !searchTerm || `${clienteById(v.clienteId)?.nombre || ''} ${eventoById(v.eventoId)?.nombre || ''}`.toLowerCase().includes(searchTerm))
    .sort((a, b) => new Date(b.fechaVenta) - new Date(a.fechaVenta));

  contentHtml(`
    ${toolbarHtml('Buscar por cliente o evento…')}
    <div class="table-wrap">
      ${rows.length ? `
      <table>
        <thead><tr><th>Cliente</th><th>Evento</th><th>Fecha</th><th>Cant.</th><th>Total</th><th></th></tr></thead>
        <tbody>
          ${rows.map((v) => {
            const c = clienteById(v.clienteId); const e = eventoById(v.eventoId);
            return `
            <tr>
              <td class="cell-strong">${c ? `${escapeHtml(c.nombre)} ${escapeHtml(c.apellido)}` : `<span class="cell-sub">Cliente #${v.clienteId}</span>`}</td>
              <td>${e ? escapeHtml(e.nombre) : `<span class="cell-sub">Evento #${v.eventoId}</span>`}</td>
              <td>${fmtDateTime(v.fechaVenta)}</td>
              <td class="cell-mono">${v.cantidadBoletas}</td>
              <td class="cell-mono">${money(v.total)}</td>
              <td>
                <div class="row-actions">
                  <button class="btn btn-ghost btn-icon" data-edit="${v.id}" title="Editar">${ICONS.edit}</button>
                  <button class="btn btn-danger btn-icon" data-del="${v.id}" title="Eliminar">${ICONS.trash}</button>
                </div>
              </td>
            </tr>
          `; }).join('')}
        </tbody>
      </table>` : emptyState('receipt', 'Sin ventas registradas', 'Registra la primera venta para comenzar a facturar.')}
    </div>
  `);

  bindToolbar(renderVentas);
  const newBtn = document.getElementById('btn-new-venta');
  if (newBtn) newBtn.addEventListener('click', () => openVentaForm());
  document.querySelectorAll('[data-edit]').forEach((b) => b.addEventListener('click', () => openVentaForm(state.ventas.find((x) => x.id === Number(b.dataset.edit)))));
  document.querySelectorAll('[data-del]').forEach((b) => b.addEventListener('click', () => {
    const v = state.ventas.find((x) => x.id === Number(b.dataset.del));
    confirmAction('Eliminar venta', 'Esta venta se eliminará del historial de forma permanente.', async () => {
      await api.ventas.remove(v.id);
      state.ventas = state.ventas.filter((x) => x.id !== v.id);
      showToast('Venta eliminada');
      renderNav(); renderVentas();
    });
  }));
}

function openVentaForm(venta) {
  const isEdit = Boolean(venta);
  const overlay = openModal(`
    <div class="modal-head">
      <h2>${isEdit ? 'Editar venta' : 'Registrar venta'}</h2>
      <button class="modal-close" data-close>${ICONS.close}</button>
    </div>
    <div class="modal-body">
      <div id="form-error"></div>
      <div class="field"><label>Cliente</label>
        <select id="f-cliente">
          <option value="">Selecciona un cliente</option>
          ${state.clientes.map((c) => `<option value="${c.id}" ${isEdit && venta.clienteId === c.id ? 'selected' : ''}>${escapeHtml(c.nombre)} ${escapeHtml(c.apellido)}</option>`).join('')}
        </select></div>
      <div class="field"><label>Evento</label>
        <select id="f-evento">
          <option value="">Selecciona un evento</option>
          ${state.eventos.map((e) => `<option value="${e.id}" data-precio="${e.precio}" ${isEdit && venta.eventoId === e.id ? 'selected' : ''}>${escapeHtml(e.nombre)} — ${money(e.precio)}</option>`).join('')}
        </select></div>
      <div class="field-row">
        <div class="field"><label>Cantidad de boletas</label>
          <input id="f-cantidad" type="number" min="1" step="1" value="${isEdit ? venta.cantidadBoletas : '1'}" /></div>
        <div class="field"><label>Total</label>
          <input id="f-total" type="number" min="0" step="0.01" value="${isEdit ? venta.total : ''}" placeholder="Se calcula solo" />
          <div class="hint">Se sugiere automáticamente según el precio del evento, puedes ajustarlo.</div></div>
      </div>
      <div class="field"><label>Fecha de venta</label>
        <input id="f-fecha" type="datetime-local" value="${isEdit ? toInputDateTime(venta.fechaVenta) : toInputDateTime(new Date())}" /></div>
    </div>
    <div class="modal-foot">
      <button class="btn btn-ghost" data-close>Cancelar</button>
      <button class="btn btn-primary" id="f-save">${isEdit ? 'Guardar cambios' : 'Registrar venta'}</button>
    </div>
  `);

  const recalc = () => {
    const evSel = overlay.querySelector('#f-evento');
    const precio = parseFloat(evSel.selectedOptions[0]?.dataset.precio || 0);
    const cantidad = parseInt(overlay.querySelector('#f-cantidad').value, 10) || 0;
    const totalField = overlay.querySelector('#f-total');
    if (!totalField.dataset.touched) totalField.value = (precio * cantidad).toFixed(2);
  };
  overlay.querySelector('#f-evento').addEventListener('change', recalc);
  overlay.querySelector('#f-cantidad').addEventListener('input', recalc);
  overlay.querySelector('#f-total').addEventListener('input', (e) => { e.target.dataset.touched = '1'; });
  if (!isEdit) recalc();

  overlay.querySelectorAll('[data-close]').forEach((b) => b.addEventListener('click', closeModal));
  overlay.querySelector('#f-save').addEventListener('click', async () => {
    const dto = {
      id: isEdit ? venta.id : 0,
      clienteId: parseInt(overlay.querySelector('#f-cliente').value, 10) || 0,
      eventoId: parseInt(overlay.querySelector('#f-evento').value, 10) || 0,
      cantidadBoletas: parseInt(overlay.querySelector('#f-cantidad').value, 10) || 0,
      total: parseFloat(overlay.querySelector('#f-total').value) || 0,
      fechaVenta: overlay.querySelector('#f-fecha').value,
    };
    if (!dto.clienteId || !dto.eventoId || !dto.cantidadBoletas || !dto.fechaVenta) {
      overlay.querySelector('#form-error').innerHTML = `<div class="form-error">Completa cliente, evento, cantidad y fecha.</div>`;
      return;
    }
    await saveEntity(overlay, isEdit, () => isEdit ? api.ventas.update(venta.id, dto) : api.ventas.create(dto), (saved) => {
      if (isEdit) state.ventas = state.ventas.map((x) => (x.id === venta.id ? saved : x));
      else state.ventas.push(saved);
    }, 'Venta registrada', renderVentas);
  });
}

// ---------- shared save helper ----------
async function saveEntity(overlay, isEdit, request, applyToState, successMsg, rerender) {
  const btn = overlay.querySelector('#f-save');
  const original = btn.textContent;
  btn.disabled = true;
  btn.textContent = 'Guardando…';
  try {
    const saved = await request();
    applyToState(saved);
    showToast(successMsg);
    closeModal();
    renderNav();
    rerender();
  } catch (err) {
    overlay.querySelector('#form-error').innerHTML = `<div class="form-error">${escapeHtml(err.message)}</div>`;
    btn.disabled = false;
    btn.textContent = original;
  }
}

// ---------- shared toolbar / empty state ----------
function toolbarHtml(placeholder) {
  return `
    <div class="toolbar">
      <div class="search-box">${ICONS.search}<input id="search-input" placeholder="${placeholder}" value="${escapeHtml(searchTerm)}" /></div>
    </div>
  `;
}
function bindToolbar(rerender) {
  const input = document.getElementById('search-input');
  if (!input) return;
  input.addEventListener('input', (e) => { searchTerm = e.target.value.toLowerCase(); rerender(); });
  input.focus();
  input.setSelectionRange(input.value.length, input.value.length);
}
function emptyState(icon, title, sub) {
  return `<div class="empty-state">
    <div class="empty-icon">${ICONS[icon]}</div>
    <p class="empty-title">${title}</p>
    <p>${sub}</p>
  </div>`;
}

// ---------- clock ----------
function tickClock() {
  const el = document.getElementById('clock');
  if (el) el.textContent = new Date().toLocaleString('es-DO', { weekday: 'short', hour: '2-digit', minute: '2-digit' });
}

// ---------- init ----------
document.addEventListener('DOMContentLoaded', () => {
  tickClock();
  setInterval(tickClock, 30000);
  loadAll();
  document.getElementById('btn-refresh').addEventListener('click', () => loadAll());
});
