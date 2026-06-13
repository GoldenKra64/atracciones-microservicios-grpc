import 'dart:convert';
import '../model/cliente_profile.dart';
import 'api_service.dart';

class ClienteService {
  final ApiService _api = ApiService();

  Future<ClienteProfile> getProfile() async {
    // Nota: El endpoint real dependerá del backend, aquí uso una suposición común.
    final res = await _api.get('/clientes/perfil'); 
    if (res.statusCode >= 200 && res.statusCode < 300) {
      final body = jsonDecode(res.body);
      return ClienteProfile.fromJson(body['data'] ?? body);
    }
    throw Exception('Error al cargar perfil');
  }

  Future<List<FacturaItem>> getFacturas(int page, int limit) async {
    final res = await _api.get('/clientes/facturas', queryParams: {'page': page, 'limit': limit}); 
    if (res.statusCode >= 200 && res.statusCode < 300) {
      final body = jsonDecode(res.body);
      final list = body['data']?['items'] ?? body['data'] as List?;
      return list?.map<FacturaItem>((e) => FacturaItem.fromJson(e)).toList() ?? [];
    }
    return [];
  }
}
