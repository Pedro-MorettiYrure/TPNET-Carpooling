using System;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Linq;
using System.Collections.Generic;

namespace Domain.Model
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string TipoUsuario { get; set; }
        public string ContraseñaHash { get; private set; }

        // Colección de vehículos del usuario
        public ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();

        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 10000;

        public Usuario() { }

        public static Usuario Crear(string nombre, string apellido, string email, string contraseña)
        {
            var u = new Usuario();
            u.SetNombre(nombre);
            u.SetApellido(apellido);
            u.SetEmail(email);
            u.SetContraseña(contraseña);
            u.TipoUsuario = "Pasajero"; // Asignar el tipo "Pasajero" por defecto
            return u;
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede ser nulo o vacío.", nameof(nombre));
            Nombre = nombre;
        }

        public void SetApellido(string apellido)
        {
            if (string.IsNullOrWhiteSpace(apellido))
                throw new ArgumentException("El apellido no puede ser nulo o vacío.", nameof(apellido));
            Apellido = apellido;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El email no puede ser nulo o vacío.", nameof(email));

            try
            {
                var mail = new MailAddress(email);
                Email = email;
            }
            catch
            {
                throw new ArgumentException("El email no tiene un formato válido.", nameof(email));
            }
        }

        public void SetContraseña(string contraseña)
        {
            if (string.IsNullOrWhiteSpace(contraseña))
                throw new ArgumentException("La contraseña no puede ser nula o vacía.", nameof(contraseña));

            ContraseñaHash = HashPassword(contraseña);
        }

        private string HashPassword(string contraseña)
        {
            using var rng = RandomNumberGenerator.Create();

            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);

            var pbkdf2 = new Rfc2898DeriveBytes(contraseña, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(KeySize);

            return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        public bool VerificarContraseña(string contraseña)
        {
            try
            {
                var parts = ContraseñaHash.Split('.');
                if (parts.Length != 2) return false;

                byte[] salt = Convert.FromBase64String(parts[0]);
                byte[] hash = Convert.FromBase64String(parts[1]);

                var pbkdf2 = new Rfc2898DeriveBytes(contraseña, salt, Iterations, HashAlgorithmName.SHA256);
                byte[] hashComprobacion = pbkdf2.GetBytes(KeySize);

                return hash.SequenceEqual(hashComprobacion);
            }
            catch
            {
                return false;
            }
        }

    
    }
}
        
