import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormArray, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ReservaService, ReservaPayload } from '../../../services/reserva.service';
import { ClienteService, ClienteProfile } from '../../../services/cliente.service';
import { HorarioService, AtraccionType } from '../../../services/horario.service';
import { AtraccionesService } from '../../../services/atracciones.service';

@Component({
  selector: 'app-admin-reserva-forms',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './admin-reserva-forms.html',
  styleUrls: ['./admin-reserva-forms.scss']
})
export class AdminReservaFormsComponent implements OnInit {
  form!: FormGroup;
  isEditing = false;
  saving = false;
  currentGuid: string | null = null;
  estadoReserva: string = 'PEN';

  clientes: ClienteProfile[] = [];
  atracciones: AtraccionType[] = [];
  horarios: any[] = [];
  tickets: any[] = [];

  loadingHorarios = false;
  loadingTickets = false;

  toast: { visible: boolean; message: string; type: 'success' | 'error' } = {
    visible: false,
    message: '',
    type: 'success'
  };

  constructor(
    private fb: FormBuilder,
    private reservaSvc: ReservaService,
    private clienteSvc: ClienteService,
    private horarioSvc: HorarioService,
    private atraccionesSvc: AtraccionesService,
    private route: ActivatedRoute,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {
    this.crearFormulario();
  }

  ngOnInit() {
    this.cargarDatosDesplegables();

    this.route.paramMap.subscribe(params => {
      const guid = params.get('guid');
      if (guid) {
        this.isEditing = true;
        this.currentGuid = guid;
        this.cargarReserva(guid);
      }
    });
  }

  crearFormulario() {
    this.form = this.fb.group({
      clienteId: ['', Validators.required],
      at_guid: ['', Validators.required],
      hor_guid: ['', Validators.required],
      origen_canal: ['Web', Validators.required],
      lineas: this.fb.array([this.crearLineaForm()])
    });
  }

  crearLineaForm(): FormGroup {
    return this.fb.group({
      tck_guid: ['', Validators.required],
      cantidad: [1, [Validators.required, Validators.min(1)]]
    });
  }

  get lineas(): FormArray {
    return this.form.get('lineas') as FormArray;
  }

  agregarLinea() {
    this.lineas.push(this.crearLineaForm());
  }

  removerLinea(index: number) {
    if (this.lineas.length > 1) {
      this.lineas.removeAt(index);
    }
  }

  // ─── Cascada: Atracción → Horarios ────────────────────────────────────────

  async onAtraccionChange(atGuid: string) {
    // Limpiar horario, tickets y líneas al cambiar atracción
    this.form.get('hor_guid')?.setValue('');
    this.horarios = [];
    this.tickets = [];
    this.limpiarLineas();

    if (!atGuid) return;

    this.loadingHorarios = true;
    this.cdr.detectChanges();
    try {
      this.horarios = await this.atraccionesSvc.getHorariosByAtraccion(atGuid);
    } catch {
      this.showToast('Error al cargar horarios de la atracción', 'error');
    } finally {
      this.loadingHorarios = false;
      this.cdr.detectChanges();
    }
  }

  // ─── Cascada: Horario → Tickets ───────────────────────────────────────────

  async onHorarioChange(horGuid: string) {
    // Limpiar tickets y líneas al cambiar horario
    this.tickets = [];
    this.limpiarLineas();

    if (!horGuid) return;

    const atGuid = this.form.get('at_guid')?.value;
    if (!atGuid) return;

    this.loadingTickets = true;
    this.cdr.detectChanges();
    try {
      this.tickets = await this.atraccionesSvc.getTicketsPorHorario(atGuid, horGuid);
    } catch {
      this.showToast('Error al cargar tickets del horario', 'error');
    } finally {
      this.loadingTickets = false;
      this.cdr.detectChanges();
    }
  }

  private limpiarLineas() {
    while (this.lineas.length > 0) {
      this.lineas.removeAt(0);
    }
    this.lineas.push(this.crearLineaForm());
  }

  // ─── Carga inicial ────────────────────────────────────────────────────────

  async cargarDatosDesplegables() {
    try {
      const [clientesData, atraccionesData] = await Promise.all([
        this.clienteSvc.getAll(),
        this.horarioSvc.getAtraccionesType()
      ]);
      this.clientes = clientesData;
      this.atracciones = atraccionesData;
      this.cdr.detectChanges();
    } catch {
      this.showToast('Error al cargar datos para los selectores', 'error');
    }
  }

  async cargarReserva(guid: string) {
    try {
      const data = await this.reservaSvc.getById(guid);

      this.estadoReserva = data.rev_estado;
      if (this.estadoReserva !== 'PEN') {
        this.form.disable();
      }

      // Cargar líneas desde los detalles existentes
      while (this.lineas.length !== 0) {
        this.lineas.removeAt(0);
      }

      if (data.detalle && data.detalle.length > 0) {
        data.detalle.forEach(d => {
          this.lineas.push(this.fb.group({
            tck_guid: [d.tck_guid, Validators.required],
            cantidad: [d.cantidad, [Validators.required, Validators.min(1)]]
          }));
        });
      } else {
        this.agregarLinea();
      }

      // Aplicar origen canal si se conoce
      this.form.patchValue({ origen_canal: 'Web' });

      // Nota: at_guid y hor_guid no se incluyen en el response actual del GET.
      // El usuario deberá re-seleccionar atracción y horario para editar las líneas.

      this.cdr.detectChanges();
    } catch {
      this.showToast('Error al cargar la reserva', 'error');
    }
  }

  // ─── Guardar ──────────────────────────────────────────────────────────────

  async guardar() {
    if (this.form.invalid) return;

    this.saving = true;
    const v = this.form.value;

    const payload: ReservaPayload = {
      at_guid: v.at_guid || undefined,
      clienteId: Number(v.clienteId),
      hor_guid: v.hor_guid,
      origen_canal: v.origen_canal,
      lineas: v.lineas.map((l: any) => ({
        tck_guid: l.tck_guid,
        cantidad: Number(l.cantidad)
      }))
    };

    try {
      if (this.isEditing) {
        await this.reservaSvc.update(this.currentGuid!, payload);
        this.showToast('Reserva actualizada correctamente', 'success');
      } else {
        await this.reservaSvc.create(payload);
        this.showToast('Reserva creada correctamente', 'success');
      }

      setTimeout(() => {
        this.router.navigate(['/admin/reserva']);
      }, 1500);
    } catch (e: any) {
      const msg = e.response?.data?.Error || e.response?.data?.message || 'Error al guardar la reserva';
      this.showToast(msg, 'error');
      this.saving = false;
      this.cdr.detectChanges();
    }
  }

  // ─── Utils ────────────────────────────────────────────────────────────────

  showToast(message: string, type: 'success' | 'error') {
    this.toast = { visible: true, message, type };
    this.cdr.detectChanges();
    setTimeout(() => {
      this.toast.visible = false;
      this.cdr.detectChanges();
    }, 3500);
  }

  async anularReserva() {
    if (!this.currentGuid) return;
    if (!confirm('¿Está seguro de que desea anular esta reserva? Esta acción no se puede deshacer.')) return;

    try {
      await this.reservaSvc.cancel(this.currentGuid);
      this.showToast('Reserva anulada correctamente', 'success');
      this.estadoReserva = 'CAN';
      this.form.disable();
    } catch (e: any) {
      const msg = e.response?.data?.Error || 'Error al anular la reserva';
      this.showToast(msg, 'error');
    }
  }
}