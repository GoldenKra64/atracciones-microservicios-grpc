class ReservaDetalle {
  final String tckGuid;
  final String tckTipoParticipante;
  final int cantidad;
  final double precioUnit;
  final double subtotal;

  ReservaDetalle({
    required this.tckGuid,
    required this.tckTipoParticipante,
    required this.cantidad,
    required this.precioUnit,
    required this.subtotal,
  });

  factory ReservaDetalle.fromJson(Map<String, dynamic> json) {
    return ReservaDetalle(
      tckGuid: json['tck_guid'] ?? '',
      tckTipoParticipante: json['tck_tipo_participante'] ?? '',
      cantidad: json['cantidad'] ?? 0,
      precioUnit: (json['precio_unit'] ?? 0).toDouble(),
      subtotal: (json['subtotal'] ?? 0).toDouble(),
    );
  }
}

class Reserva {
  final String revGuid;
  final String revCodigo;
  final String horFecha;
  final String horHoraInicio;
  final String horHoraFin;
  final String atraccionNombre;
  final double revSubtotal;
  final double revValorIva;
  final double revTotal;
  final String moneda;
  final String revEstado;
  final String revFechaReservaUtc;
  final List<ReservaDetalle> detalle;

  Reserva({
    required this.revGuid,
    required this.revCodigo,
    required this.horFecha,
    required this.horHoraInicio,
    required this.horHoraFin,
    required this.atraccionNombre,
    required this.revSubtotal,
    required this.revValorIva,
    required this.revTotal,
    required this.moneda,
    required this.revEstado,
    required this.revFechaReservaUtc,
    required this.detalle,
  });

  factory Reserva.fromJson(Map<String, dynamic> json) {
    return Reserva(
      revGuid: json['rev_guid'] ?? '',
      revCodigo: json['rev_codigo'] ?? '',
      horFecha: json['hor_fecha'] ?? '',
      horHoraInicio: json['hor_hora_inicio'] ?? '',
      horHoraFin: json['hor_hora_fin'] ?? '',
      atraccionNombre: json['atraccion_nombre'] ?? '',
      revSubtotal: (json['rev_subtotal'] ?? 0).toDouble(),
      revValorIva: (json['rev_valor_iva'] ?? 0).toDouble(),
      revTotal: (json['rev_total'] ?? 0).toDouble(),
      moneda: json['moneda'] ?? '',
      revEstado: json['rev_estado'] ?? '',
      revFechaReservaUtc: json['rev_fecha_reserva_utc'] ?? '',
      detalle: (json['detalle'] as List?)?.map((d) => ReservaDetalle.fromJson(d)).toList() ?? [],
    );
  }
}
