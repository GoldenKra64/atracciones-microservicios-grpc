import 'dart:convert';
import 'package:shared_preferences/shared_preferences.dart';
import '../model/auth_user.dart';
import 'api_service.dart';

class AuthService {
  final ApiService _api = ApiService();

  Future<bool> get isLoggedIn async {
    final prefs = await SharedPreferences.getInstance();
    return prefs.getString('atraxia_token') != null;
  }

  Future<AuthUser?> get currentUser async {
    final prefs = await SharedPreferences.getInstance();
    final userStr = prefs.getString('atraxia_user');
    if (userStr != null) {
      return AuthUser.fromJson(jsonDecode(userStr));
    }
    return null;
  }

  Future<Map<String, dynamic>> login(Map<String, dynamic> payload) async {
    try {
      final res = await _api.post('/Auth/login', payload);
      final body = jsonDecode(res.body);
      final data = body['data'];
      
      if (res.statusCode >= 200 && res.statusCode < 300 && data != null && data['success'] == true) {
        final token = data['token'];
        final prefs = await SharedPreferences.getInstance();
        await prefs.setString('atraxia_token', token);
        
        final user = AuthUser(
          username: data['username'] ?? '',
          token: token,
          expiration: data['expiration'] ?? '',
          roles: List<String>.from(data['roles'] ?? []),
        );
        await prefs.setString('atraxia_user', jsonEncode(user.toJson()));
        
        return {'success': true, 'message': 'Bienvenido, ${data['username']}'};
      }
      return {'success': false, 'message': data?['message'] ?? body['message'] ?? 'Credenciales incorrectas'};
    } catch (err) {
      return {'success': false, 'message': 'Error al iniciar sesión'};
    }
  }

  Future<Map<String, dynamic>> register(Map<String, dynamic> payload) async {
    try {
      final res = await _api.post('/Auth', payload);
      final body = jsonDecode(res.body);
      
      if (res.statusCode >= 200 && res.statusCode < 300 && (body['success'] == true || body['Success'] == true)) {
        return {'success': true, 'message': 'Cuenta creada. Ya puedes ingresar.'};
      }
      return {
        'success': false, 
        'message': body['message'] ?? body['Message'] ?? 'Error al registrarse',
        'errors': body['errors'] ?? body['Errors']
      };
    } catch (err) {
      return {'success': false, 'message': 'Error al registrarse'};
    }
  }

  Future<void> logout() async {
    final prefs = await SharedPreferences.getInstance();
    await prefs.remove('atraxia_token');
    await prefs.remove('atraxia_user');
  }
}
