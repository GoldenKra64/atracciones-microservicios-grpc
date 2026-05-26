import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { AtraccionesService, AtraccionItem } from '../../services/atracciones.service';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-atracciones',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './atracciones.html',
  styleUrls: ['./atracciones.scss']
})
export class AtraccionesComponent implements OnInit {
  items: AtraccionItem[] = [];
  loading = false;
  error = '';
  totalRecords = 0;
  totalPages = 0;
  hasPrev = false;
  hasNext = false;

  filtros = { Ciudad: '', Pais: '', Disponible: '', page: 1, limit: 9 };

  constructor(private svc: AtraccionesService, private cdr: ChangeDetectorRef) { }

  ngOnInit() { this.cargar(); }

  async cargar() {
    this.loading = true;
    this.error = '';
    try {
      const params: any = { ...this.filtros };
      if (params.Disponible === '') delete params.Disponible;
      else params.Disponible = params.Disponible === 'true';
      const res = await this.svc.getAtracciones(params);
      this.items = (res.data || []).map((item: any) => ({
        ...item,
        moneda: item.moneda || 'USD',
        total_resenias: item.total_resenas || 0,
        disponible: item.disponibilidad?.disponible || false,
        disponible_hoy: item.disponibilidad?.disponible_hoy || false,
        proxima_fecha_disponible: item.disponibilidad?.proxima_fecha_disponible || '',
        cupos_disponibles: item.disponibilidad?.cupos_disponibles || 0
      }));
      this.totalRecords = res.pagination?.total || 0;
      this.totalPages = res.pagination?.total_pages || 0;
      this.hasPrev = res.pagination ? res.pagination.page > 1 : false;
      this.hasNext = res.pagination ? res.pagination.page < res.pagination.total_pages : false;
    } catch (e: any) {
      this.error = 'No se pudo cargar las atracciones. Verifica la conexión con el servidor.';
    } finally {
      this.loading = false;
      this.cdr.detectChanges();
    }
  }

  buscar() { this.filtros.page = 1; this.cargar(); }
  limpiar() { this.filtros = { Ciudad: '', Pais: '', Disponible: '', page: 1, limit: 9 }; this.cargar(); }
  prevPage() { if (this.hasPrev) { this.filtros.page--; this.cargar(); } }
  nextPage() { if (this.hasNext) { this.filtros.page++; this.cargar(); } }

  estrellas(n: number): string[] { return Array(5).fill('').map((_, i) => i < n ? '★' : '☆'); }
}