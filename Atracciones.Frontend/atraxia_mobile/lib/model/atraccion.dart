class AtraccionItem {
  final String id;
  final String nombre;
  final String ciudad;
  final String pais;
  final String tipoNombre;
  final String descripcionCorta;
  final double precioDesde;
  final String moneda;
  final double calificacion;
  final int totalResenias;
  final int duracionMinutos;
  final String imagenPrincipal;
  final List<String> etiquetas;
  final bool disponible;
  final bool disponibleHoy;
  final String proximaFechaDisponible;
  final int cuposDisponibles;

  AtraccionItem({
    required this.id,
    required this.nombre,
    required this.ciudad,
    required this.pais,
    required this.tipoNombre,
    required this.descripcionCorta,
    required this.precioDesde,
    required this.moneda,
    required this.calificacion,
    required this.totalResenias,
    required this.duracionMinutos,
    required this.imagenPrincipal,
    required this.etiquetas,
    required this.disponible,
    required this.disponibleHoy,
    required this.proximaFechaDisponible,
    required this.cuposDisponibles,
  });

  factory AtraccionItem.fromJson(Map<String, dynamic> json) {
    return AtraccionItem(
      id: json['id'] ?? '',
      nombre: json['nombre'] ?? '',
      ciudad: json['ciudad'] ?? '',
      pais: json['pais'] ?? '',
      tipoNombre: json['tipo_nombre'] ?? '',
      descripcionCorta: json['descripcion_corta'] ?? '',
      precioDesde: (json['precio_desde'] ?? 0).toDouble(),
      moneda: json['moneda'] ?? 'USD',
      calificacion: (json['calificacion'] ?? 0).toDouble(),
      totalResenias: json['total_resenias'] ?? json['total_resenas'] ?? 0,
      duracionMinutos: json['duracion_minutos'] ?? 0,
      imagenPrincipal: json['imagen_principal'] ?? '',
      etiquetas: List<String>.from(json['etiquetas'] ?? []),
      disponible: json['disponibilidad']?['disponible'] ?? false,
      disponibleHoy: json['disponibilidad']?['disponible_hoy'] ?? false,
      proximaFechaDisponible: json['disponibilidad']?['proxima_fecha_disponible'] ?? '',
      cuposDisponibles: json['disponibilidad']?['cupos_disponibles'] ?? 0,
    );
  }
}

class AtraccionDetalle {
  final String id;
  final String nombre;
  final String descripcion;
  final List<String> imagenes;
  final List<String> incluye;
  final List<String> noIncluye;
  final String? puntoEncuentro;
  final bool incluyeTransporte;
  final bool incluyeAcompaniante;
  List<TicketInfo> tickets;
  List<HorarioProximo> horariosProximos;

  AtraccionDetalle({
    required this.id,
    required this.nombre,
    required this.descripcion,
    required this.imagenes,
    required this.incluye,
    required this.noIncluye,
    this.puntoEncuentro,
    required this.incluyeTransporte,
    required this.incluyeAcompaniante,
    required this.tickets,
    required this.horariosProximos,
  });

  factory AtraccionDetalle.fromJson(Map<String, dynamic> json) {
    return AtraccionDetalle(
      id: json['id'] ?? '',
      nombre: json['nombre'] ?? '',
      descripcion: json['descripcion'] ?? '',
      imagenes: List<String>.from(json['imagenes'] ?? []),
      incluye: List<String>.from(json['incluye'] ?? []),
      noIncluye: List<String>.from(json['no_incluye'] ?? []),
      puntoEncuentro: json['punto_encuentro'],
      incluyeTransporte: json['incluye_transporte'] ?? false,
      incluyeAcompaniante: json['incluye_acompaniante'] ?? false,
      tickets: (json['tickets'] as List?)?.map((t) => TicketInfo.fromJson(t)).toList() ?? [],
      horariosProximos: (json['horarios_proximos'] as List?)?.map((h) => HorarioProximo.fromJson(h)).toList() ?? [],
    );
  }
}

class TicketInfo {
  final int horId;
  final String tckGuid;
  final String tipo;
  final double precio;
  final String moneda;

  TicketInfo({
    required this.horId,
    required this.tckGuid,
    required this.tipo,
    required this.precio,
    required this.moneda,
  });

  factory TicketInfo.fromJson(Map<String, dynamic> json) {
    return TicketInfo(
      horId: json['horId'] ?? 0,
      tckGuid: json['tck_guid'] ?? '',
      tipo: json['tipo'] ?? '',
      precio: (json['precio'] ?? 0).toDouble(),
      moneda: json['moneda'] ?? '',
    );
  }
}

class HorarioProximo {
  final int horarioId;
  final String? horGuid;
  final int atraccionId;
  final String fecha;
  final String horaInicio;
  final String horaFin;
  final int cupos;

  HorarioProximo({
    required this.horarioId,
    this.horGuid,
    required this.atraccionId,
    required this.fecha,
    required this.horaInicio,
    required this.horaFin,
    required this.cupos,
  });

  factory HorarioProximo.fromJson(Map<String, dynamic> json) {
    return HorarioProximo(
      horarioId: json['horarioId'] ?? 0,
      horGuid: json['hor_guid'],
      atraccionId: json['atraccionId'] ?? 0,
      fecha: json['fecha'] ?? '',
      horaInicio: json['hora_inicio'] ?? '',
      horaFin: json['hora_fin'] ?? '',
      cupos: json['cupos'] ?? 0,
    );
  }
}
