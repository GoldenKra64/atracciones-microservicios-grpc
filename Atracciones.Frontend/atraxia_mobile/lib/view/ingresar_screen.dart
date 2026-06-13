import 'package:flutter/material.dart';
import '../services/auth_service.dart';

class IngresarScreen extends StatefulWidget {
  const IngresarScreen({super.key});

  @override
  State<IngresarScreen> createState() => _IngresarScreenState();
}

class _IngresarScreenState extends State<IngresarScreen> {
  final _authService = AuthService();
  bool _isLogin = true;
  bool _loading = false;
  String _mensaje = '';
  bool _esError = false;

  final _loginController = TextEditingController();
  final _passwordController = TextEditingController();

  final _regLoginController = TextEditingController();
  final _regPasswordController = TextEditingController();
  final _regIdentificacionController = TextEditingController();
  final _regCorreoController = TextEditingController();
  final _regNombresController = TextEditingController();
  final _regApellidosController = TextEditingController();
  final _regTelefonoController = TextEditingController();
  final _regDireccionController = TextEditingController();

  Future<void> _onLogin() async {
    if (_loginController.text.isEmpty || _passwordController.text.isEmpty) {
      setState(() {
        _esError = true;
        _mensaje = 'Completa todos los campos';
      });
      return;
    }
    setState(() {
      _loading = true;
      _mensaje = '';
    });
    
    final res = await _authService.login({
      'login': _loginController.text,
      'password': _passwordController.text,
    });
    
    setState(() {
      _loading = false;
    });
    
    if (res['success'] == true) {
      if (!mounted) return;
      Navigator.pushReplacementNamed(context, '/atracciones');
    } else {
      setState(() {
        _esError = true;
        _mensaje = res['message'];
      });
    }
  }

  Future<void> _onRegistro() async {
    setState(() {
      _loading = true;
      _mensaje = '';
    });
    
    final payload = {
      'login': _regLoginController.text,
      'password': _regPasswordController.text,
      'rolIds': [2],
      'cliente': {
        'usuarioId': 0,
        'tipoIdentificacion': 'CEDULA',
        'numeroIdentificacion': _regIdentificacionController.text,
        'correo': _regCorreoController.text,
        'nombres': _regNombresController.text,
        'apellidos': _regApellidosController.text,
        'telefono': _regTelefonoController.text,
        'direccion': _regDireccionController.text,
      }
    };
    
    final res = await _authService.register(payload);
    
    setState(() {
      _loading = false;
      _esError = res['success'] != true;
    });
    
    if (res['success'] == true) {
      setState(() {
        _mensaje = res['message'];
        _isLogin = true;
      });
    } else {
      String errorMsg = res['message'];
      if (res['errors'] != null && res['errors'] is List && res['errors'].isNotEmpty) {
        errorMsg += ' ' + res['errors'].join(' | ');
      }
      setState(() {
        _mensaje = errorMsg;
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text(_isLogin ? 'Ingresar' : 'Registro')),
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(16),
        child: Column(
          children: [
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                TextButton(
                  onPressed: () => setState(() => _isLogin = true),
                  child: Text('Login', style: TextStyle(fontWeight: _isLogin ? FontWeight.bold : FontWeight.normal)),
                ),
                TextButton(
                  onPressed: () => setState(() => _isLogin = false),
                  child: Text('Registro', style: TextStyle(fontWeight: !_isLogin ? FontWeight.bold : FontWeight.normal)),
                ),
              ],
            ),
            if (_mensaje.isNotEmpty)
              Container(
                padding: const EdgeInsets.all(8),
                margin: const EdgeInsets.only(bottom: 16),
                color: _esError ? Colors.red.shade100 : Colors.green.shade100,
                child: Text(_mensaje, style: TextStyle(color: _esError ? Colors.red : Colors.green)),
              ),
            if (_isLogin) ...[
              TextField(controller: _loginController, decoration: const InputDecoration(labelText: 'Usuario')),
              TextField(controller: _passwordController, decoration: const InputDecoration(labelText: 'Contraseña'), obscureText: true),
              const SizedBox(height: 16),
              ElevatedButton(
                onPressed: _loading ? null : _onLogin,
                child: _loading ? const SizedBox(width: 20, height: 20, child: CircularProgressIndicator(strokeWidth: 2)) : const Text('Ingresar'),
              )
            ] else ...[
              TextField(controller: _regLoginController, decoration: const InputDecoration(labelText: 'Usuario')),
              TextField(controller: _regPasswordController, decoration: const InputDecoration(labelText: 'Contraseña'), obscureText: true),
              TextField(controller: _regIdentificacionController, decoration: const InputDecoration(labelText: 'Identificación')),
              TextField(controller: _regNombresController, decoration: const InputDecoration(labelText: 'Nombres')),
              TextField(controller: _regApellidosController, decoration: const InputDecoration(labelText: 'Apellidos')),
              TextField(controller: _regCorreoController, decoration: const InputDecoration(labelText: 'Correo')),
              TextField(controller: _regTelefonoController, decoration: const InputDecoration(labelText: 'Teléfono')),
              TextField(controller: _regDireccionController, decoration: const InputDecoration(labelText: 'Dirección')),
              const SizedBox(height: 16),
              ElevatedButton(
                onPressed: _loading ? null : _onRegistro,
                child: _loading ? const SizedBox(width: 20, height: 20, child: CircularProgressIndicator(strokeWidth: 2)) : const Text('Registrarse'),
              )
            ]
          ],
        ),
      ),
    );
  }
}
