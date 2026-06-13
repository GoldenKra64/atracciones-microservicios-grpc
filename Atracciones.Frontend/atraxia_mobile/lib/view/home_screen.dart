import 'package:flutter/material.dart';

class HomeScreen extends StatelessWidget {
  const HomeScreen({super.key});

  @override
  Widget build(BuildContext context) {
    final features = [
      {'icon': '🗺️', 'title': 'Destinos Únicos', 'desc': 'Descubre atracciones fuera de lo común en toda Latinoamérica.'},
      {'icon': '🎟️', 'title': 'Reserva Fácil', 'desc': 'Compra tickets en segundos y recíbelos directamente en tu correo.'},
      {'icon': '⭐', 'title': 'Calidad Garantizada', 'desc': 'Todas nuestras experiencias son evaluadas por viajeros reales.'},
      {'icon': '🌍', 'title': 'Guías Locales', 'desc': 'Conecta con expertos locales que conocen cada rincón.'}
    ];

    return Scaffold(
      appBar: AppBar(
        title: const Text('Atraxia'),
        actions: [
          TextButton(
            onPressed: () => Navigator.pushNamed(context, '/ingresar'),
            child: const Text('Ingresar', style: TextStyle(color: Colors.white)),
          ),
          IconButton(
            icon: const Icon(Icons.person),
            onPressed: () => Navigator.pushNamed(context, '/perfil'),
          )
        ],
      ),
      body: SingleChildScrollView(
        child: Column(
          children: [
            Container(
              padding: const EdgeInsets.all(32),
              color: Theme.of(context).colorScheme.primaryContainer,
              width: double.infinity,
              child: Column(
                children: [
                  const Text(
                    'Descubre tu próxima aventura',
                    style: TextStyle(fontSize: 28, fontWeight: FontWeight.bold),
                    textAlign: TextAlign.center,
                  ),
                  const SizedBox(height: 16),
                  ElevatedButton(
                    onPressed: () => Navigator.pushNamed(context, '/atracciones'),
                    child: const Text('Ver Atracciones'),
                  )
                ],
              ),
            ),
            Padding(
              padding: const EdgeInsets.all(16.0),
              child: Column(
                children: features.map((f) => ListTile(
                  leading: Text(f['icon']!, style: const TextStyle(fontSize: 32)),
                  title: Text(f['title']!, style: const TextStyle(fontWeight: FontWeight.bold)),
                  subtitle: Text(f['desc']!),
                )).toList(),
              ),
            )
          ],
        ),
      ),
    );
  }
}
