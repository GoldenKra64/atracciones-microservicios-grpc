import 'package:flutter/material.dart';
import '../services/atracciones_service.dart';
import '../model/atraccion.dart';
import '../services/auth_service.dart';

class AtraccionDetalleScreen extends StatefulWidget {
  final String id;
  const AtraccionDetalleScreen({super.key, required this.id});

  @override
  State<AtraccionDetalleScreen> createState() => _AtraccionDetalleScreenState();
}

class _AtraccionDetalleScreenState extends State<AtraccionDetalleScreen> {
  final AtraccionesService _svc = AtraccionesService();
  final AuthService _auth = AuthService();

  AtraccionDetalle? _atraccion;
  bool _loading = false;
  String _error = '';

  HorarioProximo? _horarioSeleccionado;
  Map<String, int> _cantidades = {};
  bool _reserving = false;
  
  // Para el modal de pago
  String _reservaPendienteGuid = '';
  final _pagoMetodoController = TextEditingController(text: 'Tarjeta de Crédito');
  bool _confirmingPayment = false;

  @override
  void initState() {
    super.initState();
    _cargar();
  }

  Future<void> _cargar() async {
    setState(() {
      _loading = true;
      _error = '';
    });
    try {
      final atraccion = await _svc.getAtraccion(widget.id);
      final horarios = await _svc.getHorariosByAtraccion(widget.id);
      atraccion.horariosProximos = horarios;
      
      setState(() {
        _atraccion = atraccion;
      });
    } catch (e) {
      setState(() {
        _error = 'No se pudo cargar los detalles de esta atracción.';
      });
    } finally {
      setState(() {
        _loading = false;
      });
    }
  }

  Future<void> _seleccionarHorario(HorarioProximo h) async {
    setState(() {
      _horarioSeleccionado = h;
      _cantidades = {};
    });

    if (_atraccion != null && h.horGuid != null) {
      try {
        final tickets = await _svc.getTicketsPorHorario(_atraccion!.id, h.horGuid!);
        setState(() {
          _atraccion!.tickets = tickets;
          for (var t in tickets) {
            _cantidades[t.tckGuid] = 0;
          }
        });
      } catch (e) {
        debugPrint('Error fetching tickets $e');
      }
    }
  }

  void _incrementar(String guid) {
    setState(() {
      _cantidades[guid] = (_cantidades[guid] ?? 0) + 1;
    });
  }

  void _decrementar(String guid) {
    setState(() {
      if ((_cantidades[guid] ?? 0) > 0) {
        _cantidades[guid] = _cantidades[guid]! - 1;
      }
    });
  }

  int get _totalTickets => _cantidades.values.fold(0, (a, b) => a + b);

  Future<void> _reservar() async {
    if (_horarioSeleccionado == null || _totalTickets == 0) return;
    
    if (!(await _auth.isLoggedIn)) {
      if (!mounted) return;
      Navigator.pushNamed(context, '/ingresar');
      return;
    }

    setState(() => _reserving = true);
    
    final lineas = _cantidades.entries
      .where((e) => e.value > 0)
      .map((e) => {'tck_guid': e.key, 'cantidad': e.value})
      .toList();

    final payload = {
      'at_guid': widget.id,
      'hor_guid': _horarioSeleccionado!.horGuid,
      'origen_canal': 'ATRAXIA_MOBILE',
      'lineas': lineas,
    };

    try {
      final res = await _svc.reservar(payload);
      final data = res['data'];
      
      if (data != null && (data['rev_estado'] == 'PEN' || data['rev_estado'] == 'Pendiente')) {
        setState(() {
          _reservaPendienteGuid = data['rev_guid'];
          _reserving = false;
        });
        if (!mounted) return;
        _mostrarModalPago();
        return;
      }
      if (!mounted) return;
      _mostrarDialog(res['success'] != false ? 'Éxito' : 'Error', res['message'] ?? 'Reserva procesada', true);
    } catch (e) {
      if (!mounted) return;
      _mostrarDialog('Error', 'Error al procesar la reserva', false);
    } finally {
      setState(() => _reserving = false);
    }
  }

  void _mostrarDialog(String titulo, String mensaje, bool success) {
    showDialog(
      context: context,
      builder: (ctx) => AlertDialog(
        title: Text(titulo),
        content: Text(mensaje),
        actions: [
          TextButton(
            onPressed: () {
              Navigator.pop(ctx);
              if (success) {
                setState(() {
                  _horarioSeleccionado = null;
                  _cantidades = {};
                });
                _cargar();
              }
            },
            child: const Text('OK'),
          )
        ],
      )
    );
  }

  void _mostrarModalPago() {
    showModalBottomSheet(
      context: context,
      isScrollControlled: true,
      builder: (ctx) {
        return Padding(
          padding: EdgeInsets.only(bottom: MediaQuery.of(ctx).viewInsets.bottom, left: 16, right: 16, top: 16),
          child: Column(
            mainAxisSize: MainAxisSize.min,
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              const Text('Confirmar Pago', style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold)),
              const SizedBox(height: 16),
              TextField(
                controller: _pagoMetodoController,
                decoration: const InputDecoration(labelText: 'Método de Pago'),
              ),
              const SizedBox(height: 16),
              ElevatedButton(
                onPressed: _confirmingPayment ? null : () async {
                  setState(() => _confirmingPayment = true);
                  try {
                    final payload = {
                      'metodoPago': _pagoMetodoController.text,
                      'comprobante': 'TRX-${DateTime.now().millisecondsSinceEpoch}'
                    };
                    final res = await _svc.confirmarPago(_reservaPendienteGuid, payload);
                    if (!mounted) return;
                    Navigator.pop(ctx);
                    _mostrarDialog('Éxito', res['message'] ?? 'Pago confirmado', true);
                  } catch (e) {
                    if (!mounted) return;
                    Navigator.pop(ctx);
                    _mostrarDialog('Error', 'Error al confirmar el pago', false);
                  } finally {
                    setState(() => _confirmingPayment = false);
                  }
                },
                child: _confirmingPayment ? const SizedBox(width: 20, height: 20, child: CircularProgressIndicator()) : const Text('Pagar'),
              ),
              const SizedBox(height: 16),
            ],
          ),
        );
      }
    );
  }

  @override
  Widget build(BuildContext context) {
    if (_loading) return const Scaffold(body: Center(child: CircularProgressIndicator()));
    if (_error.isNotEmpty) return Scaffold(appBar: AppBar(), body: Center(child: Text(_error)));
    if (_atraccion == null) return const Scaffold(body: Center(child: Text('Atracción no encontrada')));

    return Scaffold(
      appBar: AppBar(title: Text(_atraccion!.nombre)),
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            if (_atraccion!.imagenes.isNotEmpty)
              SizedBox(
                height: 200,
                child: ListView.builder(
                  scrollDirection: Axis.horizontal,
                  itemCount: _atraccion!.imagenes.length,
                  itemBuilder: (ctx, i) => Padding(
                    padding: const EdgeInsets.only(right: 8.0),
                    child: Image.network(_atraccion!.imagenes[i], fit: BoxFit.cover, width: 250),
                  ),
                ),
              ),
            const SizedBox(height: 16),
            Text(_atraccion!.descripcion, style: const TextStyle(fontSize: 16)),
            const SizedBox(height: 16),
            const Text('Horarios Disponibles:', style: TextStyle(fontWeight: FontWeight.bold, fontSize: 18)),
            ..._atraccion!.horariosProximos.map((h) => ListTile(
              title: Text('${h.fecha} | ${h.horaInicio} - ${h.horaFin}'),
              subtitle: Text('Cupos: ${h.cupos}'),
              trailing: _horarioSeleccionado == h ? const Icon(Icons.check_circle, color: Colors.green) : null,
              onTap: () => _seleccionarHorario(h),
            )),
            if (_horarioSeleccionado != null && _atraccion!.tickets.isNotEmpty) ...[
              const Divider(),
              const Text('Tickets:', style: TextStyle(fontWeight: FontWeight.bold, fontSize: 18)),
              ..._atraccion!.tickets.map((t) => Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Text('${t.tipo} (\$${t.precio} ${t.moneda})'),
                  Row(
                    children: [
                      IconButton(icon: const Icon(Icons.remove), onPressed: () => _decrementar(t.tckGuid)),
                      Text('${_cantidades[t.tckGuid] ?? 0}'),
                      IconButton(icon: const Icon(Icons.add), onPressed: () => _incrementar(t.tckGuid)),
                    ],
                  )
                ],
              )),
              const SizedBox(height: 16),
              SizedBox(
                width: double.infinity,
                child: ElevatedButton(
                  onPressed: _totalTickets > 0 && !_reserving ? _reservar : null,
                  child: _reserving ? const SizedBox(width: 20, height: 20, child: CircularProgressIndicator()) : Text('Reservar ($_totalTickets tickets)'),
                ),
              )
            ]
          ],
        ),
      ),
    );
  }
}
