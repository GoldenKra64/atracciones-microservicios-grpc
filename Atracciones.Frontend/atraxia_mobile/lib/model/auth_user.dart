class AuthUser {
  final String username;
  final String token;
  final String expiration;
  final List<String> roles;

  AuthUser({
    required this.username,
    required this.token,
    required this.expiration,
    required this.roles,
  });

  factory AuthUser.fromJson(Map<String, dynamic> json) {
    return AuthUser(
      username: json['username'] ?? '',
      token: json['token'] ?? '',
      expiration: json['expiration'] ?? '',
      roles: List<String>.from(json['roles'] ?? []),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'username': username,
      'token': token,
      'expiration': expiration,
      'roles': roles,
    };
  }
}
