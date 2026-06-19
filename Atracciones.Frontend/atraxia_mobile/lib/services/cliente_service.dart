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

  Future<Map<String, dynamic>> getPerfilCompleto(int page, int limit) async {
    const String query = '''
      query GetPerfilCompleto(\$page: Int!, \$limit: Int!) {
        profile {
          id
          nombres
          apellidos
          correo
          telefono
          direccion
        }
        facturas(page: \$page, limit: \$limit) {
          id
          total
          estado
        }
      }
    ''';

    final res = await _api.queryGraphQL('/graphql/cliente', query, variables: {'page': page, 'limit': limit});
    if (res.hasException) throw Exception(res.exception.toString());
    
    final data = res.data;
    if (data == null) throw Exception('No data');

    return {
      'profile': ClienteProfile.fromJson(data['profile']),
      'facturas': (data['facturas'] as List?)?.map((e) => FacturaItem.fromJson(e)).toList() ?? []
    };
  }
}
