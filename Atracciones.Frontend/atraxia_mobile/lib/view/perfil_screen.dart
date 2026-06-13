import 'package:flutter/material.dart';
import '../services/cliente_service.dart';
import '../services/auth_service.dart';
import '../model/cliente_profile.dart';

class PerfilScreen extends StatefulWidget {
  const PerfilScreen({super.key});

  @override
  State<PerfilScreen> createState() => _PerfilScreenState();
}

class _PerfilScreenState extends State<PerfilScreen> {
  final ClienteService _svc = ClienteService();
  final AuthService _auth = AuthService();
  
  ClienteProfile? _profile;
  List<FacturaItem> _facturas = [];
  bool _loading = false;
  String _error = '';

  @override
  void initState() {
    super.initState();
    _cargarDatos();
  }

  Future<void> _cargarDatos() async {
    final isLoggedIn = await _auth.isLoggedIn;
    if (!isLoggedIn) {
      if (!mounted) return;
      Navigator.pushReplacementNamed(context, '/ingresar');
      return;
    }

    setState(() {
      _loading = true;
      _error = '';
    });

    try {
      final profile = await _svc.getProfile();
      final facturas = await _svc.getFacturas(1, 20);
      
      setState(() {
        _profile = profile;
        _facturas = facturas;
      });
    } catch (err) {
      setState(() {
        _error = 'No se pudieron cargar los datos del perfil.';
      });
    } finally {
      setState(() {
        _loading = false;
      });
    }
  }

  Future<void> _logout() async {
    await _auth.logout();
    if (!mounted) return;
    Navigator.pushReplacementNamed(context, '/');
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Mi Perfil'),
        actions: [
          IconButton(icon: const Icon(Icons.logout), onPressed: _logout),
        ],
      ),
      body: _loading 
        ? const Center(child: CircularProgressIndicator())
        : _error.isNotEmpty
          ? Center(child: Text(_error, style: const TextStyle(color: Colors.red)))
          : _profile == null
            ? const Center(child: Text('Sin datos'))
            : SingleChildScrollView(
                padding: const EdgeInsets.all(16),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Card(
                      child: ListTile(
                        leading: const Icon(Icons.person, size: 40),
                        title: Text('${_profile!.nombres} ${_profile!.apellidos}', style: const TextStyle(fontWeight: FontWeight.bold, fontSize: 20)),
                        subtitle: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            Text(_profile!.correo),
                            Text(_profile!.telefono),
                            Text(_profile!.direccion),
                          ],
                        ),
                      ),
                    ),
                    const SizedBox(height: 24),
                    const Text('Mis Facturas', style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold)),
                    const SizedBox(height: 8),
                    if (_facturas.isEmpty)
                      const Text('No tienes facturas recientes.')
                    else
                      ListView.builder(
                        shrinkWrap: true,
                        physics: const NeverScrollableScrollPhysics(),
                        itemCount: _facturas.length,
                        itemBuilder: (context, index) {
                          final f = _facturas[index];
                          return Card(
                            child: ListTile(
                              title: Text('Factura #${f.id}'),
                              subtitle: Text(f.fecha),
                              trailing: Column(
                                mainAxisAlignment: MainAxisAlignment.center,
                                crossAxisAlignment: CrossAxisAlignment.end,
                                children: [
                                  Text('\$${f.total.toStringAsFixed(2)}', style: const TextStyle(fontWeight: FontWeight.bold)),
                                  Text(f.estado, style: TextStyle(color: f.estado == 'Pagada' ? Colors.green : Colors.orange)),
                                ],
                              ),
                            ),
                          );
                        },
                      )
                  ],
                ),
              ),
    );
  }
}
