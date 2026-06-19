class ClienteProfile {
  final String nombres;
  final String apellidos;
  final String correo;
  final String telefono;
  final String direccion;

  ClienteProfile({
    required this.nombres,
    required this.apellidos,
    required this.correo,
    required this.telefono,
    required this.direccion,
  });

  factory ClienteProfile.fromJson(Map<String, dynamic> json) {
    return ClienteProfile(
      nombres: json['nombres'] ?? '',
      apellidos: json['apellidos'] ?? '',
      correo: json['correo'] ?? '',
      telefono: json['telefono'] ?? '',
      direccion: json['direccion'] ?? '',
    );
  }
}

class FacturaItem {
  final String id;
  final double total;
  final String estado;

  FacturaItem({
    required this.id,
    required this.total,
    required this.estado,
  });

  factory FacturaItem.fromJson(Map<String, dynamic> json) {
    return FacturaItem(
      id: json['id']?.toString() ?? '',
      total: (json['total'] ?? 0).toDouble(),
      estado: json['estado'] ?? '',
    );
  }
}
