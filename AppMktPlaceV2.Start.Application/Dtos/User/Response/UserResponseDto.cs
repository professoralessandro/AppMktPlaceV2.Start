﻿using AppMktPlaceV2.Start.Application.Enums;

namespace AppMktPlaceV2.Start.Application.Dtos.User.Response
{
    public class UserResponseDto
    {
        public Guid? Identifier { get; set; }

        public string Nome { get; set; }

        public string Sexo { get; set; }

        public string EstadoCivil { get; set; }

        public string Email { get; set; }

        public bool Bloqueado { get; set; }
    }
}
