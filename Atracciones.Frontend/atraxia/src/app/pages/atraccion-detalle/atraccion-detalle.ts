import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AtraccionesService, AtraccionDetalle, ResenaItem } from '../../services/atracciones.service';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-atraccion-detalle',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './atraccion-detalle.html',
  styleUrls: ['./atraccion-detalle.scss']
})
export class AtraccionDetalleComponent implements OnInit {
  atraccion: AtraccionDetalle | null = null;
  loading = false;
  error = '';
  selectedImg = 0;

  at_guid: string = '';
  horarioSeleccionado: any = null;
  cantidades: { [tck_guid: string]: number } = {};
  reserving = false;
  modalMsg = '';
  modalSuccess = false;
  showModal = false;

  showPaymentForm = false;
  reservaPendienteGuid = '';
  pagoMock = {
    metodoPago: 'Tarjeta de Crédito',
    numeroTarjeta: '',
    cvv: '',
    fechaExpiracion: ''
  };
  confirmingPayment = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private svc: AtraccionesService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id')!;
    this.at_guid = id;
    this.cargar(id);
  }

  async cargar(id: string) {
    this.loading = true;
    this.error = '';
    try {
      this.atraccion = await this.svc.getAtraccion(id);
      const horarios = await this.svc.getHorariosByAtraccion(id);
      if (this.atraccion) {
        this.atraccion.horarios_proximos = horarios || [];
      }
    } catch {
      this.error = 'No se pudo cargar los detalles de esta atracción.';
    } finally {
      this.loading = false;
      this.cdr.detectChanges();
    }
  }


  async seleccionarHorario(h: any) {
    this.horarioSeleccionado = h;
    this.cantidades = {};
    if (this.atraccion) {
      try {
        const tickets = await this.svc.getTicketsPorHorario(this.atraccion.id, h.hor_guid);
        this.atraccion.tickets = tickets || [];
        this.atraccion.tickets.forEach((t: any) => this.cantidades[t.tck_guid] = 0);
      } catch (e) {
        console.error('Error fetching tickets', e);
      }
    }
    this.cdr.detectChanges();
  }

  getTicketsPorHorario() {
    if (!this.atraccion || !this.horarioSeleccionado) return [];
    return this.atraccion.tickets || [];
  }

  incrementar(tck_guid: string) {
    this.cantidades[tck_guid] = (this.cantidades[tck_guid] || 0) + 1;
  }

  decrementar(tck_guid: string) {
    if (this.cantidades[tck_guid] > 0) {
      this.cantidades[tck_guid]--;
    }
  }

  get totalTickets() {
    return Object.values(this.cantidades).reduce((a, b) => a + b, 0);
  }

  async reservar() {
    if (!this.horarioSeleccionado || this.totalTickets === 0) return;
    const token = localStorage.getItem('atraxia_token');
    if (!token) {
      this.router.navigate(['/ingresar']);
      return;
    }

    this.reserving = true;
    const lineas = Object.entries(this.cantidades)
      .filter(([_, qty]) => qty > 0)
      .map(([tck_guid, cantidad]) => ({ tck_guid, cantidad }));

    const payload = {
      at_guid: this.at_guid,
      hor_guid: this.horarioSeleccionado.hor_guid,
      origen_canal: 'ATRAXIA',
      lineas
    };

    try {
      const res = await this.svc.reservar(payload);
      console.log(res);
      this.modalSuccess = res.success !== false;
      this.modalMsg = res.message || 'Reserva procesada exitosamente';

      if (res.data?.rev_estado === 'PEN' || res.data?.rev_estado === 'Pendiente') {
        this.reservaPendienteGuid = res.data.rev_guid;
        this.showPaymentForm = true;
        this.showModal = true;
        this.reserving = false;
        this.cdr.detectChanges();
        return;
      }

    } catch (e: any) {
      this.modalSuccess = false;
      this.modalMsg = e.response?.data?.message || e.response?.Error || 'Error al procesar la reserva';
    } finally {
      this.reserving = false;
      this.showModal = true;
      this.cdr.detectChanges();
    }

    this.cargar(this.atraccion?.id || '');
  }

  cerrarModal() {
    this.showModal = false;
    this.showPaymentForm = false;
    if (this.modalSuccess) {
      this.horarioSeleccionado = null;
      this.cantidades = {};
    }
  }

  async confirmarPago() {
    if (!this.reservaPendienteGuid) return;
    this.confirmingPayment = true;
    try {
      const payload = {
        metodoPago: this.pagoMock.metodoPago,
        comprobante: 'TRX-' + Math.floor(Math.random() * 1000000)
      };
      const res = await this.svc.confirmarPago(this.reservaPendienteGuid, payload);
      this.modalSuccess = res.success !== false;
      this.modalMsg = 'Pago confirmado y factura generada con éxito.';
      this.showPaymentForm = false;
    } catch (e: any) {
      this.modalSuccess = false;
      this.modalMsg = e.response?.data?.message || 'Error al confirmar el pago';
    } finally {
      this.confirmingPayment = false;
      this.cdr.detectChanges();
      this.cargar(this.atraccion?.id || '');
    }
  }
}