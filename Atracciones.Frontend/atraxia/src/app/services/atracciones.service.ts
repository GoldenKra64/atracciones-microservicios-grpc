import { Injectable } from '@angular/core';
import api from './api.service';
import axios from 'axios';
import { environment } from '../../environments/environment';

export interface ResenaItem {
  clienteId: number;
  atraccionId: number;
  calificacion: number;
  comentario: string;
  fecha: string;
}

export interface AtraccionItem {
  id: string; nombre: string; ciudad: string; pais: string;
  tipo_nombre: string; descripcion_corta: string; precio_desde: number;
  moneda: string; calificacion: number; total_resenias: number;
  duracion_minutos: number; imagen_principal: string; etiquetas: string[];
  disponible: boolean; disponible_hoy: boolean;
  proxima_fecha_disponible: string; cupos_disponibles: number;
  idiomas_disponibles?: string[];
}

export interface AtraccionDetalle {
  id: string; nombre: string; descripcion: string; imagenes: string[];
  incluye: string[]; no_incluye: string[]; punto_encuentro: string | null;
  incluye_transporte: boolean; incluye_acompaniante: boolean;
  tickets: { horId: number; tck_guid: string; tipo: string; precio: number; moneda: string }[];
  horarios_proximos: { horarioId: number; hor_guid: string | null; atraccionId: number; fecha: string; hora_inicio: string; hora_fin: string; cupos: number }[];
}

export interface AtraccionPayload {
  destinoId: number;
  nombre: string;
  descripcion: string;
  direccion: string;
  duracionMinutos: number;
  puntoEncuentro: string;
  moneda: string;
  precioReferencia: number;
  incluyeTransporte: boolean;
  incluyeAcompaniante: boolean;
  categoriaIds: number[];
  idiomaIds: number[];
  incluyeIds: number[];
  noIncluyeIds: number[];
  tagIds: number[];
}

export interface ApiResponse<T> {
  success: boolean;
  message: string;
  status: number;
  data: T;
}

function adminApi() {
  const token = localStorage.getItem('atraxia_admin_token');
  return axios.create({
    baseURL: environment.apiUrl,
    headers: {
      'Content-Type': 'application/json',
      ...(token ? { Authorization: `Bearer ${token}` } : {})
    }
  });
}

@Injectable({ providedIn: 'root' })
export class AtraccionesService {
  async getAtracciones(filtros: Record<string, any> = {}) {
    const params: any = {};
    Object.entries(filtros).forEach(([k, v]) => { if (v !== '' && v !== undefined) params[k] = v; });
    const res = await api.get('/atracciones', { params });
    return res.data;
  }

  async getAtraccion(id: string): Promise<AtraccionDetalle> {
    const res = await api.get(`/atracciones/${id}`);
    return res.data.data;
  }

  async reservar(payload: any) {
    const res = await api.post('/reservas', payload);
    return res.data;
  }

  async confirmarPago(guid: string, payload: any = {}) {
    const res = await api.post(`/reservas/${guid}/pagos/confirmacion`, payload);
    return res.data;
  }

  async getResenas(id: string): Promise<ResenaItem[]> {
    const res = await api.get(`/atracciones/${id}/resenias`);
    return res.data.data;
  }

  async getHorariosByAtraccion(guid: string): Promise<any[]> {
    const res = await api.get(`/atracciones/${guid}/horarios`);
    return res.data.data;
  }

  async getTicketsPorHorario(guid: string, horarioGuid: string): Promise<any[]> {
    const res = await api.get(`/atracciones/${guid}/horarios/${horarioGuid}/tickets`);
    return res.data.data.items;
  }

  // Admin Methods
  async getInternalById(id: string): Promise<any> {
    const res = await adminApi().get<ApiResponse<any>>(`/atracciones/internal/${id}`);
    return res.data.data;
  }

  async create(payload: AtraccionPayload): Promise<ApiResponse<any>> {
    const res = await adminApi().post<ApiResponse<any>>('/atracciones', payload);
    return res.data;
  }

  async update(id: string, payload: AtraccionPayload): Promise<ApiResponse<any>> {
    const res = await adminApi().put<ApiResponse<any>>(`/atracciones/${id}`, payload);
    return res.data;
  }

  async delete(id: string): Promise<ApiResponse<any>> {
    const res = await adminApi().delete<ApiResponse<any>>(`/atracciones/${id}`);
    return res.data;
  }
}