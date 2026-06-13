import 'package:flutter/material.dart';
import '../services/atracciones_service.dart';
import '../model/atraccion.dart';
import 'atraccion_detalle_screen.dart';

class AtraccionesScreen extends StatefulWidget {
  const AtraccionesScreen({super.key});

  @override
  State<AtraccionesScreen> createState() => _AtraccionesScreenState();
}

class _AtraccionesScreenState extends State<AtraccionesScreen> {
  final AtraccionesService _svc = AtraccionesService();
  
  List<AtraccionItem> _items = [];
  bool _loading = false;
  String _error = '';
  int _totalRecords = 0;
  int _totalPages = 0;
  bool _hasPrev = false;
  bool _hasNext = false;

  final Map<String, dynamic> _filtros = {
    'Ciudad': '',
    'Pais': '',
    'Disponible': '',
    'page': 1,
    'limit': 9,
  };

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
      final res = await _svc.getAtracciones(filtros: _filtros);
      
      final list = res['data'] as List?;
      final pagination = res['pagination'];

      setState(() {
        _items = list?.map((e) => AtraccionItem.fromJson(e)).toList() ?? [];
        _totalRecords = pagination?['total'] ?? 0;
        _totalPages = pagination?['total_pages'] ?? 0;
        
        final currentPage = pagination?['page'] ?? 1;
        _hasPrev = currentPage > 1;
        _hasNext = currentPage < _totalPages;
      });
    } catch (e) {
      setState(() {
        _error = 'No se pudo cargar las atracciones. Verifica la conexión con el servidor.';
      });
    } finally {
      setState(() {
        _loading = false;
      });
    }
  }

  void _buscar() {
    _filtros['page'] = 1;
    _cargar();
  }

  void _limpiar() {
    _filtros['Ciudad'] = '';
    _filtros['Pais'] = '';
    _filtros['Disponible'] = '';
    _filtros['page'] = 1;
    _cargar();
  }

  void _prevPage() {
    if (_hasPrev) {
      _filtros['page']--;
      _cargar();
    }
  }

  void _nextPage() {
    if (_hasNext) {
      _filtros['page']++;
      _cargar();
    }
  }

  Widget _buildEstrellas(double n) {
    int stars = n.floor();
    return Row(
      mainAxisSize: MainAxisSize.min,
      children: List.generate(5, (index) => Icon(
        index < stars ? Icons.star : Icons.star_border,
        color: Colors.amber,
        size: 16,
      )),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Atracciones')),
      body: Column(
        children: [
          Padding(
            padding: const EdgeInsets.all(8.0),
            child: Row(
              children: [
                Expanded(
                  child: TextField(
                    decoration: const InputDecoration(labelText: 'Ciudad'),
                    onChanged: (v) => _filtros['Ciudad'] = v,
                  ),
                ),
                const SizedBox(width: 8),
                Expanded(
                  child: TextField(
                    decoration: const InputDecoration(labelText: 'País'),
                    onChanged: (v) => _filtros['Pais'] = v,
                  ),
                ),
                IconButton(icon: const Icon(Icons.search), onPressed: _buscar),
                IconButton(icon: const Icon(Icons.clear), onPressed: _limpiar),
              ],
            ),
          ),
          if (_error.isNotEmpty)
            Padding(padding: const EdgeInsets.all(8), child: Text(_error, style: const TextStyle(color: Colors.red))),
          Expanded(
            child: _loading 
              ? const Center(child: CircularProgressIndicator())
              : GridView.builder(
                  padding: const EdgeInsets.all(8),
                  gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
                    crossAxisCount: 2,
                    childAspectRatio: 0.75,
                    crossAxisSpacing: 8,
                    mainAxisSpacing: 8,
                  ),
                  itemCount: _items.length,
                  itemBuilder: (context, index) {
                    final item = _items[index];
                    return GestureDetector(
                      onTap: () {
                        Navigator.push(
                          context,
                          MaterialPageRoute(builder: (_) => AtraccionDetalleScreen(id: item.id)),
                        );
                      },
                      child: Card(
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            Expanded(
                              child: Container(
                                color: Colors.grey.shade200,
                                width: double.infinity,
                                child: item.imagenPrincipal.isNotEmpty 
                                  ? Image.network(item.imagenPrincipal, fit: BoxFit.cover, errorBuilder: (_,__,___) => const Icon(Icons.image))
                                  : const Icon(Icons.image, size: 50),
                              ),
                            ),
                            Padding(
                              padding: const EdgeInsets.all(8.0),
                              child: Column(
                                crossAxisAlignment: CrossAxisAlignment.start,
                                children: [
                                  Text(item.nombre, style: const TextStyle(fontWeight: FontWeight.bold), maxLines: 1, overflow: TextOverflow.ellipsis),
                                  Text('${item.ciudad}, ${item.pais}', style: const TextStyle(fontSize: 12)),
                                  _buildEstrellas(item.calificacion),
                                  Text('\$${item.precioDesde.toStringAsFixed(2)} ${item.moneda}', style: const TextStyle(fontWeight: FontWeight.bold, color: Colors.green)),
                                ],
                              ),
                            )
                          ],
                        ),
                      ),
                    );
                  },
                ),
          ),
          if (_totalPages > 1)
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                IconButton(icon: const Icon(Icons.arrow_back), onPressed: _hasPrev ? _prevPage : null),
                Text('Página ${_filtros['page']} de $_totalPages'),
                IconButton(icon: const Icon(Icons.arrow_forward), onPressed: _hasNext ? _nextPage : null),
              ],
            )
        ],
      ),
    );
  }
}
