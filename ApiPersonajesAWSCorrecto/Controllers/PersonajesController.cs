﻿using ApiPersonajesAWSCorrecto.Models;
using ApiPersonajesAWSCorrecto.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPersonajesAWSCorrecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonajesController : ControllerBase
    {
        private RepositoryPersonajes repo;

        public PersonajesController(RepositoryPersonajes repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Personaje>>>
            GetPersonajes()
        {
            List<Personaje> personajes = await
                this.repo.GetPersonajesAsync();
            return personajes;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Personaje>>
            FindPersonaje( int id )
        {
            Personaje personaje = await
                this.repo.FindPersonajeAsync(id);
            return personaje;
        }

        [HttpPost]
        public async Task<ActionResult> 
            CreatePersonaje(Personaje personaje)
        {
            await this.repo.CreatePersonajeAsync
                (personaje.Nombre, personaje.Imagen);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult>
            UpdatePersonaje(Personaje personaje)
        {
            await this.repo.UpdatePersonajeAsync
                (personaje.IdPersonaje, personaje.Nombre,
                personaje.Imagen);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult>
            DeletePersonaje( int id)
        {
            await this.repo.DeletePersonajeAsync(id);
            return Ok();
        }
    }
}
