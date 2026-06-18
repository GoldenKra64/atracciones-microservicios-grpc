import 'dart:convert';
import '../model/atraccion.dart';
import 'api_service.dart';

class AtraccionesService {
  final ApiService _api = ApiService();

  Future<Map<String, dynamic>> getAtracciones({Map<String, dynamic>? filtros}) async {
    final params = <String, dynamic>{};
    filtros?.forEach((k, v) {
      if (v != null && v != '') {
        params[k] = v;
      }
    });
    if (params.containsKey('page')) {
      params['PageNumber'] = params['page'];
      params.remove('page');
    }
    if (params.containsKey('Disponible')) {
      params['Disponible'] = params['Disponible'] == 'true' || params['Disponible'] == true;
    }

    final res = await _api.get('/atracciones', queryParams: params);
    if (res.statusCode >= 200 && res.statusCode < 300) {
      return jsonDecode(res.body);
    }
    throw Exception('Error al cargar atracciones');
  }

  Future<AtraccionDetalle> getAtraccion(String id) async {
    final res = await _api.get('/atracciones/$id');
    if (res.statusCode >= 200 && res.statusCode < 300) {
      final body = jsonDecode(res.body);
      return AtraccionDetalle.fromJson(body['data']);
    }
    throw Exception('Error al cargar la atracción');
  }

  Future<Map<String, dynamic>> reservar(Map<String, dynamic> payload) async {
    final res = await _api.post('/reservas', payload);
    return jsonDecode(res.body);
  }

  Future<Map<String, dynamic>> confirmarPago(String guid, Map<String, dynamic> payload) async {
    final res = await _api.post('/reservas/$guid/pagos/confirmacion', payload);
    return jsonDecode(res.body);
  }

  Future<List<dynamic>> getResenas(String id) async {
    final res = await _api.get('/atracciones/$id/resenias');
    if (res.statusCode >= 200 && res.statusCode < 300) {
      final body = jsonDecode(res.body);
      return body['data'] ?? [];
    }
    return [];
  }

  Future<List<HorarioProximo>> getHorariosByAtraccion(String guid) async {
    final res = await _api.get('/atracciones/$guid/horarios');
    if (res.statusCode >= 200 && res.statusCode < 300) {
      final body = jsonDecode(res.body);
      final list = body['data'] as List?;
      return list?.map((e) => HorarioProximo.fromJson(e)).toList() ?? [];
    }
    return [];
  }

  Future<List<TicketInfo>> getTicketsPorHorario(String guid, String horarioGuid) async {
    final res = await _api.get('/atracciones/$guid/horarios/$horarioGuid/tickets');
    if (res.statusCode >= 200 && res.statusCode < 300) {
      final body = jsonDecode(res.body);
      final list = body['data']?['items'] as List?;
      return list?.map((e) => TicketInfo.fromJson(e)).toList() ?? [];
    }
    return [];
  }

  Future<AtraccionDetalle> getAtraccionCompleta(String guid) async {
    const String query = '''
      query GetAtraccionCompleta(\$guid: String!) {
        atraccion(guid: \$guid) {
          id
          nombre
          descripcion
          imagenes
        }
        horarios(guid: \$guid) {
          horGuid
          fecha
          horaInicio
          horaFin
          cupos
        }
      }
    ''';

    final res = await _api.queryGraphQL('/graphql/atraccion', query, variables: {'guid': guid});
    if (res.hasException) throw Exception(res.exception.toString());
    
    final data = res.data;
    if (data == null) throw Exception('No data');

    final atr = AtraccionDetalle.fromJson(data['atraccion']);
    final hList = data['horarios'] as List?;
    atr.horariosProximos = hList?.map((e) => HorarioProximo.fromJson(e)).toList() ?? [];
    return atr;
  }

  Future<List<TicketInfo>> getTicketsGraphQL(String guid, String horarioId) async {
    const String query = '''
      query GetTickets(\$guid: String!, \$horarioId: String!) {
        tickets(guid: \$guid, horarioId: \$horarioId) {
          tckGuid
          tipo
          precio
          moneda
        }
      }
    ''';

    final res = await _api.queryGraphQL('/graphql/atraccion', query, variables: {
      'guid': guid,
      'horarioId': horarioId
    });
    if (res.hasException) throw Exception(res.exception.toString());
    
    final list = res.data?['tickets'] as List?;
    return list?.map((e) => TicketInfo.fromJson(e)).toList() ?? [];
  }
}
