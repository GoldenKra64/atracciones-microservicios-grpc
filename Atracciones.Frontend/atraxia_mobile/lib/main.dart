import 'package:flutter/material.dart';
import 'view/home_screen.dart';
import 'view/ingresar_screen.dart';
import 'view/perfil_screen.dart';
import 'view/atracciones_screen.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Atraxia Mobile',
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(seedColor: Colors.blue),
        useMaterial3: true,
      ),
      initialRoute: '/',
      routes: {
        '/': (context) => const HomeScreen(),
        '/ingresar': (context) => const IngresarScreen(),
        '/perfil': (context) => const PerfilScreen(),
        '/atracciones': (context) => const AtraccionesScreen(),
      },
    );
  }
}
